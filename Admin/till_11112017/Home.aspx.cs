using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Home : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {
            try
            {
                if (HttpContext.Current.Session["sessionFistLogin"].ToString() == "No")
                {
                    Response.Redirect("ChangePassword.aspx");
                }

                if (HttpContext.Current.Session["sessionContractor_id"] == null)
                {
                    Response.Redirect("../Default.aspx");
                }
                else
                {
                    ContractorIDHiddenField.Value = HttpContext.Current.Session["sessionContractor_id"].ToString();
                    string contractorid = ContractorIDHiddenField.Value;
                    string status = ProjectStatusHiddenField.Value;

                    
                    // Enable the GridView sorting option. 
                    //ContractorCloseoutGridView.AllowSorting = true;
                    //ViewState["SortExpression"] = "contractorclose_id DESC";

                    BindGrid(status, contractorid);
                   
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Items.Add("Exception", ex);
                Server.Transfer("../Error.aspx");
            }
        }

        /*
        //Fetch the Cookie using its Key.
        HttpCookie userInfo = Request.Cookies["userInfo"];
        if (userInfo != null)
        {
            //If Cookie exists fetch its value.
            string userName = userInfo["UserName"].ToString() == "" ? "" : userInfo["UserName"].ToString();
            string passWord = userInfo["PassWord"].ToString() == "" ? "" : userInfo["PassWord"].ToString();           
        }*/
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
    private void BindGrid(string strQueryStatus, string contractorid)
    {
        try
        {
            string strQuery = "SELECT contractorclose_id,`ContractorCloseoutTbl`.`contractor_id`," +
                    "location,market,sitename,projecttype,`ContractorCloseoutTbl`.`contractor`," +
                    "`contractortbl`.`company`,projectstatus," +
                    "`project_open_date`," +
                    "ecr_date,ecr_document," +
                    "ret_submit_date,ret_comments,48hour_submit_date,48hour_comments,closeout_submit_date," +
                    "closeout_comments " +
                    "FROM `ContractorCloseoutTbl` ";
            strQuery = strQuery + " INNER JOIN `contractortbl` ON `ContractorCloseoutTbl`.`contractor_id`=`contractortbl`.`contractor_id` ";

            string projectstatus = "SubmitRetData";
            strQuery = strQuery + " WHERE " + "`projectstatus`='" + projectstatus + "'";
            strQuery = strQuery + " ORDER BY `ContractorCloseoutTbl`.`contractorclose_id` DESC ";

            MySqlCommand cmd = new MySqlCommand(strQuery);
            DataTable contractorcloseoutDt = IncludeDataTables.DataTables.GetData(cmd);

            
            String RowsDt = string.Empty;

            foreach (DataRow rows in contractorcloseoutDt.Rows)
            {
                RowsDt += "<tr class='gradeA'>";
                //RowsDt += "			<td class=\"center\">";
                //RowsDt += "				<a class=\"btn btn-info\" href=\"Edit.aspx?ID=" + rows[0] + "\">";
                //RowsDt += "					<i class=\"icon-edit icon-white\"></i>  ";
                //RowsDt += "					Edit                                    ";
                //RowsDt += "				</a>";
                //RowsDt += "				<a class=\"btn btn-info\" href=\"Delete.aspx?ID=" + rows[0] + "\">";
                //RowsDt += "					<i class=\"icon-edit icon-white\"></i>  ";
                //RowsDt += "					Delete                                    ";
                //RowsDt += "				</a>";
                //RowsDt += "			</td>";                
                RowsDt += "<td>" + rows["contractorclose_id"] + "</td>";
                RowsDt += "<td>" + rows["contractor_id"] + "</td>";
                RowsDt += "<td>" + rows["location"] + "</td>";
                //RowsDt += "<td>" + rows["market"] + "</td>";
                //RowsDt += "<td>" + rows["sitename"] + "</td>";
                //RowsDt += "<td>" + rows["projecttype"] + "</td>";
                //RowsDt += "<td>" + rows["contractor"] + "</td>";
                //RowsDt += "<td>" + rows["company"] + "</td>";
                //RowsDt += "<td>" + rows["projectstatus"] + "</td>";
                //RowsDt += "<td>" + rows["project_open_date"] + "</td>";
                //RowsDt += "<td>" + rows["ecr_date"] + "</td>";
                //RowsDt += "<td>" + rows["ecr_document"] + "</td>";
                //RowsDt += "<td>" + rows["ret_submit_date"] + "</td>";
                //RowsDt += "<td>RET Report</td>";
                //RowsDt += "<td>RET Comments</td>";
                //RowsDt += "<td>Send Email</td>";
                RowsDt += "<td class='actions'>";
                RowsDt += "<a href='#' class='hidden on-editing save-row'><i class='fa fa-save'></i></a>";
                RowsDt += "<a href='#' class='hidden on-editing cancel-row'><i class='fa fa-times'></i></a>";
                RowsDt += "<a href='#' class='on-default edit-row'><i class='fa fa-pencil'></i></a>";
                RowsDt += "<a href='#' class='on-default remove-row'><i class='fa fa-trash-o'></i></a>";
                RowsDt += "</td>";
                RowsDt += "</tr>";
                tlist.InnerHtml = RowsDt;
            }
            

            /*
            if (ContractorCloseoutGridView.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                ContractorCloseoutGridView.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                ContractorCloseoutGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                ContractorCloseoutGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            if (contractorcloseoutDt.Rows.Count > 0)
            {
                // Get the DataView from  DataTable. 
                DataView dvContractorCloseout = contractorcloseoutDt.DefaultView;
                // Set the sort column and sort order. 
                dvContractorCloseout.Sort = ViewState["SortExpression"].ToString();

                ContractorCloseoutGridView.DataSource = dvContractorCloseout;
                ContractorCloseoutGridView.DataKeyNames = new string[] { "contractorclose_id" };
                ContractorCloseoutGridView.DataBind();
                //totalLbl.Text = "Total No Of Records : " + contractorcloseoutDt.Rows.Count.ToString();
                //RowCountHiddenField.Value = contractorcloseoutDt.Rows.Count.ToString();//Update on 23/01/2017
            }
            else
            {
                EmptyDatatable(contractorcloseoutDt);

                // Get the DataView from  DataTable. 
                DataView dvContractorCloseout = contractorcloseoutDt.DefaultView;
                // Set the sort column and sort order. 
                dvContractorCloseout.Sort = ViewState["SortExpression"].ToString();

                ContractorCloseoutGridView.DataSource = dvContractorCloseout;
                ContractorCloseoutGridView.DataBind();
                ContractorCloseoutGridView.Rows[0].Visible = false;
                int total = contractorcloseoutDt.Rows.Count - 1;
                //totalLbl.Text = "Total No Of Records : " + total.ToString();
                //RowCountHiddenField.Value = total.ToString();//Update on 23/01/2017
            }*/
        }
        catch { }
    }
    public void EmptyDatatable(DataTable contractorcloseoutDt)
    {
        DataRow datatRow = contractorcloseoutDt.NewRow();
        datatRow["project_open_date"] = DateTime.Now.ToString("MM/dd/yyyy");

        //Inserting a new row,datatable .newrow creates a blank row
        contractorcloseoutDt.Rows.Add(datatRow);
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
    protected void ContractorCloseoutGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ContractorCloseoutGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void imgbtnSendEmail_Click(object sender, EventArgs e)
    {

    }
    //protected void ContractorCloseoutGridView_PreRender(object sender, EventArgs e)
    //{
    //    // You only need the following 2 lines of code if you are not 
    //    // using an ObjectDataSource of SqlDataSource
    //    string contractorid = ContractorIDHiddenField.Value;
    //    string status = ProjectStatusHiddenField.Value;
        
    //    BindGrid(status, contractorid);

    //    if (ContractorCloseoutGridView.Rows.Count > 0)
    //    {
    //        //This replaces <td> with <th> and adds the scope attribute
    //        ContractorCloseoutGridView.UseAccessibleHeader = true;

    //        //This will add the <thead> and <tbody> elements
    //        ContractorCloseoutGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

    //        //This adds the <tfoot> element. 
    //        //Remove if you don't have a footer row
    //        ContractorCloseoutGridView.FooterRow.TableSection = TableRowSection.TableFooter;
    //    }
    //}
}