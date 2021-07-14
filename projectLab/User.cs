using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectLab {
	class User{
		private user_type type;
		private string login;
		private UInt64 slave_id;

		public User(){
			this.reset();
		}

		public void load(user_type type, string login, UInt64 id){
			this.type = type;
			this.login = login;
			this.slave_id = id;
		}

		public void reset(){
			this.type = user_type.NONE;
			this.login = "";
			this.slave_id = 0;
		}

		public user_type get_type(){
			return this.type;
		}

		public string get_login(){
			return this.login;
		}

		public UInt64 get_id(){
			return this.slave_id;
		}
	}
}
