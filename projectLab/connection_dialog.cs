using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projectLab {
	public partial class connection_dialog:Form {
		public connection_dialog() {
			InitializeComponent();
		}

		private void cb_int_sec_CheckedChanged(object sender,EventArgs e) {
			if(cb_int_sec.Checked){
				inp_user.Enabled = false;
				inp_password.Enabled = false;
			}else{
				inp_user.Enabled = true;
				inp_password.Enabled = true;
			}
		}
	}
}
