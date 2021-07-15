using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Model_fuel_sale;

namespace Model_fuel_sale{
	class fuel{
		public string name;
		public UInt32 cost;
		public UInt64 id;
	}

	class container{
		public UInt16 id;
		public UInt64 volume;
		public UInt32 fuel_id;
	}

	class pump{
		public UInt16 id;
		public UInt16 tap_1;
		public UInt16 tap_2;
		public UInt16 tap_3;
		public UInt16 tap_4;
		public UInt16 tap_5;
		public UInt16 tap_6;
	}

	class view_data{
		public string pump_id;
		public string fuel_type;
		public string volume;
		public string cost;
		public string time_start;
		public string time_end;
		public string id;
	}
}

namespace projectLab {

	class Model_fuel_sale :IDisposable{

		private table_instruments instruments;
		private MySqlConnection connect;

		//private MySqlCommand get_count_users;

		private UInt32 str_per_page;
		private UInt32 current_page;
		private UInt32 max_page;
		private UInt32 str_count;
		private string order_by;

		UInt64 slave_id;
		Dictionary<UInt16, container> containers;
		Dictionary<UInt64, fuel> fuels;
		List<pump> pumps;
		Dictionary<Int32, view_data> data_not_exec_strings;
		
		//private Dictionary<int, string> departaments;
		//private List<int> departs_id;

		private List<ComboBox> col_pump;
		private List<ComboBox> col_fuel;
		private List<TextBox> col_volume_need;
		private List<TextBox> col_res_cost;
		private List<TextBox> col_time_start;
		private List<TextBox> col_time_end;
		private List<Button> col_but_exec;

		public Model_fuel_sale(ref table_instruments instrument_panel, ref MySqlConnection current_connect, UInt64 slave_id){
			instruments = instrument_panel;
			connect = current_connect;

			this.slave_id = slave_id;
			this.containers = new Dictionary<UInt16,container>();
			this.fuels = new Dictionary<UInt64,fuel>();
			this.pumps = new List<pump>();
			this.data_not_exec_strings = new Dictionary<Int32,view_data>();
			
			create_head_table();
			
			str_per_page = 25;
			current_page = 1;
			max_page = 1;
			str_count = 0;
			order_by = "ORDER BY `time_start` DESC";

			col_pump = new List<ComboBox>();
			col_fuel = new List<ComboBox>();
			col_volume_need = new List<TextBox>();
			col_res_cost = new List<TextBox>();
			col_time_start = new List<TextBox>();
			col_time_end = new List<TextBox>();
			col_but_exec = new List<Button>();

			instruments.inst_str_count_inp.Text = str_per_page.ToString();
			instruments.inst_curr_page_inp.Text = current_page.ToString();

			instruments.inst_new_row.Click += this.new_str;
			instruments.inst_reload.Click += this.reload_data_with_save;
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
			this.label_pump.Dispose();
			this.label_tap.Dispose();
			this.label_need_vol.Dispose();
			this.label_cost.Dispose();
			this.label_time_start.Dispose();
			this.label_time_end.Dispose();
			this.trans_table.Dispose();
		}

