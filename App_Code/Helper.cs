using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{
    static DataClassesDataContext db = new DataClassesDataContext();
    public static int totalRegistration(DateTime date)
    {
        var find = (from c in db.tblMembers
                    join d in db.PinManagaers
                   on c.pinnumber equals d.pinnumbers
                    where c.joindate.Value.Date == date && d.IsFreePin == false
                    select c.id).Count();

        return find;

    }

    public static int totalActivation(DateTime date)
    {
        var find = (from c in db.tblMembers
                    where c.isActive == 1 && c.joindate.Value.Date == date
                    select c.id).Count();

        return find;

    }

    public static string GenerateOTP()
    {
        string strPwdchar = "1234567890";
        string otp = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            otp += strPwdchar.Substring(iRandom, 1);
        }

        return otp;
    }

    public static decimal totalCollection(DateTime date)
    {
        var find = (from c in db.tblMembers
                    join d in db.PinManagaers
                   on c.pinnumber equals d.pinnumbers
                    where c.isActive == 1 && c.joindate.Value.Date == date && d.IsFreePin == false
                    select (c.amount)).Sum();

        return find ?? 0;

    }

    public static decimal totalPayout(DateTime date)
    {
        var find = (from c in db.Payouts
                    where c.status == "Send" && c.date == date
                    select c.credit).Sum();

        return find ?? 0;
    }

    public static decimal totalPinCollection(DateTime date)
    {
        var find = (from c in db.PinManagaers
                    join d in db.tblPlans on c.package equals d.PlanName
                    where c.IsFreePin == false && c.date == date
                    select d.PlanAmount).Sum();
        return find ?? 0;
    }

    public static decimal totalFundWallet(DateTime date)
    {
        var find = (from c in db.FranchiseeWalletRequests
                    where c.status == "Approved" && c.ActionOn.Value.Date == date
                    select c.amount).Sum();
        return find ?? 0;
    }

    public static decimal totalManualBilling(DateTime date)
    {
        var find = (from c in db.tblInvoices
                    where c.FranchiseeId == 1 && c.PurchaseDate.Value.Date == date
                    select c.Amount).Sum();
        return find ?? 0;
    }

    public static string Decrypt(string encrString)
    {
        byte[] b;
        string decrypted;
        try
        {
            b = Convert.FromBase64String(encrString);
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
        }
        catch (FormatException fe)
        {
            decrypted = "";
        }
        return decrypted;
    }

    public static string Encrypt(string strEncrypted)
    {
        byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
        string encrypted = Convert.ToBase64String(b);
        return encrypted;
    }
}