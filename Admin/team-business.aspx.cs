using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            db.CommandTimeout = 7200;

            string uname = Session["admin"].ToString();
            if (!IsPostBack)
            {
                // bind();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../index.html");
           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            lbl1.Visible = false;
            var uname = db.tblMembers.SingleOrDefault(x => x.username == TextBox3.Text);
            //if (uname == null)
            //{
            //    lbl1.Text = "Username not found.";
            //    lbl1.Visible = true;
            //    return;
            //}
            if (string.IsNullOrEmpty(RadioButtonList1.SelectedValue))
            {
                lbl1.Text = "Please select Income Type.";
                lbl1.Visible = true;
                return;
            }

            if (RadioButtonList1.SelectedItem.Text == "Binary")
            {
                decimal totalAmount = 0;

                if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text))
                {
                    if (uname == null)
                        totalAmount = GetAmountSum(1, 0);
                    else
                        totalAmount = GetAmountSum(uname.id, (int)uname.level);

                }
                else
                {
                    // Date filtering
                    DateTime d1, d2;
                    if (!DateTime.TryParse(TextBox1.Text, out d1) || !DateTime.TryParse(TextBox2.Text, out d2))
                    {
                        lbl1.Text = "Invalid date format.";
                        lbl1.Visible = true;
                        return;
                    }
                    if (uname == null)
                        totalAmount = GetAmountSum(1,0, d1, d2);
                    else
                        totalAmount = GetAmountSum(uname.id, (int)uname.level, d1, d2);

                }

                // Calculate and display amounts
                lbltotal.Text = totalAmount.ToString("F2"); // Two decimal places
                decimal charge = totalAmount * 10 / 100;
                lblcharge.Text = charge.ToString("F2");
                decimal netAmount = totalAmount - charge;
                lblnet.Text = netAmount.ToString("F2");
            }
            else if (RadioButtonList1.SelectedItem.Text == "Repurchase")
            {
                decimal totalRepurchase = 0;
                if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text))
                {
                    if (uname == null)
                        totalRepurchase = GetRepurchaseSum("DLS100111");
                    else
                        totalRepurchase = GetRepurchaseSum(uname.username);
                }
                else
                {
                    DateTime d1, d2;
                    if (!DateTime.TryParse(TextBox1.Text, out d1) || !DateTime.TryParse(TextBox2.Text, out d2))
                    {
                        lbl1.Text = "Invalid date format.";
                        lbl1.Visible = true;
                        return;
                    }
                    if (uname==null)
                        totalRepurchase = GetRepurchaseSum("DLS100111", d1, d2);
                    else
                        totalRepurchase = GetRepurchaseSum(uname.username, d1, d2);

                }
                // Display total repurchase amount
                lbltotal.Text = totalRepurchase.ToString("F2");
                decimal charge = totalRepurchase * 10 / 100;
                lblcharge.Text = charge.ToString("F2");
                decimal netAmount = totalRepurchase - charge;
                lblnet.Text = netAmount.ToString("F2");
            }
        }
        catch(Exception ex)
        {
            Response.Redirect("../index.html");          
        }
    }
    public decimal GetRepurchaseSum(string username)
    {
        return ExecuteRepurchaseSumQuery(username, null, null);
    }
    public decimal GetRepurchaseSum(string username, DateTime startDate, DateTime endDate)
    {
        return ExecuteRepurchaseSumQuery(username, startDate, endDate);
    }
    private decimal ExecuteRepurchaseSumQuery(string username, DateTime? startDate, DateTime? endDate)
    {
        // Build the query
        var history1 = from c in db.tblInvoices
                       join d in db.tblBVs on c.InvoiceId equals d.invId
                       join f in db.tblMembers on c.username equals f.username
                       where d.username == username && f.status == "Paid"
                       select new
                       {
                           c.PurchaseDate,
                           d.leftBv,
                           d.rightBv,
                           c.Amount
                       };

        // Apply date range filters if specified
        if (startDate.HasValue && endDate.HasValue)
        {
            history1 = from h in history1
                       where h.PurchaseDate.HasValue &&
                             h.PurchaseDate.Value.Date >= startDate.Value.Date &&
                             h.PurchaseDate.Value.Date <= endDate.Value.Date
                       select h;
        }

        // Materialize the query into a list
        var historyList = history1.ToList();

        // Calculate and return the sum or 0 if the list is empty
        return historyList.Count > 0
            ? Convert.ToDecimal(historyList.Sum(x => x.Amount))
            : 0m;
    }

    // GetAmountSum WITHOUT Date Filter
    public decimal GetAmountSum(int id, int level)
    {
        return ExecuteAmountSumQuery(id, level, null, null);
    }

    // GetAmountSum WITH Date Filter
    public decimal GetAmountSum(int id, int level, DateTime? d1, DateTime? d2)
    {
        return ExecuteAmountSumQuery(id, level, d1, d2);
    }
   
    private decimal ExecuteAmountSumQuery(int id, int level, DateTime? startDate, DateTime? endDate)
    {
        string condition = "";
        if (startDate.HasValue && endDate.HasValue)
        {
            // condition = "where ta.activatedDate between @StartDate and @EndDate";
            startDate = startDate.Value.Date; // Set to 00:00:00
            endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59); // Set to 23:59:59
            condition = "WHERE ta.activatedDate >= @StartDate AND ta.activatedDate <= @EndDate";
        }
        decimal totalAmount = 0;
        // Define the SQL query with placeholders for parameters
        string query = string.Format(@"
                                WITH t AS (
                                    SELECT 
                                        id, 
                                        refid, 
                                        (level - @Level) AS level, 
                                        [status], 
                                        name, 
                                        username, 
                                        joindate, 
                                        renewal, 
                                        [plan], 
                                        pinnumber, 
                                        mobile, 
                                        amount 
                                    FROM tblMember 
                                    WHERE refid = @Id
                                    UNION ALL
                                    SELECT 
                                        c.id, 
                                        c.refid, 
                                        (c.level - @Level) AS level, 
                                        c.[status], 
                                        c.name, 
                                        c.username, 
                                        c.joindate, 
                                        c.renewal, 
                                        c.[plan], 
                                        c.pinnumber, 
                                        c.mobile, 
                                        c.amount 
                                    FROM t p 
                                    JOIN tblMember c ON c.refid = p.id
                                )
                                SELECT SUM(ta.amount) AS totalAmount
                                FROM t
                                INNER JOIN tblActivation ta ON t.username = ta.username {0}
                                OPTION (MAXRECURSION 0);", condition);

        // Get the connection string from the config
        string connStr = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();

        // Setup the database connection and command
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Set command timeout if needed
                command.CommandTimeout = 300; // 5 minutes timeout

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Level", level);
                command.Parameters.AddWithValue("@StartDate", (object)startDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@EndDate", (object)endDate ?? DBNull.Value);

                // Open the connection
                connection.Open();

                // Execute the query and retrieve the sum of the amount column
                object result = command.ExecuteScalar();

                // Handle the case where result is DBNull or null
                if (result != DBNull.Value && result != null)
                {
                    try
                    {
                        totalAmount = Convert.ToDecimal(result);
                    }
                    catch (InvalidCastException ex)
                    {
                        // Log the error or handle it accordingly
                        throw new InvalidCastException("Failed to convert the result to decimal", ex);
                    }
                }
                else
                {
                    totalAmount = 0; // Handle NULL result as 0
                }
            }
        }

        return totalAmount;
    }

}


