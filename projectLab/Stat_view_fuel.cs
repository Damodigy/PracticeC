using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using Stat_view_fuel;

namespace Stat_view_fuel{

	class view_data{
		public UInt16 cont_id;
		public string fuel_type;
		public UInt64 volume;
		public UInt64 cost;
		public UInt64 time_start;
		public UInt64 time_end;
	}
}

namespace projectLab {
	class Stat_view_fuel :IDisposable{
		private table_instruments instruments;
		private MySqlConnection connect;

		private Dictionary<UInt16, Series> points;

		private UInt32 str_per_page;
		private UInt32 current_page;
		private UInt32 max_page;
		private UInt32 str_count;

		public Stat_view_fuel(ref table_instruments instrument_panel, ref MySqlConnection current_connect){
			instruments = instrument_panel;
			connect = current_connect;

			create_head_table();

			points = new Dictionary<UInt16,Series>();

			str_per_page = 1;
			current_page = 1;
			max_page = 1;
			str_count = 0;

			instruments.inst_str_count_inp.Text = str_per_page.ToString();
			instruments.inst_curr_page_inp.Text = current_page.ToString();

			instruments.inst_new_row.Enabled = false;
			instruments.inst_reload.Click += this.reload_data;
			instruments.inst_save.Enabled = false;

			instruments.inst_str_count_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_str_count_inp.KeyDown += this.enterkey_to_update;
			instruments.inst_str_count_inp.ToolTipText = "Кол-во смен";

			instruments.inst_left.Click += this.page_shift;
			instruments.inst_curr_page_inp.TextChanged += validator.validate_absint_ToolStripTextBox;
			instruments.inst_curr_page_inp.KeyDown += this.enterkey_to_update;
			instruments.inst_curr_page_inp.ToolTipText = "Текущая смена";
			instruments.inst_right.Click += this.page_shift;

			reload_data();
		}

		public void Dispose(){
			this.stat_chart.Dispose();
		}

		//создает шапку таблицы
		private void create_head_table(){
			stat_chart = new Chart();
			stat_chart.Dock = DockStyle.Fill;
			stat_chart.ChartAreas.Add(new ChartArea("main"));
		}

		private void create_body_table(Dictionary<UInt16, List<view_data>> data){
			points.Clear();
			stat_chart.Series.Clear();

			foreach(UInt16 cont_id in data.Keys){
				points[cont_id] = new Series();
				points[cont_id].Name = "points_"+cont_id;
				points[cont_id].AxisLabel = "Конт. #"+cont_id+ " ["+data[cont_id][0].fuel_type+"]";
				points[cont_id].ChartType = SeriesChartType.Line;
				points[cont_id].ChartArea = "main";
				Decimal old = 0;
				UInt64 old_time = 0;
				for(Int32 i=0; i<data[cont_id].Count; i++){
					if(data[cont_id][i].time_start > old_time){
						old_time = data[cont_id][i].time_start;
					}
					points[cont_id].Points.AddXY(old_time, old);
					old += data[cont_id][i].cost/100m;
					if(data[cont_id][i].time_end > old_time){
						old_time = data[cont_id][i].time_end;
					}
					points[cont_id].Points.AddXY(old_time, old);
				}
				stat_chart.Series.Add(points[cont_id]);
			}
		}

		private void reload_data(){
			MySqlDataReader result = null;
			MySqlCommand query = null;

			//Получим количество строк
			query = new MySqlCommand("SELECT COUNT(*) FROM `shifts`", connect);
			try{
				str_count = Convert.ToUInt32(query.ExecuteScalar());
			}catch (Exception ex){
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
			instruments.inst_label_2.Text = "/" + max_page;

			current_page = instruments.inst_curr_page_inp.Text == "" ? 0 : UInt32.Parse(instruments.inst_curr_page_inp.Text);
			if(current_page < 1)
				current_page = 1;
			if(current_page > max_page)
				current_page = max_page;
			instruments.inst_curr_page_inp.Text = current_page.ToString();
			
			//получим время конца конечной смены
			DateTime s_time_start, s_time_end;
			query = new MySqlCommand("SELECT `time_end` FROM `shifts` ORDER BY `time_end` DESC LIMIT 1 OFFSET @offset", connect);
			UInt32 offset = (current_page) * str_per_page - 1;
			if(offset >= str_count){
				offset = str_count-1;
			}
			query.Parameters.AddWithValue("@offset", offset);
			try{
				s_time_end = (DateTime)query.ExecuteScalar();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}
			
			//получим время начала начальной смены
			query = new MySqlCommand("SELECT `time_start` FROM `shifts` ORDER BY `time_end` DESC LIMIT 1 OFFSET @offset", connect);
			query.Parameters.AddWithValue("@offset", offset);
			try{
				s_time_start = (DateTime)query.ExecuteScalar();
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//получим транзакции в период смены
			query.CommandText = "SELECT `fuel_transactions`.`container_id`, `fuel_transactions`.`fuel_volume`, `fuel_transactions`.`cost`, `fuel_transactions`.`fuel_id`, `fuel_types`.`type`, `fuel_transactions`.`time_start`, `fuel_transactions`.`time_end`\n"
								+ "FROM `fuel_transactions`\n"
								+ "LEFT JOIN `fuel_types`\n"
								+ "ON `fuel_transactions`.`fuel_id` = `fuel_types`.`id`\n"
								+ "WHERE `fuel_transactions`.`fuel_volume` < 0 AND `fuel_transactions`.`time_start` <= @time_end AND `fuel_transactions`.`time_start` >= @time_start\n"
								+ "ORDER BY `fuel_transactions`.`time_start` ASC\n";
			query.Parameters.AddWithValue("@time_start", s_time_start);
			query.Parameters.AddWithValue("@time_end", s_time_end);
			try{
				result = query.ExecuteReader();
			}catch (Exception ex){
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				return;
			}

			//преобразуем данные в массив
			Dictionary<UInt16, List<view_data>> data = new Dictionary<UInt16, List<view_data>>();
			while(result.Read()){
				view_data tmp = new view_data();
				tmp.cont_id = Convert.ToUInt16(result.GetValue(0));					//`trans`.`container_id`
				if(Convert.IsDBNull(result.GetValue(3))){
					tmp.fuel_type = "Пусто";
				}else{
					tmp.fuel_type = Convert.ToString(result.GetValue(4))				//`fuel`.`type`
					+ "("
					+ Convert.ToString(result.GetValue(3)) + ")";					//`trans`.`fuel_id`
				}
				tmp.volume = (UInt64)Math.Abs(Convert.ToInt64(result.GetValue(1)));	//`trans`.`fuel_volume`
				tmp.cost = (UInt64)Math.Abs(Convert.ToInt64(result.GetValue(2)));	//`trans`.`cost`
				tmp.time_start = (UInt64)(((DateTime)result.GetValue(5)).Ticks - 621355968000000000) / 10000000;//`trans`.`tie_start`
				if(Convert.IsDBNull(result.GetValue(6))){
					tmp.time_end = (UInt64)(DateTime.Now.Ticks - 621355968000000000) / 10000000;
				}else{
					tmp.time_end = (UInt64)(((DateTime)result.GetValue(6)).Ticks - 621355968000000000) / 10000000;//`trans`.`tie_start`
				}
				if(!data.ContainsKey(tmp.cont_id))
					data[tmp.cont_id] = new List<view_data>();
				data[tmp.cont_id].Add(tmp);
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

		///////////////////////////////////////////////////////////////////////////////
		//		объекты форм
		///////////////////////////////////////////////////////////////////////////////

		public Chart stat_chart;
	}
}
