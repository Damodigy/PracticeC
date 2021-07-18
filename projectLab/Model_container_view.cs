using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Model_container_view;


namespace Model_container_view{

	class view_data{
		public string cont_id;
		public string cont_vol;
		public string fuel_vol;
		public string fuel_type;
	}
}

namespace projectLab {
	class Model_container_view{

		private table_instruments instruments;
		private MySqlConnection connect;

		//private MySqlCommand get_count_users;

		private UInt32 str_per_page;
		private UInt32 current_page;
		private UInt32 max_page;
		private UInt32 str_count;
		private string order_by;

		
		private List<TextBox> col_id;
		private List<TextBox> col_cont_vol;
		private List<TextBox> col_fuel_vol;
		private List<TextBox> col_fuel;


		public Model_container_view(ref table_instruments instrument_panel, ref MySqlConnection current_connect){
			instruments = instrument_panel;
			connect = current_connect;
			
			create_head_table();
			
			str_per_page = 25;
			current_page = 1;
			max_page = 1;
			str_count = 0;
			order_by = "ORDER BY `id` ASC";

			col_id = new List<TextBox>();
			col_cont_vol = new List<TextBox>();
			col_fuel_vol = new List<TextBox>();
			col_fuel = new List<TextBox>();

			instruments.inst_str_count_inp.Text = str_per_page.ToString();
			instruments.inst_curr_page_inp.Text = current_page.ToString();

			//instruments.inst_new_row.Click += this.new_str;
			instruments.inst_new_row.Enabled = false;
			instruments.inst_reload.Click += this.reload_data;
			instruments.inst_save.Enabled = false;

			instruments.inst_str_count_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_str_count_inp.KeyDown += this.enterkey_to_update;

			instruments.inst_left.Click += this.page_shift;
			instruments.inst_curr_page_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_curr_page_inp.KeyDown += this.enterkey_to_update;
			instruments.inst_right.Click += this.page_shift;

			reload_data();
		}

		public void Dispose(){
			cont_table.Dispose();
				container_label_id.Dispose();
				sort_label_id.Dispose();
				checker_sort_id.Dispose();
			container_label_cont_vol.Dispose();
				sort_label_cont_vol.Dispose();
				checker_sort_cont_vol.Dispose();
			container_label_fuel_vol.Dispose();
				sort_label_fuel_vol.Dispose();
				checker_sort_fuel_vol.Dispose();
			container_label_fuel.Dispose();
				sort_label_fuel.Dispose();
				checker_sort_fuel.Dispose();
		}

