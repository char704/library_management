using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    class DataAddStudents
    {
        SqlConnection connect;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Enroll { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int NissueBook { get; set; } 
        public string Image { get; set; }

        public List<DataAddStudents> AddStudentsData()
        {
            List<DataAddStudents> listData = new List<DataAddStudents>();
            connect = new SqlConnection(GlobalConnection.ConnectionString);

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM students WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataAddStudents das = new DataAddStudents();
                            das.ID = (int)reader["id"];
                            das.Name = reader["name"].ToString();
                            das.Enroll = reader["enroll"].ToString();
                            das.Contact = reader["contact"].ToString();
                            das.Email = reader["email"].ToString();
                            das.NissueBook = (int)reader["nissue_book"];
                            das.Image = reader["image"].ToString();

                            listData.Add(das);
                        }

                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to Database: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }

}
