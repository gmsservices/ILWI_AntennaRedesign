using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Net;

/* 
* Copyright (C) 2016-2017 Global Mobility Services (GMS) - All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited.
* Proprietary and confidential.
*/

/// <summary>
/// Summary description for ConnectionDatabase
/// </summary>
namespace ConnectionDatabase
{
    public class ConnectionDatabase
    {
        public ConnectionDatabase()
        {
            //
            // TODO: Add constructor logic here
            //
        }        
        public static MySqlConnection OpenConnection()
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

            MySqlConnection connection = new MySqlConnection(connString);
            //connection.Open();
            //Open connection
            if (connection.State == ConnectionState.Closed) connection.Open();
            return connection;
        }
        public static string FirstLetterToUpper(string str)
        {
            if (str != null)
            {
                if (str.Length > 1)
                    return char.ToUpper(str[0]) + str.Substring(1);
                else
                    return str.ToUpper();
            }
            return str;
        } 
        public static int GetNextAutoIncrementID(string tablename) 
        { 
            int incrId = 0;
            try
            {
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT AUTO_INCREMENT "+
                                     "FROM information_schema.tables "+
                                     "WHERE table_name = '" + tablename + "' " +
                                     "AND table_schema = DATABASE();";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    incrId = int.Parse(dr[0].ToString());
                }
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return incrId;
            }
            catch
            {
                return incrId;                
            }
        }
        private static string MySQLEscape(string str)
        {
            return Regex.Replace(str, @"[\x00'""\b\n\r\t\cZ\\%_]",
                delegate(Match match)
                {
                    string v = match.Value;
                    switch (v)
                    {
                        case "\x00":            // ASCII NUL (0x00) character
                            return "\\0";
                        case "\b":              // BACKSPACE character
                            return "\\b";
                        case "\n":              // NEWLINE (linefeed) character
                            return "\\n";
                        case "\r":              // CARRIAGE RETURN character
                            return "\\r";
                        case "\t":              // TAB
                            return "\\t";
                        case "\u001A":          // Ctrl-Z
                            return "\\Z";
                        default:
                            return "\\" + v;
                    }
                });
        } 
        public static int FindUser(string username, string password)
        {
            int active = 0;
            string firstName = string.Empty;
            string lastName = string.Empty;
            int userid = 0;
            int roleid = 0;
            string userfullname = string.Empty;
            string firstlogin = string.Empty;
            string contractor_id = string.Empty;
            string companylogin = string.Empty;
            string company = string.Empty;
            try
            {
                //MySQLEscape    
                username = MySQLEscape(username);
                password = MySQLEscape(password);

                DataTable userDt = new DataTable("User Table");
                //Find Username found in userlogin table
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT UL.`User_ID`, UL.`Role_ID`,UL.`FirstName`,UL.`LastName`," +
                                     "UL.`FistLogin`,UL.`contractor_id`,UL.`CompanyLogin`" +
                                     " FROM `UserLoginTable` UL " +
                                     "WHERE (UL.`UserName`='" + username + "' OR UL.`CompanyLogin`='" + username + "') " +
                                     " AND UL.`PassWord`='" + password + "'" +
                                     " AND UL.`IsActive`=1 ";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                // create data adapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(userDt);
                cmd.Dispose();
                foreach (DataRow rows in userDt.Rows)
                {
                    userid = int.Parse(rows[0].ToString());
                    roleid = int.Parse(rows[1].ToString());
                    firstName = rows[2].ToString();
                    lastName = rows[3].ToString();
                    userfullname = rows[2].ToString() + " " + rows[3].ToString();
                    firstlogin = rows[4].ToString();
                    contractor_id = rows[5].ToString();
                    companylogin = rows[6].ToString();

                    if (contractor_id == string.Empty || contractor_id == "")
                    {
                        contractor_id = "0";
                    }
                    //Find Username found in userlogin table
                    queryString = "SELECT `company` FROM `contractortbl` " +
                                         "WHERE `contractor_id`=" + contractor_id;
                    cmd = new MySqlCommand(queryString, connection);
                    cmd.CommandTimeout = 0;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            company = dr[0].ToString();
                        }//end if
                    }//end if
                    dr.Close();
                    cmd.Dispose();

                    //company login
                    if (username == companylogin && company != string.Empty)
                    {
                        roleid = 3;
                        userfullname = company;
                    }
                    active = roleid;
                    //Session Set
                    HttpContext.Current.Session["sessionUserName"] = username;
                    HttpContext.Current.Session["sessionUserID"] = userid;
                    HttpContext.Current.Session["sessionRoleID"] = roleid;
                    HttpContext.Current.Session["sessionUserFullName"] = userfullname;
                    HttpContext.Current.Session["sessionFistLogin"] = firstlogin;
                    HttpContext.Current.Session["sessionContractor_id"] = contractor_id;
                    HttpContext.Current.Session["sessionCompany"] = company;

                    //Updated On 12/08/2016 By Rinkal
                    HttpContext.Current.Session["SearchOption"] = "";
                    HttpContext.Current.Session["SearchValue"] = "";
                }

                cmd.Dispose();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }

                return active;

            }
            catch (Exception exception)
            {
                return 2;
                Console.WriteLine("Error to FindUser" + exception.Message);
            }
        }
        public static void GetUserID(string username, string password)
        {
            int userid = 0;
            int roleid = 0;
            string userfullname = string.Empty;             
            string firstlogin = string.Empty;
            string contractor_id = string.Empty;
            try
            {
                MySqlConnection connection = OpenConnection();
                               
                string queryString = "SELECT UL.`User_ID`,UL.`Role_ID`,UL.`FirstName`,UL.`LastName`, " +
                                     "UL.`FistLogin`,UL.`contractor_id`" +
                                     " FROM `UserLoginTable` UL " +
                                     "WHERE UL.`UserName`='" + username + "'  AND UL.`PassWord`='" + password + "'";

                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    userid = int.Parse(dr[0].ToString());
                    roleid = int.Parse(dr[1].ToString());
                    userfullname = dr[3].ToString() + " " + dr[2].ToString();                   
                    firstlogin = dr[4].ToString();
                    contractor_id = dr[5].ToString();
                }
                HttpContext.Current.Session["sessionUserID"] = userid;
                HttpContext.Current.Session["sessionRoleID"] = roleid;
                HttpContext.Current.Session["sessionUserFullName"] = userfullname;
                HttpContext.Current.Session["sessionFistLogin"] = firstlogin;
                HttpContext.Current.Session["sessionContractor_id"] = contractor_id;
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error to GetUserID" + exception.Message);
            }
        }
        public static string SearchUser(string username)  
        {
            string user = string.Empty;
            try
            {
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT * FROM `UserLoginTable` WHERE `UserName`='" + username + "' AND `IsActive`=1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user = dr[1].ToString();
                }                
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return user;
            }
            catch (Exception exception)
            {
                return user;                
            }
        }
        public static string[] ForgotPasswordByEmail(string email)  
        {
            string[] user = new string[6];
            try
            {
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT * FROM `UserLoginTable` WHERE `Email`='" + email + "' AND `IsActive`=1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user[0] = dr[0].ToString();
                    user[1] = dr[1].ToString();
                    user[2] = dr[2].ToString();
                    user[3] = dr[3].ToString();
                    user[4] = dr[4].ToString();
                    user[5] = dr[5].ToString();  
                }
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return user;
            }
            catch (Exception exception)
            {
                return user;
            }
        }
        public static string[] ForgotPasswordByUsername(string Username) 
        {
            string[] user = new string[6];
            try
            {
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT * FROM `UserLoginTable` WHERE `UserName`='" + Username + "' AND `IsActive`=1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user[0] = dr[0].ToString();
                    user[1] = dr[1].ToString();
                    user[2] = dr[2].ToString();
                    user[3] = dr[3].ToString();
                    user[4] = dr[4].ToString();
                    user[5] = dr[5].ToString();  
                }
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return user;
            }
            catch (Exception exception)
            {
                return user;
            }
        }
        public static string[] ForgotPassword(string Username, string email) 
        {
            string[] user = new string[6];
            try
            {
                MySqlConnection connection = OpenConnection();
                string queryString = "SELECT * FROM `UserLoginTable` WHERE `Email`='" + email + "' AND `UserName`='" + Username + "' AND `IsActive`=1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user[0] = dr[0].ToString();
                    user[1] = dr[1].ToString();
                    user[2] = dr[2].ToString();
                    user[3] = dr[3].ToString();
                    user[4] = dr[4].ToString();
                    user[5] = dr[5].ToString();  
                }
                dr.Close();
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return user;
            }
            catch (Exception exception)
            {
                return user;
            }
        }
        public static void LogInOfUser(int userid, string ip, string date, string time)
        {
            if (userid != 0 || ip != "" || date != "" || time != "")
            {
                try
                {
                    MySqlConnection connection = OpenConnection();
                    MySqlCommand cmd = new MySqlCommand("INSERT into `historytable` (`User_ID`,`LoginIP`,`LoginDate`,`LoginTime`,`LoginKey`) values(" + userid + ",'" + ip + "','" + date + "','" + time + "','I')", connection);
                    cmd.ExecuteNonQuery();
                    cmd.CommandTimeout = 0;
                    //connection.Close();
                    //connection.Dispose();
                    //Close connection
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                catch (Exception rty) { }
            }
        }
        public static void LogOutOfUser(int userid, string ip, string date, string time)
        {
            if (userid != 0 || ip != "" || date != "" || time != "")
            {
                try
                {
                    MySqlConnection connection = OpenConnection();
                    MySqlCommand cmd = new MySqlCommand("INSERT into `historytable` (`User_ID`,`LoginIP`,`LoginDate`,`LoginTime`,`LoginKey`) values(" + userid + ",'" + ip + "','" + date + "','" + time + "','O')", connection);
                    cmd.ExecuteNonQuery();
                    cmd.CommandTimeout = 0;
                    //connection.Close();
                    //connection.Dispose();
                    //Close connection
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                catch { }
            }
        }                
        public static bool CopyTable(string query)
        {
            bool copyEvent = false; 
            MySqlConnection connection1 = OpenConnection();
            string queryString1 = query;
            MySqlCommand cmd1 = new MySqlCommand(queryString1, connection1);
            int result = cmd1.ExecuteNonQuery();
            if (result > 0)
            {
                copyEvent = true;
            }
            else 
            {
                copyEvent = false;
            }
            cmd1.CommandTimeout = 0;
            //connection1.Close();
            //connection1.Dispose();
            //Close connection
            if (connection1.State == ConnectionState.Open)
            {
                connection1.Close();
                connection1.Dispose();
            }
            return copyEvent;
        }              
        public static int InsertInToTable(string query)
        {   
            int id = 0;            
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery(); // last inserted ID is recieved as any resultset on the first column of the first row                
            id = (int)cmd.LastInsertedId;  // this value will be changed if insertion suceede                
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return id;           
        }
        public static bool DeleteInToTable(string query)  
        {
            bool fireEvent = false;
           
            try
            {
                MySqlConnection connection = OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    fireEvent = true;
                }
                else
                {
                    fireEvent = false;
                }
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                return fireEvent;
            }
            catch (Exception rty)
            {
                return fireEvent;
            }
        }
        public static int SelectInToTable(string queryString) 
        {
            int active = 0;            
            MySqlConnection connection = OpenConnection();                
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            cmd.CommandTimeout = 0;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                active = int.Parse(dr[0].ToString());                  
            }
            dr.Close();
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return active;             
        }
        public static string SelectColumnInToTable(string queryString)
        {
            string active = string.Empty;
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            cmd.CommandTimeout = 0;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                active = dr[0].ToString();
            }
            dr.Close();
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return active;
        }
        public static string[] SelectColumnsInToTable(string queryString, int totalindex)
        {
            string[] columns = new string[totalindex]; 
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            cmd.CommandTimeout = 0;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int count = dr.FieldCount;
                for (int i = 0; i < count; i++)
                {
                    columns[i] = dr[i].ToString();
                }
            }
            dr.Close(); 
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return columns;
        }
        public static bool SelectRecordsInToTable(string queryString)
        {
            bool find = false;
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            cmd.CommandTimeout = 0;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                find = true;
            }
            dr.Close();
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return find;
        }
        public static bool UpdateInToTable(string query) 
        {
            bool msg = false;
            int id = 0;            
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                msg = true;
            }
            else
            {
                msg = false;
            }
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return msg;            
        }
        public static string SelectLatestIdInToTable(string queryString) 
        {
            string active = string.Empty;
            MySqlConnection connection = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            cmd.CommandTimeout = 0;
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                active = dr[0].ToString();
            }
            dr.Close();
            //connection.Close();
            //connection.Dispose();
            //Close connection
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return active;
        }
        //Update on 27/02/2017 Strat
        public static int InsertRetToolLogs(string Action, int contractorclose_id, int contractor_id, int User_ID,
                                    string User, string ipaddress, string filename, string GeneratedVai,
                                    string GeneratedTime, string Message)
        {
            string query = "INSERT INTO rettoollogstbl (`ActionTaken`,`contractorclose_id`,`contractor_id`,`User_ID`,`User`,`IPAddress`," +
                " `FileName`,`GeneratedVai`,`GeneratedTime`,`Message`) VALUES('" + Action + "'," + contractorclose_id + "," +
                " " + contractor_id + "," + User_ID + ",'" + User + "','" + ipaddress + "','" + filename + "'," +
                " '" + GeneratedVai + "','" + GeneratedTime + "','" + Message + "')";
            int id = InsertInToTable(query);

            return id;
        }
        //Update on 27/02/2017 End
        //Update On 06/03/2017 Start
        public static bool IsLocalIpAddress(string host)
        {
            try
            { // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch { }
            return false;
        }
        //Update On 06/03/2017 End
    }
}