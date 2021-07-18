using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Model_shifts_manager;


namespace Model_shifts_manager{

	class slave{
		public UInt64 id;
		public string type;
	}

	class view_data{
		public view_data(){
			slaves_id = new List<UInt64>();
		}
		public UInt64 id;
		public DateTime time_start;
		public DateTime time_end;
		public List<UInt64> slaves_id;
	}
}

namespace projectLab {
	class Model_shifts_manager:IDisposable{

		private table_instruments instruments;
		private MySqlConnection connect;
		
		private UInt32 str_per_page;
		private UInt32 current_page;
		private UInt32 max_page;
		private UInt32 str_count;
		private string order_by;

		private Dictionary<UInt64, slave> slaves;


		private List<TextBox> col_id;
		private List<DateTimePicker> col_time_start;
		private List<DateTimePicker> col_time_end;
		private List<FlowLayoutPanel> col_container;
		private List<List<ComboBox>> col_slaves;
		private List<List<UInt64>> col_used_slaves;
		private List<Button> col_but_add;

		public Model_shifts_manager(ref table_instruments instrument_panel, ref MySqlConnection current_connect){
			instruments = instrument_panel;
			connect = current_connect;
			
			create_head_table();

			str_per_page = 5;
			current_page = 1;
			max_page = 1;
			str_count = 0;
			order_by = "ORDER BY `id` ASC ";

			slaves = new Dictionary<UInt64, slave>();

			col_id			= new List<TextBox>();
			col_time_start	= new List<DateTimePicker>();
			col_time_end	= new List<DateTimePicker>();
			col_container	= new List<FlowLayoutPanel>();
			col_slaves		= new List<List<ComboBox>>();
			col_used_slaves	= new List<List<UInt64>>();
			col_but_add		= new List<Button>();

			instruments.inst_str_count_inp.Text = str_per_page.ToString();
			instruments.inst_curr_page_inp.Text = current_page.ToString();

			instruments.inst_new_row.Click += this.new_str;
			instruments.inst_reload.Click += this.reload_data_with_save;
			instruments.inst_save.Click += this.save_changes;

			instruments.inst_str_count_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_str_count_inp.KeyDown += this.enterkey_to_update;

			instruments.inst_left.Click += this.page_shift;
			instruments.inst_curr_page_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_curr_page_inp.KeyDown += this.enterkey_to_update;
			instruments.inst_right.Click += this.page_shift;

			reload_data();
		}


		public void Dispose(){
		shifts_table.Dispose();
			container_label_id.Dispose();
				sort_label_id.Dispose();
				checker_sort_id.Dispose();
			container_label_time_start.Dispose();
				label_sort_time_start.Dispose();
				checker_sort_time_start.Dispose();
			label_time_end.Dispose();
			label_slaves.Dispose();
		}

