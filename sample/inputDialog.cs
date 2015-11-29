using System;
using Gtk;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.CSharp;
using Microsoft.Win32;
using System.Text;
using System.IO;

namespace sample
{
	public partial class inputDialog : Gtk.Dialog
	{
		public inputDialog ()
		{
			this.Build ();
		}

		public string Text{
			get {
				return textview1.Buffer.Text;
			}
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{

			this.Respond (ResponseType.Accept);

			//throw new NotImplementedException ();
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Respond (ResponseType.Cancel);
			//throw new NotImplementedException ();
		}
	}
}

