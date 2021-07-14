using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectLab {

	enum user_type : byte{
		NONE		= 0,//гость, нихрена не может
		//Данные + Администрирование
		ADMIN		= 1,//может всё, также управлять пользователями
		/*
		 * SELECT `users`
		 * 
		 * SELECT, LOCK TABLES `containers`
		 * SELECT, LOCK TABLES `fuel_types`
		 * SELECT, LOCK TABLES `fuel_transactions`
		 * 
		 * SELECT, UPDATE, INSERT, DELETE, LOCK TABLES `shifts`
		 * SELECT, UPDATE, INSERT, DELETE, LOCK TABLES `slaves_shifts`
		 * SELECT, UPDATE, INSERT, DELETE, LOCK TABLES `slaves`
		 */
		MANAGER		= 2,//может управлять сменами, смотреть статистику
		/*
		 * GRANT SELECT ON `politeh_prac`.`users` TO 'prodovan'@'localhost';
		 * GRANT SELECT ON `politeh_prac`.`containers` TO 'prodovan'@'localhost';
		 * GRANT SELECT ON `politeh_prac`.`pumps` TO 'prodovan'@'localhost';
		 * GRANT SELECT ON `politeh_prac`.`fuel_types` TO 'prodovan'@'localhost';
		 * GRANT SELECT, UPDATE, INSERT ON `politeh_prac`.`fuel_transactions` TO 'prodovan'@'localhost';
		 * GRANT LOCK TABLES ON `politeh_prac`.* TO 'prodovan'@'localhost';
		 */
		PAYMASTER	= 3,//может продавать бензыч
		/*
		 * SELECT `users`
		 * 
		 * SELECT, UPDATE, LOCK TABLES `pumps`
		 * SELECT, UPDATE, LOCK TABLES `containers`
		 * SELECT, LOCK TABLES `fuel_types`
		 * SELECT, UPDATE, INSERT, LOCK TABLES `fuel_transactions`
		 */
		ENGINEER	= 4 //может управлять баками и колонками, принимать бензыч
	};

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
