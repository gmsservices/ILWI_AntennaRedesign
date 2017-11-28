using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//Install-Package EPPlus -Version 4.1.1

/* 
* Copyright (C) 2016-2017 Global Mobility Services (GMS) - All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited.
* Proprietary and confidential.
*/

/// <summary>
/// Summary description for AnatennaPlatforms
/// </summary>
namespace AnatennaPlatforms
{
    public class AnatennaPlatforms
    {
        public AnatennaPlatforms()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string Encode(string toEncode)
        {
            if (toEncode.Contains("'"))
            {
                toEncode = toEncode.Replace("'", "''");
            }
            else if (toEncode.Contains("%"))
            {
                toEncode = toEncode.Replace("%", "\\%");
            }
            else if (toEncode.Contains("\\"))
            {
                toEncode = toEncode.Replace("\\", "\\\\");
            }
            else
            {
                toEncode = toEncode;
            }
            return toEncode;
        }
        public static void BindDropDownList(DropDownList ddl, string query, int index,int valueindex)
        {
            MySqlConnection myconn = null;
            MySqlCommand cmd2 = null;
            MySqlDataAdapter da2 = null;
            DataSet ds2 = null;

            //Create the connection object
            myconn = ConnectionDatabase.ConnectionDatabase.OpenConnection();

            //Open connection
            if (myconn.State == ConnectionState.Closed) myconn.Open();

            //Create the command object
            cmd2 = new MySqlCommand(query, myconn);
            cmd2.CommandTimeout = 0;
            
            da2 = new MySqlDataAdapter(cmd2);
            ds2 = new DataSet();
            da2.Fill(ds2, "qual");
            //myconn.Close();
            //myconn.Dispose();    
            //Close connection
            if (myconn.State == ConnectionState.Open)
            {
                myconn.Close();
                myconn.Dispose();
            }

            ListItem item2 = new ListItem("...Select...", "0");
            //item2.Selected = true;

            ddl.Items.Clear();
            //ddl.Items.Add(item2);

            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = ds2.Tables["qual"];
                ddl.DataTextField = ds2.Tables["qual"].Columns[valueindex].ColumnName.ToString();
                ddl.DataValueField = ds2.Tables["qual"].Columns[index].ColumnName.ToString();
                ddl.DataBind();
                ddl.Items.Insert(0, item2);
            }
           
        }
        public static string[] FindAvailableAntenna(string queryString) 
        {
            string[] ids = new string[1];
            MySqlConnection connection = ConnectionDatabase.ConnectionDatabase.OpenConnection();
            try
            {  
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.CommandTimeout = 0;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ids[0] = dr[0].ToString();
                }
                else 
                {
                    ids[0] = "false";
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
                return ids;
            }
            catch (Exception exception)
            {
                ids[0] = "false";
                return ids;
            }
            finally
            {
                //connection.Close();
                //connection.Dispose();
                //Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        
        public static DataSet Ret_ConnectionString(string FilePath, string sheetName)
        {
            DataSet modeTrackerDs = new DataSet();

            System.Data.DataTable dt = new System.Data.DataTable(sheetName);
            DataRow dr = null;

            // Get the file we are going to process
            var existingFile = new FileInfo(FilePath);
            // Open and read the XlSX file.
            using (var package = new ExcelPackage(existingFile))
            {
                // Get the work book in the file
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // Get the worksheet                    
                        ExcelWorksheet currentWorksheet = workBook.Worksheets[sheetName];

                        // read some data 
                        int totalRows = currentWorksheet.Dimension.End.Row;
                        int totalCols = currentWorksheet.Dimension.End.Column;

                        // Get header values
                        var column1Header = currentWorksheet.Cells["A2"].GetValue<string>(); //1 
                        var column2Header = currentWorksheet.Cells["B2"].GetValue<string>(); //2 
                        var column3Header = currentWorksheet.Cells["C2"].GetValue<string>(); //3 
                        var column4Header = currentWorksheet.Cells["D2"].GetValue<string>(); //4 
                        var column5Header = currentWorksheet.Cells["E2"].GetValue<string>(); //5 
                        var column6Header = currentWorksheet.Cells["F2"].GetValue<string>(); //6                    
                        var column7Header = currentWorksheet.Cells["G2"].GetValue<string>(); //7 

                        for (int i = 1; i <= totalRows; i++)
                        {
                            if (i == 1)
                            {
                                dt.Columns.Add(column1Header);
                                dt.Columns.Add(column2Header);
                                dt.Columns.Add(column3Header);
                                dt.Columns.Add(column4Header);
                                dt.Columns.Add(column5Header);
                                dt.Columns.Add(column6Header);
                                dt.Columns.Add(column7Header);
                            }
                            if (i > 2)
                            {
                                dr = dt.Rows.Add();
                            }

                            for (int j = 1; j <= totalCols; j++)
                            {
                                if ((i == 1 || j == 1) || (i == 2 || j == 2))
                                { }
                                else
                                {
                                    if (column1Header.Contains("Position on mount") == true ||
                                        column2Header.Contains("Position Number") == true ||
                                        column3Header.Contains("Sector Color Code") == true ||
                                        column4Header.Contains("Insert the required photo below") == true ||
                                        column5Header.Contains("RET Source") == true ||
                                        column6Header.Contains("Casscading Order") == true ||
                                        column7Header.Contains("RET Serial Number") == true
                                        )
                                    {
                                        if (currentWorksheet.Cells[i, 1].Value != null)
                                        {
                                            //Position on mount
                                            if (currentWorksheet.Cells[i, 1].Value != null)
                                            {
                                                dr[column1Header] = currentWorksheet.Cells[i, 1].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column1Header] = "";
                                            }
                                            //Position Number
                                            if (currentWorksheet.Cells[i, 2].Value != null)
                                            {
                                                dr[column2Header] = currentWorksheet.Cells[i, 2].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column2Header] = "";
                                            }
                                            //Sector Color Code
                                            if (currentWorksheet.Cells[i, 3].Value != null)
                                            {
                                                dr[column3Header] = currentWorksheet.Cells[i, 3].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column3Header] = "";
                                            }
                                            //Insert the required photo below
                                            if (currentWorksheet.Cells[i, 4].Value != null)
                                            {
                                                dr[column4Header] = currentWorksheet.Cells[i, 4].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column4Header] = "";
                                            }
                                            //RET Source
                                            if (currentWorksheet.Cells[i, 5].Value != null)
                                            {
                                                dr[column5Header] = currentWorksheet.Cells[i, 5].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column5Header] = "";
                                            }
                                            //Casscading Order
                                            if (currentWorksheet.Cells[i, 6].Value != null)
                                            {
                                                dr[column6Header] = currentWorksheet.Cells[i, 6].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column6Header] = "";
                                            }
                                            //RET Serial Number
                                            if (currentWorksheet.Cells[i, 7].Value != null)
                                            {
                                                dr[column7Header] = currentWorksheet.Cells[i, 7].Value.ToString();
                                            }
                                            else
                                            {
                                                dr[column7Header] = "";
                                            }
                                        }
                                    }//end if - check header
                                }//end else if
                            }//end rows for loop

                        }//end columns for loop

                        //Remove Blank Rows from data table
                        RemoveAllNullRowsFromDataTable(dt);
                        //Response.Write("No. of rows : " + dt.Rows.Count.ToString());

                    }// end if
                }//end if
            }
            //return dt;
            modeTrackerDs.Tables.Add(dt);
            return modeTrackerDs;
        }
        private static String ColorHexValue(System.Drawing.Color C)
        {
            return C.A.ToString("X2") + C.R.ToString("X2") + C.G.ToString("X2") + C.B.ToString("X2");
        }
        public static void RemoveAllNullRowsFromDataTable(System.Data.DataTable dt)
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
        }
        public static string[] FindAllColumns(DataSet Ds, string[] arrStr)
        {
            int same = 0;           
            string[] findArr = new string[2];
            string getColumns = "";

            //Fill Header in Array                    
            string[] headerarr = new string[11];
            
            //Fill Header in Array                     
            headerarr = (from DataColumn x in Ds.Tables[0].Columns
                         select x.ColumnName).ToArray();

            foreach (var arr in arrStr)
            {
                same = 0;                
                foreach (var header in headerarr)
                {
                    if (header.ToString().Contains(arr.ToString()) == true)
                    {
                        same = 1;                        
                        break;
                    }
                }
                if (same == 0)
                {
                    getColumns = getColumns + "[" + arr.ToString() + "] ";
                    findArr[0] = " not in fixed positions.";
                    findArr[1] = getColumns;
                }
            }

            return findArr;
        }
    }
}