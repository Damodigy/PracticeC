namespace projectLab {
	partial class info_dialog {
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
			this.textbox = new System.Windows.Forms.RichTextBox();
			this.but_ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textbox
			// 
			this.textbox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.textbox.BackColor = System.Drawing.SystemColors.Control;
			this.textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textbox.Location = new System.Drawing.Point(13, 13);
			this.textbox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.textbox.Name = "textbox";
			this.textbox.ReadOnly = true;
			this.textbox.Size = new System.Drawing.Size(267, 219);
			this.textbox.TabIndex = 0;
			this.textbox.Text = "";
			// 
			// but_ok
			// 
			this.but_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.but_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.but_ok.Location = new System.Drawing.Point(112, 241);
			this.but_ok.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.but_ok.Name = "but_ok";
			this.but_ok.Size = new System.Drawing.Size(75, 23);
			this.but_ok.TabIndex = 1;
			this.but_ok.Text = "ОК";
			this.but_ok.UseVisualStyleBackColor = true;
			this.but_ok.Click += new System.EventHandler(this.close);
			// 
			// info_dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.but_ok);
			this.Controls.Add(this.textbox);
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "info_dialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}

		#endregion

		public void change_title(string title){
			this.Text = title;
		}

		private void close(object sender, System.EventArgs e) {
			this.Dispose();
		}

		public System.Windows.Forms.RichTextBox textbox;
		private System.Windows.Forms.Button but_ok;
	}
}