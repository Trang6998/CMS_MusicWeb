using CMS.Web.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HvitFramework.CoreData
{
    public sealed class MailHelper
    {

        public static List<string> CreateMailingList(params object[] mail)
        {
            var result = new List<string>();
            foreach (var m in mail)
            {
                if (!(m is string))
                    continue;

                var ms = (string)m;

                if (string.IsNullOrEmpty(ms))
                    continue;

                if (!new RegexUtilities().IsValidEmail(ms))
                    continue;

                if (result.Contains(ms))
                    continue;

                result.Add(ms);
            }

            return result;
        }

        public static void SendEmail(string to, string subject, string body)
        {
            Task t = Task.Run(() =>
            {
                using (MailMessage mm = new MailMessage(Const.MAILKYTHUAT, to))
                {
                    mm.Subject = subject;
                    mm.Body = body;

                    mm.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.office365.com";
                        smtp.Credentials = new NetworkCredential(Const.MAILKYTHUAT, Const.PASSMAILKYTHUAT);

                        smtp.EnableSsl = true;
                        // smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Port = 25;
                        smtp.Timeout = 60000;
                        try
                        {
                            smtp.Send(mm);
                            // InsertMailSent(to, subject, body, true, "");
                        }
                        catch (SmtpException ex)
                        {
                            string msg = ex.Message + "\n" + ex.StackTrace;
                            if (ex.InnerException != null)
                            {
                                msg += "\n" + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                            }

                            // InsertMailSent(to, subject, body, false, msg);
                            return;
                        }
                    }
                }
            });
        }

        public static void SendEmail(List<string> to, string subject, string body)
        {
            foreach (string mailto in to)
            {
                using (MailMessage mm = new MailMessage(Const.MAILKYTHUAT, mailto))
                {
                    SendEmail(mailto, subject, body);
                }
            }
        }
        //private static void InsertMailSent(string sendTo, string subject, string body, bool status, string exception)
        //{
        //    // Todo
        //}
    }
}