		//создает шапку таблицы
		private void create_head_table(){
			this.trans_table = new System.Windows.Forms.TableLayoutPanel();
			this.label_pump = new System.Windows.Forms.Label();
			this.label_tap = new System.Windows.Forms.Label();
			this.label_need_vol = new System.Windows.Forms.Label();
			this.label_cost = new System.Windows.Forms.Label();
			this.label_time_start = new System.Windows.Forms.Label();
			this.label_time_end = new System.Windows.Forms.Label();
			
			this.trans_table.SuspendLayout();

			// 
			// trans_table
			// 
			this.trans_table.Dock = System.Windows.Forms.DockStyle.Top;
			//this.trans_table.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.trans_table.AutoSize = true;
			this.trans_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.trans_table.ColumnCount = 7;
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.trans_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.trans_table.Controls.Add(this.label_pump, 0, 0);
			this.trans_table.Controls.Add(this.label_tap, 1, 0);
			this.trans_table.Controls.Add(this.label_need_vol, 2, 0);
			this.trans_table.Controls.Add(this.label_cost, 3, 0);
			this.trans_table.Controls.Add(this.label_time_start, 4, 0);
			this.trans_table.Controls.Add(this.label_time_end, 5, 0);
			this.trans_table.Location = new System.Drawing.Point(0, 0);
			this.trans_table.Name = "trans_table";
			this.trans_table.RowCount = 1;
			this.trans_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			//this.trans_table.Size = new System.Drawing.Size(584, 66);
			this.trans_table.TabIndex = 0;
			// 
			// label_pump
			// 
			this.label_pump.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_pump.AutoSize = true;
			this.label_pump.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			this.label_pump.Location = new System.Drawing.Point(5, 5);
			this.label_pump.Margin = new System.Windows.Forms.Padding(2);
			this.label_pump.Name = "label_pump";
			//this.label_pump.Size = new System.Drawing.Size(48, 13);
			//this.label_data.TabIndex = 1;
			this.label_pump.Text = "№ колонки";
			this.label_pump.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_tap
			// 
			this.label_tap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_tap.AutoSize = true;
			this.label_tap.Location = new System.Drawing.Point(5, 5);
			this.label_tap.Margin = new System.Windows.Forms.Padding(2);
			this.label_tap.Name = "label_tap";
			this.label_tap.Text = "Тип топлива";
			this.label_tap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_need_vol
			// 
			this.label_need_vol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_need_vol.AutoSize = true;
			this.label_need_vol.Location = new System.Drawing.Point(5, 5);
			this.label_need_vol.Margin = new System.Windows.Forms.Padding(2);
			this.label_need_vol.Name = "label_need_vol";
			this.label_need_vol.Text = "Объем топлива, л";
			this.label_need_vol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_cost
			// 
			this.label_cost.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_cost.AutoSize = true;
			this.label_cost.Location = new System.Drawing.Point(5, 5);
			this.label_cost.Margin = new System.Windows.Forms.Padding(2);
			this.label_cost.Name = "label_cost";
			this.label_cost.Text = "Цена л/всего, руб";
			this.label_cost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_time_start
			// 
			this.label_time_start.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_time_start.AutoSize = true;
			this.label_time_start.Location = new System.Drawing.Point(5, 5);
			this.label_time_start.Margin = new System.Windows.Forms.Padding(2);
			this.label_time_start.Name = "label_time_start";
			this.label_time_start.Text = "Время старта";
			this.label_time_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_time_end
			// 
			this.label_time_end.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_time_end.AutoSize = true;
			this.label_time_end.Location = new System.Drawing.Point(5, 5);
			this.label_time_end.Margin = new System.Windows.Forms.Padding(2);
			this.label_time_end.Name = "label_time_end";
			this.label_time_end.Text = "Время конца";
			this.label_time_end.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			
			this.trans_table.ResumeLayout(false);
			this.trans_table.PerformLayout();
		}

