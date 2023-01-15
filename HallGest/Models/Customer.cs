using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HallGest.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string FiscalCode { get; set; }

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Mail { get; set; }

        public string PhoneNum { get; set; }

        public string MobileNum { get; set; }

        public static List<Customer> AllCustomers()
        {
            List<Customer> CustomersList = new List<Customer>();

            SqlConnection con = ConControl.DBConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM CustomersTab", con);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            FiscalCode = reader["FiscalCode"].ToString(),
                            Lastname = reader["Lastname"].ToString(),
                            Name = reader["Name"].ToString(),
                            City = reader["City"].ToString(),
                            Mail = reader["Mail"].ToString(),
                            MobileNum = reader["MobileNum"].ToString(),
                        };
                        if(reader["County"] != DBNull.Value)
                        {
                            customer.County = reader["County"].ToString();
                        }
                        if (reader["PhoneNum"] != DBNull.Value)
                        {
                            customer.PhoneNum = reader["PhoneNum"].ToString();
                        }
                        CustomersList.Add(customer);
                    }
                }

            }catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }




            return CustomersList;
        }

        public static Customer GetByID(int id)
        {
            Customer current = new Customer();

            SqlConnection con = ConControl.DBConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM CustomersTab WHERE CustomerID = @id", con);
            cmd.Parameters.AddWithValue("id", id);

            try 
            {
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerID = id,
                            FiscalCode = reader["FiscalCode"].ToString(),
                            Lastname = reader["Lastname"].ToString(),
                            Name = reader["Name"].ToString(),
                            City = reader["City"].ToString(),
                            Mail = reader["Mail"].ToString(),
                            MobileNum = reader["MobileNum"].ToString(),
                        };
                        if (reader["County"] != DBNull.Value)
                        {
                            customer.County = reader["County"].ToString();
                        }
                        if (reader["PhoneNum"] != DBNull.Value)
                        {
                            customer.PhoneNum = reader["PhoneNum"].ToString();
                        }
                        current = customer;
                    }
                    
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return current;
            
        }

        public static void AddCustomer(Customer c)
        {
            SqlConnection con = ConControl.DBConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO CustomersTab VALUES (@FC, @Lastname, @Name, @City, @County, @Mail, @PN, @MN)", con);
            cmd.Parameters.AddWithValue("FC", c.FiscalCode);
            cmd.Parameters.AddWithValue("Lastname",c.Lastname);
            cmd.Parameters.AddWithValue("Name", c.Name);
            cmd.Parameters.AddWithValue("City", c.City);
            if(c.County != null)
            {
                cmd.Parameters.AddWithValue("County", c.County);
            }
            else
            {
                cmd.Parameters.AddWithValue("County", string.Empty);
            }
            cmd.Parameters.AddWithValue("Mail", c.Mail);
            if(c.PhoneNum != null)
            {
                cmd.Parameters.AddWithValue("PN", c.PhoneNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("PN", string.Empty);
            }
            
            cmd.Parameters.AddWithValue("MN", c.MobileNum);

            cmd.ExecuteNonQuery();
        }
    }
}