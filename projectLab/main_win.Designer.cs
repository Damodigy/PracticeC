namespace projectLab {
	partial class main_win {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_win));
			this.main_status = new System.Windows.Forms.StatusStrip();
			this.main_status_text_1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.main_status_progress = new System.Windows.Forms.ToolStripProgressBar();
			this.main_menu = new System.Windows.Forms.MenuStrip();
			this.main_menu_bd = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_bd_item_fast = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_bd_sep = new System.Windows.Forms.ToolStripSeparator();
			this.menu_bd_item_current = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_bd_item_break = new System.Windows.Forms.ToolStripMenuItem();

			this.work_frame = new System.Windows.Forms.TabControl();
			this.solvers_tab = new System.Windows.Forms.TabPage();
			this.solvers_container = new System.Windows.Forms.Panel();
			this.container_instruments = new table_instruments();
			this.tabPage2 = new System.Windows.Forms.TabPage();

			this.main_status.SuspendLayout();
			this.main_menu.SuspendLayout();

			this.work_frame.SuspendLayout();
			this.solvers_tab.SuspendLayout();
			this.solvers_container.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// main_status
			// 
			this.main_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_status_text_1,
            this.main_status_progress});
			this.main_status.Location = new System.Drawing.Point(0, 251);
			this.main_status.Name = "main_status";
			this.main_status.Size = new System.Drawing.Size(292, 22);
			this.main_status.TabIndex = 0;
			this.main_status.Text = "main_status";
			// 
			// main_status_text_1
			// 
			this.main_status_text_1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
			this.main_status_text_1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.main_status_text_1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.main_status_text_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.main_status_text_1.Name = "main_status_text_1";
			this.main_status_text_1.Size = new System.Drawing.Size(173, 19);
			this.main_status_text_1.Spring = true;
			// 
			// main_status_progress
			// 
			this.main_status_progress.Margin = new System.Windows.Forms.Padding(2, 3, 2, 1);
			this.main_status_progress.Name = "main_status_progress";
			this.main_status_progress.Size = new System.Drawing.Size(100, 18);
			// 
			// main_menu
			// 
			this.main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_menu_bd});
			this.main_menu.Location = new System.Drawing.Point(0, 0);
			this.main_menu.Name = "main_menu";
			this.main_menu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.main_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.main_menu.Size = new System.Drawing.Size(292, 24);
			this.main_menu.TabIndex = 1;
			this.main_menu.Text = "main_menu";
			// 
			// main_menu_bd
			// 
			this.main_menu_bd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_bd_item_fast,
            this.menu_bd_sep,
			this.menu_bd_item_current,
            this.menu_bd_item_break});
			this.main_menu_bd.Name = "main_menu_bd";
			this.main_menu_bd.Size = new System.Drawing.Size(85, 20);
			this.main_menu_bd.Text = "&База Данных";
			// menu_bd_item_fast
			// 
			this.menu_bd_item_fast.Name = "menu_bd_item_fast";
			this.menu_bd_item_fast.ShortcutKeyDisplayString = "Ctrl+F";
			this.menu_bd_item_fast.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F;
			this.menu_bd_item_fast.Size = new System.Drawing.Size(254, 22);
			this.menu_bd_item_fast.Text = "Быстрое &подключение";
			this.menu_bd_item_fast.Click += fast_connect;
			// 
			// menu_bd_sep
			// 
			this.menu_bd_sep.Name = "menu_bd_sep";
			this.menu_bd_sep.Size = new System.Drawing.Size(251, 6);
			// 
			// menu_bd_item_current
			// 
			this.menu_bd_item_current.Enabled = false;
			this.menu_bd_item_current.Name = "menu_bd_item_current";
			this.menu_bd_item_current.ShortcutKeyDisplayString = "Ctrl+T";
			this.menu_bd_item_current.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T;
			this.menu_bd_item_current.Text = "&Текущее подключение";
			this.menu_bd_item_current.Click += view_current_connect;
			// 
			// menu_bd_item_break
			// 
			this.menu_bd_item_break.Enabled = false;
			this.menu_bd_item_break.Name = "menu_bd_item_break";
			this.menu_bd_item_break.ShortcutKeyDisplayString = "Ctrl+B";
			this.menu_bd_item_break.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B;
			this.menu_bd_item_break.Size = new System.Drawing.Size(254, 22);
			this.menu_bd_item_break.Text = "&Разорвать подключение";
			this.menu_bd_item_break.Click += current_disconnect;

			// 
			// work_frame
			// 
			this.work_frame.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.work_frame.Controls.Add(this.solvers_tab);
			this.work_frame.Controls.Add(this.tabPage2);
			this.work_frame.Location = new System.Drawing.Point(0, 24);
			this.work_frame.Margin = new System.Windows.Forms.Padding(0);
			this.work_frame.Name = "work_frame";
			this.work_frame.SelectedIndex = 0;
			this.work_frame.Size = new System.Drawing.Size(592, 327);
			this.work_frame.Visible = false;
			//this.work_frame.TabIndex = 2;
			// 
			// solvers_tab
			// 
			this.solvers_tab.Controls.Add(this.container_instruments);
			this.solvers_tab.Controls.Add(this.solvers_container);
			this.solvers_tab.Location = new System.Drawing.Point(4, 22);
			this.solvers_tab.Name = "solvers_tab";
			this.solvers_tab.Padding = new System.Windows.Forms.Padding(3);
			this.solvers_tab.Size = new System.Drawing.Size(584, 301);
			this.solvers_tab.TabIndex = 0;
			this.solvers_tab.Text = "Сотрудники";
			this.solvers_tab.UseVisualStyleBackColor = true;
			// 
			// solvers_container
			// 
			this.solvers_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.solvers_container.AutoScroll = true;
			//this.solvers_container.Controls.Add(this.solvers_table);
			this.solvers_container.Location = new System.Drawing.Point(0, 0);
			this.solvers_container.Name = "solvers_container";
			this.solvers_container.Size = new System.Drawing.Size(584, 245);
			this.solvers_container.TabIndex = 0;
			// 
			// container_instruments
			// 
			this.container_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.container_instruments.Location = new System.Drawing.Point(3, 251);
			this.container_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.container_instruments.Name = "container_instruments";
			this.container_instruments.Size = new System.Drawing.Size(578, 47);
			this.container_instruments.TabIndex = 1;
			this.container_instruments.TabStop = false;
			this.container_instruments.Text = "Инструменты";

			// 
			// main_win
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 373);
			this.Controls.Add(this.work_frame);
			this.Controls.Add(this.main_status);
			this.Controls.Add(this.main_menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "main_win";
			this.Text = "RADIOFAN Slave Manager";
			this.main_status.ResumeLayout(false);
			this.main_status.PerformLayout();
			this.main_menu.ResumeLayout(false);
			this.main_menu.PerformLayout();
			this.work_frame.ResumeLayout(false);
			this.solvers_tab.ResumeLayout(false);
			this.solvers_container.ResumeLayout(false);
			this.solvers_container.PerformLayout();
			this.container_instruments.ResumeLayout(false);
			this.container_instruments.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip main_status;
			private System.Windows.Forms.ToolStripStatusLabel main_status_text_1;
			private System.Windows.Forms.ToolStripProgressBar main_status_progress;
		private System.Windows.Forms.MenuStrip main_menu;
			private System.Windows.Forms.ToolStripMenuItem main_menu_bd;
				private System.Windows.Forms.ToolStripMenuItem menu_bd_item_fast;
				private System.Windows.Forms.ToolStripSeparator menu_bd_sep;
				private System.Windows.Forms.ToolStripMenuItem menu_bd_item_current;
				private System.Windows.Forms.ToolStripMenuItem menu_bd_item_break;

		private System.Windows.Forms.TabControl work_frame;
			private System.Windows.Forms.TabPage solvers_tab;
				private System.Windows.Forms.Panel solvers_container;
				private table_instruments container_instruments;
			private System.Windows.Forms.TabPage tabPage2;
	}
}