using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace projectLab {
	class solvers_table_data :IDisposable{

		private table_instruments instruments;
		private MySqlConnection connect;

		//private MySqlCommand get_count_users;

		private UInt32 str_per_page;
		private UInt32 current_page;
		private UInt32 max_page;
		private UInt32 str_count;
		private string order_by;

		private Dictionary<int, string> departaments;
		private List<int> departs_id;

		private List<TextBox> col_id;
		private List<TextBox> col_name;
		private List<ComboBox> col_departament;
		private List<TextBox> col_data;
		private List<Button> col_but_del;

		public solvers_table_data(ref table_instruments instrument_panel, ref MySqlConnection current_connect){
			instruments = instrument_panel;
			connect = current_connect;
			
			create_head_table();

			if(!test_tables_exist()){
				MessageBox.Show("В базе отсутствуют таблица `solvers` или `departament` или их структура не верна."+Environment.NewLine+"Дальнейшая работа с сотрудниками невозможна.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//get_count_users = new MySqlCommand("SELECT COUNT(*) FROM `solvers`", connect);

			str_per_page = 25;
			current_page = 1;
			max_page = 1;
			str_count = 0;
			order_by = "ORDER BY `id` ASC ";
			departaments = new Dictionary<int,string>();

			col_id = new List<TextBox>();
			col_name = new List<TextBox>();
			col_data = new List<TextBox>();
			col_departament = new List<ComboBox>();
			col_but_del = new List<Button>();
			departs_id = new List<int>();

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
			solvers_table.Dispose();
			container_label_id.Dispose();
			sort_label_id.Dispose();
			checker_sort_id.Dispose();
			container_label_name.Dispose();
			label_sort_name.Dispose();
			checker_sort_name.Dispose();
			container_label_departament.Dispose();
			label_sort_departament.Dispose();
			checker_sort_departament.Dispose();
			container_label_data.Dispose();
			label_data.Dispose();
		}

		//создает шапку таблицы
		private void create_head_table(){
			this.solvers_table = new System.Windows.Forms.TableLayoutPanel();
				this.container_label_id = new System.Windows.Forms.Panel();
					this.sort_label_id = new System.Windows.Forms.LinkLabel();
					this.checker_sort_id = new System.Windows.Forms.Label();
				this.container_label_name = new System.Windows.Forms.Panel();
					this.label_sort_name = new System.Windows.Forms.LinkLabel();
					this.checker_sort_name = new System.Windows.Forms.Label();
				this.container_label_departament = new System.Windows.Forms.Panel();
					this.label_sort_departament = new System.Windows.Forms.LinkLabel();
					this.checker_sort_departament = new System.Windows.Forms.Label();
				this.container_label_data = new System.Windows.Forms.Panel();
					this.label_data = new System.Windows.Forms.Label();
			
			this.container_label_id.SuspendLayout();
			this.container_label_name.SuspendLayout();
			this.container_label_departament.SuspendLayout();
			this.container_label_data.SuspendLayout();
			this.solvers_table.SuspendLayout();

			// 
			// solvers_table
			// 
			this.solvers_table.Dock = System.Windows.Forms.DockStyle.Top;
			//this.solvers_table.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.solvers_table.AutoSize = true;
			this.solvers_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.solvers_table.ColumnCount = 5;
			this.solvers_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.solvers_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.solvers_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.solvers_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.solvers_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.solvers_table.Controls.Add(this.container_label_id, 0, 0);
			this.solvers_table.Controls.Add(this.container_label_name, 1, 0);
			this.solvers_table.Controls.Add(this.container_label_departament, 2, 0);
			this.solvers_table.Controls.Add(this.container_label_data, 3, 0);
			this.solvers_table.Location = new System.Drawing.Point(0, 0);
			this.solvers_table.Name = "solvers_table";
			this.solvers_table.RowCount = 1;
			this.solvers_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			//this.solvers_table.Size = new System.Drawing.Size(584, 66);
			this.solvers_table.TabIndex = 0;
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
			this.checker_sort_id.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.checker_sort_id.Location = new System.Drawing.Point(31, 2);
			this.checker_sort_id.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_id.Name = "checker_sort_id";
			this.checker_sort_id.Size = new System.Drawing.Size(19, 17);
			//this.checker_sort_id.TabIndex = 1;
			this.checker_sort_id.Text = "6";
			this.checker_sort_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_name
			// 
			this.container_label_name.Controls.Add(this.checker_sort_name);
			this.container_label_name.Controls.Add(this.label_sort_name);
			this.container_label_name.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_name.Location = new System.Drawing.Point(54, 2);
			this.container_label_name.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_name.Name = "container_label_name";
			this.container_label_name.Size = new System.Drawing.Size(144, 20);
			//this.container_label_name.TabIndex = 6;
			// 
			// label_sort_name
			// 
			this.label_sort_name.ActiveLinkColor = System.Drawing.Color.Blue;
			this.label_sort_name.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_sort_name.AutoSize = true;
			this.label_sort_name.LinkColor = System.Drawing.Color.DarkBlue;
			this.label_sort_name.Location = new System.Drawing.Point(89, 4);
			this.label_sort_name.Name = "label_sort_name";
			this.label_sort_name.Size = new System.Drawing.Size(34, 13);
			this.label_sort_name.TabIndex = 2;
			this.label_sort_name.TabStop = true;
			this.label_sort_name.Text = "ФИО";
			this.label_sort_name.Click += this.sort_str;
			// 
			// checker_sort_name
			// 
			this.checker_sort_name.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_name.AutoSize = true;
			this.checker_sort_name.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.checker_sort_name.Location = new System.Drawing.Point(126, 2);
			this.checker_sort_name.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_name.Name = "checker_sort_name";
			this.checker_sort_name.Size = new System.Drawing.Size(19, 17);
			//this.checker_sort_name.TabIndex = 1;
			//this.checker_sort_name.Text = "6";
			this.checker_sort_name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_departament
			// 
			this.container_label_departament.Controls.Add(this.checker_sort_departament);
			this.container_label_departament.Controls.Add(this.label_sort_departament);
			this.container_label_departament.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_departament.Location = new System.Drawing.Point(200, 2);
			this.container_label_departament.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_departament.Name = "container_label_departament";
			this.container_label_departament.Size = new System.Drawing.Size(144, 20);
			//this.container_label_departament.TabIndex = 7;
			// 
			// label_sort_departament
			// 
			this.label_sort_departament.ActiveLinkColor = System.Drawing.Color.Blue;
			this.label_sort_departament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_sort_departament.AutoSize = true;
			this.label_sort_departament.LinkColor = System.Drawing.Color.DarkBlue;
			this.label_sort_departament.Location = new System.Drawing.Point(88, 4);
			this.label_sort_departament.Name = "label_sort_departament";
			this.label_sort_departament.Size = new System.Drawing.Size(38, 13);
			this.label_sort_departament.TabIndex = 3;
			this.label_sort_departament.TabStop = true;
			this.label_sort_departament.Text = "Отдел";
			this.label_sort_departament.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label_sort_departament.Click += this.sort_str;
			// 
			// checker_sort_departament
			// 
			this.checker_sort_departament.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.checker_sort_departament.AutoSize = true;
			this.checker_sort_departament.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.checker_sort_departament.Location = new System.Drawing.Point(129, 2);
			this.checker_sort_departament.Margin = new System.Windows.Forms.Padding(0);
			this.checker_sort_departament.Name = "checker_sort_departament";
			this.checker_sort_departament.Size = new System.Drawing.Size(19, 17);
			//this.checker_sort_departament.TabIndex = 1;
			//this.checker_sort_departament.Text = "6";
			this.checker_sort_departament.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// container_label_data
			// 
			this.container_label_data.Controls.Add(this.label_data);
			this.container_label_data.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container_label_data.Location = new System.Drawing.Point(346, 2);
			this.container_label_data.Margin = new System.Windows.Forms.Padding(0);
			this.container_label_data.Name = "container_label_data";
			this.container_label_data.Size = new System.Drawing.Size(192, 20);
			//this.container_label_data.TabIndex = 8;
			// 
			// label_data
			// 
			this.label_data.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label_data.AutoSize = true;
			//this.label_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label_data.Location = new System.Drawing.Point(141, 4);
			this.label_data.Margin = new System.Windows.Forms.Padding(0);
			this.label_data.Name = "label_data";
			this.label_data.Size = new System.Drawing.Size(48, 13);
			//this.label_data.TabIndex = 1;
			this.label_data.Text = "Данные";
			this.label_data.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			
			this.container_label_id.ResumeLayout(false);
			this.container_label_id.PerformLayout();
			this.container_label_name.ResumeLayout(false);
			this.container_label_name.PerformLayout();
			this.container_label_departament.ResumeLayout(false);
			this.container_label_departament.PerformLayout();
			this.container_label_data.ResumeLayout(false);
			this.container_label_data.PerformLayout();
			this.solvers_table.ResumeLayout(false);
			this.solvers_table.PerformLayout();
		}

		/*
		private int get_ind_departement(Dictionary<UInt32,string> dict, UInt32 key){
			for(int i=0; i<dict.Count ; i++){
				if(dict.ElementAt(i).Key == key)
					return i+1;
			}
			return -1;
		}
		*/

		//создает данные таблицы
		private void create_body_table(ref MySqlDataReader data){
			//очищаем таблицу
			solvers_table.SuspendLayout();
			while(solvers_table.RowCount > 1){
				int row = solvers_table.RowCount - 1;
				for(Int32 a = 0; a < solvers_table.ColumnCount; a++){
					Control c = solvers_table.GetControlFromPosition(a, row);
					solvers_table.Controls.Remove(c);
					c.Dispose();
				}

				//solvers_table.RowStyles.RemoveAt(row);
				solvers_table.RowCount--;
			}
			col_id.Clear();
			col_but_del.Clear();
			col_data.Clear();
			col_departament.Clear();
			col_name.Clear();
			departs_id.Clear();

			Int32 i = 1;
			while(data.Read()){
				//собираем таблицу с данными
				solvers_table.RowCount += 1;
				// 
				// inp_id
				// 
				col_id.Add(new TextBox());
				col_id[i-1].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_id[i-1].BackColor = System.Drawing.SystemColors.Control;
				col_id[i-1].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_id[i-1].Location = new System.Drawing.Point(5, 37);
				col_id[i-1].MaxLength = 20;
				col_id[i-1].Name = "inp_id_"+i;
				col_id[i-1].Text = data.GetValue(0).ToString();
				col_id[i-1].ReadOnly = true;
				col_id[i-1].Size = new System.Drawing.Size(44, 13);
				col_id[i-1].TabStop = false;
				solvers_table.Controls.Add(col_id[i-1], 0, i);
				// 
				// inp_name
				// 
				col_name.Add(new TextBox());
				col_name[i-1].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_name[i-1].Location = new System.Drawing.Point(57, 34);
				col_name[i-1].MaxLength = 500;
				col_name[i-1].Name = "inp_name_"+i;
				col_name[i-1].Text = data.GetValue(1).ToString();
				col_name[i-1].Size = new System.Drawing.Size(138, 20);
				col_name[i-1].TabIndex = i*4;
				col_name[i-1].TextChanged += set_unsave;
				solvers_table.Controls.Add(col_name[i-1], 1, i);
				// 
				// inp_departament
				// 
				col_departament.Add(new ComboBox());
				col_departament[i-1].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				col_departament[i-1].FormattingEnabled = true;
				col_departament[i-1].Location = new System.Drawing.Point(203, 33);
				//col_departament[i-1].MaxDropDownItems = 50;
				col_departament[i-1].Name = "col_departament_"+i;
				col_departament[i-1].DisplayMember = "Value";
				col_departament[i-1].ValueMember = "Key";
				departs_id.Add(Convert.ToInt32(data.GetValue(2)));
				col_departament[i-1].DataSource = new BindingSource(departaments, null);
				col_departament[i-1].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				col_departament[i-1].Size = new System.Drawing.Size(138, 21);
				col_departament[i-1].TabIndex = i*4 + 1;
				solvers_table.Controls.Add(col_departament[i-1], 2, i);
				// 
				// inp_data
				// 
				col_data.Add(new TextBox());
				col_data[i-1].BorderStyle = System.Windows.Forms.BorderStyle.None;
				col_data[i-1].Dock = System.Windows.Forms.DockStyle.Fill;
				col_data[i-1].Location = new System.Drawing.Point(349, 27);
				col_data[i-1].Multiline = true;
				col_data[i-1].Name = "inp_data_"+i;
				col_data[i-1].Text = data.GetValue(3).ToString();
				//this.inp_data.Size = new System.Drawing.Size(186, 34);
				col_data[i-1].TabIndex = i*4 + 2;
				col_data[i-1].WordWrap = false;
				col_data[i-1].TextChanged += set_unsave;
				solvers_table.Controls.Add(col_data[i-1], 3, i);
				// 
				// but_del
				// 
				col_but_del.Add(new Button());
				col_but_del[i-1].Anchor = System.Windows.Forms.AnchorStyles.None;
				col_but_del[i-1].Cursor = System.Windows.Forms.Cursors.Hand;
				col_but_del[i-1].FlatStyle = System.Windows.Forms.FlatStyle.System;
				col_but_del[i-1].Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
				col_but_del[i-1].Location = new System.Drawing.Point(548, 32);
				col_but_del[i-1].Name = "but_del_"+i;
				col_but_del[i-1].Size = new System.Drawing.Size(26, 24);
				col_but_del[i-1].TabIndex = i*4 + 3;
				col_but_del[i-1].Text = "r";
				col_but_del[i-1].UseVisualStyleBackColor = true;
				col_but_del[i-1].Click += del_str;
				solvers_table.Controls.Add(col_but_del[i-1], 4, i);

				i++;
			}

			solvers_table.ResumeLayout(false);
			solvers_table.PerformLayout();
			//ГРЁБАННЫЙ КОСТЫЛЬ ДЛЯ ЗАДАГНИЯ КОМБОБОКСА
			//СУКА ЧО ЗА ДРЬМОВЫЙ ЯЗЫК
			//КАКОГО ХЕРА КОГДА Я БИНДЮ ДЕПАРТАМЕНТЫ ИХ НЕТ, НО ПРИ ОТРИСОВКЕ ОНИ ЕСТЬ?????
			//СУКА
			solvers_table.Paint += solvers_table_Paint;
		}

		private void solvers_table_Paint(object sender, PaintEventArgs e) {
			for(int i=0; i<col_departament.Count; i++){
				col_departament[i].SelectedValue = departs_id[i];
				col_departament[i].SelectedValueChanged += combobox_change;
			}

			solvers_table.Paint -= solvers_table_Paint;
		}

		//проверяет присутствует ли необходимые таблицы
		private bool test_tables_exist(){
			MySqlDataReader result;
			MySqlCommand test_command = new MySqlCommand("SELECT `id`, `departament_id`, `name`, `data` FROM `solvers` LIMIT 1", connect);
			//MySqlParameter db_name = new MySqlParameter("@db_name", connect.Database);
			//test_command.Parameters.Add(db_name);
			try{
				result = test_command.ExecuteReader();
			}catch(Exception ex){
				//MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return false;
			}
			result.Close();
			//test_command = new MySqlCommand("SELECT `id`, `name`, `data` FROM `departament` LIMIT 1", connect);
			test_command.CommandText = "SELECT `id`, `name`, `data` FROM `departament` LIMIT 1";
			try{
				result = test_command.ExecuteReader();
			}catch(Exception ex){
				//MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return false;
			}
			result.Close();
			return true;
		}

		//Генерирует список комманд для обновления
		private List<MySqlCommand> get_updates(){
			List<MySqlCommand> ret = new List<MySqlCommand>();
			MySqlCommand tmp;
			
			for(Int32 i=0; i<col_id.Count; i++){
				//добавление новых строк в таблицу
				if(col_id[i].Text == "*"){
					//если она добавлена но удалена, пропускаем
					if(col_id[i].Font.Strikeout)
						continue;
					tmp = new MySqlCommand("INSERT INTO `solvers` (`name`, `departament_id`, `data`) VALUES (@name, @departament_id, @data)", connect);
					tmp.Parameters.AddWithValue("@name", col_name[i].Text);
					tmp.Parameters.AddWithValue("@departament_id", col_departament[i].SelectedValue);
					tmp.Parameters.AddWithValue("@data", col_data[i].Text);
					ret.Add(tmp);

				//удаление строк из таблицы
				}else if(col_id[i].Font.Strikeout){
					tmp = new MySqlCommand("DELETE FROM `solvers` WHERE `id` = @id", connect);
					tmp.Parameters.AddWithValue("@id", validator.extract_uint_from_start(col_id[i].Text));
					ret.Add(tmp);

				//обновление существующих строк
				}else if(col_id[i].Text.Substring(col_id[i].Text.Length-1) == "*"){
					tmp = new MySqlCommand("UPDATE `solvers` SET `name` = @name, `departament_id` = @departament_id, `data` = @data WHERE `id` = @id", connect);
					tmp.Parameters.AddWithValue("@name", col_name[i].Text);
					tmp.Parameters.AddWithValue("@departament_id", col_departament[i].SelectedValue);
					tmp.Parameters.AddWithValue("@data", col_data[i].Text);
					tmp.Parameters.AddWithValue("@id", validator.extract_uint_from_start(col_id[i].Text));
					ret.Add(tmp);
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
					if(MessageBox.Show("При сохранении изменений произошла ошибка, откатить внесенные изменения?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes){
						transaction.Rollback();
						return false;
					}
				}
			
			return true;
		}

		//перезагружает данные в таблицу
		private void reload_data(){
			//получим кол-во сотрудников
			MySqlCommand query = new MySqlCommand("SELECT COUNT(*) FROM `solvers`", connect);
			try{
				str_count = Convert.ToUInt32(query.ExecuteScalar());
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//получим список департаментов с их ID
			query.CommandText = "SELECT `id`, `name` FROM `departament`";
			MySqlDataReader result = null;
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			departaments.Clear();
			while(result.Read()){
				departaments.Add(Convert.ToInt32(result.GetValue(0)), result.GetValue(1).ToString());
			}
			result.Close();

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

			//подготовим запрос на выборку
			MySqlParameter limit = new MySqlParameter("@limit", str_per_page);
			MySqlParameter offset = new MySqlParameter("@offset", (current_page-1)*str_per_page);

			query.CommandText = "SELECT `id`, `name`, `departament_id`, `data` FROM `solvers` "+order_by+"LIMIT @limit OFFSET @offset";
			query.Parameters.Add(limit);
			query.Parameters.Add(offset);
			//произведем её
			try{
				result = query.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			create_body_table(ref result);
			result.Close();
		}

///////////////////////////////////////////////////////////////////////////////
//		обработчики событий
///////////////////////////////////////////////////////////////////////////////

		private void new_str(Object sender, EventArgs e){
			int ind = solvers_table.RowCount-1;
			solvers_table.SuspendLayout();
			solvers_table.RowCount += 1;
			// 
			// inp_id
			// 
			col_id.Add(new TextBox());
			col_id[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_id[ind].BackColor = System.Drawing.SystemColors.Control;
			col_id[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_id[ind].Location = new System.Drawing.Point(5, 37);
			col_id[ind].MaxLength = 20;
			col_id[ind].Name = "inp_id_"+(ind+1);
			col_id[ind].Text = "*";
			col_id[ind].ReadOnly = true;
			col_id[ind].Size = new System.Drawing.Size(44, 13);
			col_id[ind].TabStop = false;
			solvers_table.Controls.Add(col_id[ind], 0, ind+1);
			// 
			// inp_name
			// 
			col_name.Add(new TextBox());
			col_name[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_name[ind].Location = new System.Drawing.Point(57, 34);
			col_name[ind].MaxLength = 500;
			col_name[ind].Name = "inp_name_"+(ind+1);
			col_name[ind].Size = new System.Drawing.Size(138, 20);
			col_name[ind].TabIndex = (ind+1)*4;
			col_name[ind].TextChanged += set_unsave;
			solvers_table.Controls.Add(col_name[ind], 1, (ind+1));
			// 
			// inp_departament
			// 
			col_departament.Add(new ComboBox());
			col_departament[ind].Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			col_departament[ind].FormattingEnabled = true;
			col_departament[ind].Location = new System.Drawing.Point(203, 33);
			//col_departament[ind].MaxDropDownItems = 50;
			col_departament[ind].Name = "col_departament_"+(ind+1);
			col_departament[ind].DisplayMember = "Value";
			col_departament[ind].ValueMember = "Key";
			col_departament[ind].DataSource = new BindingSource(departaments, null);
			col_departament[ind].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			col_departament[ind].Size = new System.Drawing.Size(138, 21);
			col_departament[ind].TabIndex = (ind+1)*4 + 1;
			col_departament[ind].DropDownClosed += combobox_change;
			solvers_table.Controls.Add(col_departament[ind], 2, (ind+1));
			// 
			// inp_data
			// 
			col_data.Add(new TextBox());
			col_data[ind].BorderStyle = System.Windows.Forms.BorderStyle.None;
			col_data[ind].Dock = System.Windows.Forms.DockStyle.Fill;
			col_data[ind].Location = new System.Drawing.Point(349, 27);
			col_data[ind].Multiline = true;
			col_data[ind].Name = "inp_data_"+(ind+1);
			//this.inp_data.Size = new System.Drawing.Size(186, 34);
			col_data[ind].TabIndex = (ind+1)*4 + 2;
			col_data[ind].WordWrap = false;
			col_data[ind].TextChanged += set_unsave;
			solvers_table.Controls.Add(col_data[ind], 3, (ind+1));
			// 
			// but_del
			// 
			col_but_del.Add(new Button());
			col_but_del[ind].Anchor = System.Windows.Forms.AnchorStyles.None;
			col_but_del[ind].Cursor = System.Windows.Forms.Cursors.Hand;
			col_but_del[ind].FlatStyle = System.Windows.Forms.FlatStyle.System;
			col_but_del[ind].Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			col_but_del[ind].Location = new System.Drawing.Point(548, 32);
			col_but_del[ind].Name = "but_del_"+(ind+1);
			col_but_del[ind].Size = new System.Drawing.Size(26, 24);
			col_but_del[ind].TabIndex = (ind+1)*4 + 3;
			col_but_del[ind].Text = "r";
			col_but_del[ind].UseVisualStyleBackColor = true;
			col_but_del[ind].Click += del_str;
			solvers_table.Controls.Add(col_but_del[ind], 4, (ind+1));
			solvers_table.ResumeLayout(false);
			solvers_table.PerformLayout();
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
						checker_sort_name.Text = "";
						checker_sort_departament.Text = "";
					}
					break;
				case "label_sort_name":
					//была включена сортировка ASC
					if(checker_sort_name.Text == "6"){
						checker_sort_name.Text = "5";
						order_by = "ORDER BY `name` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_name.Text == "5"){
						checker_sort_name.Text = "6";
						order_by = "ORDER BY `name` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_name.Text = "6";
						order_by = "ORDER BY `name` ASC ";
						checker_sort_id.Text = "";
						checker_sort_departament.Text = "";
					}
					break;
				case "label_sort_departament":
					//была включена сортировка ASC
					if(checker_sort_departament.Text == "6"){
						checker_sort_departament.Text = "5";
						order_by = "ORDER BY `departament_id` DESC ";

					//была включена сортировка DESC
					}else if(checker_sort_departament.Text == "5"){
						checker_sort_departament.Text = "6";
						order_by = "ORDER BY `departament_id` ASC ";

					//Этот столбик не сортировался
					}else{
						checker_sort_departament.Text = "6";
						order_by = "ORDER BY `departament_id` ASC ";
						checker_sort_name.Text = "";
						checker_sort_id.Text = "";
					}
					break;
			}
			instruments.inst_curr_page_inp.Text = "1";
			reload_data_with_save(sender, e);
		}

		private void set_unsave(Object sender,EventArgs e){
			int ind = ((Int32)validator.extract_uint_from_end(((TextBox)sender).Name))-1;
			string str = col_id[ind].Text;
			if(str.Substring(str.Length-1) != "*")
				col_id[ind].Text += "*";
		}

		private void combobox_change(Object sender,EventArgs e) {
			int ind = ((Int32)validator.extract_uint_from_end(((ComboBox)sender).Name))-1;
			string str = col_id[ind].Text;
			if(str.Substring(str.Length-1) != "*")
				col_id[ind].Text += "*";
		}

		private void del_str(Object sender,EventArgs e){
			int ind = ((Int32)validator.extract_uint_from_end(((Button)sender).Name))-1;
			string str = col_id[ind].Text;
			if(col_id[ind].Font.Strikeout)
				return;

			/*
			if(str != "*"){
				//удаляем из БД
				//if(MessageBox.Show("Вы действительно хотите удалить сотрудника?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) != DialogResult.Yes)
					//return;
				
				int id = (Int32)validator.extract_uint_from_start(str);
				
				MySqlCommand command = new MySqlCommand("DELETE FROM `solvers` WHERE `id` = @id", connect);
				MySqlParameter id_param = new MySqlParameter("@id", id);
				command.Parameters.Add(id_param);
				try{
					command.ExecuteNonQuery();
				}catch(Exception ex){
					MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
					return;
				}
			}
			*/
			
			col_id[ind].Font = new System.Drawing.Font(col_id[ind].Font, System.Drawing.FontStyle.Strikeout);col_but_del[ind].Enabled = false;
			col_id[ind].ForeColor = System.Drawing.Color.DarkRed;
			col_but_del[ind].Enabled = false;
			col_departament[ind].Enabled = false;
			col_name[ind].Enabled = false;
			col_data[ind].Enabled = false;
		}

///////////////////////////////////////////////////////////////////////////////
//		объекты форм
///////////////////////////////////////////////////////////////////////////////

		public System.Windows.Forms.TableLayoutPanel solvers_table;
			private System.Windows.Forms.Panel container_label_id;
				private System.Windows.Forms.LinkLabel sort_label_id;
				private System.Windows.Forms.Label checker_sort_id;
			private System.Windows.Forms.Panel container_label_name;
				private System.Windows.Forms.LinkLabel label_sort_name;
				private System.Windows.Forms.Label checker_sort_name;
			private System.Windows.Forms.Panel container_label_departament;
				private System.Windows.Forms.LinkLabel label_sort_departament;
				private System.Windows.Forms.Label checker_sort_departament;
			private System.Windows.Forms.Panel container_label_data;
				private System.Windows.Forms.Label label_data;
	}
}
