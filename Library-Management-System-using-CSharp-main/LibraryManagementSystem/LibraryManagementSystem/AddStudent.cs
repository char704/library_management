using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AddStudent : UserControl
    {
        SqlConnection connect;
        private int studentID = 0;
        public AddStudent()
        {
            InitializeComponent();
            connect = new SqlConnection(GlobalConnection.ConnectionString);
            displayStudents();
            displayViewImage(null);
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayStudents();
            displayViewImage(null);
        }
        private String imagePath;
        private void addStudents_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addStudents_picture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addStudent_addBtn_Click(object sender, EventArgs e)
        {
            if (addStudents_picture.Image == null
               || addStudents_name.Text == ""
               || addStudents_enroll.Text == ""
               || addStudents_contact.Text == ""
               || addStudents_email.Text == ""
               || addStudents_picture.Image == null)
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
                        // 1.Kiểm tra học sinh tồn tại trong bảng student
                        string checkStudentQuery = "SELECT COUNT(*) FROM students WHERE enroll =@Enroll and name = @Name and contact= @Contact and email=@Email";
                        using (SqlCommand cmdCheckStudent = new SqlCommand(checkStudentQuery, connect))
                        { 
                            cmdCheckStudent.Parameters.AddWithValue("@Name", addStudents_name.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Contact", addStudents_contact.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Email", addStudents_email.Text.Trim());
                            cmdCheckStudent.Parameters.AddWithValue("@Enroll", addStudents_enroll.Text.Trim());

                            int studentExists = Convert.ToInt32(cmdCheckStudent.ExecuteScalar());

                            if (studentExists > 0)
                            {
                                MessageBox.Show("Student information is exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        DateTime today = DateTime.Today;
                        string insertData = "INSERT INTO students " +
                            "(name, enroll, contact, email, nissue_book, image, date_insert) " +
                            "VALUES(@name, @enroll, @contact, @email, @nissueBook, @image, @dateInsert)";

                        string path = Path.Combine(GlobalConnection.Student_Directory +
                            addStudents_name.Text + addStudents_enroll.Text.Trim() + ".jpg");

                        string directoryPath = Path.GetDirectoryName(path);

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        File.Copy(addStudents_picture.ImageLocation, path, true);

                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@name", addStudents_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@enroll", addStudents_enroll.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", addStudents_contact.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", addStudents_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@nissueBook", 0); // Mặc định là 0
                            cmd.Parameters.AddWithValue("@image", path);
                            cmd.Parameters.AddWithValue("@dateInsert", today);

                            cmd.ExecuteNonQuery();

                            displayStudents();

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
            addStudents_name.Text = "";
            addStudents_enroll.Text = "";
            addStudents_contact.Text = "";
            addStudents_email.Text = "";
            addStudents_picture.Image = null;
        }
        public void displayStudents()
        {
            DataAddStudents das = new DataAddStudents();
            List<DataAddStudents> listData = das.AddStudentsData();

            dataGridView1.DataSource = listData;
        }

        private void addStudent_updateBtn_Click(object sender, EventArgs e)
        {
            if (addStudents_picture.Image == null
             || addStudents_name.Text == ""
             || addStudents_enroll.Text == ""
             || addStudents_contact.Text == ""
             || addStudents_email.Text == ""
             || addStudents_picture.Image == null)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Student ID:"
                        + studentID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            // 1.Kiểm tra học sinh tồn tại trong bảng student
                            string checkStudentQuery = "SELECT COUNT(*) FROM students WHERE enroll =@Enroll and name = @Name and contact= @Contact and email=@Email";
                            using (SqlCommand cmdCheckStudent = new SqlCommand(checkStudentQuery, connect))
                            {
                                cmdCheckStudent.Parameters.AddWithValue("@Name", addStudents_name.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Contact", addStudents_contact.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Email", addStudents_email.Text.Trim());
                                cmdCheckStudent.Parameters.AddWithValue("@Enroll", addStudents_enroll.Text.Trim());

                                int studentExists = Convert.ToInt32(cmdCheckStudent.ExecuteScalar());

                                if (studentExists > 0)
                                {
                                    MessageBox.Show("Student information is exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE students SET name = @name" +
                                ", enroll = @enroll, contact = @contact" +
                                ", email = @email, date_update = @dateUpdate WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@name", addStudents_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@enroll", addStudents_enroll.Text.Trim());
                                cmd.Parameters.AddWithValue("@contact", addStudents_contact.Text.Trim());
                                cmd.Parameters.AddWithValue("@email", addStudents_email.Text.Trim());
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@id", studentID);

                                cmd.ExecuteNonQuery();

                                displayStudents();

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

        private void addStudent_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addStudent_deleteBtn_Click(object sender, EventArgs e)
        {
            if (addStudents_picture.Image == null
                || addStudents_name.Text == ""
                || addStudents_enroll.Text == ""
                || addStudents_contact.Text == ""
                || addStudents_email.Text == "")
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    DialogResult check = MessageBox.Show("Are you sure you want to DELETE Student ID:"
                        + studentID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (check == DialogResult.Yes)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;
                            string updateData = "UPDATE students SET date_delete = @dateDelete WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@dateDelete", today);
                                cmd.Parameters.AddWithValue("@id", studentID);

                                cmd.ExecuteNonQuery();

                                displayStudents();

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
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                studentID = (int)row.Cells[0].Value;
                addStudents_name.Text = row.Cells[1].Value.ToString();
                addStudents_enroll.Text = row.Cells[2].Value.ToString();
                addStudents_contact.Text = row.Cells[3].Value.ToString();
                addStudents_email.Text = row.Cells[4].Value.ToString();

                string imagePath = row.Cells[6].Value.ToString();

                if (imagePath != null || imagePath.Length >= 1)
                {
                    addStudents_picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    addStudents_picture.Image = null;
                }
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_search.Text.Trim();
            displayViewImage(keyword);
            string query = "SELECT * FROM students WHERE date_delete IS NULL AND name LIKE @Keyword";
            List<DataAddStudents> listData = new List<DataAddStudents>();
            try
            {
                if (connect.State == ConnectionState.Closed)
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
                            DataAddStudents das = new DataAddStudents
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader["name"].ToString(),
                                Enroll = reader["enroll"].ToString(),
                                Contact = reader["contact"].ToString(),
                                Email = reader["email"].ToString(),
                                NissueBook = reader.GetInt32(reader.GetOrdinal("nissue_book")),
                                Image = reader["image"].ToString()
                            };

                            listData.Add(das);
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
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
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
        private void displayViewImage(string keyword)
        {
            try
            {
                string selectQuery = string.IsNullOrEmpty(keyword) ?
                              "SELECT id, name, enroll, contact, email, nissue_book, image FROM students WHERE date_delete IS NULL" :
                              "SELECT id, name, enroll, contact, email, nissue_book, image FROM students WHERE date_delete IS NULL AND name LIKE @Keyword";
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
                        combinedColumn.HeaderText = "Student Info";
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
                            string StudentID = row.Cells["id"].Value.ToString();
                            string name = row.Cells["name"].Value.ToString();
                            string Contact = row.Cells["contact"].Value.ToString();
                            string Email = row.Cells["email"].Value.ToString();
                            string Nissue = row.Cells["nissue_book"].Value.ToString();

                            // Set the ID in the ID column
                            row.Cells["IDColumn"].Value = StudentID;

                            // Combine the book title, author, and published date into separate lines
                            string combinedInfo = $"Name: {name}{Environment.NewLine}Contact: {Contact}{Environment.NewLine}Email: {Email}{Environment.NewLine}Number of Issues: {Nissue}";

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
