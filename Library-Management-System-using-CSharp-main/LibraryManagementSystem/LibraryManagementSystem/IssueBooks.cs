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
using System.Net;

namespace LibraryManagementSystem
{
    public partial class IssueBooks : UserControl
    {

        SqlConnection connect;
      
        public IssueBooks()
        {
            InitializeComponent();
            connect = new SqlConnection(GlobalConnection.ConnectionString);
            displayBookIssueData();
            DataBookTitle();
            DataStudentTitle();


        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayBookIssueData();
            DataBookTitle();
            DataStudentTitle();
        }

        public void displayBookIssueData()
        {

            DataIssueBooks dib = new DataIssueBooks();
            List<DataIssueBooks> listData = dib.IssueBooksData();

            dataGridView1.DataSource = listData;
        }

        private void bookIssue_addBtn_Click(object sender, EventArgs e)
        {
            if(bookIssue_author.Text == ""
                || bookIssue_name.Text == ""
                || bookIssue_contact.Text == ""
                || bookIssue_email.Text == ""
                || bookIssue_bookTitle.Text == ""
                || bookIssue_id.Text == ""
                || bookIssue_issueDate.Value == null
                || bookIssue_returnDate.Value == null
                || bookIssue_status.Text == ""
                || bookIssue_picture.Image == null)
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        bookIssue_status.Text = "Not Return";
                        // 1.Kiểm tra học sinh tồn tại trong bảng student
                        string checkStudentQuery = "SELECT COUNT(*) FROM students WHERE id = @studentID and name = @Name and contact= @Contact and email=@Email";
                        using (SqlCommand cmdCheckStudent = new SqlCommand(checkStudentQuery, connect))
                        {
                            cmdCheckStudent.Parameters.AddWithValue("@studentID", bookIssue_id.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Name", bookIssue_name.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Contact", bookIssue_contact.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Email", bookIssue_email.Text.Trim());

                            int studentExists = Convert.ToInt32(cmdCheckStudent.ExecuteScalar());

                            if (studentExists == 0)
                            {
                                MessageBox.Show("Student information is false.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // 2.Kiểm tra sách tồn tại trong bảng book
                        string checkBookQuery = "SELECT COUNT(*) FROM books WHERE book_title = @bookTitle and author= @bookAuthor";
                        using (SqlCommand cmdCheckBook = new SqlCommand(checkBookQuery, connect))
                        {
                            cmdCheckBook.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                            cmdCheckBook.Parameters.AddWithValue("@bookAuthor", bookIssue_author.Text.Trim());

                            int bookExists = Convert.ToInt32(cmdCheckBook.ExecuteScalar());

                            if (bookExists == 0)
                            {
                                MessageBox.Show("Book information is false.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        // 3.Kiểm tra số lượng sách chưa trả của học sinh(student_id)
                        string checkIssuedBooksQuery = "SELECT COUNT(*) FROM issues WHERE issue_id = @issueID AND status = 'Not Return' AND date_delete IS NULL";
                        SqlCommand cmdCheckIssuedBooks = new SqlCommand(checkIssuedBooksQuery, connect);
                        cmdCheckIssuedBooks.Parameters.AddWithValue("@issueID", bookIssue_id.Text.Trim());
                        int issuedBooksCount = (int)cmdCheckIssuedBooks.ExecuteScalar();

                        if (issuedBooksCount >= 3)
                        {
                            MessageBox.Show("A student can only have 3 books that are not returned at the same time.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 4. Kiểm tra xem sách có đang được cấp phát và chưa trả hay không
                        string checkBookStatusQuery = "SELECT COUNT(*) FROM issues WHERE book_title = @bookTitle AND status = 'Not Return' AND date_delete IS NULL";
                        SqlCommand cmdCheckBookStatus = new SqlCommand(checkBookStatusQuery, connect);
                        cmdCheckBookStatus.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                        int bookStatusCount = (int)cmdCheckBookStatus.ExecuteScalar();

                        if (bookStatusCount > 0)
                        {
                            MessageBox.Show("The book title is already issued and not returned. You cannot issue it again until it is returned.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DateTime today = DateTime.Today;

                        string insertData = "INSERT INTO issues " +
                            "(issue_id, full_name, contact, email, book_title, author, status, issue_date, return_date, date_insert) " +
                            "VALUES(@issueID, @fullname, @contact, @email, @bookTitle, @author, @status, @issueDate, @returnDate, @dateInsert)";

                        using(SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@issueID", bookIssue_id.Text.Trim());
                            cmd.Parameters.AddWithValue("@fullname", bookIssue_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", bookIssue_contact.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", bookIssue_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@author", bookIssue_author.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", bookIssue_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@issueDate", bookIssue_issueDate.Value);
                            cmd.Parameters.AddWithValue("@returnDate", bookIssue_returnDate.Value); ;
                            cmd.Parameters.AddWithValue("@dateInsert", today);

                            cmd.ExecuteNonQuery();
                            // 5. Cập nhật giá trị Nissuebook trong bảng students
                            if (bookIssue_status.Text.Trim() == "Not Return")
                            {
                                string updateNissueQuery = "UPDATE students SET nissue_book = nissue_book + 1 WHERE id = @studentID";
                                using (SqlCommand cmdUpdateNissue = new SqlCommand(updateNissueQuery, connect))
                                {
                                    cmdUpdateNissue.Parameters.AddWithValue("@studentID", bookIssue_id.Text.Trim());
                                    cmdUpdateNissue.ExecuteNonQuery();
                                }
                            }

                            displayBookIssueData();

                            MessageBox.Show("Issued successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();

                        }
                    }
                    catch(Exception ex)
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

        public void clearFields()
        {
            bookIssue_author.Text = "";
            bookIssue_name.Text = "";
            bookIssue_contact.Text = "";
            bookIssue_email.Text = "";
            bookIssue_bookTitle.SelectedIndex = -1;
            bookIssue_id.SelectedIndex = -1;
            bookIssue_status.Text = "Not Return";
            bookIssue_picture.Image = null;
        }

        public void DataBookTitle()
        {
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT id, book_title FROM books WHERE status = 'Available' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        bookIssue_bookTitle.DataSource = table;
                        bookIssue_bookTitle.DisplayMember = "book_title";
                        bookIssue_bookTitle.ValueMember = "id";

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
            
        }
        public void DataStudentTitle()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT id FROM students WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        bookIssue_id.DataSource = table;
                        bookIssue_id.DisplayMember = "id";
                        bookIssue_id.ValueMember = "id";

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

        private void bookIssue_bookTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(connect.State != ConnectionState.Open)
            {
                if (bookIssue_bookTitle.SelectedValue != null)
                {
                    DataRowView selectedRow = (DataRowView)bookIssue_bookTitle.SelectedItem;
                    int selectID = Convert.ToInt32(selectedRow["id"]);
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM books WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", selectID);

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                bookIssue_author.Text = table.Rows[0]["author"].ToString();

                                string imagePath = table.Rows[0]["image"].ToString();

                                if (imagePath != null)
                                {
                                    bookIssue_picture.Image = Image.FromFile(imagePath);
                                }
                                else
                                {
                                    bookIssue_picture.Image = null;
                                }
                            }
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
        private void bookIssue_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                if (bookIssue_id.SelectedValue != null)
                {
                    DataRowView selectedRow = (DataRowView)bookIssue_id.SelectedItem;
                    int selectID = Convert.ToInt32(selectedRow["id"]);
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM students WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", selectID);

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                bookIssue_name.Text = table.Rows[0]["name"].ToString();
                                bookIssue_contact.Text = table.Rows[0]["contact"].ToString();
                                bookIssue_email.Text = table.Rows[0]["email"].ToString();
                            }
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
        int ID; 
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ID = (int)row.Cells[0].Value;
                bookIssue_id.Text = row.Cells[1].Value.ToString();
                bookIssue_name.Text = row.Cells[2].Value.ToString();
                bookIssue_contact.Text = row.Cells[3].Value.ToString();
                bookIssue_email.Text = row.Cells[4].Value.ToString();
                bookIssue_bookTitle.Text = row.Cells[5].Value.ToString();
                bookIssue_author.Text = row.Cells[6].Value.ToString();
                bookIssue_issueDate.Text = row.Cells[7].Value.ToString();
                bookIssue_returnDate.Text = row.Cells[8].Value.ToString();
                bookIssue_status.Text = row.Cells[9].Value.ToString();

            }
        }

        private void bookIssue_updateBtn_Click(object sender, EventArgs e)
        {
            if (bookIssue_author.Text == ""
                || bookIssue_name.Text == ""
                || bookIssue_contact.Text == ""
                || bookIssue_email.Text == ""
                || bookIssue_bookTitle.Text == ""
                || bookIssue_id.Text == ""
                || bookIssue_issueDate.Value == null
                || bookIssue_returnDate.Value == null
                || bookIssue_status.Text == ""
                || bookIssue_picture.Image == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Issue ID:"
                        + ID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            // 1.Kiểm tra học sinh tồn tại trong bảng student
                            string checkStudentQuery = "SELECT COUNT(*) FROM students WHERE id = @studentID and name = @Name and contact= @Contact and email=@Email";
                            using (SqlCommand cmdCheckStudent = new SqlCommand(checkStudentQuery, connect))
                            {
                                cmdCheckStudent.Parameters.AddWithValue("@studentID", bookIssue_id.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Name", bookIssue_name.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Contact", bookIssue_contact.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Email", bookIssue_email.Text.Trim());

                                int studentExists = Convert.ToInt32(cmdCheckStudent.ExecuteScalar());

                                if (studentExists == 0)
                                {
                                    MessageBox.Show("Student information is false.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            // 2.Kiểm tra sách tồn tại trong bảng book
                            string checkBookQuery = "SELECT COUNT(*) FROM books WHERE book_title = @bookTitle and author= @bookAuthor";
                            using (SqlCommand cmdCheckBook = new SqlCommand(checkBookQuery, connect))
                            {
                                cmdCheckBook.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                                cmdCheckBook.Parameters.AddWithValue("@bookAuthor", bookIssue_author.Text.Trim());

                                int bookExists = Convert.ToInt32(cmdCheckBook.ExecuteScalar());

                                if (bookExists == 0)
                                {
                                    MessageBox.Show("Book information is false.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            // 3.Kiểm tra số lượng sách chưa trả của học sinh(student_id)
                            string checkIssuedBooksQuery = "SELECT COUNT(*) FROM issues WHERE issue_id = @issueID AND status = 'Not Return' AND date_delete IS NULL";
                            SqlCommand cmdCheckIssuedBooks = new SqlCommand(checkIssuedBooksQuery, connect);
                            cmdCheckIssuedBooks.Parameters.AddWithValue("@issueID", bookIssue_id.Text.Trim());
                            int issuedBooksCount = (int)cmdCheckIssuedBooks.ExecuteScalar();

                            if (issuedBooksCount >= 3)
                            {
                                MessageBox.Show("A student can only have 3 books that are not returned at the same time.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // 4. Kiểm tra xem sách có đang được cấp phát và chưa trả hay không
                            string checkBookStatusQuery = "SELECT COUNT(*) FROM issues WHERE book_title = @bookTitle AND status = 'Not Return' AND date_delete IS NULL";
                            SqlCommand cmdCheckBookStatus = new SqlCommand(checkBookStatusQuery, connect);
                            cmdCheckBookStatus.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                            int bookStatusCount = (int)cmdCheckBookStatus.ExecuteScalar();

                            if (bookStatusCount > 0)
                            {
                                MessageBox.Show("The book title is already issued and not returned. You cannot issue it again until it is returned.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE issues SET issue_id = @issueID, full_name = @fullName, contact = @contact, email = @email" +
                                ", book_title = @bookTitle, author = @author, status = @status, issue_date = @issueDate" +
                                ", return_date = @returnDate, date_update = @dateUpdate WHERE id = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@fullName", bookIssue_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@contact", bookIssue_contact.Text.Trim());
                                cmd.Parameters.AddWithValue("@email", bookIssue_email.Text.Trim());
                                cmd.Parameters.AddWithValue("@bookTitle", bookIssue_bookTitle.Text.Trim());
                                cmd.Parameters.AddWithValue("@author", bookIssue_author.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", bookIssue_status.Text.Trim());
                                cmd.Parameters.AddWithValue("@issueDate", bookIssue_issueDate.Value);
                                cmd.Parameters.AddWithValue("@returnDate", bookIssue_returnDate.Value);
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@issueID", bookIssue_id.Text.Trim());
                                cmd.Parameters.AddWithValue("@ID", ID);

                                cmd.ExecuteNonQuery();

                                displayBookIssueData();

                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    else
                    {
                        MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private void bookIssue_deleteBtn_Click(object sender, EventArgs e)
        {
            if (bookIssue_author.Text == ""
                || bookIssue_name.Text == ""
                || bookIssue_contact.Text == ""
                || bookIssue_email.Text == ""
                || bookIssue_bookTitle.Text == ""
                || bookIssue_id.Text == ""
                || bookIssue_issueDate.Value == null
                || bookIssue_returnDate.Value == null
                || bookIssue_status.Text == ""
                || bookIssue_picture.Image == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to DELETE Issue ID:"
                        + ID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE issues SET date_delete = @dateDelete WHERE id = @ID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@dateDelete", today);
                                cmd.Parameters.AddWithValue("@ID",ID);

                                cmd.ExecuteNonQuery();

                                displayBookIssueData();

                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    else
                    {
                        MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void bookIssue_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_search.Text.Trim();
            string query = "SELECT * FROM issues WHERE date_delete IS NULL AND full_name LIKE @Keyword";
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
