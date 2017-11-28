using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

/* 
* Copyright (C) 2016-2017 Global Mobility Services (GMS) - All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited.
* Proprietary and confidential.
*/

/// <summary>
/// Summary description for EmailUtility
/// </summary> 
namespace EmailUtilitys
{
    public class EmailUtility
    {
        public EmailUtility()
        {
            //
            // TODO: Add constructor logic here
            //
        } 
        public static void CreateEmailUtility(string contractorEmailId, string projecttype, string sitename, string projectstatus, 
            string requestedby, string contractor, string company,
            string actiontacken,  string description, string retcomments,
            string roleName, string comments, string subject)
        {
            string datetimeadded = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");

            string bigfont = "font-size:18px;";
            string smaillfont = "font-size:16px;";
            string fontfamily = "font-family: \"wf_segoe-ui_normal\", \"Segoe UI\", \"Segoe WP\", Tahoma, Arial, sans-serif;";
            string style = " style=\"" + smaillfont + fontfamily + "\"";

            string body = "<table> " +
                "<tr>" +
                    "<td style=\"width:500px; height:60px; color:#FFFFFF; background-color:#000000; border:0px;"+
                    bigfont + fontfamily +
                    "\" colspan=\"2\">" +
                        "<p>ILWI - Construction Antenna Platform Sheet</p>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style=\"height:25px; width:600px; background-color:#D99594;\" colspan=\"2\">" +
                        "<p></p>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td colspan=\"2\">" +
                        "<p style=\"margin-top: 0.5em;margin-bottom: 0.5em;margin-left:5px;margin-right:5px;border-style: inset;border-width: 1px;\"></p>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Project: " +
                    "</td>" +
                    "<td>" +
                    "<label><em>" + projecttype + "</em></label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Title: " +
                    "</td>" +
                    "<td>" +
                     "<label style=\"margin-left:-450px;\"><em>" + sitename + "</em></label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Status: " +
                    "</td>" +
                    "<td>" +
                      "<label style=\"margin-left:-450px;\"><em>" + projectstatus + "</em></label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Description: " +
                    "</td>" +
                    "<td>" +
                    "<br />" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style=\"padding-left:10px;\">" +
                        "<br /> " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\"> " + description + " </label>" +
                    "</td>" +
                "</tr>" +               
                "<tr>" +
                    "<td colspan=\"2\">" +
                        "<p style=\"margin-top:0px;margin-bottom: 0.5em;margin-left:5px;margin-right:5px;border-style: inset;border-width: 1px;\"></p>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td colspan=\"2\">" +
                        "<p><h2 style=\"padding-left:10px; margin-top:-10px;"+
                        bigfont + fontfamily +
                        "\">Scope of Work</h2></p>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td colspan=\"2\">" +
                        "<p style=\" display: block;margin-top:-18px;margin-bottom: 0.5em;margin-left:5px;margin-right:5px;border-style: inset;border-width: 1px;\"></p>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Requested By: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + requestedby + "</label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Contractor: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + contractor + "</label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Company: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + company + "</label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Role: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">"+ roleName +"</label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Action Taken: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + actiontacken + "</label>" +
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Comment: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + comments + "</label>" + //retcomments
                    "</td>" +
                "</tr>" +
                "<tr " + style + ">" +
                    "<td style=\"padding-left:10px;\">" +
                        "Date/Time: " +
                    "</td>" +
                    "<td>" +
                    "<label style=\"margin-left: -450px;\">" + @AnatennaPlatforms.AnatennaPlatforms.Encode(datetimeadded) + "</label>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style=\"height:25px; width:600px; background-color:#D99594;\" colspan=\"2\">" +
                        "<p></p>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style=\"height:20px; width:600px; background-color:#000000;\" colspan=\"2\">" +
                        "<p></p>" +
                    "</td>" +
                "</tr>" +
                "</table>";
            body = body + "<br/><br/>";

            //Update On 05/04/2015 Start
            string adminEmailId = string.Empty;
            if (roleName.ToUpper() == "ADMIN MASTER")
            {
                string output = "SELECT GROUP_CONCAT(distinct `userlogintable`.Email SEPARATOR ';') FROM `userlogintable` ";
                output = output + " INNER JOIN `roletbl` ON ";
                output = output + " `roletbl`.`Role_ID` = `userlogintable`.`Role_ID` ";
                output = output + " WHERE ";
                output = output + " `userlogintable`.`Role_ID`='1' AND `userlogintable`.`UserName`='adminad' ";
                output = output + " GROUP BY `userlogintable`.`Role_ID` ";
                string[] ids = AnatennaPlatforms.AnatennaPlatforms.FindAvailableAntenna(output);
                if (ids.Length > 0)
                {
                    if (ids[0].ToString() != "false")
                    {
                        //contractorEmailId = contractorEmailId + ";" + ids[0].ToString();
                        adminEmailId = ids[0].ToString();
                    }
                }
            }
            //Update On 05/04/2015 End

            SendWorkingFileMail(body, contractorEmailId, adminEmailId, subject);
        }
        public static void SendWorkingFileMail(string body, string contractorEmailId,string adminEmailId, string subject)
        {
            try
            {
                //Update On 08/02/2017 Start
                string NetworkEmailId = System.Configuration.ConfigurationManager.AppSettings["EMailId"];
                string NetworkEmailIdPassword = System.Configuration.ConfigurationManager.AppSettings["EMailIdPassword"];
                //Update On 08/02/2017 End

                MailMessage message = new MailMessage();
                //message.From = new MailAddress("tech@gmobility.com");
                message.From = new MailAddress(NetworkEmailId); //Update On 08/02/2017
                /*
                if (contractorEmailId != string.Empty)
                {                   
                    message.To.Add(new MailAddress(contractorEmailId));
                }*/

                //Update On 05/04/2017 Start
                if (contractorEmailId != string.Empty)
                {
                    foreach (var address in contractorEmailId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.To.Add(new MailAddress(address));
                    }
                }
                if (adminEmailId != string.Empty)
                {
                    foreach (var address in adminEmailId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.Bcc.Add(new MailAddress(address));
                    }
                }
                //Update On 05/04/2017 End

                //Get BCC Email
                string strQuery = "SELECT * " + "FROM `bcctbl`";
                DataTable getBCCDt = new DataTable();
                MySqlConnection con = ConnectionDatabase.ConnectionDatabase.OpenConnection();
                MySqlCommand marketcmd = new MySqlCommand(strQuery);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                marketcmd.CommandType = CommandType.Text;
                marketcmd.Connection = con;
                sda.SelectCommand = marketcmd;
                sda.Fill(getBCCDt);
                //con.Close();
                //Close connection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
                // For each row, print the values of each column.
                foreach (DataRow row in getBCCDt.Rows)
                {
                    message.Bcc.Add(new MailAddress(row["email"].ToString()));
                }

                //message.Subject = "New IL/WI Region - Construction Antenna Platform Notification";
                //message.Subject = "Test : New IL/WI Region - Construction Antenna Platform Notification";
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Body = body;
                message.IsBodyHtml = true;
                //System.Net.NetworkCredential aCred = new System.Net.NetworkCredential("tech@gmobility.com", "Gms$@123");
                System.Net.NetworkCredential aCred = new System.Net.NetworkCredential(NetworkEmailId, NetworkEmailIdPassword); //Update On 08/02/2017
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 0;
                client.UseDefaultCredentials = false;
                client.Credentials = aCred;
                client.Send(message);
            }
            catch (Exception msg)
            {

            }
        }
        public static void AddAttachment(string filePath, MailMessage message)
        {
            Attachment outputFile = new Attachment(filePath);
            message.Attachments.Add(outputFile);
        }
        public static void SendScriptFileMail(string contractorEmailId, string UserEmailId, string subject, string filePath)
        {
            string fontweight = "font-size: 8pt;";
            string fontsize = "font-size: 8pt;";
            string fontfamily = "font-family: Verdana;";

            string body = "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "'>Find the attachment of script file.</span>";
            body = body + "<br/>";
            body = body + "<br/>";
            body = body + "<span style='color: #ff0000;" + fontfamily + "" + fontsize + "" + fontweight + "'>* Do not respond to this message. Please contact <a href='mailto:tech@gmobility.com'>" + "tech@gmobility.com.</a>" + "</span>";

            string NetworkEmailId = System.Configuration.ConfigurationManager.AppSettings["EMailId"];
            string NetworkEmailIdPassword = System.Configuration.ConfigurationManager.AppSettings["EMailIdPassword"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(NetworkEmailId);
            //if (contractorEmailId != string.Empty)
            //{
            //    message.To.Add(new MailAddress(contractorEmailId));
            //}

            if (UserEmailId != string.Empty)
            {
                foreach (var address in UserEmailId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(new MailAddress(address));
                }
            }

            //Get BCC Email
            string strQuery = "SELECT * " + "FROM `bcctbl`";
            DataTable getBCCDt = new DataTable();
            MySqlConnection con = ConnectionDatabase.ConnectionDatabase.OpenConnection();
            MySqlCommand marketcmd = new MySqlCommand(strQuery);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            marketcmd.CommandType = CommandType.Text;
            marketcmd.Connection = con;
            sda.SelectCommand = marketcmd;
            sda.Fill(getBCCDt);
            //Close connection
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
            // For each row, print the values of each column.
            foreach (DataRow row in getBCCDt.Rows)
            {
                message.Bcc.Add(new MailAddress(row["email"].ToString()));
            }

            AddAttachment(filePath, message);

            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = body;
            message.IsBodyHtml = true;
            System.Net.NetworkCredential aCred = new System.Net.NetworkCredential(NetworkEmailId, NetworkEmailIdPassword);
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Timeout = 0;
            client.UseDefaultCredentials = false;
            client.Credentials = aCred;
            client.Send(message);
        }//End
        public static string SendScriptMailUtility(ValidationTable validationtable)
        {
            string msg = string.Empty;

            string UserName = validationtable.username;
            string IPAddress = validationtable.ipaddress;
            string Contractorclose_idVal = validationtable.contractorclose_id;
            string Contractor_idVal = validationtable.contractor_id;
            string LocationVal = validationtable.location;
            string MarketVal = validationtable.market;
            string SitenameVal = validationtable.sitename;
            string ProjecttypeVal = validationtable.projecttype;
            string ContractorVal = validationtable.contractor;
            string ProjectstatusVal = validationtable.projectstatus;
            string StatusTypeVal = validationtable.statustype;
            string scripttype = validationtable.scripttype;

            int action_id = 0;
            if (StatusTypeVal == "Start")
            {
                action_id = 15;
            }
            else
            {
                action_id = 16;
            }

            string strQuery = "SELECT * FROM `emailutilityformattbl` WHERE `action_id`=" + action_id;
            MySqlCommand cmd1 = new MySqlCommand(strQuery);
            DataTable actionDt = IncludeDataTables.DataTables.GetData(cmd1);
            string subject = string.Empty;
            foreach (DataRow dr in actionDt.Rows)
            {
                subject = dr["subject"].ToString();
            }

            string start_time = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");

            string body = "Find the below details of " + StatusTypeVal.ToLower() + " script :- <br/><br/>";
            body = body + "<table border='0'>" +
                          "<tr>" +
                            "<td>Script Type</td>" +
                            "<td> : " + scripttype + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>User By</td>" +
                            "<td> : " + UserName + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>IP Address</td>" +
                            "<td> : " + IPAddress + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Start Time</td>" +
                            "<td> : " + start_time + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Contractorclose_id</td>" +
                            "<td> : " + Contractorclose_idVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Contractor_id</td>" +
                            "<td> : " + Contractor_idVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Location</td>" +
                            "<td> : " + LocationVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Market</td>" +
                            "<td> : " + MarketVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Sitename</td>" +
                            "<td> : " + SitenameVal + "</td>" +
                          "</tr>" +
                           "<tr>" +
                            "<td>Projecttype</td>" +
                            "<td> : " + ProjecttypeVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Contractor</td>" +
                            "<td> : " + ContractorVal + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Project status</td>" +
                            "<td> : " + ProjectstatusVal + "</td>" +
                          "</tr>" +
                          "</tr>" +
                        "</table>";

            SendStatusMail(body, subject);

            return msg;
        }
        public class ValidationTable
        {
            public string statustype { get; set; }
            public string scripttype { get; set; }
            public string username { get; set; }
            public string contractorclose_id { get; set; }
            public string contractor_id { get; set; }
            public string location { get; set; }
            public string market { get; set; }
            public string sitename { get; set; }
            public string projecttype { get; set; }
            public string contractor { get; set; }
            public string projectstatus { get; set; }
            public string ipaddress { get; set; }
        }
        public static void SendStatusMail(string body, string subject)
        {
            try
            {
                //Update On 08/02/2017 Start
                string NetworkEmailId = System.Configuration.ConfigurationManager.AppSettings["EMailId"];
                string NetworkEmailIdPassword = System.Configuration.ConfigurationManager.AppSettings["EMailIdPassword"];
                //Update On 08/02/2017 End

                MailMessage message = new MailMessage();
                message.From = new MailAddress(NetworkEmailId);

                //Get To Email
                string strQuery = "SELECT * " + "FROM `bcctbl`";
                DataTable getBCCDt = new DataTable();
                MySqlConnection con = ConnectionDatabase.ConnectionDatabase.OpenConnection();
                MySqlCommand marketcmd = new MySqlCommand(strQuery);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                marketcmd.CommandType = CommandType.Text;
                marketcmd.Connection = con;
                sda.SelectCommand = marketcmd;
                sda.Fill(getBCCDt);
                //con.Close();
                //Close connection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
                // For each row, print the values of each column.
                foreach (DataRow row in getBCCDt.Rows)
                {
                    message.To.Add(new MailAddress(row["email"].ToString()));
                }

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Body = body;
                message.IsBodyHtml = true;
                System.Net.NetworkCredential aCred = new System.Net.NetworkCredential(NetworkEmailId, NetworkEmailIdPassword);
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 0;
                client.UseDefaultCredentials = false;
                client.Credentials = aCred;
                client.Send(message);
            }
            catch (Exception msg)
            {

            }
        }
    }
}