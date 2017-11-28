using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    // static variable
    static string prevPage = String.Empty;
    string defaultUrl = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpSessionState ss = HttpContext.Current.Session;
            if (HttpContext.Current.Session["sessionUserName"].ToString() != "")
            {
                string userName = HttpContext.Current.Session["sessionUserName"].ToString();
                string passWord = HttpContext.Current.Session["sessionPassWord"].ToString();

                CheckSession(userName, passWord);
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                Response.Redirect("Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Logout"] != "" && Request.QueryString["Logout"] != null)
                {
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Clear();

                    //Fetch the Cookie using its Key.
                    HttpCookie nameCookie = Request.Cookies["userInfo"];
                    //Set the Expiry date to past date.
                    nameCookie.Expires = DateTime.Now.AddDays(-1);
                    //Update the Cookie in Browser.
                    Response.Cookies.Add(nameCookie);

                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch
        {

        }
        try
        {
            defaultUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (defaultUrl.ToString() != "")
            { Session["defaultpage"] = defaultUrl.ToString(); }
        }
        catch { }
    }
    //Get Lan Connected IP address method
    public string GetIP()
    {
        string Str = "";
        Str = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
        IPAddress[] addr = ipEntry.AddressList;
        return addr[addr.Length - 1].ToString();
    }
    //Get Visitor IP address method
    public string GetVisitorIpAddress()
    {
        string stringIpAddress;
        stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
        {
            stringIpAddress = Request.ServerVariables["REMOTE_ADDR"];//we can use REMOTE_ADDR
        }
        //return "Your ip is " + stringIpAddress;
        return stringIpAddress;
    }
    protected void LoginBtn_Click(object sender, EventArgs e)
    {
        string userName = UserNameTxt.Text;
        string passWord = PassWordTxt.Text;

        DateTime now = DateTime.Now;
        //string date = now.GetDateTimeFormats('d')[0];
        string time = now.GetDateTimeFormats('t')[0];
        
        string date = now.ToString("yyyy-MM-dd");
        try
        {
            if (userName != "" && passWord != "")
            {
                int value = ConnectionDatabase.ConnectionDatabase.FindUser(userName, passWord);

                if (value != 0)
                {
                    if (RememberMeChk.Checked == true)
                    {
                        HttpCookie userInfo = new HttpCookie("userInfo");
                        userInfo["UserName"] = userName;
                        userInfo["PassWord"] = passWord;
                        userInfo.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(userInfo);
                    }
                }

                if (value == 1)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Admin/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {                        
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 2)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("User/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {                       
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 3)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Company/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {                        
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 4)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord;
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Admin MGT/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {                       
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 5)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Supar User/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {                       
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 6)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                HttpContext.Current.Session["IsNeedPopupFlag"] = true;
                                Response.Redirect("Ops Admin/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 7)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                HttpContext.Current.Session["IsNeedPopupFlag"] = true;
                                Response.Redirect("Ops Operation/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                           
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 8)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                HttpContext.Current.Session["IsNeedPopupFlag"] = true;
                                Response.Redirect("Ops Market Admin/UpdateMaster.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                          
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 9)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                HttpContext.Current.Session["IsNeedPopupFlag"] = true;
                                Response.Redirect("Admin Master/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();

                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                          
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }                
                if (value == 0)
                {
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Clear();

                    string message = "Please enter correct username & password !";
                    string title = "Message";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);                           
                   
                }
            }
            else
            {
                string message = "UserName & Password does not blank.";
                string title = "Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
                
            }
        }
        catch (Exception ex)
        {
            string message = "User Session Expired....!";
            string title = "Error";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
        }
    }
    //Update On 18/04/2017 Start
    public void CheckSession(string userName, string passWord)
    {
        DateTime now = DateTime.Now;
        //string date = now.GetDateTimeFormats('d')[0];
        string time = now.GetDateTimeFormats('t')[0];

        string date = now.ToString("yyyy-MM-dd");
        try
        {
            if (userName != "" && passWord != "")
            {
                int value = ConnectionDatabase.ConnectionDatabase.FindUser(userName, passWord);
                if (value == 1)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord;
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Admin/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 2)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("User/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 3)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord;
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Company/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 4)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Admin MGT/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 5)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord;
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Supar User/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 6)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Ops Admin/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                if (value == 7)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Ops Operation/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }                
                if (value == 8)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Ops Market Admin/UpdateMaster.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }

                if (value == 9)
                {
                    if (Session["sessionUserName"] != null)
                    {
                        HttpContext.Current.Session["sessionPassWord"] = passWord; 
                        try
                        {
                            int userId = int.Parse(HttpContext.Current.Session["sessionUserID"].ToString());
                            ConnectionDatabase.ConnectionDatabase.LogInOfUser(userId, GetVisitorIpAddress(), date, time);
                            if (Session["sessionUserID"] != null)
                            {
                                Response.Redirect("Admin Master/Home.aspx", false);
                            }
                        }
                        catch (Exception e1)
                        {
                            //ErrorMsgLbl.Text = e1.Message.ToString();
                            string message = e1.Message.ToString();
                            string title = "Error";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                
                if (value == 0)
                {
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Clear();
                   
                    string message = "Please enter correct username & password !";
                    string title = "Message";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
                }
            }
            else
            {
                string message = "UserName & Password does not blank.";
                string title = "Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
            }
        }
        catch (Exception ex)
        {
            string message = "User Session Expired....!";
            string title = "Error";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowValidation", "javascript:OpenNotify('" + message + "', '" + title + "');", true);
            
        }
    }       
}