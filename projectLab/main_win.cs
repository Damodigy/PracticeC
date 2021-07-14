﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

//TODO добавить валидатор данных!!!

namespace projectLab {

	enum connection_dialog_type : byte{
		NEW,
		EDIT,
		FAST_NEW,
		VIEW
	};

	enum user_type : byte{
		NONE		= 0,//гость, нихрена не может
		//Данные + Администрирование
		ADMIN		= 1,//может всё, также управлять пользователями
		/*
		 * SELECT `users`
		 * 
		 * SELECT `containers`
		 * SELECT `fuel_types`
		 * SELECT `fuel_transactions`
		 * 
		 * SELECT, UPDATE, INSERT, DELETE `shifts`
		 * SELECT, UPDATE, INSERT, DELETE `slaves_shifts`
		 * SELECT, UPDATE, INSERT, DELETE `slaves`
		 */
		MANAGER		= 2,//может управлять сменами, смотреть статистику
		/*
		 * SELECT `users`
		 * 
		 * SELECT `pumps`
		 * SELECT `containers`
		 * SELECT `fuel_types`
		 * SELECT, UPDATE, INSERT `fuel_transactions`
		 */
		PAYMASTER	= 3,//может продавать бензыч
		/*
		 * SELECT `users`
		 * 
		 * SELECT, UPDATE `pumps`
		 * SELECT, UPDATE `containers`
		 * SELECT `fuel_types`
		 * SELECT, UPDATE, INSERT `fuel_transactions`
		 */
		ENGINEER	= 4 //может управлять баками и колонками, принимать бензыч
	};


	public partial class main_win:Form {

		private int stop_soft;

		private connection current_connection_data;
		private MySqlConnection current_SqlConnection;

		private User current_user;

		//private solvers_table_data solvers_data;

		public main_win() {
			before_init();
			InitializeComponent();
			after_init();
		}

///////////////////////////////////////////////////////////////////////////////
//		Обработчики триггеров
///////////////////////////////////////////////////////////////////////////////

		//триггер перед загрузкой формы
		private void before_init(){
			current_connection_data = null;
			current_SqlConnection = null;
			current_user = new User();
		}

		//триггер после загрузки формы
		private void after_init(){
		}

		//триггер после успешного подключения к БД
		private void after_success_connect(){
			this.menu_bd_item_current.Enabled = true;
			this.menu_bd_item_break.Enabled = true;
			load_current_user();


			//solvers_data = new solvers_table_data(ref container_instruments, ref current_SqlConnection);
			//this.solvers_container.Controls.Add(solvers_data.solvers_table);
			this.work_frame.Visible = true;
		}

		//триггер после отключения от БД
		private void after_success_disconnect(){
			this.menu_bd_item_current.Enabled = false;
			this.menu_bd_item_break.Enabled = false;
			this.work_frame.Visible = false;
			this.current_user.reset();
			//this.solvers_container.Controls.RemoveByKey("solvers_table");
			//solvers_data = null;
		}

///////////////////////////////////////////////////////////////////////////////
//		Обработчики событий
///////////////////////////////////////////////////////////////////////////////

		//отображает данные о текущем соединении
		private void view_current_connect(Object sender, EventArgs e){
			//open_connection_dialog(ref current_connection_data, connection_dialog_type.VIEW);
			string info = "Сервер:\t\t"+current_SqlConnection.DataSource+Environment.NewLine
						+ "База данных:\t"+current_SqlConnection.Database+Environment.NewLine
						+ "Пользователь:\t"+current_connection_data.user+Environment.NewLine
						+ "Версия сервера:\t"+current_SqlConnection.ServerVersion+Environment.NewLine
						+ "Состояние:\t"+current_SqlConnection.State+Environment.NewLine;
			show_info_dialog("Текущее соединение", info);
		}

		//разрывает текущее соединение, запускает триггер after_success_disconnect
		private void current_disconnect(Object sender, EventArgs e){
			if(current_SqlConnection == null){
				throw new Exception("Connection already closed");
			}
			current_SqlConnection.Close();
			current_SqlConnection = null;
			current_connection_data = null;
			this.main_status_text_1.Text = "База отключена";
			after_success_disconnect();
		}