		//создает данные таблицы
		private void create_body_table(List<view_data> data){
			//очищаем таблицу
			trans_table.SuspendLayout();
			while(trans_table.RowCount > 1){
				int row = trans_table.RowCount - 1;
				for(Int32 a = 0; a < trans_table.ColumnCount; a++){
					Control c = trans_table.GetControlFromPosition(a, row);
					trans_table.Controls.Remove(c);
					c.Dispose();
				}

				//trans_table.RowStyles.RemoveAt(row);
				trans_table.RowCount--;
			}
			col_pump.Clear();
			col_fuel.Clear();
			col_volume_need.Clear();
			col_res_cost.Clear();
			col_time_start.Clear();
			col_time_end.Clear();
			col_but_exec.Clear();

			
			//собираем таблицу с данными
			trans_table.RowCount += data.Count;
			for(Int32 i=0; i<data.Count; i++){
				// 
				// cb_pump
				// 
				col_pump.Add(new ComboBox());
				col_pump[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_pump[i].FormattingEnabled = true;
				col_pump[i].Location = new System.Drawing.Point(203, 33);
				//col_pump[i].MaxDropDownItems = 50;
				col_pump[i].Name = "col_pump_"+i;
				col_pump[i].Items.Add(data[i].pump_id);
				col_pump[i].SelectedIndex = 0;
				col_pump[i].Enabled = false;
				col_pump[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				col_pump[i].Size = new System.Drawing.Size(138, 21);
				col_pump[i].TabIndex = i*7 + 1;
				trans_table.Controls.Add(col_pump[i], 0, i+1);
				// 
				// cb_tap
				// 
				col_fuel.Add(new ComboBox());
				col_fuel[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_fuel[i].FormattingEnabled = true;
				col_fuel[i].Location = new System.Drawing.Point(203, 33);
				//col_fuel[i].MaxDropDownItems = 50;
				col_fuel[i].Name = "col_fuel_"+i;
				col_fuel[i].Items.Add(data[i].fuel_type);
				col_fuel[i].SelectedIndex = 0;
				col_fuel[i].Enabled = false;
				col_fuel[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				col_fuel[i].Size = new System.Drawing.Size(138, 21);
				col_fuel[i].TabIndex = i*7 + 2;
				trans_table.Controls.Add(col_fuel[i], 1, i+1);
				// 
				// inp_volume
				// 
				col_volume_need.Add(new TextBox());
				col_volume_need[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_volume_need[i].BackColor = System.Drawing.SystemColors.Control;
				col_volume_need[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_volume_need[i].Location = new System.Drawing.Point(5, 37);
				col_volume_need[i].Name = "col_volume_need_"+i;
				col_volume_need[i].Text = data[i].volume;
				col_volume_need[i].ReadOnly = true;
				col_volume_need[i].Size = new System.Drawing.Size(50, 13);
				col_volume_need[i].TabIndex = i*7 + 3;
				trans_table.Controls.Add(col_volume_need[i], 2, i+1);
				// 
				// inp_cost
				// 
				col_res_cost.Add(new TextBox());
				col_res_cost[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_res_cost[i].BackColor = System.Drawing.SystemColors.Control;
				col_res_cost[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_res_cost[i].Location = new System.Drawing.Point(5, 37);
				col_res_cost[i].Name = "col_res_cost_"+i;
				col_res_cost[i].Text = data[i].cost;
				col_res_cost[i].ReadOnly = true;
				col_res_cost[i].Size = new System.Drawing.Size(80, 13);
				col_res_cost[i].TabIndex = i*7 + 4;
				trans_table.Controls.Add(col_res_cost[i], 3, i+1);
				// 
				//  inp_time_start
				// 
				col_time_start.Add(new TextBox());
				col_time_start[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_time_start[i].BackColor = System.Drawing.SystemColors.Control;
				col_time_start[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_time_start[i].Location = new System.Drawing.Point(5, 37);
				col_time_start[i].Name = "col_time_start_"+i;
				col_time_start[i].Text = data[i].time_start;
				col_time_start[i].ReadOnly = true;
				col_time_start[i].Size = new System.Drawing.Size(70, 13);
				col_time_start[i].TabIndex = i*7 + 5;
				trans_table.Controls.Add(col_time_start[i], 4, i+1);
				// 
				//  inp_time_end
				// 
				col_time_end.Add(new TextBox());
				col_time_end[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_time_end[i].BackColor = System.Drawing.SystemColors.Control;
				col_time_end[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_time_end[i].Location = new System.Drawing.Point(5, 37);
				col_time_end[i].Name = "col_time_end_"+i;
				col_time_end[i].Text = data[i].time_end;
				col_time_end[i].ReadOnly = true;
				col_time_end[i].Size = new System.Drawing.Size(70, 13);
				col_time_end[i].TabIndex = i*7 + 6;
				trans_table.Controls.Add(col_time_end[i], 5, i+1);
				// 
				// but_exec
				// 
				col_but_exec.Add(new Button());
				col_but_exec[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_but_exec[i].Cursor = System.Windows.Forms.Cursors.Hand;
				col_but_exec[i].FlatStyle = System.Windows.Forms.FlatStyle.System;
				col_but_exec[i].Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
				col_but_exec[i].Location = new System.Drawing.Point(548, 32);
				col_but_exec[i].Name = "col_but_exec_"+i;
				col_but_exec[i].Size = new System.Drawing.Size(26, 24);
				col_but_exec[i].TabIndex = i*7 + 7;
				if(data[i].time_end != ""){
					col_but_exec[i].Enabled = false;
				}else{
					this.data_not_exec_strings[i] = data[i];
				}
				col_but_exec[i].Text = "<";
				col_but_exec[i].UseVisualStyleBackColor = true;
				col_but_exec[i].Click += execute_trans;
				trans_table.Controls.Add(col_but_exec[i], 6, i+1);
			}
			trans_table.ResumeLayout(false);
			trans_table.PerformLayout();
		}

		//перезагружает данные в таблицу
		private void reload_data(){
			MySqlDataReader result = null;
			//получим время начала и конца текущей смены
			MySqlCommand query = new MySqlCommand("SELECT `time_start`, `time_end` FROM `shifts` WHERE NOW() >= `time_start` AND NOW() <= `time_end`", connect);
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			DateTime s_time_start, s_time_end;
			if(result.Read()){
				s_time_start = (DateTime)result.GetValue(0);
				s_time_end = (DateTime)result.GetValue(1);
			}else{
				MessageBox.Show("На данный момент смена не активна. Обратитесь к манагеру", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			result.Close();

			//заблочим таблицы `fuel_transactions`, `containers`, `fuel_types`, `pumps`
			query.CommandText = "LOCK TABLES `fuel_transactions` READ, `containers` READ, `fuel_types` READ, `pumps` READ";
			try{
				query.ExecuteNonQuery();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//Получим количество строк
			query.CommandText = "SELECT COUNT(*) FROM `fuel_transactions` WHERE `responsible_for` = @slave_id AND `time_start` >= @time_start AND (`time_end` <= @time_end OR `time_end` IS NULL)";
			query.Parameters.AddWithValue("@slave_id", this.slave_id);
			query.Parameters.AddWithValue("@time_start", s_time_start);
			query.Parameters.AddWithValue("@time_end", s_time_end);
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

			
			this.containers.Clear();
			this.fuels.Clear();
			this.pumps.Clear();
			this.data_not_exec_strings.Clear();


			//получим список контейнеров
			query = new MySqlCommand("SELECT `id`, `fuel_volume`, `fuel_id` FROM `containers`", connect);
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			HashSet<UInt32> fuels_id = new HashSet<UInt32>();
			while(result.Read()){
				container tmp = new container();
				tmp.id = Convert.ToUInt16(result.GetValue(0));
				tmp.volume = Convert.ToUInt64(result.GetValue(1));
				tmp.fuel_id = Convert.IsDBNull(result.GetValue(2)) ? 0 : Convert.ToUInt32(result.GetValue(2));
				fuels_id.Add(tmp.fuel_id);
				containers[tmp.id] = tmp;
			}
			result.Close();

			if(containers.Count < 1){
				MessageBox.Show("Топливные контейнеры отсутсвуют! Обратитесь к админу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//Получим список топлива используемого в баках
			query.CommandText = "SELECT `id`, `type`, `cost_sale` FROM `fuel_types` WHERE `id` IN("+string.Join(", ", fuels_id)+")";
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			fuels_id = null;
			while(result.Read()){
				fuel tmp = new fuel();
				tmp.id = Convert.ToUInt16(result.GetValue(0));
				tmp.name = Convert.ToString(result.GetValue(1));
				tmp.cost = Convert.ToUInt32(result.GetValue(2));
				fuels[tmp.id] = tmp;
			}
			result.Close();

			if(fuels.Count < 1){
				MessageBox.Show("Контейнеры не имеют топливо! Обратитесь к админу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//Получим список колонок
			query = new MySqlCommand("SELECT `id`, `tap_1`, `tap_2`, `tap_3`, `tap_4`, `tap_5`, `tap_6` FROM `pumps`", connect);
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			while(result.Read()){
				pump tmp = new pump();
				tmp.id = Convert.ToUInt16(result.GetValue(0));
				tmp.tap_1 = Convert.IsDBNull(result.GetValue(1)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(1));
				tmp.tap_2 = Convert.IsDBNull(result.GetValue(2)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(2));
				tmp.tap_3 = Convert.IsDBNull(result.GetValue(3)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(3));
				tmp.tap_4 = Convert.IsDBNull(result.GetValue(4)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(4));
				tmp.tap_5 = Convert.IsDBNull(result.GetValue(5)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(5));
				tmp.tap_6 = Convert.IsDBNull(result.GetValue(6)) ? (UInt16)0 : Convert.ToUInt16(result.GetValue(6));
				pumps.Add(tmp);
			}
			result.Close();

			if(pumps.Count < 1){
				MessageBox.Show("Колонки отсутствуют! Обратитесь к админу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			query.CommandText	= "SELECT `fuel_transactions`.`id`, `fuel_transactions`.`pump_id`, `fuel_transactions`.`container_id`, `fuel_transactions`.`fuel_id`, `fuel_types`.`type`, `fuel_transactions`.`fuel_volume`, `fuel_transactions`.`cost`, `fuel_transactions`.`time_start`, `fuel_transactions`.`time_end`\n"
								+ "FROM `fuel_transactions`\n"
								+ "LEFT JOIN `fuel_types`\n"
								+ "ON `fuel_transactions`.`fuel_id` = `fuel_types`.`id`\n"
								+ "WHERE `fuel_transactions`.`responsible_for` = @slave_id AND `fuel_transactions`.`time_start` >= @time_start AND (`fuel_transactions`.`time_end` <= @time_end OR `fuel_transactions`.`time_end` IS NULL)\n"
								+ order_by+"\n"
								+ "LIMIT @limit OFFSET @offset";

			query.Parameters.AddWithValue("@slave_id", this.slave_id);
			query.Parameters.AddWithValue("@time_start", s_time_start);
			query.Parameters.AddWithValue("@time_end", s_time_end);
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
				string str;
				Int64 cost, vol;
				DateTime time;
				tmp.id = Convert.ToString(result.GetValue(0));			//`trans`.`id`
				tmp.pump_id = Convert.ToString(result.GetValue(1));		//`trans`.`pump_id`
				str = "Конт. #" +Convert.ToString(result.GetValue(2))	//`trans`.`container_id`
					+ " ["+Convert.ToString(result.GetValue(4))+"("		//`fuel`.`type`
					+ Convert.ToString(result.GetValue(3))+")]";		//`trans`.`fuel_id`
				tmp.fuel_type = str;
				vol = Convert.ToInt64(result.GetValue(5));				//`trans`.`fuel_volume`
				cost = Convert.ToInt64(result.GetValue(6));				//`trans`.`cost`
				vol = Math.Abs(vol);
				tmp.volume = vol.ToString();
				str = Math.Round(((Double)cost/100.0)/(Double)vol, 2) +"/"+Math.Round((Double)cost/100.0, 2);
				tmp.cost = str;
				time = (DateTime)result.GetValue(7);					//`trans`.`time_start`
				tmp.time_start = time.ToString("HH:mm:ss");
				if(Convert.IsDBNull(result.GetValue(8))){				//`trans`.`time_end`
					tmp.time_end = "";
				}else{
					time = (DateTime)result.GetValue(8);				//`trans`.`time_end`
					tmp.time_end = time.ToString("HH:mm:ss");
				}
				data.Add(tmp);
			}
			result.Close();

			//разлочим таблицы
			query.CommandText = "UNLOCK TABLES";
			try{
				query.ExecuteNonQuery();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			
			//выведем таблицу
			create_body_table(data);
		}

///////////////////////////////////////////////////////////////////////////////
//		обработчики событий
///////////////////////////////////////////////////////////////////////////////

		private void new_str(Object sender, EventArgs e){
			//TODO тест на заливку топлива
			int ind = trans_table.RowCount-1;
			trans_table.SuspendLayout();
			trans_table.RowCount += 1;

			view_data tmp = new view_data();

			// 
			// cb_pump
			// 
			col_pump.Add(new ComboBox());
			col_pump[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_pump[ind].FormattingEnabled = true;
			col_pump[ind].Location = new System.Drawing.Point(203, 33);
			//col_pump[i].MaxDropDownItems = 50;
			col_pump[ind].Name = "col_pump_"+ind;
			col_pump[ind].Items.Add("---");
			for(Int32 i=0; i<pumps.Count; i++){
				col_pump[ind].Items.Add(pumps[i].id);
			}
			col_pump[ind].SelectedIndex = 0;
			col_pump[ind].SelectedIndexChanged += change_pump;
			col_pump[ind].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			col_pump[ind].Size = new System.Drawing.Size(138, 21);
			col_pump[ind].TabIndex = ind*7 + 1;
			trans_table.Controls.Add(col_pump[ind], 0, ind+1);
			tmp.pump_id = "0";
			// 
			// cb_tap
			// 
			col_fuel.Add(new ComboBox());
			col_fuel[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_fuel[ind].FormattingEnabled = true;
			col_fuel[ind].Location = new System.Drawing.Point(203, 33);
			//col_fuel[i].MaxDropDownItems = 50;
			col_fuel[ind].Name = "col_fuel_"+ind;
			col_fuel[ind].Items.Add("---");
			col_fuel[ind].SelectedIndex = 0;
			col_fuel[ind].Enabled = false;
			col_fuel[ind].SelectedIndexChanged += change_tap;
			col_fuel[ind].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			col_fuel[ind].Size = new System.Drawing.Size(138, 21);
			col_fuel[ind].TabIndex = ind*7 + 2;
			trans_table.Controls.Add(col_fuel[ind], 1, ind+1);
			tmp.fuel_type = "0";
			// 
			// inp_volume
			// 
			col_volume_need.Add(new TextBox());
			col_volume_need[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			//col_volume_need[ind].BackColor = System.Drawing.SystemColors.Control;
			//col_volume_need[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_volume_need[ind].Location = new System.Drawing.Point(5, 37);
			col_volume_need[ind].Name = "col_volume_need_"+ind;
			col_volume_need[ind].Text = "";
			col_volume_need[ind].TextChanged += validator.validate_absint_TextBox;
			col_volume_need[ind].TextChanged += change_cost;
			col_volume_need[ind].Size = new System.Drawing.Size(50, 13);
			col_volume_need[ind].TabIndex = ind*7 + 3;
			trans_table.Controls.Add(col_volume_need[ind], 2, ind+1);
			tmp.volume = "0";
			// 
			// inp_cost
			// 
			col_res_cost.Add(new TextBox());
			col_res_cost[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_res_cost[ind].BackColor = System.Drawing.SystemColors.Control;
			col_res_cost[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_res_cost[ind].Location = new System.Drawing.Point(5, 37);
			col_res_cost[ind].Name = "col_res_cost_"+ind;
			col_res_cost[ind].Text = "0/0";
			col_res_cost[ind].ReadOnly = true;
			col_res_cost[ind].Size = new System.Drawing.Size(80, 13);
			col_res_cost[ind].TabIndex = ind*7 + 4;
			trans_table.Controls.Add(col_res_cost[ind], 3, ind+1);
			tmp.cost = "0";
			// 
			//  inp_time_start
			// 
			col_time_start.Add(new TextBox());
			col_time_start[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_time_start[ind].BackColor = System.Drawing.SystemColors.Control;
			col_time_start[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_time_start[ind].Location = new System.Drawing.Point(5, 37);
			col_time_start[ind].Name = "col_time_start_"+ind;
			col_time_start[ind].Text = "";
			col_time_start[ind].ReadOnly = true;
			col_time_start[ind].Size = new System.Drawing.Size(70, 13);
			col_time_start[ind].TabIndex = ind*7 + 5;
			trans_table.Controls.Add(col_time_start[ind], 4, ind+1);
			tmp.time_start = "";
			// 
			//  inp_time_end
			// 
			col_time_end.Add(new TextBox());
			col_time_end[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_time_end[ind].BackColor = System.Drawing.SystemColors.Control;
			col_time_end[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_time_end[ind].Location = new System.Drawing.Point(5, 37);
			col_time_end[ind].Name = "col_time_end_"+ind;
			col_time_end[ind].Text = "";
			col_time_end[ind].ReadOnly = true;
			col_time_end[ind].Size = new System.Drawing.Size(70, 13);
			col_time_end[ind].TabIndex = ind*7 + 6;
			trans_table.Controls.Add(col_time_end[ind], 5, ind+1);
			tmp.time_end = "";
			// 
			// but_exec
			// 
			col_but_exec.Add(new Button());
			col_but_exec[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_but_exec[ind].Cursor = System.Windows.Forms.Cursors.Hand;
			col_but_exec[ind].FlatStyle = System.Windows.Forms.FlatStyle.System;
			col_but_exec[ind].Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			col_but_exec[ind].Location = new System.Drawing.Point(548, 32);
			col_but_exec[ind].Name = "col_but_exec_"+ind;
			col_but_exec[ind].Size = new System.Drawing.Size(26, 24);
			col_but_exec[ind].TabIndex = ind*7 + 7;
			col_but_exec[ind].Enabled = false;
			col_but_exec[ind].Text = "4";
			col_but_exec[ind].UseVisualStyleBackColor = true;
			col_but_exec[ind].Click += execute_trans;
			trans_table.Controls.Add(col_but_exec[ind], 6, ind+1);
			tmp.id = "0";

			data_not_exec_strings[ind] = tmp;

			trans_table.ResumeLayout(false);
			trans_table.PerformLayout();
			
		}

		private void change_pump(object sender,EventArgs e){
			ComboBox curr_cb = (ComboBox)sender;
			Int32 ind = (Int32)validator.extract_uint_from_end(curr_cb.Name);
			col_fuel[ind].Items.Clear();
			col_fuel[ind].Items.Add("---");
			col_fuel[ind].SelectedIndex = 0;
			col_fuel[ind].Enabled = false;
			col_res_cost[ind].Text = "0/0";
			col_but_exec[ind].Enabled = false;
			if(curr_cb.SelectedIndex != 0){
				Int32 pump_ind = curr_cb.SelectedIndex-1;
				//data_not_exec_strings[ind].pump_id = pump_ind.ToString();
				UInt64 fuel_id;

				for(Int16 i=1; i<=6; i++){
					UInt16 cont_id = 0;
					switch(i){
						case 1:
							cont_id = pumps[pump_ind].tap_1;
							break;
						case 2:
							cont_id = pumps[pump_ind].tap_2;
							break;
						case 3:
							cont_id = pumps[pump_ind].tap_3;
							break;
						case 4:
							cont_id = pumps[pump_ind].tap_4;
							break;
						case 5:
							cont_id = pumps[pump_ind].tap_5;
							break;
						case 6:
							cont_id = pumps[pump_ind].tap_6;
							break;
					}

					if(cont_id != 0){
						fuel_id = containers[cont_id].fuel_id;
						if(fuel_id != 0){
							col_fuel[ind].Items.Add("Пист."+i+" ["+fuels[fuel_id].name+"("+fuels[fuel_id].id+")]");
						}else{
							col_fuel[ind].Items.Add("Пист."+i+" [откл]");
						}
					}else{
						col_fuel[ind].Items.Add("Пист."+i+" [откл]");
					}
				}

				col_fuel[ind].Enabled = true;
			}
		}

		private void change_tap(object sender,EventArgs e){
			ComboBox curr_cb = (ComboBox)sender;
			Int32 ind = (Int32)validator.extract_uint_from_end(curr_cb.Name);

			if(curr_cb.SelectedIndex != 0){
				Int32 pump_ind = col_pump[ind].SelectedIndex-1;
				UInt16 cont_id = 0;
				switch(curr_cb.SelectedIndex){
					case 1:
						cont_id = pumps[pump_ind].tap_1;
						break;
					case 2:
						cont_id = pumps[pump_ind].tap_2;
						break;
					case 3:
						cont_id = pumps[pump_ind].tap_3;
						break;
					case 4:
						cont_id = pumps[pump_ind].tap_4;
						break;
					case 5:
						cont_id = pumps[pump_ind].tap_5;
						break;
					case 6:
						cont_id = pumps[pump_ind].tap_6;
						break;
				}
				if(cont_id != 0){
					UInt64 fuel_id = containers[cont_id].fuel_id;
					if(fuel_id != 0){
						Double cost = (double)fuels[fuel_id].cost/100.0;
						Double vol = validator.extract_uint_from_start(col_volume_need[ind].Text);
						data_not_exec_strings[ind].cost = fuels[fuel_id].cost.ToString();
						col_res_cost[ind].Text = cost + "/" + (cost*vol);
						col_but_exec[ind].Enabled = true;
					}else{
						data_not_exec_strings[ind].cost = "0";
						col_res_cost[ind].Text = "0/0";
						col_but_exec[ind].Enabled = false;
					}
				}else{
					data_not_exec_strings[ind].cost = "0";
					col_res_cost[ind].Text = "0/0";
					col_but_exec[ind].Enabled = false;
				}
			}else{
				data_not_exec_strings[ind].cost = "0";
				col_res_cost[ind].Text = "0/0";
				col_but_exec[ind].Enabled = false;
			}
		}

		private void change_cost(object sender,EventArgs e){
			Int32 ind = (Int32)validator.extract_uint_from_end(((TextBox)sender).Name);
			Double cost = Convert.ToDouble(data_not_exec_strings[ind].cost)/100.0;
			Double vol = validator.extract_uint_from_start(col_volume_need[ind].Text);
			col_res_cost[ind].Text = cost + "/" + (cost*vol);
		}

		private void execute_trans(object sender,EventArgs e){
			Button curr_but = (Button)sender;
			curr_but.Enabled = false;
			Int32 ind = (Int32)validator.extract_uint_from_end(curr_but.Name);
			if(data_not_exec_strings[ind].id == "0"){
				//запуск транзакции


				Int32 pump_ind = col_pump[ind].SelectedIndex-1;
				UInt16 pump_id = 0;
				UInt16 cont_id = 0;
				UInt64 fuel_id = 0;
				if(pump_ind < 0){
					MessageBox.Show("Не выбрана колонка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}
				pump_id = pumps[pump_ind].id;
				switch(col_fuel[ind].SelectedIndex){
					case 1:
						cont_id = pumps[pump_ind].tap_1;
						break;
					case 2:
						cont_id = pumps[pump_ind].tap_2;
						break;
					case 3:
						cont_id = pumps[pump_ind].tap_3;
						break;
					case 4:
						cont_id = pumps[pump_ind].tap_4;
						break;
					case 5:
						cont_id = pumps[pump_ind].tap_5;
						break;
					case 6:
						cont_id = pumps[pump_ind].tap_6;
						break;
				}
				if(cont_id != 0){
					fuel_id = containers[cont_id].fuel_id;
					if(fuel_id == 0){
						MessageBox.Show("Контейнер пуст!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
						curr_but.Enabled = true;
						return;
					}
				}else{
					MessageBox.Show("Не выбран пистолет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				Int64 volume_need = validator.extract_uint_from_start(col_volume_need[ind].Text);
				if(0 >= volume_need){
					MessageBox.Show("Указан нулевой объем", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				MySqlDataReader result = null;
				//заблочим таблицы `fuel_transactions`, `containers`, `fuel_types`
				MySqlCommand query = new MySqlCommand("LOCK TABLES `fuel_transactions` WRITE, `containers` WRITE, `fuel_types` READ", connect);
				try{
					query.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				Int64 volume_exist = 0;

				//Получим остаток топлива в контейнере
				query.CommandText = "SELECT `fuel_volume` FROM `containers` WHERE `id` = @cont_id";
				query.Parameters.AddWithValue("@cont_id", cont_id);
				try{
					volume_exist = Convert.ToInt64(query.ExecuteScalar());
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				//топлива в контейнере меньше чем нужно
				if(volume_exist < volume_need){
					MessageBox.Show("Не хватает топлива: "+volume_exist+" л./"+volume_need+" л. (есть/нужно)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					query = new MySqlCommand("UNLOCK TABLES", connect);
					try{
						query.ExecuteNonQuery();
					}catch(Exception ex){
						MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
						curr_but.Enabled = true;
						return;
					}
					curr_but.Enabled = true;
					return;
				}

				MySqlTransaction transaction = connect.BeginTransaction();
				//обновим количество топлива в контейнере
				MySqlCommand update_cont = new MySqlCommand("UPDATE `containers` SET `fuel_volume` = `fuel_volume` - @volume_need WHERE `id` = @cont_id", connect);
				update_cont.Parameters.AddWithValue("@cont_id", cont_id);
				update_cont.Parameters.AddWithValue("@volume_need", volume_need);
				update_cont.Transaction = transaction;
				update_cont.CommandTimeout = 30;

				//запишем транзакцию
				MySqlCommand insert_trans = new MySqlCommand(
					"INSERT INTO `fuel_transactions` (`responsible_for`, `fuel_id`, `container_id`, `pump_id`, `fuel_volume`, `cost`) VALUES (@slave_id, @fuel_id, @cont_id, @pump_id, -(@volume), (SELECT @volume * `cost_sale` FROM `fuel_types` WHERE `id` = @fuel_id))",
					connect
				);
				insert_trans.Parameters.AddWithValue("@slave_id", slave_id);
				insert_trans.Parameters.AddWithValue("@fuel_id", fuel_id);
				insert_trans.Parameters.AddWithValue("@cont_id", cont_id);
				insert_trans.Parameters.AddWithValue("@pump_id", pump_id);
				insert_trans.Parameters.AddWithValue("@volume", volume_need);
				insert_trans.Transaction = transaction;
				insert_trans.CommandTimeout = 120;

				//получим id вставки
				MySqlCommand get_insert_ind = new MySqlCommand("SELECT LAST_INSERT_ID()", connect);
				get_insert_ind.Transaction = transaction;
				get_insert_ind.CommandTimeout = 30;

				UInt64 inserted_id = 0;

				col_pump[ind].Enabled = false;
				col_fuel[ind].Enabled = false;
				col_volume_need[ind].ReadOnly = true;

				try{
					update_cont.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show("Произошла ошибка, изменения откатились."+Environment.NewLine+ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					transaction.Rollback();
					curr_but.Enabled = true;
					return;
				}
				try{
					insert_trans.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show("Произошла ошибка, изменения откатились."+Environment.NewLine+ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					transaction.Rollback();
					curr_but.Enabled = true;
					return;
				}
				try{
					inserted_id = Convert.ToUInt64(get_insert_ind.ExecuteScalar());
				}catch(Exception ex){
					MessageBox.Show("Произошла ошибка, изменения откатились."+Environment.NewLine+ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					transaction.Rollback();
					curr_but.Enabled = true;
					return;
				}
				try{
					transaction.Commit();
				}catch(Exception ex){
					MessageBox.Show("Произошла ошибка, изменения откатились."+Environment.NewLine+ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					transaction.Rollback();
					curr_but.Enabled = true;
					return;
				}

				data_not_exec_strings[ind].id = inserted_id.ToString();
				DateTime start;
				//получим время старта транзакции
				query = new MySqlCommand("SELECT `time_start` FROM `fuel_transactions` WHERE `id` = @ins_id", connect);
				query.Parameters.AddWithValue("@ins_id", inserted_id);
				try{
					start = (DateTime)query.ExecuteScalar();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}
				col_time_start[ind].Text = start.ToString("HH:mm:ss");

				//разблокируем таблицы
				query = new MySqlCommand("UNLOCK TABLES", connect);
				try{
					query.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}
				curr_but.Text = "<";
				curr_but.Enabled = true;

			}else{
				//остановка транзакции

				//заблочим таблицы `fuel_transactions`
				MySqlCommand query = new MySqlCommand("LOCK TABLES `fuel_transactions` WRITE", connect);
				try{
					query.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				//установим время конца транзакции
				query.CommandText = "UPDATE `fuel_transactions` SET `time_end` = NOW() WHERE `id` = @trans_id";
				query.Parameters.AddWithValue("@trans_id", data_not_exec_strings[ind].id);
				try{
					query.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

				curr_but.Enabled = false;

				DateTime end;
				//получим время конца транзакции
				query.CommandText = "SELECT `time_end` FROM `fuel_transactions` WHERE `id` = @trans_id";
				try{
					end = (DateTime)query.ExecuteScalar();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}
				col_time_end[ind].Text = end.ToString("HH:mm:ss");


				//разблокируем таблицы
				query = new MySqlCommand("UNLOCK TABLES", connect);
				try{
					query.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					curr_but.Enabled = true;
					return;
				}

			}

		}

		private void reload_data_with_save(Object sender, EventArgs e){
			if(true){//TODO
				DialogResult ret = MessageBox.Show("Имеются несохраненные данные. Произвести обновление?", "Несохраненные данные", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				if(ret == DialogResult.No){
					return;
				}
			}
			reload_data();
		}

		private void enterkey_to_update(Object sender, KeyEventArgs e){
			if(e.KeyCode == Keys.Enter){
				reload_data_with_save(sender, e);
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
				reload_data_with_save(sender, e);
			}
		}

///////////////////////////////////////////////////////////////////////////////
//		объекты форм
///////////////////////////////////////////////////////////////////////////////

		public System.Windows.Forms.TableLayoutPanel trans_table;
			private System.Windows.Forms.Label label_pump;
			private System.Windows.Forms.Label label_tap;
			private System.Windows.Forms.Label label_need_vol;
			private System.Windows.Forms.Label label_cost;
			private System.Windows.Forms.Label label_time_start;
			private System.Windows.Forms.Label label_time_end;
	}
}
