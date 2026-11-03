using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Linq;
using System.Web.UI;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repurchase();
        }
    }

    private void Repurchase()
    {
        var users = new[] { "DLS681837", "DLS795316" };
        DateTime fromDate = DateTime.Parse("2025-12-19");

        var getpayout =
            (from m in db.tblMembers
             join i in db.tblInvoices on m.username equals i.username
             where i.PurchaseDate >= fromDate
                   && users.Contains(m.username)
             select new
             {
                 Member = m,
                 Invoice = i
             })
            .ToList();

        foreach (var row in getpayout)
        {
            ProcessRepurchase(row.Member, row.Invoice);
        }
    }

    private void ProcessRepurchase(tblMember user,tblInvoice invoice)
    {
        if (user == null || invoice == null)
            return;

        // 🔐 Prevent duplicate BV
        bool bvExists = db.tblBVs.Any(x =>
            x.bvFrom == user.username &&
            x.invId == invoice.InvoiceId &&
            x.date==invoice.PurchaseDate &&
            x.desc != "Self" &&
            x.bvType == "Repurchase");

        if (bvExists) return;

        AddRepurchaseBV(user, invoice);
    }

    private void AddRepurchaseBV(tblMember gen, tblInvoice invoice)
    {
        if (invoice.BV <= 0 || gen == null) return;

        DateTime date = (DateTime)invoice.PurchaseDate;

        var bvs = db.tblBVs.Where(x => x.username == gen.username && x.status == "Pending" && x.bvType == "Repurchase");

        var left = bvs.Where(x => x.desc == "Left");
        var right = bvs.Where(x => x.desc == "Right");

        decimal currentLeftBv = 0;
        decimal currentRightBv = 0;
        if (left.Any())
        {
            currentLeftBv = left.Sum(x => x.leftBv.Value);
        }
        if (right.Any())
        {
            currentRightBv = right.Sum(x => x.rightBv.Value);
        }

        // ✅ Select lower BV
        decimal lowerBv = Math.Min(currentLeftBv, currentRightBv);
        string weakerLeg =
                currentLeftBv < currentRightBv ? "Left" :
                currentRightBv < currentLeftBv ? "Right" : "Equal";

        if (!string.IsNullOrEmpty(weakerLeg))
        {
            AddBv(gen.username, (decimal)invoice.BV, date, weakerLeg, "", gen.username, invoice.InvoiceId, "Repurchase");
        }

        //Level BV
        int refid = gen.refid.Value;
        string leg = gen.leg;
        int i = 0;
        while (refid > 0)
        {
            i++;
            var bv = (from c in db.tblMembers
                      where c.id == refid
                      select c).SingleOrDefault();
            AddBv(bv.username, (decimal)invoice.BV, date, leg, i.ToString(), gen.username, invoice.InvoiceId, "Repurchase");
            refid = bv.refid.Value;
            leg = bv.leg;
            if (refid == 0)
            {
                break;
            }
        }
        db.SubmitChanges();
    }

    private void AddBv(string uname, decimal pv, DateTime date, string desc, string remarks, string bvfrom, int invId, string type)
    {
        var history = new tblBV
        {
            username = uname,
            Bv = 0,
            leftBv = 0,
            rightBv = 0,
            bvFrom = bvfrom,
            date = date.Date,
            desc = desc,
            remarks = remarks,
            status = "Pending",
            invId = invId,
            lcarry = 0,
            rcarry = 0,
            bvType = type
        };
        if (desc == "Self")
            history.Bv = pv;
        else if (desc == "Left")
            history.leftBv = pv;
        else if (desc == "Right")
            history.rightBv = pv;

        db.tblBVs.InsertOnSubmit(history);
        db.SubmitChanges();
    }


    protected void payoutB(decimal amt, string uname)
    {
        var insert = new Payout
        {
            username = uname,
            date = DateTime.UtcNow.Date,
            confirmdate = DateTime.UtcNow.AddHours(5.5),
            desc = "Bonanza Income",
            tax = 0,
            netpayout = amt,
            credit = amt,
            debit = 0,
            status = "Send",
            incomefrom = uname,
            remarks = "Bonanza Wallet",
        };

        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }
}
