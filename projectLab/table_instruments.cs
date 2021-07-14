using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectLab {
	class table_instruments : System.Windows.Forms.GroupBox{

		public table_instruments(){
			this.solvers_tools = new System.Windows.Forms.ToolStrip();
			this.inst_new_row = new System.Windows.Forms.ToolStripButton();
			this.inst_reload = new System.Windows.Forms.ToolStripButton();
			this.inst_save = new System.Windows.Forms.ToolStripButton();
			this.inst_sep_1 = new System.Windows.Forms.ToolStripSeparator();
			this.inst_label_1 = new System.Windows.Forms.ToolStripLabel();
			this.inst_str_count_inp = new System.Windows.Forms.ToolStripTextBox();
			this.inst_sep_2 = new System.Windows.Forms.ToolStripSeparator();
			this.inst_left = new System.Windows.Forms.ToolStripButton();
			this.inst_curr_page_inp = new System.Windows.Forms.ToolStripTextBox();
			this.inst_label_2 = new System.Windows.Forms.ToolStripLabel();
			this.inst_right = new System.Windows.Forms.ToolStripButton();
			this.inst_sep_3 = new System.Windows.Forms.ToolStripSeparator();
			this.inst_selector = new System.Windows.Forms.ToolStripDropDownButton();

			this.SuspendLayout();
			this.Controls.Add(this.solvers_tools);

			// 
			// solvers_tools
			// 
			this.solvers_tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.solvers_tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inst_new_row,
            this.inst_reload,
            this.inst_save,
            this.inst_sep_1,
            this.inst_left,
            this.inst_curr_page_inp,
            this.inst_label_2,
            this.inst_right,
            this.inst_sep_2,
            this.inst_label_1,
            this.inst_str_count_inp,
            this.inst_sep_3,
            this.inst_selector});
			this.solvers_tools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.solvers_tools.Location = new System.Drawing.Point(3, 16);
			this.solvers_tools.Name = "solvers_tools";
			this.solvers_tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			//this.solvers_tools.Size = new System.Drawing.Size(572, 31);
			this.solvers_tools.Stretch = true;
			// 
			// inst_new_row
			// 
			this.inst_new_row.AutoSize = false;
			this.inst_new_row.AutoToolTip = false;
			this.inst_new_row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_new_row.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_new_row.Name = "inst_new_row";
			this.inst_new_row.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.inst_new_row.Size = new System.Drawing.Size(28, 28);
			this.inst_new_row.Text = "!";
			this.inst_new_row.ToolTipText = "Новая строка";
			// 
			// inst_reload
			// 
			this.inst_reload.AutoSize = false;
			this.inst_reload.AutoToolTip = false;
			this.inst_reload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_reload.Font = new System.Drawing.Font("Webdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_reload.Name = "inst_reload";
			this.inst_reload.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.inst_reload.Size = new System.Drawing.Size(28, 28);
			this.inst_reload.Text = "q";
			this.inst_reload.ToolTipText = "Обновить (внесенные изменения пропадут)";
			// 
			// inst_save
			// 
			this.inst_save.AutoSize = false;
			this.inst_save.AutoToolTip = false;
			this.inst_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_save.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_save.Name = "inst_save";
			this.inst_save.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.inst_save.Size = new System.Drawing.Size(28, 28);
			this.inst_save.Text = "<";
			this.inst_save.ToolTipText = "Сохранить изменения";
			// 
			// inst_sep_1
			// 
			this.inst_sep_1.Name = "inst_sep_1";
			this.inst_sep_1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.inst_sep_1.Size = new System.Drawing.Size(6, 31);
			// 
			// inst_label_1
			// 
			this.inst_label_1.Name = "inst_label_1";
			this.inst_label_1.Size = new System.Drawing.Size(76, 28);
			this.inst_label_1.Text = "Выводить по ";
			// 
			// inst_str_count_inp
			// 
			this.inst_str_count_inp.BackColor = System.Drawing.SystemColors.Control;
			this.inst_str_count_inp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.inst_str_count_inp.MaxLength = 10;
			this.inst_str_count_inp.Name = "inst_str_count_inp";
			this.inst_str_count_inp.Size = new System.Drawing.Size(40, 31);
			//this.inst_str_count_inp.ToolTipText = "";
			// 
			// inst_sep_2
			// 
			this.inst_sep_2.Name = "inst_sep_2";
			this.inst_sep_2.Size = new System.Drawing.Size(6, 31);
			// 
			// inst_left
			// 
			this.inst_left.AutoSize = false;
			this.inst_left.AutoToolTip = false;
			this.inst_left.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_left.Font = new System.Drawing.Font("Webdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_left.Name = "inst_left";
			this.inst_left.Size = new System.Drawing.Size(28, 28);
			this.inst_left.Text = "3";
			this.inst_left.ToolTipText = "Пред.";
			// 
			// inst_curr_page_inp
			// 
			this.inst_curr_page_inp.BackColor = System.Drawing.SystemColors.Control;
			this.inst_curr_page_inp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.inst_curr_page_inp.MaxLength = 10;
			this.inst_curr_page_inp.Name = "inst_curr_page_inp";
			this.inst_curr_page_inp.Size = new System.Drawing.Size(31, 31);
			this.inst_curr_page_inp.ToolTipText = "Текущая страница";
			// 
			// inst_label_2
			// 
			this.inst_label_2.AutoSize = false;
			this.inst_label_2.Name = "inst_label_2";
			this.inst_label_2.Size = new System.Drawing.Size(50, 28);
			this.inst_label_2.Text = "/";
			this.inst_label_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// inst_right
			// 
			this.inst_right.AutoSize = false;
			this.inst_right.AutoToolTip = false;
			this.inst_right.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_right.Font = new System.Drawing.Font("Webdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_right.Name = "inst_right";
			this.inst_right.Size = new System.Drawing.Size(28, 28);
			this.inst_right.Text = "4";
			this.inst_right.ToolTipText = "След.";
			// 
			// inst_sep_3
			// 
			this.inst_sep_3.Name = "inst_sep_3";
			this.inst_sep_3.Size = new System.Drawing.Size(6, 31);
			// 
			// inst_selector
			// 
			this.inst_selector.AutoSize = false;
			this.inst_selector.AutoToolTip = false;
			this.inst_selector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.inst_selector.Font = new System.Drawing.Font("Webdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.inst_selector.Name = "inst_selector";
			this.inst_selector.Size = new System.Drawing.Size(38, 28);
			this.inst_selector.Text = "L";
			this.inst_selector.ToolTipText = "Выборки";


			this.solvers_tools.ResumeLayout(false);
			this.solvers_tools.PerformLayout();
		}

		private System.Windows.Forms.ToolStrip solvers_tools;
		public System.Windows.Forms.ToolStripButton inst_new_row;
		public System.Windows.Forms.ToolStripButton inst_reload;
		public System.Windows.Forms.ToolStripButton inst_save;
		private System.Windows.Forms.ToolStripSeparator inst_sep_1;
		private System.Windows.Forms.ToolStripLabel inst_label_1;
		public System.Windows.Forms.ToolStripTextBox inst_str_count_inp;
		private System.Windows.Forms.ToolStripSeparator inst_sep_2;
		public System.Windows.Forms.ToolStripButton inst_left;
		public System.Windows.Forms.ToolStripTextBox inst_curr_page_inp;
		public System.Windows.Forms.ToolStripLabel inst_label_2;
		public System.Windows.Forms.ToolStripButton inst_right;
		private System.Windows.Forms.ToolStripSeparator inst_sep_3;
		private System.Windows.Forms.ToolStripDropDownButton inst_selector;
	}
}