		//создает шапку таблицы
		private void create_head_table(){
			this.cont_table = new System.Windows.Forms.TableLayoutPanel();
				this.container_label_id = new System.Windows.Forms.Panel();
					this.sort_label_id = new System.Windows.Forms.LinkLabel();
					this.checker_sort_id = new System.Windows.Forms.Label();
				this.container_label_cont_vol = new System.Windows.Forms.Panel();
					this.sort_label_cont_vol = new System.Windows.Forms.LinkLabel();
					this.checker_sort_cont_vol = new System.Windows.Forms.Label();
				this.container_label_fuel_vol = new System.Windows.Forms.Panel();
					this.sort_label_fuel_vol = new System.Windows.Forms.LinkLabel();
					this.checker_sort_fuel_vol = new System.Windows.Forms.Label();
				this.container_label_fuel = new System.Windows.Forms.Panel();
					this.sort_label_fuel = new System.Windows.Forms.LinkLabel();
					this.checker_sort_fuel = new System.Windows.Forms.Label();
			
			this.container_label_id.SuspendLayout();
			this.container_label_cont_vol.SuspendLayout();
			this.container_label_fuel_vol.SuspendLayout();
			this.container_label_fuel.SuspendLayout();
			this.cont_table.SuspendLayout();

			// 
			// cont_table
			// 
			this.cont_table.Dock = System.Windows.Forms.DockStyle.Top;
			this.cont_table.AutoSize = true;
			this.cont_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.cont_table.ColumnCount = 4;
			this.cont_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.cont_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.cont_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.cont_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.cont_table.Controls.Add(this.container_label_id, 0, 0);
			this.cont_table.Controls.Add(this.container_label_cont_vol, 1, 0);
			this.cont_table.Controls.Add(this.container_label_fuel_vol, 2, 0);
			this.cont_table.Controls.Add(this.container_label_fuel, 3, 0);
			this.cont_table.Location = new System.Drawing.Point(0, 0);
			this.cont_table.Name = "cont_table";
			this.cont_table.RowCount = 1;
			this.cont_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.cont_table.TabIndex = 0;
			// 
			// container_label_id
			// 
			this.container_label_id.Controls.Add(this.checker_sort_id);
			this.container_label_id.Controls.Add(this.sort_label_id);
			this.container_label_id.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_id.Location = new System.Drawing.Point(2, 2);
			this.container_label_id.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_id.Name = "container_label_id";
			this.container_label_id.Size = new System.Drawing.Size(50, 20);
			//this.container_label_id.TabIndex = 5;
			// 
			// sort_label_id
			// 
			this.sort_label_id.ActiveLinkColor = System.Drawing.Color.Blue;
			this.sort_label_id.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.sort_label_id.AutoSize = true;
			this.sort_label_id.LinkColor = System.Drawing.Color.DarkBlue;
			this.sort_label_id.Location = new System.Drawing.Point(10, 4);
			this.sort_label_id.Name = "sort_label_id";
			this.sort_label_id.Size = new System.Drawing.Size(18, 13);
			this.sort_label_id.TabIndex = 1;
			this.sort_label_id.TabStop = true;
			this.sort_label_id.Text = "ID";
			this.sort_label_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.sort_label_id.Click += this.sort_str;
			// 
			// checker_sort_id
			// 
			this.checker_sort_id.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_id.AutoSize = true;
			this.checker_sort_id.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
			this.checker_sort_id.Location = new System.Drawing.Point(31, 2);
			this.checker_sort_id.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_id.Name = "checker_sort_id";
			this.checker_sort_id.Size = new System.Drawing.Size(19, 17);
			this.checker_sort_id.Text = "6";
			this.checker_sort_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_cont_vol
			// 
			this.container_label_cont_vol.Controls.Add(this.checker_sort_cont_vol);
			this.container_label_cont_vol.Controls.Add(this.sort_label_cont_vol);
			this.container_label_cont_vol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_cont_vol.Location = new System.Drawing.Point(2, 2);
			this.container_label_cont_vol.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_cont_vol.Name = "container_label_cont_vol";
			this.container_label_cont_vol.Size = new System.Drawing.Size(144, 20);
			// 
			// sort_label_cont_vol
			// 
			this.sort_label_cont_vol.ActiveLinkColor = System.Drawing.Color.Blue;
			this.sort_label_cont_vol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.sort_label_cont_vol.AutoSize = true;
			this.sort_label_cont_vol.LinkColor = System.Drawing.Color.DarkBlue;
			this.sort_label_cont_vol.Location = new System.Drawing.Point(10, 4);
			this.sort_label_cont_vol.Name = "sort_label_cont_vol";
			this.sort_label_cont_vol.Size = new System.Drawing.Size(34, 13);
			this.sort_label_cont_vol.TabIndex = 2;
			this.sort_label_cont_vol.TabStop = true;
			this.sort_label_cont_vol.Text = "Объем контейнера, л";
			this.sort_label_cont_vol.Click += this.sort_str;
			// 
			// checker_sort_cont_vol
			// 
			this.checker_sort_cont_vol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_cont_vol.AutoSize = true;
			this.checker_sort_cont_vol.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
			this.checker_sort_cont_vol.Location = new System.Drawing.Point(126, 2);
			this.checker_sort_cont_vol.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_cont_vol.Name = "checker_sort_cont_vol";
			this.checker_sort_cont_vol.Size = new System.Drawing.Size(19, 17);
			this.checker_sort_cont_vol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_fuel_vol
			// 
			this.container_label_fuel_vol.Controls.Add(this.checker_sort_fuel_vol);
			this.container_label_fuel_vol.Controls.Add(this.sort_label_fuel_vol);
			this.container_label_fuel_vol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_fuel_vol.Location = new System.Drawing.Point(2, 2);
			this.container_label_fuel_vol.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_fuel_vol.Name = "container_label_fuel_vol";
			this.container_label_fuel_vol.Size = new System.Drawing.Size(144, 20);
			// 
			// sort_label_fuel_vol
			// 
			this.sort_label_fuel_vol.ActiveLinkColor = System.Drawing.Color.Blue;
			this.sort_label_fuel_vol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.sort_label_fuel_vol.AutoSize = true;
			this.sort_label_fuel_vol.LinkColor = System.Drawing.Color.DarkBlue;
			this.sort_label_fuel_vol.Location = new System.Drawing.Point(30, 4);
			this.sort_label_fuel_vol.Name = "sort_label_fuel_vol";
			this.sort_label_fuel_vol.Size = new System.Drawing.Size(34, 13);
			this.sort_label_fuel_vol.TabIndex = 3;
			this.sort_label_fuel_vol.TabStop = true;
			this.sort_label_fuel_vol.Text = "Объем топлива, л";
			this.sort_label_fuel_vol.Click += this.sort_str;
			// 
			// checker_sort_fuel_vol
			// 
			this.checker_sort_fuel_vol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_fuel_vol.AutoSize = true;
			this.checker_sort_fuel_vol.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.checker_sort_fuel_vol.Location = new System.Drawing.Point(126, 2);
			this.checker_sort_fuel_vol.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_fuel_vol.Name = "checker_sort_fuel_vol";
			this.checker_sort_fuel_vol.Size = new System.Drawing.Size(19, 17);
			this.checker_sort_fuel_vol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_fuel
			// 
			this.container_label_fuel.Controls.Add(this.checker_sort_fuel);
			this.container_label_fuel.Controls.Add(this.sort_label_fuel);
			this.container_label_fuel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_fuel.Location = new System.Drawing.Point(2, 2);
			this.container_label_fuel.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_fuel.Name = "container_label_fuel";
			this.container_label_fuel.Size = new System.Drawing.Size(144, 20);
			// 
			// sort_label_fuel
			// 
			this.sort_label_fuel.ActiveLinkColor = System.Drawing.Color.Blue;
			this.sort_label_fuel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.sort_label_fuel.AutoSize = true;
			this.sort_label_fuel.LinkColor = System.Drawing.Color.DarkBlue;
			this.sort_label_fuel.Location = new System.Drawing.Point(55, 4);
			this.sort_label_fuel.Name = "sort_label_fuel";
			this.sort_label_fuel.Size = new System.Drawing.Size(34, 13);
			this.sort_label_fuel.TabIndex = 2;
			this.sort_label_fuel.TabStop = true;
			this.sort_label_fuel.Text = "Тип топлива";
			this.sort_label_fuel.Click += this.sort_str;
			// 
			// checker_sort_fuel
			// 
			this.checker_sort_fuel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_fuel.AutoSize = true;
			this.checker_sort_fuel.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.checker_sort_fuel.Location = new System.Drawing.Point(126, 2);
			this.checker_sort_fuel.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_fuel.Name = "checker_sort_fuel";
			this.checker_sort_fuel.Size = new System.Drawing.Size(19, 17);
			this.checker_sort_fuel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			
			
			this.container_label_id.ResumeLayout(false);
			this.container_label_id.PerformLayout();
			this.container_label_cont_vol.ResumeLayout(false);
			this.container_label_cont_vol.PerformLayout();
			this.container_label_fuel_vol.ResumeLayout(false);
			this.container_label_fuel_vol.PerformLayout();
			this.container_label_fuel.ResumeLayout(false);
			this.container_label_fuel.PerformLayout();
			this.cont_table.ResumeLayout(false);
			this.cont_table.PerformLayout();
		}//создает данные таблицы
		
