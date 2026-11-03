using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;

/// <summary>
/// Summary description for sendsms
/// </summary>
public class sendsms
{
    public sendsms()
    {


        //
        // TODO: Add constructor logic here
        //
    }
    public string sms(string mob, string msg, string templateId)
    {
        Task.Factory.StartNew(() =>
        {
            if (ConfigurationManager.AppSettings["env"] == "prod")
            {
                string sURL;

                string sender = "DLSBTC";
                string key = "d41a9d93c6baf5122a0e0a00d0992ab9";
                WebRequest wrGETURL;
                StreamReader objReader;

                //red sms2
                sURL = string.Format("http://login.redsms.in/api/smsapi?key={0}&route=2&sender={1}&number={2}&sms={3}&templateid={4}", key, sender, mob, msg, templateId);

                wrGETURL = WebRequest.Create(sURL);
                try
                {
                    Stream objStream;
                    objStream = wrGETURL.GetResponse().GetResponseStream();
                    objReader = new StreamReader(objStream);
                    objReader.Close();

                }
                catch
                {

                }
            }
        });
        return "Sms has been sent ";
    }
}