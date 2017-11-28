using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmailUtility : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpSessionState ss = HttpContext.Current.Session;
            if (HttpContext.Current.Session["sessionUserFullName"] == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                String sessionUserName = HttpContext.Current.Session["sessionUserFullName"].ToString();
                String user = ConnectionDatabase.ConnectionDatabase.FirstLetterToUpper(sessionUserName);
                string userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user);
                UserNameLbl.Text = userName;
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Items.Add("Exception", ex);
            Server.Transfer("../Error.aspx");
        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    protected void OpenTicketsLinkBtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["defaultpage"] != null ||
                HttpContext.Current.Session["sessionRoleID"] != null ||
                HttpContext.Current.Session["sessionUserName"] != null)
            {
                string defaultURL = Session["defaultpage"].ToString();
                string roleid = HttpContext.Current.Session["sessionRoleID"].ToString();
                string sessionUser = HttpContext.Current.Session["sessionUserName"].ToString();
                string URIVal = HttpContext.Current.Request.Url.AbsoluteUri;

                string querystr = "Select `userlogintable`.`Email`,`roletbl`.`Role` FROM `userlogintable` " +
                    " INNER JOIN `roletbl` ON `roletbl`.`Role_ID`=`userlogintable`.`Role_ID` " +
                    "where `userlogintable`.`UserName`='" + sessionUser + "' AND `userlogintable`.`Role_ID`='" + roleid + "'";

                string EmailIdOfUser = string.Empty;
                string RoleOfUser = string.Empty;

                int totalindex = 2;
                string[] columns = ConnectionDatabase.ConnectionDatabase.SelectColumnsInToTable(querystr, totalindex);
                int count = columns.Length;
                for (int i = 0; i < count; i++)
                {
                    EmailIdOfUser = columns[0].ToString();
                    RoleOfUser = columns[1].ToString();
                }
                if (EmailIdOfUser != string.Empty && RoleOfUser != string.Empty)
                {
                    //to identify project type
                    string urlabvr = "ILANP";
                    string project = "ILWI ANTENNA PORTAL";

                    //pass the default page url
                    string defaulturi = HttpUtility.UrlEncode(Encrypt(defaultURL.ToString()));
                    //passing email of current user                
                    string urlAntenna = HttpUtility.UrlEncode(Encrypt(URIVal.Trim()));
                    string loginid = HttpUtility.UrlEncode(Encrypt(sessionUser.Trim()));
                    string link = ConfigurationManager.AppSettings["TicketSystemLink"];
                    Response.Redirect(string.Format(link + "User/Dashboard.aspx?name1={0}&user={1}&usertype={2}&url={3}&pname={4}&defaulturl={5}",
                        loginid, urlabvr, RoleOfUser, urlAntenna, project, defaulturi));
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch
        {

        }
    }   
}