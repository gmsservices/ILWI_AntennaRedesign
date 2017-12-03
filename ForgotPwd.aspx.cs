using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            String Email = txtEmail.Text.Trim().ToString();
            if (Email == "")
            {
                string message = "Please enter email address to recover your login credentials.";
                string title = "Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                //lblErrorMsg.Text = "Please enter email address or username to recover your login credentials.";
            }
			//ttesting
            else if (Email != "")
            {
                string[] user = ConnectionDatabase.ConnectionDatabase.ForgotPasswordByEmail(Email);
                if (user.Length > 0)
                {
                    string UserName = user[1].ToString();
                    string Password = user[2].ToString();
                    string FirstName = user[3].ToString();
                    string LastName = user[4].ToString();
                    string emailId = user[5].ToString();

                    string userfullname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FirstName + " " + LastName);
                    SendWorkingFileMail(emailId, UserName, Password, userfullname);
                    //lblErrorMsg.Text = "Your login account credentials send to you vai mail.Please check mail.";

                    string message = "Your login account credentials send to you vai mail.Please check mail.";
                    string title = "Message";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                    txtEmail.Text = "";
                }
            }
            else
            {
                string message = "Please enter email address to recover your login credentials.";
                string title = "Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                    
                //lblErrorMsg.Text = "Please enter email address or username to recover your login credentials.";
            }
        }
        catch (Exception thy)
        {
            //lblErrorMsg.Text = "Error : " + thy.Message;
            string message = thy.Message;
            string title = "Error";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                 
        }
    }
    public void SendWorkingFileMail(string emailId, string userName, string passWord, string fullname)
    {
        try
        {
            MailMessage message = new MailMessage();
            string fontweight = "font-size: 10pt;";
            string fontsize = "font-size: 10pt;";
            string fontfamily = "font-family: Tahoma;";

            String msgTxt = "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "'>" + fullname + ",</span><br/>" +
                    "<p style='margin-left: 40px'>" +
                    "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "'>ILWI Antenna Platform User " +
                    "login credentials is below :</span></p>" +
                    "<p style='margin-left: 40px'>" +
                    "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "'>&nbsp;Username : <span style='text-decoration: underline;'>" + userName + "</span> </span></p>" +
                    "<p style='margin-left: 40px'>" +
                    "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "'>&nbsp;Password : " +
                    "<span style='color: #000035;" + fontfamily + "" + fontsize + "" + fontweight + "text-decoration: underline;'>" + passWord + "</span></span></p>";

            msgTxt = msgTxt + "<br/><br/><br/><span style='color: #ff0000;" + fontfamily + "" + fontsize + "" + fontweight + "'>* Do not respond to this message. Please contact <a href='mailto:tech@gmobility.com'>" + "tech@gmobility.com.</a>" + "</span>";

            string NetworkEmailId = System.Configuration.ConfigurationManager.AppSettings["EMailId"];
            string NetworkEmailIdPassword = System.Configuration.ConfigurationManager.AppSettings["EMailIdPassword"];

            message.From = new MailAddress(NetworkEmailId);
            message.To.Add(new MailAddress(emailId));

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

            string subject = string.Empty;

            int action_id = 10;
            string strQuery1 = "SELECT * FROM `emailutilityformattbl` WHERE `action_id`=" + action_id;
            MySqlCommand cmd = new MySqlCommand(strQuery1);
            DataTable actionDt = IncludeDataTables.DataTables.GetData(cmd);

            foreach (DataRow dr in actionDt.Rows)
            {
                subject = dr["subject"].ToString();
            }

            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = msgTxt;
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
            //lblErrorMsg.Text = "Error in send mail : " + msg.Message;

            string message = msg.Message;
            string title = "Error";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
        }
    }
}