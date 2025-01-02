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
using System.IO;

namespace LibraryManagementSystem
{
    public partial class AddBooks : UserControl
    {
        SqlConnection connect;
        
        public AddBooks()
        {
            InitializeComponent();
            connect = new SqlConnection(GlobalConnection.ConnectionString);
            displayBooks();
            displayViewImage(null);

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayBooks();
            displayViewImage(null);
        }

        private String imagePath;
        private void addBooks_importBtn_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addBooks_picture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addBooks_addBtn_Click(object sender, EventArgs e)
        {
            if (addBooks_picture.Image == null
                || addBooks_bookTitle.Text == ""
                || addBooks_author.Text == ""
                || addBooks_published.Value == null
                || addBooks_status.Text == ""
                || addBooks_picture.Image == null)
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {               
                        connect.Open();
                        //2.Kiểm tra sách tồn tại trong bảng book
                        string checkBookQuery = "SELECT COUNT(*) FROM books WHERE book_title = @bookTitle and author= @bookAuthor and published_date=@published_date";
                        using (SqlCommand cmdCheckBook = new SqlCommand(checkBookQuery, connect))
                        {
                            cmdCheckBook.Parameters.AddWithValue("@bookTitle", addBooks_bookTitle.Text.Trim());
                            cmdCheckBook.Parameters.AddWithValue("@bookAuthor", addBooks_author.Text.Trim());
                            cmdCheckBook.Parameters.AddWithValue("@published_date", addBooks_published.Value);

                            int bookExists = Convert.ToInt32(cmdCheckBook.ExecuteScalar());

                            if (bookExists > 0)
                            {
                                MessageBox.Show("Book information is exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        DateTime today = DateTime.Today;
                        string insertData = "INSERT INTO books " +
                            "(book_title, author, published_date, status, image, date_insert) " +
                            "VALUES(@bookTitle, @author, @published_date, @status,@image ,@dateInsert)";

                        string path = Path.Combine(GlobalConnection.Books_Diretory +
                            addBooks_bookTitle.Text + addBooks_author.Text.Trim() + ".jpg");

                        string directoryPath = Path.GetDirectoryName(path);

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        File.Copy(addBooks_picture.ImageLocation, path, true);
                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@bookTitle", addBooks_bookTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@author", addBooks_author.Text.Trim());
                            cmd.Parameters.AddWithValue("@published_date", addBooks_published.Value);
                            cmd.Parameters.AddWithValue("@status", addBooks_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@image", path);
                            cmd.Parameters.AddWithValue("@dateInsert", today);

                            cmd.ExecuteNonQuery();

                            displayBooks();

                            MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        public void clearFields()
        {
            addBooks_bookTitle.Text = "";
            addBooks_author.Text = "";
            addBooks_picture.Image = null;
            addBooks_status.SelectedIndex = -1;
        }

        public void displayBooks()
        {
            DataAddBooks dab = new DataAddBooks();
            List<DataAddBooks> listData = dab.addBooksData();

            dataGridView1.DataSource = listData;

        }

        private int bookID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                bookID = (int)row.Cells[0].Value;
                addBooks_bookTitle.Text = row.Cells[1].Value.ToString();
                addBooks_author.Text = row.Cells[2].Value.ToString();
                addBooks_published.Text = row.Cells[3].Value.ToString();

                string imagePath = row.Cells[4].Value.ToString();


                if (imagePath != null || imagePath.Length >= 1)
                {
                    addBooks_picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    addBooks_picture.Image = null;
                }
                addBooks_status.Text = row.Cells[5].Value.ToString();
            }
        }

        private void addBooks_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addBooks_updateBtn_Click(object sender, EventArgs e)
        {
            if (addBooks_picture.Image == null
                || addBooks_bookTitle.Text == ""
                || addBooks_author.Text == ""
                || addBooks_published.Value == null
                || addBooks_status.Text == ""
                || addBooks_picture.Image == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Book ID:"
                        + bookID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            string checkBookQuery = "SELECT COUNT(*) FROM books WHERE book_title = @bookTitle and author= @bookAuthor and published_date=@published_date";
                            using (SqlCommand cmdCheckBook = new SqlCommand(checkBookQuery, connect))
                            {
                                cmdCheckBook.Parameters.AddWithValue("@bookTitle", addBooks_bookTitle.Text.Trim());
                                cmdCheckBook.Parameters.AddWithValue("@bookAuthor", addBooks_author.Text.Trim());
                                cmdCheckBook.Parameters.AddWithValue("@published_date", addBooks_published.Value);
                                cmdCheckBook.Parameters.AddWithValue("@Status", addBooks_status.Text);

                                int bookExists = Convert.ToInt32(cmdCheckBook.ExecuteScalar());

                                if (bookExists > 0)
                                {
                                    MessageBox.Show("Book information is exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE books SET book_title = @bookTitle" +
                                ", author = @author, published_date = @published" +
                                ", status = @status, date_update = @dateUpdate WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@bookTitle", addBooks_bookTitle.Text.Trim());
                                cmd.Parameters.AddWithValue("@author", addBooks_author.Text.Trim());
                                cmd.Parameters.AddWithValue("@published", addBooks_published.Value);
                                cmd.Parameters.AddWithValue("@status", addBooks_status.Text.Trim());
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@id", bookID);

                                cmd.ExecuteNonQuery();

                                displayBooks();

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

        private void addBooks_deleteBtn_Click(object sender, EventArgs e)
        {
            if (addBooks_picture.Image == null
                || addBooks_bookTitle.Text == ""
                || addBooks_author.Text == ""
                || addBooks_published.Value == null
                || addBooks_status.Text == ""
                || addBooks_picture.Image == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to DELETE Book ID:"
                        + bookID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE books SET date_delete = @dateDelete WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@dateDelete", today);
                                cmd.Parameters.AddWithValue("@id", bookID);

                                cmd.ExecuteNonQuery();

                                displayBooks();

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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_search.Text.Trim();
            displayViewImage(keyword);
            string query = "SELECT * FROM Books WHERE date_delete IS NULL AND book_title LIKE @Keyword";
            List<DataAddBooks> listData = new List<DataAddBooks>();
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
                            DataAddBooks dab = new DataAddBooks
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                BookTitle = reader["book_title"].ToString(),
                                Author = reader["author"].ToString(),
                                Pulished = reader["published_date"].ToString(),
                                image = reader["image"].ToString(),
                                Status = reader["status"].ToString()
                            };

                            listData.Add(dab);
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
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void displayViewImage(string keyword)
        {
            try
            {
                // SQL query to select the ID, book title, author, published date, and image

                string selectQuery = string.IsNullOrEmpty(keyword) ?
                              "SELECT id, book_title, author, published_date, image FROM books WHERE date_delete IS NULL" :
                              "SELECT id, book_title, author, published_date, image FROM books WHERE date_delete IS NULL AND book_title LIKE @Keyword" ;
                using (SqlCommand cmd = new SqlCommand(selectQuery, connect))
                {
                    // Configure DataGridView
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    }
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.RowTemplate.Height = 100;
                    dataGridView2.AllowUserToAddRows = false;
                    dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;  // Enable word wrapping

                    // Create a DataTable to store the result
                    DataTable table = new DataTable();

                    // Use SqlDataAdapter to fill the DataTable with data from the database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);

                    // Set the DataGridView's DataSource to the DataTable
                    dataGridView2.DataSource = table;
                    // Add a new column for ID
                    if (!dataGridView2.Columns.Contains("IDColumn"))
                    {
                        DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                        idColumn.HeaderText = "ID";
                        idColumn.Name = "IDColumn";
                        dataGridView2.Columns.Add(idColumn);
                    }
                    // Add an image column to DataGridView if it doesn't exist
                    if (!dataGridView2.Columns.Contains("ImageColumn"))
                    {
                        DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                        imgColumn.HeaderText = "Image";
                        imgColumn.Name = "ImageColumn";
                        imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Set zoom layout for image
                        dataGridView2.Columns.Add(imgColumn);
                    }
                    // Add a new column to combine multiple attributes (book title, author, published date)
                    if (!dataGridView2.Columns.Contains("CombinedInfo"))
                    {
                        DataGridViewTextBoxColumn combinedColumn = new DataGridViewTextBoxColumn();
                        combinedColumn.HeaderText = "Book Info";
                        combinedColumn.Name = "CombinedInfo";
                        dataGridView2.Columns.Add(combinedColumn);
                    }
                    dataGridView2.Columns["ImageColumn"].Width = 150; // Sets the width of the Image column
                    dataGridView2.Columns["IDColumn"].Width = 30; // Sets the width of the ID column
                    dataGridView2.Columns["CombinedInfo"].Width = 250; // Sets the width of the Combined Info column

                    // Hide the unnecessary columns
                    foreach (DataGridViewColumn column in dataGridView2.Columns)
                    {
                        if (column.Name != "IDColumn" && column.Name != "ImageColumn" && column.Name != "CombinedInfo")
                        {
                            column.Visible = false;
                        }
                    }

                    // Loop through the rows to set the image, ID, and combined info
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            // Get the data for the current row
                            string bookID = row.Cells["id"].Value.ToString();
                            string bookTitle = row.Cells["book_title"].Value.ToString();
                            string author = row.Cells["author"].Value.ToString();
                            string publishedDate = row.Cells["published_date"].Value.ToString();

                            // Set the ID in the ID column
                            row.Cells["IDColumn"].Value = bookID;

                            // Combine the book title, author, and published date into separate lines
                            string combinedInfo = $"Title: {bookTitle}{Environment.NewLine}Author: {author}{Environment.NewLine}Published: {publishedDate}";

                            // Set the combined info in the new column
                            row.Cells["CombinedInfo"].Value = combinedInfo;

                            // Handle the image column by checking the image path
                            if (row.Cells["image"].Value != DBNull.Value)
                            {
                                string imagePath = row.Cells["image"].Value.ToString();
                                if (File.Exists(imagePath)) // Check if the file exists
                                {
                                    Image img = Image.FromFile(imagePath); // Load the image from the file path
                                    row.Cells["ImageColumn"].Value = img; // Set the image in the ImageColumn
                                }
                                else
                                {
                                    row.Cells["ImageColumn"].Value = null; // Set null if the image doesn't exist
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close(); // Close the connection
            }
        }

        private void Change_Stage_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Visible)
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
                Change_Stage.Text = "Can Edit";
            }
            else if (dataGridView1.Visible)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                Change_Stage.Text = "View Only";
            }
        }
        public void SetButtonText(string username)
        {
            if (username.ToLower().Contains("admin"))
            {
                Change_Stage.Visible = true;  // Hiển thị nút nếu có từ khóa 'admin'
            }
            else
            {
                Change_Stage.Visible = false;  // Ẩn nút nếu không có từ khóa 'admin'
            }
        }
    }

}




