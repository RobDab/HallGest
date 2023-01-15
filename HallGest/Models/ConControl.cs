using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HallGest.Models
{
    public static class ConControl
    {
        public static SqlConnection DBConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IbisHotelDbConnetion"].ToString());

            return con;
        }

        public static SqlDataReader DBDataReader(SqlCommand Cmd) 
        {
            SqlDataReader reader = Cmd.ExecuteReader();

            return reader;
        }
    }
}