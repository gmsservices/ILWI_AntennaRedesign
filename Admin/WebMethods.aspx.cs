using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_WebMethods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string GetStatus(ProjectStatusClass projectstatus)
    {
        string SelectString = "Select count(*) from contractorcloseouttbl";
        SelectString += " Where projectstatus ='" + projectstatus.projectstatus + "'";        
        int status = ConnectionDatabase.ConnectionDatabase.SelectInToTable(SelectString);
        return status.ToString();
    }
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string GetOpenStatus() 
    {
        string SelectString = "Select count(*) from contractorcloseouttbl";
        SelectString += " Where projectstatus ='Open'";
        int status = ConnectionDatabase.ConnectionDatabase.SelectInToTable(SelectString);
        return status.ToString();
    }
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string GetCloseStatus()
    {
        string SelectString = "Select count(*) from contractorcloseouttbl";
        SelectString += " Where projectstatus ='Close'";
        int status = ConnectionDatabase.ConnectionDatabase.SelectInToTable(SelectString);
        return status.ToString();
    }
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string GetRetSubmitStatus()
    {
        string SelectString = "Select count(*) from contractorcloseouttbl";
        SelectString += " Where projectstatus ='SubmitRetData'";
        int status = ConnectionDatabase.ConnectionDatabase.SelectInToTable(SelectString);
        return status.ToString();
    }
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string GetRetRejectStatus()
    {
        string SelectString = "Select count(*) from contractorcloseouttbl";
        SelectString += " Where projectstatus ='RejectRetData'";
        int status = ConnectionDatabase.ConnectionDatabase.SelectInToTable(SelectString);
        return status.ToString();
    }
    public class ProjectStatusClass
    {
        public string projectstatus
        {
            get;
            set;
        }
        
    }
}