		//создает шапку таблицы
		private void create_head_table(){
			this.shifts_table = new System.Windows.Forms.TableLayoutPanel();
				this.container_label_id = new System.Windows.Forms.Panel();
					this.sort_label_id = new System.Windows.Forms.LinkLabel();
					this.checker_sort_id = new System.Windows.Forms.Label();
				this.container_label_time_start = new System.Windows.Forms.Panel();
					this.label_sort_time_start = new System.Windows.Forms.LinkLabel();
					this.checker_sort_time_start = new System.Windows.Forms.Label();
				this.label_time_end = new System.Windows.Forms.Label();
				this.label_slaves = new System.Windows.Forms.Label();
			
			this.container_label_id.SuspendLayout();
			this.container_label_time_start.SuspendLayout();
			this.shifts_table.SuspendLayout();

			// 
			// shifts_table
			// 
			this.shifts_table.Dock = System.Windows.Forms.DockStyle.Top;
			this.shifts_table.AutoSize = true;
			this.shifts_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.shifts_table.ColumnCount = 4;
			this.shifts_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.shifts_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.shifts_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.shifts_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.shifts_table.Controls.Add(this.container_label_id, 0, 0);
			this.shifts_table.Controls.Add(this.container_label_time_start, 1, 0);
			this.shifts_table.Controls.Add(this.label_time_end, 2, 0);
			this.shifts_table.Controls.Add(this.label_slaves, 3, 0);
			this.shifts_table.Location = new System.Drawing.Point(0, 0);
			this.shifts_table.Name = "shifts_table";
			this.shifts_table.RowCount = 1;
			this.shifts_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.shifts_table.TabIndex = 0;
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
			//this.checker_sort_id.TabIndex = 1;
			this.checker_sort_id.Text = "6";
			this.checker_sort_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_time_start
			// 
			this.container_label_time_start.Controls.Add(this.checker_sort_time_start);
			this.container_label_time_start.Controls.Add(this.label_sort_time_start);
			this.container_label_time_start.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_time_start.Location = new System.Drawing.Point(54, 2);
			this.container_label_time_start.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_time_start.Name = "container_label_time_start";
			this.container_label_time_start.Size = new System.Drawing.Size(144, 20);
			//this.container_label_name.TabIndex = 6;
			// 
			// label_sort_time_start
			// 
			this.label_sort_time_start.ActiveLinkColor = System.Drawing.Color.Blue;
			this.label_sort_time_start.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_sort_time_start.AutoSize = true;
			this.label_sort_time_start.LinkColor = System.Drawing.Color.DarkBlue;
			this.label_sort_time_start.Location = new System.Drawing.Point(50, 4);
			this.label_sort_time_start.Name = "label_sort_time_start";
			this.label_sort_time_start.Size = new System.Drawing.Size(34, 13);
			this.label_sort_time_start.TabIndex = 2;
			this.label_sort_time_start.TabStop = true;
			this.label_sort_time_start.Text = "Время начала";
			this.label_sort_time_start.Click += this.sort_str;
			// 
			// checker_sort_time_start
			// 
			this.checker_sort_time_start.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_time_start.AutoSize = true;
			this.checker_sort_time_start.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
			this.checker_sort_time_start.Location = new System.Drawing.Point(126, 2);
			this.checker_sort_time_start.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_time_start.Name = "checker_sort_time_start";
			this.checker_sort_time_start.Size = new System.Drawing.Size(19, 17);
			this.checker_sort_time_start.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// 
			// label_slaves
			// 
			this.label_slaves.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_slaves.AutoSize = true;
			this.label_slaves.Location = new System.Drawing.Point(5, 5);
			this.label_slaves.Margin = new System.Windows.Forms.Padding(2);
			this.label_slaves.Name = "label_slaves";
			this.label_slaves.Text = "Сотрудники";
			this.label_slaves.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

			
			this.container_label_id.ResumeLayout(false);
			this.container_label_id.PerformLayout();
			this.container_label_time_start.ResumeLayout(false);
			this.container_label_time_start.PerformLayout();
			this.shifts_table.ResumeLayout(false);
			this.shifts_table.PerformLayout();
		}