		//запускает новое быстрое соединение, без запоминания данных
		private void fast_connect(Object sender, EventArgs e){
			if(current_SqlConnection != null){
				MessageBox.Show("Перед открытием нового соединения, закройте текущее", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			connection new_connect = new connection();
			//получаем данные о подключении
			if(!open_connection_dialog(ref new_connect, connection_dialog_type.FAST_NEW))
				return;
			connect_with_db(new_connect);
		}

///////////////////////////////////////////////////////////////////////////////
//		Вспомогательные функции
///////////////////////////////////////////////////////////////////////////////

		//загружает данные (тип) пользователя из БД
		private void load_current_user(){
			MySqlDataReader result;
			MySqlCommand test_command = new MySqlCommand("SELECT `slave_id`, `type` FROM `users` WHERE `login` = @login LIMIT 1", current_SqlConnection);
			test_command.Parameters.AddWithValue("@login", current_connection_data.user);
			try{
				result = test_command.ExecuteReader();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			while(result.Read()){
				this.current_user.load((user_type)Convert.ToInt32(result.GetValue(1)), current_connection_data.user, Convert.ToUInt64(result.GetValue(0)));
			}
			result.Close();
		}

		//производит соединение с БД, выбирает его текущим, запускает триггер after_success_connect
		private bool connect_with_db(connection connect){
			if(current_SqlConnection != null){
				throw new Exception("Close old connection, before open new");
			}
			this.main_status_text_1.Text = "Идет подключение...";
			//подключаемся
			current_SqlConnection = new MySqlConnection(create_connect_str(connect));
			try{
				current_SqlConnection.Open();
			}catch(MySqlException ex){
				//неудача, покажем ошибку, и выйдем из функции
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				this.main_status_text_1.Text = "Попытка не удалась";
				current_SqlConnection = null;
				return false;
			}
			this.main_status_text_1.Text = "База данных подключена";
			current_connection_data = connect;
			after_success_connect();
			return true;
		}

		//диалог управления данными соединений
		//если там будет нажата ОК, то данные в connect будут изменены
		//возвращает true, если данные были изменены
		private bool open_connection_dialog(ref connection connect, connection_dialog_type type){
			connection_dialog cd_dialog = new connection_dialog();
			cd_dialog.Owner = this;
			if(type == connection_dialog_type.FAST_NEW){
				cd_dialog.inp_name.Enabled = false;
				cd_dialog.change_title(this.menu_bd_item_fast.Text.Replace("&", ""));
			}else if(type == connection_dialog_type.NEW){
				cd_dialog.change_title("Новое подключение");
			}else if(type == connection_dialog_type.EDIT){
				cd_dialog.change_title("Редактировать подключение");
				cd_dialog.inp_name.Text = connect.name;
				cd_dialog.inp_address.Text = connect.address;
				cd_dialog.inp_bd.Text = connect.db_name;
				cd_dialog.inp_user.Text = connect.user;
				cd_dialog.inp_password.Text = connect.pswd;
				cd_dialog.cb_enc.Checked = connect.encrypt;
				cd_dialog.cb_int_sec.Checked = connect.integrated_security;
				/*
				if(connect.integrated_security){
					cd_dialog.inp_user.Enabled = false;
					cd_dialog.inp_password.Enabled = false;
				}
				*/
			}else if(type == connection_dialog_type.VIEW){
				cd_dialog.change_title("Просмотр текущего подключения");
				cd_dialog.inp_name.Text = connect.name;
				cd_dialog.inp_name.ReadOnly = true;

				cd_dialog.inp_address.Text = connect.address;
				cd_dialog.inp_address.ReadOnly = true;

				cd_dialog.inp_bd.Text = connect.db_name;
				cd_dialog.inp_bd.ReadOnly = true;

				cd_dialog.inp_user.Text = connect.user;
				cd_dialog.inp_user.ReadOnly = true;

				cd_dialog.inp_password.Text = connect.pswd;
				cd_dialog.inp_password.ReadOnly = true;

				cd_dialog.cb_enc.Checked = connect.encrypt;
				cd_dialog.cb_enc.Enabled = false;

				cd_dialog.cb_int_sec.Checked = connect.integrated_security;
				cd_dialog.cb_int_sec.Enabled = false;
			}else{
				return false;
			}

			cd_dialog.ShowDialog();

			if(type == connection_dialog_type.VIEW)
				return true;

			if(cd_dialog.DialogResult == System.Windows.Forms.DialogResult.OK){
				//тут валидация (или в диалоге)
				connect.name = cd_dialog.inp_name.Text;
				connect.address = cd_dialog.inp_address.Text;
				connect.db_name = cd_dialog.inp_bd.Text;
				connect.encrypt = cd_dialog.cb_enc.Checked;
				connect.integrated_security = cd_dialog.cb_int_sec.Checked;
				if(connect.integrated_security){
					connect.user = "";
					connect.pswd = "";
				}else{
					connect.user = cd_dialog.inp_user.Text;
					connect.pswd = cd_dialog.inp_password.Text;
				}
				return true;
			}else{
				return false;
			}
		}

		//диалог вывода информации в ричбокс
		private void show_info_dialog(string title, string text){
			info_dialog inf_dialog = new info_dialog();
			inf_dialog.Owner = this;
			inf_dialog.change_title(title);
			inf_dialog.textbox.Text = text;
			inf_dialog.Show();
		}

		//возвращает строку подключения собранную из структуры connect
		private string create_connect_str(connection connect){
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
				builder.Server = connect.address;
				builder.Database = connect.db_name;
			//если включен энкрипт
			if(connect.encrypt){
				//то то требуем ssl
				builder.SslMode = MySqlSslMode.Required;
			}else{
				//иначе не используем его
				builder.SslMode = MySqlSslMode.None;
			}
			if(connect.integrated_security){
				builder.IntegratedSecurity = connect.integrated_security;
				return builder.GetConnectionString(false);
			}else{
				builder.Password = connect.pswd;
				builder.UserID = connect.user;
				return builder.GetConnectionString(true);
			}
		}
	}
	
	public static class validator{
		public static int MAX_LENGTH = 128;

		public static void validate_absint_TextBox(object sender,EventArgs e) {
			System.Windows.Forms.TextBox inp = (System.Windows.Forms.TextBox) sender;
			UInt32 tmp;
			if(inp.Text != "" && !UInt32.TryParse(inp.Text, out tmp)){
				inp.Text = inp.Text.Substring(0, inp.Text.Length-1);
			}else{
				inp.Select(inp.Text.Length, 0);
			}
		}

		public static void validate_decimal_TextBox(object sender,EventArgs e) {
			System.Windows.Forms.TextBox inp = (System.Windows.Forms.TextBox) sender;
			Decimal tmp;
			if(inp.Text != "" && !Decimal.TryParse(inp.Text, out tmp)){
				inp.Text = inp.Text.Substring(0, inp.Text.Length-1);
			}else{
				inp.Select(inp.Text.Length, 0);
			}
		}

		public static void validate_absint_ToolStripTextBox(object sender,EventArgs e) {
			System.Windows.Forms.ToolStripTextBox inp = (System.Windows.Forms.ToolStripTextBox) sender;
			UInt32 tmp;
			if(inp.Text != "" && !UInt32.TryParse(inp.Text, out tmp)){
				inp.Text = inp.Text.Substring(0, inp.Text.Length-1);
			}else{
				inp.Select(inp.Text.Length, 0);
			}
		}

		public static void validate_decimal_ToolStripTextBox(object sender,EventArgs e) {
			System.Windows.Forms.ToolStripTextBox inp = (System.Windows.Forms.ToolStripTextBox) sender;
			Decimal tmp;
			if(inp.Text != "" && !Decimal.TryParse(inp.Text, out tmp)){
				inp.Text = inp.Text.Substring(0, inp.Text.Length-1);
			}else{
				inp.Select(inp.Text.Length, 0);
			}
		}

		public static UInt32 extract_uint_from_end(string str){
			UInt32 ret;
			while(str != ""){
				if(UInt32.TryParse(str, out ret)){
					return ret;
				}else{
					str = str.Substring(1, str.Length-1);
				}
			}
			return 0;
		}

		public static UInt32 extract_uint_from_start(string str){
			UInt32 ret;
			while(str != ""){
				if(UInt32.TryParse(str, out ret)){
					return ret;
				}else{
					str = str.Substring(0, str.Length-1);
				}
			}
			return 0;
		}
	}

	public class connection{
		public string name;
		public string address;
		public string db_name;
		public string user;
		public string pswd;
		public bool integrated_security;
		public bool encrypt;
	}
}
