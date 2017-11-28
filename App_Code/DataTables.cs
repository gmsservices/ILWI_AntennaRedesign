using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/* 
* Copyright (C) 2016-2017 Global Mobility Services (GMS) - All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited.
* Proprietary and confidential.
*/

/// <summary>
/// Summary description for DataTables
/// </summary>
namespace IncludeDataTables
{ 
    public class DataTables
    {
        public DataTables()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataTable GetData(MySqlCommand cmd)
        {            
            DataTable dt = new DataTable();
            MySqlConnection con = ConnectionDatabase.ConnectionDatabase.OpenConnection();
            try
            {                
                MySqlDataAdapter sda = new MySqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                //con.Close();
                //Close connection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            catch { }
            finally
            {
                //con.Close();
                //con.Dispose();                
                //Close connection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return dt;
        }        
    }
}