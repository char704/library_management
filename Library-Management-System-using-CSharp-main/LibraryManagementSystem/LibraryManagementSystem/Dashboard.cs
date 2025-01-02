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
    public partial class Dashboard : UserControl
    {
        SqlConnection connect;
        
        private string[] imageFiles; // Mảng lưu các file hình ảnh
        private int currentImageIndex = 0; // Chỉ số hiện tại của hình ảnh
        private Timer imageChangeTimer; // Timer để đổi hình mỗi 2 giây


        public Dashboard()
        {
            InitializeComponent();
            connect = new SqlConnection(GlobalConnection.ConnectionString);
            displayAB();
            displayIB();
            displayRB();
           
            // Lấy tất cả các hình ảnh trong thư mục (bao gồm .jpg, .gif, .png)
            imageFiles = Directory.GetFiles(GlobalConnection.ImagesFolder, "*.*")
                                  .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                 file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                                 file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                                 file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                                  .ToArray();

            // Nếu có hình ảnh trong thư mục
            if (imageFiles.Length > 0)
            {
                // Thiết lập Timer
                imageChangeTimer = new Timer();
                imageChangeTimer.Interval = 5000; // Đổi hình mỗi 2 giây (2000ms)
                imageChangeTimer.Tick += timer1_Tick;
                imageChangeTimer.Start();

                // Hiển thị hình ảnh đầu tiên ngay khi khởi động
                pictureBox4.ImageLocation = imageFiles[currentImageIndex];
            }
            else
            {
                MessageBox.Show("Không có hình ảnh trong thư mục.");
            }
        }

        public void refreshData()
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAB();
            displayIB();
            displayRB();
        }

        public void displayAB()
        {
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM books " +
                        "WHERE status = 'Available' AND date_delete IS NULL";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempAB = 0;

                        if (reader.Read())
                        {
                            tempAB = Convert.ToInt32(reader[0]);

                            dashboard_AB.Text = tempAB.ToString();
                        }
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

        public void displayIB()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM issues " +
                        "WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempIB = 0;

                        if (reader.Read())
                        {
                            tempIB = Convert.ToInt32(reader[0]);

                            dashboard_IB.Text = tempIB.ToString();
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

        public void displayRB()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM issues " +
                        " WHERE status = 'Return' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempRB = 0;

                        if (reader.Read())
                        {
                            tempRB = Convert.ToInt32(reader[0]);

                            dashboard_RB.Text = tempRB.ToString();
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

        private void dashboard_IB_Click(object sender, EventArgs e)
        {

        }

        private void dashboard_AB_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (imageFiles.Length > 0)
            {
                // Hiển thị hình ảnh mới
                pictureBox4.ImageLocation = imageFiles[currentImageIndex];

                // Cập nhật chỉ số hình ảnh để thay đổi
                currentImageIndex = (currentImageIndex + 1) % imageFiles.Length; // Sau khi đến ảnh cuối cùng, quay lại ảnh đầu tiên
            }
        }
    }
}
