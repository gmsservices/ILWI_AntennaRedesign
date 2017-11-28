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
using System.Data.OleDb;
using System.IO;

/* 
* Copyright (C) 2016-2017 Global Mobility Services (GMS) - All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited.
* Proprietary and confidential.
*/

/// <summary>
/// Summary description for DataTable
/// </summary>
namespace DumpDataTables
{
    public class DumpDataTables
    {
        public DumpDataTables() 
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static System.Data.DataTable RemoveAllNullRowsFromDataTable(System.Data.DataTable dt)
        {
            int columnCount = dt.Columns.Count;

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool allNull = true;
                for (int j = 0; j < columnCount; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value)
                    {
                        allNull = false;
                    }
                }
                if (allNull)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        //-----------------------------------------------------------------
        public static string setConnectionStringForExcel(string FilePath, string Extension, string isHDR) 
        {
            string connString = "";
            string conStr = "";
            string file = "";
            switch (Extension)
            {
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    //Input File
                    file = conStr;
                    break;
            }
            //Connection String to Excel Workbook 
            connString = String.Format(file, FilePath, isHDR);
            return connString;
        }
        //-----------------------------------------------------------------
        public static DataSet ReadExcelToGetSheetname(string FilePath, string querypart,string wherequery, int indexNo)  
        {
            String query = string.Empty;

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header); 
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetname = string.Empty;
            sheetname = Sheets.Rows[indexNo]["TABLE_NAME"].ToString().Replace("'", ""); 
            //Console.Write(sheetname);
            //query = String.Format("SELECT * FROM [{0}" + querypart + "]", sheetname);
            query = String.Format("SELECT * FROM [{0}{1}]", sheetname, querypart);
            query = query + wherequery;
            /*
            for (int i = 0; i < Sheets.Rows.Count; i++)
            {
                string worksheets = Sheets.Rows[i]["TABLE_NAME"].ToString();
                query = String.Format("SELECT * FROM [{0}]", worksheets);
            } */

            /*
            foreach (DataRow dr in Sheets.Rows)
            {
                sheetname = dr[indexNo].ToString().Replace("'", "");  
                //sheetname = dr["TABLE_NAME"].ToString();
                query = String.Format("SELECT * FROM [{0}]", sheetname);
            }  */          
            //if (sheetname != string.Empty) 
            //{
            //    query = "select * from [" + sheetname + "]" + querypart;
            //}  
         
            //Create the command object
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();

            da.Fill(ds);
            dt = ds.Tables[0];

            //Close OleDbConnection Connection
            da.Dispose();
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            RemoveAllNullRowsFromDataTable(dt);            
            return ds;
        }
        public static bool CheckExcelSheetName(string FilePath, string sheetName)   
        {
            bool find = false;

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            cmd.Connection = conn;
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //Console.Write(sheetname);

            foreach (DataRow dr in Sheets.Rows) 
            {
                string sheetname = dr["TABLE_NAME"].ToString().Replace("'", "");
                if (sheetname.Contains("$"))
                {
                    sheetname = sheetname.Replace("$", ""); 
                }
                if (sheetName == sheetname)
                {
                    find = true;
                    break;
                }
                else 
                {
                    find = false;
                }
            }            

            //Close OleDbConnection Connection            
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }

            return find;
        }
        public static bool CheckExcelSheetByWord(string FilePath, string[] sheetNameWord)  
        {
            bool find = false;

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            cmd.Connection = conn;
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //Console.Write(sheetname);
            int same = 0; 
            string getsheet = string.Empty;
            foreach (DataRow dr in Sheets.Rows)
            {
                string sheetname = dr["TABLE_NAME"].ToString().Replace("'", "");
                if (sheetname.Contains("$"))
                {
                    sheetname = sheetname.Replace("$", "");
                }
                if (!sheetname.Contains("_xlnm"))
                {
                    same = 0;
                    foreach (string sheetName in sheetNameWord)
                    {
                        if (sheetname.Contains(sheetName) == true)
                        {
                            same = 1;                            
                            getsheet = sheetname;
                            break;
                        }                        
                    }                    
                }
            }

            if (getsheet != null)
            {
                find = true;
            }
            else 
            {
                find = false;
            }
            //Close OleDbConnection Connection            
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }

            return find;
        }
        public static bool CheckExcelSheet(string FilePath, string sheetName) 
        {
            bool find = false;

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            cmd.Connection = conn;
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //Console.Write(sheetname);

            foreach (DataRow dr in Sheets.Rows)
            {
                string sheetname = dr["TABLE_NAME"].ToString().Replace("'", "");
                if (sheetname.Contains("$"))
                {
                    sheetname = sheetname.Replace("$", "");
                }
                if (sheetname.Contains(sheetName) == true)
                {
                    find = true;
                    break;
                }
                else
                {
                    find = false;
                }
            }

            //Close OleDbConnection Connection            
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }

            return find;
        }
        public static DataSet ReadExcelOfSheetToGetDataTable(string FilePath, string query, string querypart, string wherequery, int indexNo, string[] sheetNameWord)  
        {
            String findSheet = string.Empty; 

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            cmd.Connection = conn;
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //Console.Write(sheetname);

            foreach (DataRow dr in Sheets.Rows)
            {
                string sheetname = dr["TABLE_NAME"].ToString().Replace("'", "");
                if (sheetname.Contains("$"))
                {
                    sheetname = sheetname.Replace("$", "");
                }
                if (!sheetname.Contains("_xlnm"))
                { 
                    foreach (string word in sheetNameWord)
                    {
                        if (sheetname.Contains(word) == true)
                        {
                            findSheet = sheetname;
                            break;
                        }
                    }
                }
            }

            if (findSheet != string.Empty)
            {

                //Create the command object            
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                cmd.CommandText = "SELECT " + query + " From [" + findSheet + "$" + querypart + "]" + wherequery;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            //Close OleDbConnection Connection
            da.Dispose();
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            RemoveAllNullRowsFromDataTable(ds.Tables[0]);
            return ds;
        }
        public static DataSet ReadExcel(string FilePath,string query, string querypart, string wherequery, int indexNo) 
        {
            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();            

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            cmd.Connection = conn; 
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);                       
            //Console.Write(sheetname);

            //Create the command object            
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            cmd.CommandText = "SELECT " + query + " From [" + Sheets.Rows[indexNo]["TABLE_NAME"].ToString().Replace("'", "") + "" + querypart + "]" + wherequery;
            da.SelectCommand = cmd;
            da.Fill(ds);                       

            //Close OleDbConnection Connection
            da.Dispose();
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            RemoveAllNullRowsFromDataTable(ds.Tables[0]);
            return ds;
        }
        public static DataSet ReadExcelAnalysisSheet(string FilePath, string querypart, string wherequery, int indexNo) 
        {
            String query = string.Empty;

            //First Tab Connection 
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            //Response.Write(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string header = "Yes";
            string connString = setConnectionStringForExcel(FilePath, Extension, header);
            //Response.Write(connString);
            //Response.Write(query);        

            //Create the connection object
            conn = new OleDbConnection(connString);
            //Open connection
            if (conn.State == ConnectionState.Closed) conn.Open();

            DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetname = string.Empty;
            sheetname = "Analysis";
            query = "SELECT * FROM [" + sheetname + "$" + querypart + "]" + wherequery;           
            //Create the command object
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();

            da.Fill(ds);
            dt = ds.Tables[0];

            //Close OleDbConnection Connection
            da.Dispose();
            //conn.Dispose();
            //conn.Close();
            //Close connection
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            RemoveAllNullRowsFromDataTable(dt);
            return ds;
        }
    }
}