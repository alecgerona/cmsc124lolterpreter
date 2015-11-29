
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TreeView treeview1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	
	private global::Gtk.TextView textview1;
	
	private global::Gtk.Label label1;
	
	private global::Gtk.Label label2;
	
	private global::Gtk.Button button1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow2;
	
	private global::Gtk.TreeView symboltreeview;
	
	private global::Gtk.Label label3;
	
	private global::Gtk.FileChooserButton filechooserbutton1;
	
	private global::Gtk.Button button2;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow3;
	
	private global::Gtk.TextView consoletext;
	
	private global::Gtk.Button button3;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed ();
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.treeview1 = new global::Gtk.TreeView ();
		this.treeview1.WidthRequest = 200;
		this.treeview1.HeightRequest = 300;
		this.treeview1.CanFocus = true;
		this.treeview1.Name = "treeview1";
		this.GtkScrolledWindow.Add (this.treeview1);
		this.fixed1.Add (this.GtkScrolledWindow);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.GtkScrolledWindow]));
		w2.X = 467;
		w2.Y = 146;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.HeightRequest = 300;
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview1 = new global::Gtk.TextView ();
		this.textview1.WidthRequest = 300;
		this.textview1.HeightRequest = 500;
		this.textview1.CanFocus = true;
		this.textview1.Name = "textview1";
		this.GtkScrolledWindow1.Add (this.textview1);
		this.fixed1.Add (this.GtkScrolledWindow1);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.GtkScrolledWindow1]));
		w4.X = 72;
		w4.Y = 153;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Lexemes");
		this.fixed1.Add (this.label1);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.label1]));
		w5.X = 525;
		w5.Y = 100;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Code:");
		this.fixed1.Add (this.label2);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.label2]));
		w6.X = 75;
		w6.Y = 98;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("Open File");
		this.fixed1.Add (this.button1);
		global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.button1]));
		w7.X = 124;
		w7.Y = 91;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.symboltreeview = new global::Gtk.TreeView ();
		this.symboltreeview.WidthRequest = 200;
		this.symboltreeview.HeightRequest = 300;
		this.symboltreeview.CanFocus = true;
		this.symboltreeview.Name = "symboltreeview";
		this.GtkScrolledWindow2.Add (this.symboltreeview);
		this.fixed1.Add (this.GtkScrolledWindow2);
		global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.GtkScrolledWindow2]));
		w9.X = 783;
		w9.Y = 149;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label3 = new global::Gtk.Label ();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Symbol Table");
		this.fixed1.Add (this.label3);
		global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.label3]));
		w10.X = 836;
		w10.Y = 102;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.filechooserbutton1 = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select a File"), ((global::Gtk.FileChooserAction)(0)));
		this.filechooserbutton1.Name = "filechooserbutton1";
		this.fixed1.Add (this.filechooserbutton1);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.filechooserbutton1]));
		w11.X = 131;
		w11.Y = 119;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button2 = new global::Gtk.Button ();
		this.button2.WidthRequest = 900;
		this.button2.CanFocus = true;
		this.button2.Name = "button2";
		this.button2.UseUnderline = true;
		this.button2.Label = global::Mono.Unix.Catalog.GetString ("Execute");
		this.fixed1.Add (this.button2);
		global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.button2]));
		w12.X = 84;
		w12.Y = 478;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
		this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
		this.consoletext = new global::Gtk.TextView ();
		this.consoletext.WidthRequest = 1000;
		this.consoletext.HeightRequest = 200;
		this.consoletext.CanFocus = true;
		this.consoletext.Name = "consoletext";
		this.GtkScrolledWindow3.Add (this.consoletext);
		this.fixed1.Add (this.GtkScrolledWindow3);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.GtkScrolledWindow3]));
		w14.X = 84;
		w14.Y = 560;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button3 = new global::Gtk.Button ();
		this.button3.CanFocus = true;
		this.button3.Name = "button3";
		this.button3.UseUnderline = true;
		this.button3.Label = global::Mono.Unix.Catalog.GetString ("Accept Input");
		this.fixed1.Add (this.button3);
		global::Gtk.Fixed.FixedChild w15 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.button3]));
		w15.X = 81;
		w15.Y = 521;
		this.Add (this.fixed1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1368;
		this.DefaultHeight = 1111;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.button2.Clicked += new global::System.EventHandler (this.OnButton2Clicked);
	}
}