		//создает данные таблицы
		private void create_body_table(Dictionary<UInt64, view_data> data){
			//очищаем таблицу
			shifts_table.SuspendLayout();
			while(shifts_table.RowCount > 1){
				int row = shifts_table.RowCount - 1;
				for(Int32 a = 0; a < shifts_table.ColumnCount; a++){
					Control c = shifts_table.GetControlFromPosition(a, row);
					shifts_table.Controls.Remove(c);
					c.Dispose();
				}

				//solvers_table.RowStyles.RemoveAt(row);
				shifts_table.RowCount--;
			}
			col_id.Clear();
			col_time_start.Clear();
			col_time_end.Clear();
			col_container.Clear();
			col_slaves.Clear();
			col_used_slaves.Clear();
			col_but_add.Clear();

			//собираем таблицу с данными
			shifts_table.RowCount += data.Count;
			Int32 i=0;
			foreach(UInt64 key in data.Keys){
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
				col_id[i].Text = data[key].id.ToString();
				col_id[i].ReadOnly = true;
				col_id[i].Size = new System.Drawing.Size(44, 13);
				col_id[i].TabStop = false;
				shifts_table.Controls.Add(col_id[i], 0, i+1);
				// 
				// dt_time_start
				// 
				col_time_start.Add(new DateTimePicker());
				col_time_start[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_time_start[i].Location = new System.Drawing.Point(203, 33);
				col_time_start[i].Name = "dt_time_start_"+i;
				col_time_start[i].Format = System.Windows.Forms.DateTimePickerFormat.Custom;
				col_time_start[i].CustomFormat = "dd.MM.yyyy HH:mm";
				col_time_start[i].MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
				col_time_start[i].Value = data[key].time_start;
				col_time_start[i].Size = new System.Drawing.Size(138, 21);
				col_time_start[i].TabIndex = i*3 + 3;
				col_time_start[i].ValueChanged += set_unsave_dt;
				shifts_table.Controls.Add(col_time_start[i], 1, i+1);
				// 
				// dt_time_end
				// 
				col_time_end.Add(new DateTimePicker());
				col_time_end[i].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_time_end[i].Location = new System.Drawing.Point(203, 33);
				col_time_end[i].Name = "dt_time_end_"+i;
				col_time_end[i].Format = System.Windows.Forms.DateTimePickerFormat.Custom;
				col_time_end[i].CustomFormat = "dd.MM.yyyy HH:mm";
				col_time_end[i].MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
				col_time_end[i].Value = data[key].time_end;
				col_time_end[i].Size = new System.Drawing.Size(138, 21);
				col_time_end[i].TabIndex = i*3 + 4;
				col_time_end[i].ValueChanged += set_unsave_dt;
				shifts_table.Controls.Add(col_time_end[i], 2, i+1);
				// 
				// box_slaves
				// 
				col_container.Add(new FlowLayoutPanel());
				col_container[i].SuspendLayout();
				col_container[i].AutoSize = true;
				col_container[i].Dock = System.Windows.Forms.DockStyle.Fill;
				col_container[i].FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
				col_container[i].Location = new System.Drawing.Point(0, 0);
				col_container[i].Name = "box_slaves_"+i;
				//col_container[i].Size = new System.Drawing.Size(266, 191);
				col_container[i].TabIndex = i*3 + 5;
				shifts_table.Controls.Add(col_container[i], 3, i+1);
				// 
				// but_exec
				// 
				col_but_add.Add(new Button());
				col_but_add[i].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_but_add[i].Cursor = System.Windows.Forms.Cursors.Hand;
				col_but_add[i].FlatStyle = System.Windows.Forms.FlatStyle.System;
				//col_but_add[i].Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
				col_but_add[i].Location = new System.Drawing.Point(548, 32);
				col_but_add[i].Name = "col_but_add_"+i;
				col_but_add[i].AutoSize = true;
				//col_but_add[i].Size = new System.Drawing.Size(26, 24);
				col_but_add[i].TabIndex = 0;
				col_but_add[i].Text = "+";
				col_but_add[i].UseVisualStyleBackColor = true;
				col_but_add[i].Click += add_slave;
				col_container[i].Controls.Add(col_but_add[i]);

				//заполняем ячейку с комбобоксами сотрудников
				col_slaves.Add(new List<ComboBox>());
				col_used_slaves.Add(new List<UInt64>());

				for(Int32 i1=0; i1<data[key].slaves_id.Count; i1++){
					col_used_slaves[i].Add(data[key].slaves_id[i1]);
					// 
					// cb_slave
					// 
					col_slaves[i].Add(new ComboBox());
					col_slaves[i][i1].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
					col_slaves[i][i1].FormattingEnabled = true;
					col_slaves[i][i1].Location = new System.Drawing.Point(203, 33);
					//col_slaves[i][i1].MaxDropDownItems = 50;
					col_slaves[i][i1].Name = "cb_slave_"+i+"_"+i1;
					col_slaves[i][i1].Items.Add("---");
					//добавим items сотрудников
					Int32 curr_slave=0, i2=0;
					foreach(UInt64 slave_id in slaves.Keys){
						col_slaves[i][i1].Items.Add(slaves[slave_id].type);
						if(data[key].slaves_id[i1] == slave_id)
							curr_slave = i2+1;
						i2++;
					}
					col_slaves[i][i1].SelectedIndex = curr_slave;
					col_slaves[i][i1].SelectedIndexChanged += set_unsave_cb;
					col_slaves[i][i1].SelectedIndexChanged += change_slave;
					col_slaves[i][i1].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					col_slaves[i][i1].Size = new System.Drawing.Size(138, 21);
					col_slaves[i][i1].AutoSize = true;
					col_slaves[i][i1].TabIndex = i1+1;
					col_container[i].Controls.Add(col_slaves[i][i1]);
				}

				col_container[i].ResumeLayout(false);
				col_container[i].PerformLayout();

				i++;
			}

			shifts_table.ResumeLayout(false);
			shifts_table.PerformLayout();
		}



		//Генерирует список комманд для обновления
		private List<MySqlCommand> get_updates(){
			List<MySqlCommand> ret = new List<MySqlCommand>();
			MySqlCommand tmp;
			
			for(Int32 i=0; i<col_id.Count; i++){
				UInt64 shift_id = validator.extract_uint_from_start(col_id[i].Text);
				bool add_link = false;
				//добавление новых строк в таблицу
				if(col_id[i].Text == "*"){
					//если она добавлена но удалена, пропускаем
					if(col_id[i].Font.Strikeout)
						continue;
					tmp = new MySqlCommand("INSERT INTO `shifts` (`time_start`, `time_end`) VALUES (@time_start, @time_end)", connect);
					tmp.Parameters.AddWithValue("@time_start", col_time_start[i].Value);
					tmp.Parameters.AddWithValue("@time_end", col_time_end[i].Value);
					ret.Add(tmp);

					
					//вставим связи смен с сотрудниками
					tmp = new MySqlCommand("INSERT INTO `slaves_shifts`(`shift_id`, `slave_id`) VALUES ", connect);
					for(Int32 ind=0; ind<col_used_slaves[i].Count; ind++){
						if(col_used_slaves[i][ind] != 0){
							add_link = true;
							tmp.CommandText += "(LAST_INSERT_ID(), "+col_used_slaves[i][ind]+"),";
						}
					}
					if(add_link){
						tmp.CommandText = tmp.CommandText.Substring(0, tmp.CommandText.Length-1);
						ret.Add(tmp);
					}

				//удаление строк из таблицы
				}else if(col_id[i].Font.Strikeout){
					tmp = new MySqlCommand("DELETE FROM `shifts` WHERE `id` = @id", connect);
					tmp.Parameters.AddWithValue("@id", shift_id);
					ret.Add(tmp);
					tmp = new MySqlCommand("DELETE FROM `slaves_shifts` WHERE `shift_id` = @id", connect);
					tmp.Parameters.AddWithValue("@id", shift_id);
					ret.Add(tmp);

				//обновление существующих строк
				}else if(col_id[i].Text.Substring(col_id[i].Text.Length-1) == "*"){
					tmp = new MySqlCommand("UPDATE `shifts` SET `time_start` = @time_start, `time_end` = @time_end WHERE `id` = @id", connect);
					tmp.Parameters.AddWithValue("@time_start", col_time_start[i].Value);
					tmp.Parameters.AddWithValue("@time_end", col_time_end[i].Value);
					tmp.Parameters.AddWithValue("@id", shift_id);
					ret.Add(tmp);
					
					//обновим связи смен с сотрудниками
					tmp = new MySqlCommand("DELETE FROM `slaves_shifts` WHERE `shift_id` = @id", connect);
					tmp.Parameters.AddWithValue("@id", shift_id);
					ret.Add(tmp);

					tmp = new MySqlCommand("INSERT INTO `slaves_shifts`(`shift_id`, `slave_id`) VALUES ", connect);
					for(Int32 ind=0; ind<col_used_slaves[i].Count; ind++){
						if(col_used_slaves[i][ind] != 0){
							add_link = true;
							tmp.CommandText += "("+shift_id+", "+col_used_slaves[i][ind]+"),";
						}
					}
					if(add_link){
						tmp.CommandText = tmp.CommandText.Substring(0, tmp.CommandText.Length-1);
						ret.Add(tmp);
					}
				}
			}


			return ret;
		}

		//выполняет список команд
		private bool commit(List<MySqlCommand> commands){
			MySqlTransaction transaction = connect.BeginTransaction();

			for(Int32 i=0; i<commands.Count; i++){
				commands[i].Transaction = transaction;
				commands[i].CommandTimeout = 5;
			}
			for(Int32 i=0; i<commands.Count; i++){
				try{
					commands[i].ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					if(MessageBox.Show("При сохранении изменений произошла ошибка, откатить внесенные изменения?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes){
						transaction.Rollback();
						return false;
					}
				}
			}
			try{
				transaction.Commit();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				MessageBox.Show("При сохранении изменений произошла ошибка, изменения откатились.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				transaction.Rollback();
				return false;
			}
			
			return true;
		}

		
		//перезагружает данные в таблицу
		private void reload_data(){

			//TODO LOCK TABLES
			//получим кол-во сотрудников
			MySqlCommand query = new MySqlCommand("SELECT COUNT(*) FROM `shifts`", connect);
			try{
				str_count = Convert.ToUInt32(query.ExecuteScalar());
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//соберем пагинацию
			str_per_page = instruments.inst_str_count_inp.Text == "" ? 0 : UInt32.Parse(instruments.inst_str_count_inp.Text);
			if(str_per_page < 1){
				str_per_page = 1;
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

			slaves.Clear();
			MySqlDataReader result = null;

			//подготовим запрос на выборку
			query.CommandText = "SELECT `id`, `time_start`, `time_end` FROM `shifts` "+order_by+"LIMIT @limit OFFSET @offset";
			query.Parameters.AddWithValue("@limit", str_per_page);
			query.Parameters.AddWithValue("@offset", (current_page-1)*str_per_page);
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			List<UInt64> shifts_id = new List<UInt64>();
			Dictionary<UInt64, view_data> data = new Dictionary<UInt64, view_data>();

			while(result.Read()){
				view_data tmp = new view_data();
				tmp.id = Convert.ToUInt64(result.GetValue(0));	//`shifts`.`id`
				tmp.time_start = (DateTime)result.GetValue(1);	//`shifts`.`time_start`
				tmp.time_end = (DateTime)result.GetValue(2);	//`shifts`.`time_end`
				shifts_id.Add(tmp.id);
				data[tmp.id] = tmp;
			}
			result.Close();

			//Получим связь смен с сотрудниками
			query = new MySqlCommand("SELECT `shift_id`, `slave_id` FROM `slaves_shifts` WHERE `shift_id` IN("+String.Join(", ", shifts_id)+")", connect);
			shifts_id = null;
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			UInt64 shift_id, slave_id;
			while(result.Read()){
				shift_id = Convert.ToUInt64(result.GetValue(0));	//`slaves_shifts`.`shift_id`
				slave_id = Convert.ToUInt64(result.GetValue(1));	//`slaves_shifts`.`slave_id`
				data[shift_id].slaves_id.Add(slave_id);
			}
			result.Close();

			//получим сотрудников
			query.CommandText = "SELECT `id`, `fio`, `rank` FROM `slaves`";
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			while(result.Read()){
				slave_id = Convert.ToUInt64(result.GetValue(0));				 //`slaves`.`id`
				slaves[slave_id] = new slave();
				slaves[slave_id].type = "#"+slave_id
								 + " " + Convert.ToString(result.GetValue(1))	 //`slaves`.`fio`
								 + " - " + Convert.ToString(result.GetValue(2));//`slaves`.`rank`
				slaves[slave_id].id = slave_id;

			}
			result.Close();

			create_body_table(data);
		}

///////////////////////////////////////////////////////////////////////////////
//		обработчики событий
///////////////////////////////////////////////////////////////////////////////

		private void new_str(Object sender, EventArgs e){
			int ind = shifts_table.RowCount-1;
			shifts_table.SuspendLayout();
			shifts_table.RowCount += 1;

			// 
			// inp_id
			// 
			col_id.Add(new TextBox());
			col_id[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_id[ind].BackColor = System.Drawing.SystemColors.Control;
			col_id[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_id[ind].Location = new System.Drawing.Point(5, 37);
			col_id[ind].MaxLength = 20;
			col_id[ind].Name = "inp_id_"+ind;
			col_id[ind].Text = "*";
			col_id[ind].ReadOnly = true;
			col_id[ind].Size = new System.Drawing.Size(44, 13);
			col_id[ind].TabStop = false;
			shifts_table.Controls.Add(col_id[ind], 0, ind+1);
			// 
			// dt_time_start
			// 
			col_time_start.Add(new DateTimePicker());
			col_time_start[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_time_start[ind].Location = new System.Drawing.Point(203, 33);
			col_time_start[ind].Name = "dt_time_start_"+ind;
			col_time_start[ind].Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			col_time_start[ind].CustomFormat = "dd.MM.yyyy HH:mm";
			col_time_start[ind].MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
			col_time_start[ind].Value = DateTime.Now;
			col_time_start[ind].Size = new System.Drawing.Size(138, 21);
			col_time_start[ind].TabIndex = ind*3 + 3;
			col_time_start[ind].ValueChanged += set_unsave_dt;
			shifts_table.Controls.Add(col_time_start[ind], 1, ind+1);
			// 
			// dt_time_end
			// 
			col_time_end.Add(new DateTimePicker());
			col_time_end[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_time_end[ind].Location = new System.Drawing.Point(203, 33);
			col_time_end[ind].Name = "dt_time_end_"+ind;
			col_time_end[ind].Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			col_time_end[ind].CustomFormat = "dd.MM.yyyy HH:mm";
			col_time_end[ind].MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
			col_time_end[ind].Value = DateTime.Now;
			col_time_end[ind].Size = new System.Drawing.Size(138, 21);
			col_time_end[ind].TabIndex = ind*3 + 4;
			col_time_end[ind].ValueChanged += set_unsave_dt;
			shifts_table.Controls.Add(col_time_end[ind], 2, ind+1);
			// 
			// box_slaves
			// 
			col_container.Add(new FlowLayoutPanel());
			col_container[ind].SuspendLayout();
			col_container[ind].AutoSize = true;
			col_container[ind].Dock = System.Windows.Forms.DockStyle.Fill;
			col_container[ind].FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			col_container[ind].Location = new System.Drawing.Point(0, 0);
			col_container[ind].Name = "box_slaves_"+ind;
			//col_container[i].Size = new System.Drawing.Size(266, 191);
			col_container[ind].TabIndex = ind*3 + 5;
			shifts_table.Controls.Add(col_container[ind], 3, ind+1);

			//заполняем ячейку с комбобоксами сотрудников
			col_slaves.Add(new List<ComboBox>());
			col_used_slaves.Add(new List<UInt64>());
			// 
			// but_exec
			// 
			col_but_add.Add(new Button());
			col_but_add[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_but_add[ind].Cursor = System.Windows.Forms.Cursors.Hand;
			col_but_add[ind].FlatStyle = System.Windows.Forms.FlatStyle.System;
			col_but_add[ind].Location = new System.Drawing.Point(548, 32);
			col_but_add[ind].Name = "col_but_add_"+ind;
			col_but_add[ind].AutoSize = true;
			col_but_add[ind].Text = "+";
			col_but_add[ind].UseVisualStyleBackColor = true;
			col_but_add[ind].TabIndex = 0;
			col_but_add[ind].Click += add_slave;
			col_container[ind].Controls.Add(col_but_add[ind]);

			col_container[ind].ResumeLayout(false);
			col_container[ind].PerformLayout();

			
			shifts_table.ResumeLayout(false);
			shifts_table.PerformLayout();
		}

		private void add_slave(object sender,EventArgs e) {
			Int32 ind = (Int32)validator.extract_uint_from_end(((Button)sender).Name);

			col_used_slaves[ind].Add(0);
			// 
			// cb_slave
			// 
			Int32 end = col_slaves[ind].Count;
			col_slaves[ind].Add(new ComboBox());
			col_slaves[ind][end].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_slaves[ind][end].FormattingEnabled = true;
			col_slaves[ind][end].Location = new System.Drawing.Point(203, 33);
			col_slaves[ind][end].Name = "cb_slave_"+ind+"_"+end;
			col_slaves[ind][end].Items.Add("---");
			//добавим items сотрудников
			foreach(UInt64 slave_id in slaves.Keys){
				col_slaves[ind][end].Items.Add(slaves[slave_id].type);
			}
			col_slaves[ind][end].SelectedIndex = 0;
			col_slaves[ind][end].SelectedIndexChanged += set_unsave_cb;
			col_slaves[ind][end].SelectedIndexChanged += change_slave;
			col_slaves[ind][end].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			col_slaves[ind][end].Size = new System.Drawing.Size(138, 21);
			col_slaves[ind][end].AutoSize = true;
			col_slaves[ind][end].TabIndex = end+1;
			col_container[ind].Controls.Add(col_slaves[ind][end]);
		}

		private void change_slave(object sender,EventArgs e) {
			Int32 ind = (Int32)validator.extract_uint_from_end(((ComboBox)sender).Name);
			Int32 box = (Int32)validator.extract_uint_from_start(((ComboBox)sender).Name.Substring(9));

			if(col_slaves[box][ind].SelectedIndex == 0){
				col_used_slaves[box][ind] = 0;
			}else{
				UInt64 slave_id = slaves.ElementAt(col_slaves[box][ind].SelectedIndex - 1).Key;
				col_used_slaves[box][ind] = slave_id;
				for(Int32 i=0; i<col_slaves[box].Count; i++){
					if(i == ind)
						continue;
					if(col_used_slaves[box][ind] == col_used_slaves[box][i]){
						col_used_slaves[box][i] = 0;
						col_slaves[box][i].SelectedIndex = 0;
					}
				}
			}
		}

		private void reload_data_with_save(Object sender, EventArgs e){
			List<MySqlCommand> commands = get_updates();
			if(commands.Count > 0){
				DialogResult ret = MessageBox.Show("Имеются несохраненные данные. Произвести обновление?", "Несохраненные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				if(ret == DialogResult.Yes){
					commit(commands);
				}else if(ret == DialogResult.Cancel){
					return;
				}
			}
			reload_data();
		}

		private void save_changes(Object sender, EventArgs e){
			commit(get_updates());
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
						checker_sort_time_start.Text = "";
					}
					break;
				case "label_sort_time_start":
					//была включена сортировка ASC
					if(checker_sort_time_start.Text == "6"){
						checker_sort_time_start.Text = "5";
						order_by = "ORDER BY `time_start` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_time_start.Text == "5"){
						checker_sort_time_start.Text = "6";
						order_by = "ORDER BY `time_start` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_time_start.Text = "6";
						order_by = "ORDER BY `time_start` ASC ";
						checker_sort_id.Text = "";
					}
					break;
			}
			instruments.inst_curr_page_inp.Text = "1";
			reload_data_with_save(sender, e);
		}

		private void set_unsave_cb(Object sender,EventArgs e){
			int ind = (Int32)validator.extract_uint_from_start(((ComboBox)sender).Name.Substring(9));
			string str = col_id[ind].Text;
			if(str.Substring(str.Length-1) != "*")
				col_id[ind].Text += "*";
		}

		private void set_unsave_dt(Object sender,EventArgs e){
			int ind = (Int32)validator.extract_uint_from_end(((DateTimePicker)sender).Name);
			string str = col_id[ind].Text;
			if(str.Substring(str.Length-1) != "*")
				col_id[ind].Text += "*";
		}

///////////////////////////////////////////////////////////////////////////////
//		объекты форм
///////////////////////////////////////////////////////////////////////////////

		public System.Windows.Forms.TableLayoutPanel shifts_table;
			private System.Windows.Forms.Panel container_label_id;
				private System.Windows.Forms.LinkLabel sort_label_id;
				private System.Windows.Forms.Label checker_sort_id;
			private System.Windows.Forms.Panel container_label_time_start;
				private System.Windows.Forms.LinkLabel label_sort_time_start;
				private System.Windows.Forms.Label checker_sort_time_start;
			private System.Windows.Forms.Label label_time_end;
			private System.Windows.Forms.Label label_slaves;
	}
}
