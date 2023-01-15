using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HallGest.Models
{
    public class Board
    {
        public int BoardTypeID { get; set; }

        public string BoardType { get; set; }

        public static List<Board> GetAllBoards()
        {
            List<Board> boardList = new List<Board>();
            SqlConnection con = ConControl.DBConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM BoardTypesTab", con);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    Board current = new Board()
                    {
                        BoardTypeID = Convert.ToInt32(reader["BoardTypeID"]),
                        BoardType = reader["BoardType"].ToString()
                    };
                    boardList.Add(current);
                }
            }

            con.Close();
            return new List<Board>();
        }
    }
}