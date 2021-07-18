namespace projectLab {
	partial class connection_dialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.main_box = new System.Windows.Forms.Panel();
            this.label_db = new System.Windows.Forms.Label();
            this.inp_bd = new System.Windows.Forms.TextBox();
            this.cb_enc = new System.Windows.Forms.CheckBox();
            this.cb_int_sec = new System.Windows.Forms.CheckBox();
            this.label_password = new System.Windows.Forms.Label();
            this.inp_password = new System.Windows.Forms.TextBox();
            this.label_user = new System.Windows.Forms.Label();
            this.inp_user = new System.Windows.Forms.TextBox();
            this.label_address = new System.Windows.Forms.Label();
            this.inp_address = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.inp_name = new System.Windows.Forms.TextBox();
            this.but_ok = new System.Windows.Forms.Button();
            this.but_cancel = new System.Windows.Forms.Button();
            this.main_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_box
            // 
            this.main_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_box.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_box.Controls.Add(this.label_db);
            this.main_box.Controls.Add(this.inp_bd);
            this.main_box.Controls.Add(this.cb_enc);
            this.main_box.Controls.Add(this.cb_int_sec);
            this.main_box.Controls.Add(this.label_password);
            this.main_box.Controls.Add(this.inp_password);
            this.main_box.Controls.Add(this.label_user);
            this.main_box.Controls.Add(this.inp_user);
            this.main_box.Controls.Add(this.label_address);
            this.main_box.Controls.Add(this.inp_address);
            this.main_box.Controls.Add(this.label_name);
            this.main_box.Controls.Add(this.inp_name);
            this.main_box.Location = new System.Drawing.Point(0, 0);
            this.main_box.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.main_box.Name = "main_box";
            this.main_box.Size = new System.Drawing.Size(296, 167);
            this.main_box.TabIndex = 0;
            // 
            // label_db
            // 
            this.label_db.AutoSize = true;
            this.label_db.Location = new System.Drawing.Point(11, 63);
            this.label_db.Name = "label_db";
            this.label_db.Size = new System.Drawing.Size(64, 13);
            this.label_db.TabIndex = 13;
            this.label_db.Text = "Имя базы: ";
            // 
            // inp_bd
            // 
            this.inp_bd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inp_bd.Location = new System.Drawing.Point(115, 60);
            this.inp_bd.MaxLength = 128;
            this.inp_bd.Name = "inp_bd";
            this.inp_bd.Size = new System.Drawing.Size(165, 20);
            this.inp_bd.TabIndex = 3;
            // 
            // cb_enc
            // 
            this.cb_enc.AutoSize = true;
            this.cb_enc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_enc.Location = new System.Drawing.Point(205, 138);
            this.cb_enc.Name = "cb_enc";
            this.cb_enc.Size = new System.Drawing.Size(52, 17);
            this.cb_enc.TabIndex = 7;
            this.cb_enc.Text = "SSL: ";
            this.cb_enc.UseVisualStyleBackColor = true;
            // 
            // cb_int_sec
            // 
            this.cb_int_sec.AutoSize = true;
            this.cb_int_sec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_int_sec.Enabled = false;
            this.cb_int_sec.Location = new System.Drawing.Point(9, 138);
            this.cb_int_sec.Name = "cb_int_sec";
            this.cb_int_sec.Size = new System.Drawing.Size(184, 17);
            this.cb_int_sec.TabIndex = 6;
            this.cb_int_sec.Text = "Исп. учетные данные windows: ";
            this.cb_int_sec.UseVisualStyleBackColor = true;
            this.cb_int_sec.CheckedChanged += new System.EventHandler(this.cb_int_sec_CheckedChanged);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(11, 115);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(51, 13);
            this.label_password.TabIndex = 7;
            this.label_password.Text = "Пароль: ";
            // 
            // inp_password
            // 
            this.inp_password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inp_password.Location = new System.Drawing.Point(115, 112);
            this.inp_password.MaxLength = 128;
            this.inp_password.Name = "inp_password";
            this.inp_password.Size = new System.Drawing.Size(165, 20);
            this.inp_password.TabIndex = 5;
            this.inp_password.UseSystemPasswordChar = true;
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Location = new System.Drawing.Point(11, 89);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(94, 13);
            this.label_user.TabIndex = 5;
            this.label_user.Text = "Учетная запись: ";
            // 
            // inp_user
            // 
            this.inp_user.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inp_user.Location = new System.Drawing.Point(115, 86);
            this.inp_user.MaxLength = 128;
            this.inp_user.Name = "inp_user";
            this.inp_user.Size = new System.Drawing.Size(165, 20);
            this.inp_user.TabIndex = 4;
            // 
            // label_address
            // 
            this.label_address.AutoSize = true;
            this.label_address.Location = new System.Drawing.Point(11, 37);
            this.label_address.Name = "label_address";
            this.label_address.Size = new System.Drawing.Size(50, 13);
            this.label_address.TabIndex = 3;
            this.label_address.Text = "Сервер: ";
            // 
            // inp_address
            // 
            this.inp_address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inp_address.Location = new System.Drawing.Point(115, 34);
            this.inp_address.MaxLength = 128;
            this.inp_address.Name = "inp_address";
            this.inp_address.Size = new System.Drawing.Size(165, 20);
            this.inp_address.TabIndex = 2;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(11, 11);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(98, 13);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "Имя соединения: ";
            // 
            // inp_name
            // 
            this.inp_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inp_name.Location = new System.Drawing.Point(115, 8);
            this.inp_name.MaxLength = 128;
            this.inp_name.Name = "inp_name";
            this.inp_name.Size = new System.Drawing.Size(165, 20);
            this.inp_name.TabIndex = 1;
            // 
            // but_ok
            // 
            this.but_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.but_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.but_ok.Location = new System.Drawing.Point(205, 167);
            this.but_ok.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(75, 23);
            this.but_ok.TabIndex = 8;
            this.but_ok.Text = "OK";
            this.but_ok.UseVisualStyleBackColor = true;
            // 
            // but_cancel
            // 
            this.but_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.but_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.but_cancel.Location = new System.Drawing.Point(120, 167);
            this.but_cancel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(75, 23);
            this.but_cancel.TabIndex = 9;
            this.but_cancel.Text = "Отмена";
            this.but_cancel.UseVisualStyleBackColor = true;
            // 
            // connection_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.main_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 230);
            this.MinimumSize = new System.Drawing.Size(300, 230);
            this.Name = "connection_dialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.main_box.ResumeLayout(false);
            this.main_box.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		public void change_title(string title){
			this.Text = title;
		}

		private System.Windows.Forms.Panel		main_box;
		private System.Windows.Forms.Button		but_ok;
		private System.Windows.Forms.Button		but_cancel;
		private System.Windows.Forms.Label		label_name;
		public  System.Windows.Forms.TextBox	inp_name;
		private System.Windows.Forms.Label		label_password;
		public  System.Windows.Forms.TextBox	inp_password;
		private System.Windows.Forms.Label		label_user;
		public  System.Windows.Forms.TextBox	inp_user;
		private System.Windows.Forms.Label		label_address;
		public  System.Windows.Forms.TextBox	inp_address;
		public  System.Windows.Forms.CheckBox	cb_int_sec;
		public  System.Windows.Forms.CheckBox	cb_enc;
		private System.Windows.Forms.Label		label_db;
		public System.Windows.Forms.TextBox		inp_bd;
	}
}