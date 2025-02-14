﻿namespace projectLab {
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
			//this.menu_bd_item_break = new System.Windows.Forms.ToolStripMenuItem();

			this.work_frame = new System.Windows.Forms.TabControl();
				this.fuel_sale_tab = new System.Windows.Forms.TabPage();
					this.fuel_sale_container = new System.Windows.Forms.Panel();
					this.fuel_sale_instruments = new table_instruments();
				this.fuel_buy_tab = new System.Windows.Forms.TabPage();
					this.fuel_buy_container = new System.Windows.Forms.Panel();
					this.fuel_buy_instruments = new table_instruments();
				this.slave_manager_tab = new System.Windows.Forms.TabPage();
					this.slave_manager_container = new System.Windows.Forms.Panel();
					this.slave_manager_instruments = new table_instruments();
				this.shifts_manager_tab = new System.Windows.Forms.TabPage();
					this.shifts_manager_container = new System.Windows.Forms.Panel();
					this.shifts_manager_instruments = new table_instruments();
				this.container_view_tab = new System.Windows.Forms.TabPage();
					this.container_view_container = new System.Windows.Forms.Panel();
					this.container_view_instruments = new table_instruments();
				this.stat_view_fuel_tab = new System.Windows.Forms.TabPage();
					this.stat_view_fuel_container = new System.Windows.Forms.Panel();
					this.stat_view_fuel_instruments = new table_instruments();

			this.main_status.SuspendLayout();
			this.main_menu.SuspendLayout();

			this.work_frame.SuspendLayout();
			this.fuel_sale_tab.SuspendLayout();
			this.fuel_sale_container.SuspendLayout();
			this.fuel_buy_tab.SuspendLayout();
			this.fuel_buy_container.SuspendLayout();
			this.slave_manager_tab.SuspendLayout();
			this.slave_manager_container.SuspendLayout();
			this.shifts_manager_tab.SuspendLayout();
			this.shifts_manager_container.SuspendLayout();
			this.container_view_tab.SuspendLayout();
			this.container_view_container.SuspendLayout();
			this.stat_view_fuel_tab.SuspendLayout();
			this.stat_view_fuel_container.SuspendLayout();
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
			this.menu_bd_item_current});
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
			/*
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
			 * */

			// 
			// work_frame
			// 
			this.work_frame.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			//this.work_frame.Controls.Add(this.fuel_sale_tab);
			this.work_frame.Location = new System.Drawing.Point(0, 24);
			this.work_frame.Margin = new System.Windows.Forms.Padding(0);
			this.work_frame.Name = "work_frame";
			this.work_frame.SelectedIndex = 0;
			this.work_frame.Size = new System.Drawing.Size(592, 327);
			this.work_frame.Visible = false;
			//this.work_frame.TabIndex = 2;
			// 
			// fuel_sale_tab
			// 
			this.fuel_sale_tab.Controls.Add(this.fuel_sale_instruments);
			this.fuel_sale_tab.Controls.Add(this.fuel_sale_container);
			this.fuel_sale_tab.Location = new System.Drawing.Point(4, 22);
			this.fuel_sale_tab.Name = "fuel_sale_tab";
			this.fuel_sale_tab.Padding = new System.Windows.Forms.Padding(3);
			this.fuel_sale_tab.Size = new System.Drawing.Size(584, 301);
			this.fuel_sale_tab.TabIndex = 0;
			this.fuel_sale_tab.Text = "Продажа";
			this.fuel_sale_tab.UseVisualStyleBackColor = true;
			// 
			// fuel_sale_container
			// 
			this.fuel_sale_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.fuel_sale_container.AutoScroll = true;
			//this.fuel_sale_container.Controls.Add(this.solvers_table);
			this.fuel_sale_container.Location = new System.Drawing.Point(0, 0);
			this.fuel_sale_container.Name = "fuel_sale_container";
			this.fuel_sale_container.Size = new System.Drawing.Size(584, 245);
			this.fuel_sale_container.TabIndex = 0;
			// 
			// fuel_sale_instruments
			// 
			this.fuel_sale_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.fuel_sale_instruments.Location = new System.Drawing.Point(3, 251);
			this.fuel_sale_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.fuel_sale_instruments.Name = "fuel_sale_instruments";
			this.fuel_sale_instruments.Size = new System.Drawing.Size(578, 47);
			this.fuel_sale_instruments.TabIndex = 1;
			this.fuel_sale_instruments.TabStop = false;
			this.fuel_sale_instruments.Text = "Инструменты";
			// 
			// fuel_buy_tab
			// 
			this.fuel_buy_tab.Controls.Add(this.fuel_buy_instruments);
			this.fuel_buy_tab.Controls.Add(this.fuel_buy_container);
			this.fuel_buy_tab.Location = new System.Drawing.Point(4, 22);
			this.fuel_buy_tab.Name = "fuel_buy_tab";
			this.fuel_buy_tab.Padding = new System.Windows.Forms.Padding(3);
			this.fuel_buy_tab.Size = new System.Drawing.Size(584, 301);
			this.fuel_buy_tab.TabIndex = 0;
			this.fuel_buy_tab.Text = "Заправка контейнеров";
			this.fuel_buy_tab.UseVisualStyleBackColor = true;
			// 
			// fuel_buy_container
			// 
			this.fuel_buy_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.fuel_buy_container.AutoScroll = true;
			//this.fuel_buy_container.Controls.Add(this.solvers_table);
			this.fuel_buy_container.Location = new System.Drawing.Point(0, 0);
			this.fuel_buy_container.Name = "fuel_buy_container";
			this.fuel_buy_container.Size = new System.Drawing.Size(584, 245);
			this.fuel_buy_container.TabIndex = 0;
			// 
			// fuel_buy_instruments
			// 
			this.fuel_buy_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.fuel_buy_instruments.Location = new System.Drawing.Point(3, 251);
			this.fuel_buy_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.fuel_buy_instruments.Name = "fuel_buy_instruments";
			this.fuel_buy_instruments.Size = new System.Drawing.Size(578, 47);
			this.fuel_buy_instruments.TabIndex = 1;
			this.fuel_buy_instruments.TabStop = false;
			this.fuel_buy_instruments.Text = "Инструменты";
			// 
			// slave_manager_tab
			// 
			this.slave_manager_tab.Controls.Add(this.slave_manager_instruments);
			this.slave_manager_tab.Controls.Add(this.slave_manager_container);
			this.slave_manager_tab.Location = new System.Drawing.Point(4, 22);
			this.slave_manager_tab.Name = "slave_manager_tab";
			this.slave_manager_tab.Padding = new System.Windows.Forms.Padding(3);
			this.slave_manager_tab.Size = new System.Drawing.Size(584, 301);
			this.slave_manager_tab.TabIndex = 0;
			this.slave_manager_tab.Text = "Сотрудники";
			this.slave_manager_tab.UseVisualStyleBackColor = true;
			// 
			// slave_manager_container
			// 
			this.slave_manager_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.slave_manager_container.AutoScroll = true;
			this.slave_manager_container.Location = new System.Drawing.Point(0, 0);
			this.slave_manager_container.Name = "slave_manager_container";
			this.slave_manager_container.Size = new System.Drawing.Size(584, 245);
			this.slave_manager_container.TabIndex = 0;
			// 
			// slave_manager_instruments
			// 
			this.slave_manager_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.slave_manager_instruments.Location = new System.Drawing.Point(3, 251);
			this.slave_manager_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.slave_manager_instruments.Name = "slave_manager_instruments";
			this.slave_manager_instruments.Size = new System.Drawing.Size(578, 47);
			this.slave_manager_instruments.TabIndex = 1;
			this.slave_manager_instruments.TabStop = false;
			this.slave_manager_instruments.Text = "Инструменты";
			// 
			// shifts_manager_tab
			// 
			this.shifts_manager_tab.Controls.Add(this.shifts_manager_instruments);
			this.shifts_manager_tab.Controls.Add(this.shifts_manager_container);
			this.shifts_manager_tab.Location = new System.Drawing.Point(4, 22);
			this.shifts_manager_tab.Name = "shifts_manager_tab";
			this.shifts_manager_tab.Padding = new System.Windows.Forms.Padding(3);
			this.shifts_manager_tab.Size = new System.Drawing.Size(584, 301);
			this.shifts_manager_tab.TabIndex = 0;
			this.shifts_manager_tab.Text = "Смены";
			this.shifts_manager_tab.UseVisualStyleBackColor = true;
			// 
			// shifts_manager_container
			// 
			this.shifts_manager_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.shifts_manager_container.AutoScroll = true;
			this.shifts_manager_container.Location = new System.Drawing.Point(0, 0);
			this.shifts_manager_container.Name = "shifts_manager_container";
			this.shifts_manager_container.Size = new System.Drawing.Size(584, 245);
			this.shifts_manager_container.TabIndex = 0;
			// 
			// shifts_manager_instruments
			// 
			this.shifts_manager_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.shifts_manager_instruments.Location = new System.Drawing.Point(3, 251);
			this.shifts_manager_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.shifts_manager_instruments.Name = "shifts_manager_instruments";
			this.shifts_manager_instruments.Size = new System.Drawing.Size(578, 47);
			this.shifts_manager_instruments.TabIndex = 1;
			this.shifts_manager_instruments.TabStop = false;
			this.shifts_manager_instruments.Text = "Инструменты";
			// 
			// container_view_tab
			// 
			this.container_view_tab.Controls.Add(this.container_view_instruments);
			this.container_view_tab.Controls.Add(this.container_view_container);
			this.container_view_tab.Location = new System.Drawing.Point(4, 22);
			this.container_view_tab.Name = "container_view_tab";
			this.container_view_tab.Padding = new System.Windows.Forms.Padding(3);
			this.container_view_tab.Size = new System.Drawing.Size(584, 301);
			this.container_view_tab.TabIndex = 0;
			this.container_view_tab.Text = "Контейнеры";
			this.container_view_tab.UseVisualStyleBackColor = true;
			// 
			// container_view_container
			// 
			this.container_view_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.container_view_container.AutoScroll = true;
			this.container_view_container.Location = new System.Drawing.Point(0, 0);
			this.container_view_container.Name = "container_view_container";
			this.container_view_container.Size = new System.Drawing.Size(584, 245);
			this.container_view_container.TabIndex = 0;
			// 
			// container_view_instruments
			// 
			this.container_view_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.container_view_instruments.Location = new System.Drawing.Point(3, 251);
			this.container_view_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.container_view_instruments.Name = "container_view_instruments";
			this.container_view_instruments.Size = new System.Drawing.Size(578, 47);
			this.container_view_instruments.TabIndex = 1;
			this.container_view_instruments.TabStop = false;
			this.container_view_instruments.Text = "Инструменты";
			// 
			// stat_view_fuel_tab
			// 
			this.stat_view_fuel_tab.Controls.Add(this.stat_view_fuel_instruments);
			this.stat_view_fuel_tab.Controls.Add(this.stat_view_fuel_container);
			this.stat_view_fuel_tab.Location = new System.Drawing.Point(4, 22);
			this.stat_view_fuel_tab.Name = "stat_view_fuel_tab";
			this.stat_view_fuel_tab.Padding = new System.Windows.Forms.Padding(3);
			this.stat_view_fuel_tab.Size = new System.Drawing.Size(584, 301);
			this.stat_view_fuel_tab.TabIndex = 0;
			this.stat_view_fuel_tab.Text = "График прибыли";
			this.stat_view_fuel_tab.UseVisualStyleBackColor = true;
			// 
			// stat_view_fuel_container
			// 
			this.stat_view_fuel_container.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.stat_view_fuel_container.AutoScroll = true;
			this.stat_view_fuel_container.Location = new System.Drawing.Point(0, 0);
			this.stat_view_fuel_container.Name = "stat_view_fuel_container";
			this.stat_view_fuel_container.Size = new System.Drawing.Size(584, 245);
			this.stat_view_fuel_container.TabIndex = 0;
			// 
			// stat_view_fuel_instruments
			// 
			this.stat_view_fuel_instruments.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.stat_view_fuel_instruments.Location = new System.Drawing.Point(3, 251);
			this.stat_view_fuel_instruments.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.stat_view_fuel_instruments.Name = "stat_view_fuel_instruments";
			this.stat_view_fuel_instruments.Size = new System.Drawing.Size(578, 47);
			this.stat_view_fuel_instruments.TabIndex = 1;
			this.stat_view_fuel_instruments.TabStop = false;
			this.stat_view_fuel_instruments.Text = "Инструменты";

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
			this.Text = "Королева бензоколонки";
			this.main_status.ResumeLayout(false);
			this.main_status.PerformLayout();
			this.main_menu.ResumeLayout(false);
			this.main_menu.PerformLayout();
			this.work_frame.ResumeLayout(false);
			this.fuel_sale_tab.ResumeLayout(false);
			this.fuel_sale_container.ResumeLayout(false);
			this.fuel_sale_container.PerformLayout();
			this.fuel_sale_instruments.ResumeLayout(false);
			this.fuel_sale_instruments.PerformLayout();
			this.fuel_buy_tab.ResumeLayout(false);
			this.fuel_buy_container.ResumeLayout(false);
			this.fuel_buy_container.PerformLayout();
			this.fuel_buy_instruments.ResumeLayout(false);
			this.fuel_buy_instruments.PerformLayout();
			this.slave_manager_tab.ResumeLayout(false);
			this.slave_manager_container.ResumeLayout(false);
			this.slave_manager_container.PerformLayout();
			this.slave_manager_instruments.ResumeLayout(false);
			this.slave_manager_instruments.PerformLayout();
			this.shifts_manager_tab.ResumeLayout(false);
			this.shifts_manager_container.ResumeLayout(false);
			this.shifts_manager_container.PerformLayout();
			this.shifts_manager_instruments.ResumeLayout(false);
			this.shifts_manager_instruments.PerformLayout();
			this.container_view_tab.ResumeLayout(false);
			this.container_view_container.ResumeLayout(false);
			this.container_view_container.PerformLayout();
			this.container_view_instruments.ResumeLayout(false);
			this.container_view_instruments.PerformLayout();
			this.stat_view_fuel_tab.ResumeLayout(false);
			this.stat_view_fuel_container.ResumeLayout(false);
			this.stat_view_fuel_container.PerformLayout();
			this.stat_view_fuel_instruments.ResumeLayout(false);
			this.stat_view_fuel_instruments.PerformLayout();
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
				//private System.Windows.Forms.ToolStripMenuItem menu_bd_item_break;

		private System.Windows.Forms.TabControl work_frame;
			private System.Windows.Forms.TabPage fuel_sale_tab;
				private System.Windows.Forms.Panel fuel_sale_container;
				private table_instruments fuel_sale_instruments;
			private System.Windows.Forms.TabPage fuel_buy_tab;
				private System.Windows.Forms.Panel fuel_buy_container;
				private table_instruments fuel_buy_instruments;
			private System.Windows.Forms.TabPage slave_manager_tab;
				private System.Windows.Forms.Panel slave_manager_container;
				private table_instruments slave_manager_instruments;
			private System.Windows.Forms.TabPage shifts_manager_tab;
				private System.Windows.Forms.Panel shifts_manager_container;
				private table_instruments shifts_manager_instruments;
			private System.Windows.Forms.TabPage container_view_tab;
				private System.Windows.Forms.Panel container_view_container;
				private table_instruments container_view_instruments;
			private System.Windows.Forms.TabPage stat_view_fuel_tab;
				private System.Windows.Forms.Panel stat_view_fuel_container;
				private table_instruments stat_view_fuel_instruments;
	}
}