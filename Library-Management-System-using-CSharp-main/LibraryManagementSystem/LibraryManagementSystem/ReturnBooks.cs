using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class ReturnBooks : UserControl
    {
        SqlConnection connect;


        public ReturnBooks()
        {
            InitializeComponent();
            connect = new SqlConnection(GlobalConnection.ConnectionString);

            displayIssuedBooksData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayIssuedBooksData();
        }

        private void returnBooks_returnBtn_Click(object sender, EventArgs e)
        {
            if(returnBooks_issueID.Text == ""
                || returnBooks_name.Text == ""
                || returnBooks_contact.Text == ""
                || returnBooks_email.Text == ""
                || returnBooks_bookTitle.Text == ""
                || returnBooks_author.Text == ""
                || bookIssue_issueDate.Value == null){
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    DialogResult check = MessageBox.Show("Are you sure that ID: "
                        + ID + " is return already?", "Confirmation Message", MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question);

                    if(check == DialogResult.Yes)
                    {
                        try
                        {
                            DateTime today = DateTime.Today;
                            connect.Open();

                            string updateData = "UPDATE issues SET status = @status, date_update = @dateUpdate " +
                                "WHERE id = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@status", "Return");
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@ID", ID);

                                cmd.ExecuteNonQuery();
                                // 5. Cập nhật giá trị Nissuebook trong bảng students
                                    string updateNissueQuery = "UPDATE students SET nissue_book = nissue_book -1 WHERE id = @studentID";
                                    using (SqlCommand cmdUpdateNissue = new SqlCommand(updateNissueQuery, connect))
                                    {
                                        cmdUpdateNissue.Parameters.AddWithValue("@studentID", returnBooks_issueID.Text.Trim());
                                        cmdUpdateNissue.ExecuteNonQuery();
                                    }                                
                                displayIssuedBooksData();

                                MessageBox.Show("Returned successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                    
                }
            }
        }

        public void displayIssuedBooksData()
        {
            DataIssueBooks dib = new DataIssueBooks();
            List<DataIssueBooks> listData = dib.ReturnIssueBooksData();

            dataGridView1.DataSource = listData;
        }
        int ID;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ID = (int)row.Cells[0].Value;
                returnBooks_issueID.Text = row.Cells[1].Value.ToString();
                returnBooks_name.Text = row.Cells[2].Value.ToString();
                returnBooks_contact.Text = row.Cells[3].Value.ToString();
                returnBooks_email.Text = row.Cells[4].Value.ToString();
                returnBooks_bookTitle.Text = row.Cells[5].Value.ToString();
                returnBooks_author.Text = row.Cells[6].Value.ToString();
                bookIssue_issueDate.Text = row.Cells[7].Value.ToString();
            }
        }

        public void clearFields()
        {
            returnBooks_issueID.Text = "";
            returnBooks_name.Text = "";
            returnBooks_contact.Text = "";
            returnBooks_email.Text = "";
            returnBooks_bookTitle.Text = "";
            returnBooks_author.Text = "";
        }

        private void returnBooks_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_search.Text.Trim();
            string query = "SELECT * FROM issues WHERE status = 'Not Return' AND date_delete IS NULL AND full_name LIKE @Keyword";
            List<DataIssueBooks> listData = new List<DataIssueBooks>();
            try
            {
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                }
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataIssueBooks dib = new DataIssueBooks();
                            dib.ID = (int)reader["id"];
                            dib.IssueID = reader["issue_id"].ToString();
                            dib.Name = reader["full_name"].ToString();
                            dib.Contact = reader["contact"].ToString();
                            dib.Email = reader["email"].ToString();
                            dib.BookTitle = reader["book_title"].ToString();
                            dib.Author = reader["author"].ToString();
                            dib.DateIssue = reader["issue_date"].ToString();
                            dib.DateReturn = reader["return_date"].ToString();
                            dib.Status = reader["status"].ToString();

                            listData.Add(dib);
                        }
                    }
                }
                dataGridView1.DataSource = listData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Database: " + ex.Message);
            }
            finally
            {
                if (connect.State == System.Data.ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

    }
}
