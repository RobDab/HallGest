using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HallGest.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public string ResDate { get; set; }

        public string YearSerial { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string ResYear { get; set; }

        public string Deposit { get; set; }

        public string Fee { get; set; }

        public int Board { get; set; }

        public string BoardType { get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFiscalCode { get; set; }

        public string RoomNum { get; set; }

        public static List<Reservation> AllReservations()
        {
            List<Reservation> ReservationsList = new List<Reservation>();
            SqlConnection con = ConControl.DBConnection();
            con.Open();
            SqlCommand Cmd = new SqlCommand("SELECT * FROM ReservationsTab INNER JOIN BoardTypesTab ON Board = BoardTypeID", con);

            try
            {
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reservation res = new Reservation()
                        {
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            ResDate = Convert.ToDateTime(reader["ResDate"]).ToString("d"),
                            DateFrom = Convert.ToDateTime(reader["DateFrom"]).ToString("d"),
                            DateTo = Convert.ToDateTime(reader["DateTo"]).ToString("d"),
                            //ResYear = reader["ResYear"].ToString(),
                            //Deposit = Convert.ToDecimal(reader["Deposit"]),
                            Fee = Convert.ToDecimal(reader["Fee"]).ToString("C"),
                            //BoardType = reader["BoardType"].ToString(),
                            CustomerName = Customer.GetByID(Convert.ToInt32(reader["CustomerID"])).Lastname,
                            CustomerFiscalCode = Customer.GetByID(Convert.ToInt32(reader["CustomerID"])).FiscalCode,
                            RoomNum = reader["RoomNum"].ToString()
                        };

                        if (reader["YearSerial"] != DBNull.Value)
                        {
                            res.YearSerial = reader["YearSerial"].ToString();
                        }
                        if (reader["ResYear"] != DBNull.Value)
                        {
                            res.ResYear = reader["ResYear"].ToString();
                        }
                        if (reader["Deposit"] != DBNull.Value)
                        {
                            res.Deposit = Convert.ToDecimal(reader["Deposit"]).ToString("C");
                        }
                        if (reader["Board"] != DBNull.Value)
                        {
                            res.BoardType = reader["BoardType"].ToString();
                        }
                        ReservationsList.Add(res);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
              

            return ReservationsList;
        }

        public static void AddReservation(Reservation current, int customerID)
        {
            Customer currentCustomer = Customer.GetByID(customerID);

            SqlConnection con = ConControl.DBConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO ReservationTab VALUES (@ResDate, @YearSerial, @DF, @DT, @ResYear, @Deposit, @Fee, @Board, @CustomerID, @RoomNum)", con);
            cmd.Parameters.AddWithValue("ResDate", DateTime.Now.ToString("d"));
            cmd.Parameters.AddWithValue("YearSerial", current.ResYear + "-" + current.ReservationID);
            cmd.Parameters.AddWithValue("DF", current.DateFrom);
            cmd.Parameters.AddWithValue("DT", current.DateTo);
            if(current.ResYear != null)
            {
                cmd.Parameters.AddWithValue("ResYear", current.ResYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("ResYear", string.Empty);
            }
            cmd.Parameters.AddWithValue("Deposit", current.Deposit);
            cmd.Parameters.AddWithValue("Fee", current.Fee);
            cmd.Parameters.AddWithValue("Board", current.Board);
            cmd.Parameters.AddWithValue("CustomerID", current.CustomerID);
            cmd.Parameters.AddWithValue("RoomNum", current.RoomNum);

            cmd.ExecuteNonQuery();
        }
        
    }
}