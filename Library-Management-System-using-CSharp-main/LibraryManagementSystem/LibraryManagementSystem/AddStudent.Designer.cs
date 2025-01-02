namespace LibraryManagementSystem
{
    partial class AddStudent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Change_Stage = new System.Windows.Forms.Button();
            this.addStudents_email = new System.Windows.Forms.TextBox();
            this.addStudents_contact = new System.Windows.Forms.TextBox();
            this.addStudents_importBtn = new System.Windows.Forms.Button();
            this.addStudent_clearBtn = new System.Windows.Forms.Button();
            this.addStudent_deleteBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addStudent_updateBtn = new System.Windows.Forms.Button();
            this.addStudent_addBtn = new System.Windows.Forms.Button();
            this.addStudents_name = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.addStudents_enroll = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addStudents_picture = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addStudents_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txt_search);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(14, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(553, 526);
            this.panel2.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(15, 58);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(521, 447);
            this.dataGridView2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(268, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Search by Name:";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(385, 23);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(151, 20);
            this.txt_search.TabIndex = 2;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(15, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(521, 447);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Students";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BackgroundImage = global::LibraryManagementSystem.Properties.Resources._31;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Change_Stage);
            this.panel1.Controls.Add(this.addStudents_email);
            this.panel1.Controls.Add(this.addStudents_contact);
            this.panel1.Controls.Add(this.addStudents_importBtn);
            this.panel1.Controls.Add(this.addStudent_clearBtn);
            this.panel1.Controls.Add(this.addStudent_deleteBtn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.addStudent_updateBtn);
            this.panel1.Controls.Add(this.addStudent_addBtn);
            this.panel1.Controls.Add(this.addStudents_name);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.addStudents_enroll);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.addStudents_picture);
            this.panel1.Location = new System.Drawing.Point(585, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 526);
            this.panel1.TabIndex = 4;
            // 
            // Change_Stage
            // 
            this.Change_Stage.BackColor = System.Drawing.Color.IndianRed;
            this.Change_Stage.Location = new System.Drawing.Point(95, 476);
            this.Change_Stage.Name = "Change_Stage";
            this.Change_Stage.Size = new System.Drawing.Size(91, 29);
            this.Change_Stage.TabIndex = 26;
            this.Change_Stage.Text = "View Only";
            this.Change_Stage.UseVisualStyleBackColor = false;
            this.Change_Stage.Click += new System.EventHandler(this.Change_Stage_Click);
            // 
            // addStudents_email
            // 
            this.addStudents_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudents_email.Location = new System.Drawing.Point(95, 269);
            this.addStudents_email.Name = "addStudents_email";
            this.addStudents_email.Size = new System.Drawing.Size(168, 22);
            this.addStudents_email.TabIndex = 25;
            // 
            // addStudents_contact
            // 
            this.addStudents_contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudents_contact.Location = new System.Drawing.Point(95, 229);
            this.addStudents_contact.Name = "addStudents_contact";
            this.addStudents_contact.Size = new System.Drawing.Size(168, 22);
            this.addStudents_contact.TabIndex = 24;
            // 
            // addStudents_importBtn
            // 
            this.addStudents_importBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.addStudents_importBtn.FlatAppearance.BorderSize = 0;
            this.addStudents_importBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStudents_importBtn.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudents_importBtn.ForeColor = System.Drawing.Color.White;
            this.addStudents_importBtn.Location = new System.Drawing.Point(90, 115);
            this.addStudents_importBtn.Name = "addStudents_importBtn";
            this.addStudents_importBtn.Size = new System.Drawing.Size(100, 23);
            this.addStudents_importBtn.TabIndex = 23;
            this.addStudents_importBtn.Text = "Import";
            this.addStudents_importBtn.UseVisualStyleBackColor = false;
            this.addStudents_importBtn.Click += new System.EventHandler(this.addStudents_importBtn_Click);
            // 
            // addStudent_clearBtn
            // 
            this.addStudent_clearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.addStudent_clearBtn.FlatAppearance.BorderSize = 0;
            this.addStudent_clearBtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_clearBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStudent_clearBtn.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudent_clearBtn.ForeColor = System.Drawing.Color.White;
            this.addStudent_clearBtn.Location = new System.Drawing.Point(146, 406);
            this.addStudent_clearBtn.Name = "addStudent_clearBtn";
            this.addStudent_clearBtn.Size = new System.Drawing.Size(99, 34);
            this.addStudent_clearBtn.TabIndex = 22;
            this.addStudent_clearBtn.Text = "CLEAR";
            this.addStudent_clearBtn.UseVisualStyleBackColor = false;
            this.addStudent_clearBtn.Click += new System.EventHandler(this.addStudent_clearBtn_Click);
            // 
            // addStudent_deleteBtn
            // 
            this.addStudent_deleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.addStudent_deleteBtn.FlatAppearance.BorderSize = 0;
            this.addStudent_deleteBtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_deleteBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStudent_deleteBtn.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudent_deleteBtn.ForeColor = System.Drawing.Color.White;
            this.addStudent_deleteBtn.Location = new System.Drawing.Point(27, 406);
            this.addStudent_deleteBtn.Name = "addStudent_deleteBtn";
            this.addStudent_deleteBtn.Size = new System.Drawing.Size(99, 34);
            this.addStudent_deleteBtn.TabIndex = 21;
            this.addStudent_deleteBtn.Text = "DELETE";
            this.addStudent_deleteBtn.UseVisualStyleBackColor = false;
            this.addStudent_deleteBtn.Click += new System.EventHandler(this.addStudent_deleteBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Email:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Contact #:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addStudent_updateBtn
            // 
            this.addStudent_updateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.addStudent_updateBtn.FlatAppearance.BorderSize = 0;
            this.addStudent_updateBtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_updateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_updateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStudent_updateBtn.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudent_updateBtn.ForeColor = System.Drawing.Color.White;
            this.addStudent_updateBtn.Location = new System.Drawing.Point(146, 354);
            this.addStudent_updateBtn.Name = "addStudent_updateBtn";
            this.addStudent_updateBtn.Size = new System.Drawing.Size(99, 34);
            this.addStudent_updateBtn.TabIndex = 16;
            this.addStudent_updateBtn.Text = "UPDATE";
            this.addStudent_updateBtn.UseVisualStyleBackColor = false;
            this.addStudent_updateBtn.Click += new System.EventHandler(this.addStudent_updateBtn_Click);
            // 
            // addStudent_addBtn
            // 
            this.addStudent_addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.addStudent_addBtn.FlatAppearance.BorderSize = 0;
            this.addStudent_addBtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_addBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.addStudent_addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStudent_addBtn.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudent_addBtn.ForeColor = System.Drawing.Color.White;
            this.addStudent_addBtn.Location = new System.Drawing.Point(27, 354);
            this.addStudent_addBtn.Name = "addStudent_addBtn";
            this.addStudent_addBtn.Size = new System.Drawing.Size(99, 34);
            this.addStudent_addBtn.TabIndex = 15;
            this.addStudent_addBtn.Text = "ADD";
            this.addStudent_addBtn.UseVisualStyleBackColor = false;
            this.addStudent_addBtn.Click += new System.EventHandler(this.addStudent_addBtn_Click);
            // 
            // addStudents_name
            // 
            this.addStudents_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudents_name.Location = new System.Drawing.Point(95, 187);
            this.addStudents_name.Name = "addStudents_name";
            this.addStudents_name.Size = new System.Drawing.Size(168, 22);
            this.addStudents_name.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(44, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Name:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addStudents_enroll
            // 
            this.addStudents_enroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStudents_enroll.Location = new System.Drawing.Point(95, 149);
            this.addStudents_enroll.Name = "addStudents_enroll";
            this.addStudents_enroll.Size = new System.Drawing.Size(168, 22);
            this.addStudents_enroll.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(44, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Enroll:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addStudents_picture
            // 
            this.addStudents_picture.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.addStudents_picture.Location = new System.Drawing.Point(90, 18);
            this.addStudents_picture.Name = "addStudents_picture";
            this.addStudents_picture.Size = new System.Drawing.Size(100, 100);
            this.addStudents_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.addStudents_picture.TabIndex = 8;
            this.addStudents_picture.TabStop = false;
            // 
            // AddStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(70)))), ((int)(((byte)(200)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AddStudent";
            this.Size = new System.Drawing.Size(880, 565);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addStudents_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addStudents_importBtn;
        private System.Windows.Forms.Button addStudent_clearBtn;
        private System.Windows.Forms.Button addStudent_deleteBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addStudent_updateBtn;
        private System.Windows.Forms.Button addStudent_addBtn;
        private System.Windows.Forms.TextBox addStudents_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox addStudents_enroll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox addStudents_picture;
        private System.Windows.Forms.TextBox addStudents_email;
        private System.Windows.Forms.TextBox addStudents_contact;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button Change_Stage;
    }
}