		private void create_body_table(List<view_data> data){
			//очищаем таблицу
			cont_table.SuspendLayout();
			while(cont_table.RowCount > 1){
				int row = cont_table.RowCount - 1;
				for(Int32 a = 0; a < cont_table.ColumnCount; a++){
					Control c = cont_table.GetControlFromPosition(a, row);
					cont_table.Controls.Remove(c);
					c.Dispose();
				}

				//solvers_table.RowStyles.RemoveAt(row);
				cont_table.RowCount--;
			}
			col_id.Clear();
			col_cont_vol.Clear();
			col_fuel_vol.Clear();
			col_fuel.Clear();

			//собираем таблицу с данными
			cont_table.RowCount += data.Count;
			for(Int32 i=0; i<data.Count; i++){
				// 
				// inp_id
				// 
				col_id.Add(new TextBox());
				col_id[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_id[i].BackColor = System.Drawing.SystemColors.Control;
				col_id[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_id[i].Location = new System.Drawing.Point(5, 37);
				col_id[i].MaxLength = 20;
				col_id[i].Name = "inp_id_"+i;
				col_id[i].Text = data[i].cont_id;
				col_id[i].ReadOnly = true;
				col_id[i].Size = new System.Drawing.Size(44, 13);
				col_id[i].TabStop = false;
				cont_table.Controls.Add(col_id[i], 0, i+1);
				// 
				// inp_cont_vol
				// 
				col_cont_vol.Add(new TextBox());
				col_cont_vol[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_cont_vol[i].BackColor = System.Drawing.SystemColors.Control;
				col_cont_vol[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_cont_vol[i].Location = new System.Drawing.Point(57, 34);
				col_cont_vol[i].MaxLength = 500;
				col_cont_vol[i].Name = "inp_cont_vol_"+i;
				col_cont_vol[i].Text = data[i].cont_vol;
				col_cont_vol[i].ReadOnly = true;
				col_cont_vol[i].Size = new System.Drawing.Size(138, 20);
				cont_table.Controls.Add(col_cont_vol[i], 1, i+1);
				// 
				// inp_fuel_vol
				// 
				col_fuel_vol.Add(new TextBox());
				col_fuel_vol[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_fuel_vol[i].BackColor = System.Drawing.SystemColors.Control;
				col_fuel_vol[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_fuel_vol[i].Location = new System.Drawing.Point(57, 34);
				col_fuel_vol[i].MaxLength = 500;
				col_fuel_vol[i].Name = "inp_fuel_vol_"+i;
				col_fuel_vol[i].Text = data[i].fuel_vol;
				col_fuel_vol[i].ReadOnly = true;
				col_fuel_vol[i].Size = new System.Drawing.Size(138, 20);
				cont_table.Controls.Add(col_fuel_vol[i], 2, i+1);
				// 
				// inp_fuel
				// 
				col_fuel.Add(new TextBox());
				col_fuel[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_fuel[i].BackColor = System.Drawing.SystemColors.Control;
				col_fuel[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_fuel[i].Location = new System.Drawing.Point(57, 34);
				col_fuel[i].Name = "inp_fuel_"+i;
				col_fuel[i].Text = data[i].fuel_type;
				col_fuel[i].ReadOnly = true;
				col_fuel[i].Size = new System.Drawing.Size(138, 20);
				cont_table.Controls.Add(col_fuel[i], 3, i+1);
			}

			cont_table.ResumeLayout(false);
			cont_table.PerformLayout();
		}
		
		//перезагружает данные в таблицу
		private void reload_data(){


			MySqlDataReader result = null;
			//Получим количество строк
			MySqlCommand query = new MySqlCommand("SELECT COUNT(*) FROM `containers`", connect);
			try{
				str_count = Convert.ToUInt32(query.ExecuteScalar());
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//соберем пагинацию
			str_per_page = instruments.inst_str_count_inp.Text == "" ? 0 : UInt32.Parse(instruments.inst_str_count_inp.Text);
			if(str_per_page < 5){
				str_per_page = 5;
			}
			instruments.inst_str_count_inp.Text = str_per_page.ToString();

			max_page = str_count / str_per_page;
			if(max_page == 0 || str_count % str_per_page != 0){
				max_page++;
			}
			instruments.inst_label_2.Text = "/"+max_page;
	
			current_page = instruments.inst_curr_page_inp.Text == "" ? 0 : UInt32.Parse(instruments.inst_curr_page_inp.Text);
			if(current_page < 1)
				current_page = 1;
			if(current_page > max_page)
				current_page = max_page;
			instruments.inst_curr_page_inp.Text = current_page.ToString();

			
			query.CommandText = "SELECT `cont`.`id`, `cont`.`container_volume`, `cont`.`fuel_volume`, `cont`.`fuel_id`, `fuel`.`type`, `fuel`.`cost_buy`, `fuel`.`cost_sale`\n"
								+"FROM `containers` AS `cont`"
								+"LEFT JOIN `fuel_types` AS `fuel`"
								+"ON `cont`.`fuel_id` = `fuel`.`id`"
								+order_by+"\n"
								+"LIMIT @limit OFFSET @offset";
			query.Parameters.AddWithValue("@limit", str_per_page);
			query.Parameters.AddWithValue("@offset", (current_page-1)*str_per_page);

			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//преобразуем данные в массив
			List<view_data> data = new List<view_data>();
			while(result.Read()){
				view_data tmp = new view_data();
				UInt64 cont_vol, fuel_vol;
				tmp.cont_id = Convert.ToString(result.GetValue(0));			//`cont`.`id`
				cont_vol = Convert.ToUInt64(result.GetValue(1));			//`cont`.`container_volume`
				fuel_vol = Convert.ToUInt64(result.GetValue(2));			//`cont`.`fuel_volume`
				tmp.cont_vol = cont_vol.ToString();
				tmp.fuel_vol = fuel_vol + "\t("+Math.Round((fuel_vol*100.0)/(double)cont_vol, 2)+"%)";
				if(Convert.IsDBNull(result.GetValue(3))){					//`cont`.`fuel_id`
					tmp.fuel_type = "Отсутсвует";
				}else{
					tmp.fuel_type = Convert.ToString(result.GetValue(4))	//`fuel`.`type`
								+"("+Convert.ToString(result.GetValue(3))	//`cont`.`fuel_id`
								+")\tпрод. "
								+(Convert.ToUInt64(result.GetValue(6))/100.00)//`fuel`.`cost_sale`
								+" руб\t|\tпокуп. "
								+(Convert.ToUInt64(result.GetValue(5))/100.00)//`fuel`.`cost_buy`
								+" руб";
				}
				data.Add(tmp);
			}
			result.Close();
			
			//выведем таблицу
			create_body_table(data);
		}

///////////////////////////////////////////////////////////////////////////////
//		обработчики событий
///////////////////////////////////////////////////////////////////////////////
		//перезагружает данные в таблицу
		private void reload_data(object sender,EventArgs e){
			reload_data();
		}

		private void enterkey_to_update(Object sender, KeyEventArgs e){
			if(e.KeyCode == Keys.Enter){
				reload_data();
			}
		}

		private void page_shift(Object sender, EventArgs e){
			bool update = false;
			if(((ToolStripButton)sender).Name == "inst_left"){
				if(current_page > 1){
					current_page--;
					update = true;
				}
			}else if(((ToolStripButton)sender).Name == "inst_right"){
				if(current_page < max_page){
					current_page++;
					update = true;
				}
			}
			if(update){
				instruments.inst_curr_page_inp.Text = current_page.ToString();
				reload_data();
			}
		}

		private void sort_str(Object sender, EventArgs e){
			string name = ((LinkLabel)sender).Name;
			switch(name){
				case "sort_label_id":
					//была включена сортировка ASC
					if(checker_sort_id.Text == "6"){
						checker_sort_id.Text = "5";
						order_by = "ORDER BY `id` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_id.Text == "5"){
						checker_sort_id.Text = "6";
						order_by = "ORDER BY `id` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_id.Text = "6";
						order_by = "ORDER BY `id` ASC ";
						checker_sort_cont_vol.Text = "";
						checker_sort_fuel_vol.Text = "";
						checker_sort_fuel.Text = "";
					}
					break;
				case "sort_label_cont_vol":
					//была включена сортировка ASC
					if(checker_sort_cont_vol.Text == "6"){
						checker_sort_cont_vol.Text = "5";
						order_by = "ORDER BY `container_volume` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_cont_vol.Text == "5"){
						checker_sort_cont_vol.Text = "6";
						order_by = "ORDER BY `container_volume` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_cont_vol.Text = "6";
						order_by = "ORDER BY `container_volume` ASC ";
						checker_sort_id.Text = "";
						checker_sort_fuel_vol.Text = "";
						checker_sort_fuel.Text = "";
					}
					break;
				case "sort_label_fuel_vol":
					//была включена сортировка ASC
					if(checker_sort_fuel_vol.Text == "6"){
						checker_sort_fuel_vol.Text = "5";
						order_by = "ORDER BY `fuel_volume` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_fuel_vol.Text == "5"){
						checker_sort_fuel_vol.Text = "6";
						order_by = "ORDER BY `fuel_volume` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_fuel_vol.Text = "6";
						order_by = "ORDER BY `fuel_volume` ASC ";
						checker_sort_id.Text = "";
						checker_sort_cont_vol.Text = "";
						checker_sort_fuel.Text = "";
					}
					break;
				case "sort_label_fuel":
					//была включена сортировка ASC
					if(checker_sort_fuel.Text == "6"){
						checker_sort_fuel.Text = "5";
						order_by = "ORDER BY `fuel_id` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_fuel.Text == "5"){
						checker_sort_fuel.Text = "6";
						order_by = "ORDER BY `fuel_id` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_fuel.Text = "6";
						order_by = "ORDER BY `fuel_id` ASC ";
						checker_sort_id.Text = "";
						checker_sort_cont_vol.Text = "";
						checker_sort_fuel_vol.Text = "";
					}
					break;
			}
			instruments.inst_curr_page_inp.Text = "1";
			reload_data();
		}

///////////////////////////////////////////////////////////////////////////////
//		объекты форм
///////////////////////////////////////////////////////////////////////////////

		public System.Windows.Forms.TableLayoutPanel cont_table;
			private System.Windows.Forms.Panel container_label_id;
				private System.Windows.Forms.LinkLabel sort_label_id;
				private System.Windows.Forms.Label checker_sort_id;
			private System.Windows.Forms.Panel container_label_cont_vol;
				private System.Windows.Forms.LinkLabel sort_label_cont_vol;
				private System.Windows.Forms.Label checker_sort_cont_vol;
			private System.Windows.Forms.Panel container_label_fuel_vol;
				private System.Windows.Forms.LinkLabel sort_label_fuel_vol;
				private System.Windows.Forms.Label checker_sort_fuel_vol;
			private System.Windows.Forms.Panel container_label_fuel;
				private System.Windows.Forms.LinkLabel sort_label_fuel;
				private System.Windows.Forms.Label checker_sort_fuel;
	}
}
