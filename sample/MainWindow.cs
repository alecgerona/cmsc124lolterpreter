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

public partial class MainWindow: Gtk.Window
{	
	public static bool waitingForInput = false;
	public static Hashtable varList = new Hashtable ();
	public static string stringholder;
	public static Dictionary<string, string> library = new Dictionary<string, string> (StringComparer.OrdinalIgnoreCase);
	public static bool printed = false;


	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		treeviewchuchu ();
		symboltable ();


		//Initialize definitions


		library.Add ("HAI", "Code Delimiter");
		library.Add ("I HAS A", "Var declaration");
		library.Add ("VISIBLE", "Output Keyword");
		library.Add ("\"", "String Delimiter");
		library.Add ("OBTW", "Comment delimiter");
		library.Add ("TLDR", "Comment delimiter");
		library.Add ("KTHXBYE", "Code delimiter");
		library.Add ("SUM OF", "Addition operator");
		library.Add ("DIFF OF", "Subtraction operator");
		library.Add ("PRODUKT OF", "Product operator");
		library.Add ("QUOSHUNT OF", "Division operator");
		library.Add ("MOD OF", "Modulo operator");
		library.Add ("SMALLR OF", "Comparison operator");
		library.Add ("BIGGR OF", "Cmparison operator");
		library.Add ("BOTH OF", "Logical operator");
		library.Add ("EITHER OF", "Logical operator");
		library.Add ("AN", "Keyword");
		library.Add ("GIMMEH", "Scan operator");
		library.Add ("noVariable", "Variable does not exist: ");
		library.Add ("ITZ", "Assignment Indicator");
		library.Add ("number", "Number Literal");
		library.Add ("variable", "Variable");
		library.Add ("MKAY", "ANY OF and ALL OF delimiter");
		library.Add ("ANY OF", "Multiple OR operator");
		library.Add ("ALL OF", "Multiple AND operator");
		library.Add ("BOTH SAEM", "Equality operator");
		library.Add ("DIFFRINT", "Inequality operator");
		library.Add ("WON OF", "XOR operator");
		varList.Add ("IT", ""); //Create LOLCODE's implicit variable, IT.




	}




	private void symboltable(){
		// Create a column for the lexeme 
		Gtk.TreeViewColumn identifier = new Gtk.TreeViewColumn ();
		identifier.Title = "Identifier";

		// Create the text cell that will display the lexeme
		Gtk.CellRendererText identifierCell = new Gtk.CellRendererText ();

		// Add the cell to the column
		identifier.PackStart (identifierCell, true);

		// Create a column for the classification
		Gtk.TreeViewColumn value = new Gtk.TreeViewColumn ();
		value.Title = "Value";

		// Do the same for the classification column
		Gtk.CellRendererText valueCell = new Gtk.CellRendererText ();
		value.PackStart (valueCell, true);

		// Add the columns to the TreeView
		symboltreeview.AppendColumn (identifier);
		symboltreeview.AppendColumn (value);

		// Tell the Cell Renderers which items in the model to display
		identifier.AddAttribute (identifierCell, "text", 0);
		value.AddAttribute (valueCell, "text", 1);

		// Create a model that will hold two strings - lexeme and classification
		Gtk.ListStore symbol = new Gtk.ListStore (typeof (string), typeof (string));

		// Add some data to the store
		symbol.AppendValues ("Sample Identifier", "1");
		symbol.AppendValues ("Sample Identifier 2", "2");
		symbol.AppendValues ("Sample Identifier 3", "3");

		// Assign the model to the TreeView
		symboltreeview.Model = symbol;
	}

	private void treeviewchuchu(){
		// Create a column for the lexeme 
		Gtk.TreeViewColumn lexeme = new Gtk.TreeViewColumn ();
		lexeme.Title = "Lexeme";

		// Create the text cell that will display the lexeme
		Gtk.CellRendererText lexemeCell = new Gtk.CellRendererText ();

		// Add the cell to the column
		lexeme.PackStart (lexemeCell, true);

		// Create a column for the classification
		Gtk.TreeViewColumn classification = new Gtk.TreeViewColumn ();
		classification.Title = "Classification";

		// Do the same for the classification column
		Gtk.CellRendererText classCell = new Gtk.CellRendererText ();
		classification.PackStart (classCell, true);

		// Add the columns to the TreeView
		treeview1.AppendColumn (lexeme);
		treeview1.AppendColumn (classification);

		// Tell the Cell Renderers which items in the model to display
		lexeme.AddAttribute (lexemeCell, "text", 0);
		classification.AddAttribute (classCell, "text", 1);

		// Create a model that will hold two strings - lexeme and classification
		Gtk.ListStore lex = new Gtk.ListStore (typeof (string), typeof (string));

		// Add some data to the store
		lex.AppendValues ("HAI", "Code Definition");
		lex.AppendValues ("HAI", "Code Definition");
		lex.AppendValues ("HAI", "Code Definition");

		// Assign the model to the TreeView
		treeview1.Model = lex;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void OnButton2Clicked (object sender, EventArgs e)
	{
		printed = false;
		varList.Clear ();
		consoletext.Buffer.Clear();

		string code = textview1.Buffer.Text;
		string[] codearray = code.Split ('\n');


		string hairegex = @"^\s*(I HAS A) ([a-zA-Z][a-zA-Z0-9]*) ?(ITZ)? ?(.+)?";
		string startregex = @"^\s*(HAI)\s*$";
		string visibleregex = @"\s*(VISIBLE) (.*)";
		string commentregex = @"\s*(OBTW)";
		string endregex = @"^\s*(KTHXBYE)\s*$";
		string endcommentregex = @"(TLDR)\s*$";
		string numvarwtfregex = @"(([a-zA-Z][a-zA-Z0-9]*), WTF?)|((-?[0-9]*\.?[0-9]+), WTF?)";
		string switchcaseregex = @"(.+), WTF\?";

		string addregex = @"^\s*(SUM OF)";
		string subregex = @"^\s*(DIFF OF)";
		string mulregex = @"^\s*(PRODUKT OF)";
		string divregex = @"^\s*(QUOSHUNT OF)";
		string modregex = @"^\s*(MOD OF)";
		string biggrregex = @"^\s*(BIGGR OF)";
		string smallrregex = @"^\s*(SMALLR OF)";
		string scommentregex = "(BTW) (.*)";
		string bothofregex = @"^\s*(BOTH OF)";
		string eitherofregex = @"^\s*(EITHER OF)";
		string wonofregex = @"^\s*(WON OF)";
		string notregex = @"^\s*(NOT)";
		string allofregex = @"^\s*(ALL OF)";
		string anyofregex = @"^\s*(ANY OF)";
		string bothsaemregex = @"^\s*(BOTH SAEM)";
		string diffrintregex = @"^\s*(DIFFRINT)";
		string smooshregex = "(DIFF OF) (\")(.*)(\") (AN) (\")(.*)(\")";
		string gimmehregex = "(GIMMEH) ([a-zA-Z][a-zA-Z0-9]*)";
		string assregex = "([a-zA-Z][a-zA-Z0-9]*) (R) (.*)";
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		string stringregex ="(\")(.*)(\")";
		string opsregex = "(SUM OF)|(DIFF OF)|(PRODUKT OF)|(QUOSHUNT OF)|(MOD OF)|(BIGGR OF)|(SMALLR OF)|(AN)";


		Gtk.ListStore lex = new Gtk.ListStore (typeof (string), typeof (string));
		Gtk.ListStore sym = new Gtk.ListStore (typeof (string), typeof (string));

		lex.Clear ();
		treeview1.Model = lex;
		sym.Clear ();
		symboltreeview.Model = sym;

		//Keyword regexes
		Regex start = new Regex (startregex);
		Regex vardec = new Regex (hairegex);
		Regex print = new Regex (visibleregex);
		Regex comment = new Regex (commentregex);
		Regex endcomment = new Regex (endcommentregex);
		Regex end = new Regex (endregex);
		Regex add = new Regex (addregex);
		Regex sub = new Regex (subregex);
		Regex mul = new Regex (mulregex);
		Regex div = new Regex (divregex);
		Regex mod = new Regex (modregex);
		Regex big = new Regex (biggrregex);
		Regex small = new Regex (smallrregex);
		Regex both = new Regex (bothofregex);
		Regex either = new Regex (eitherofregex);
		Regex won = new Regex (wonofregex);
		Regex not = new Regex (notregex);
		Regex allof = new Regex (allofregex);
		Regex anyof = new Regex (anyofregex);
		Regex bothsaem = new Regex (bothsaemregex);
		Regex diffrint = new Regex (diffrintregex);
		Regex switchcase = new Regex (switchcaseregex);

		//Data type regexes
		Regex numvarwtf = new Regex(numvarwtfregex);
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		Regex str =new Regex (stringregex);
		Regex gimmeh = new Regex (gimmehregex);
		Regex ops = new Regex (opsregex);
		Regex ass = new Regex (assregex);

		HashSet <string> lexemeHash = new HashSet<string> (StringComparer.OrdinalIgnoreCase);





		//Initialize requirements
		Stack delimiter = new Stack();
		List<string> ifelse = new List<string> ();
		int linenumber = 0;
		int len = codearray.Length;
		int commentline = 0;
		bool satisfy = true;
		bool gtfo = false;
		bool mebbe = false;
		int startpoint = 0;
		int endpoint = 0;
		int ifcaller = 0;
		int ifcounter = 0;
		int oiccounter = 0;
		int j = 0;
		string targetArray = "";



	
	





		for(int i = 0; i<codearray.Length; i++){
			linenumber++;

			Match m = vardec.Match (codearray[i]);
			Match startmatch = start.Match(codearray[i]);
			Match printmatch = print.Match (codearray[i]);
			Match commentmatch = comment.Match (codearray[i]);
			Match endcommentmatch = endcomment.Match (codearray[i]);
			Match endmatch = end.Match (codearray[i]);
			Match addmatch = add.Match (codearray[i]);
			Match submatch = sub.Match (codearray[i]);
			Match mulmatch = mul.Match (codearray[i]);
			Match divmatch = div.Match (codearray[i]);
			Match modmatch = mod.Match (codearray[i]);
			Match bigmatch = big.Match (codearray[i]);
			Match smallmatch = small.Match (codearray[i]);
			Match bothmatch = both.Match (codearray[i]);
			Match eithermatch = either.Match (codearray[i]);
			Match wonmatch = won.Match (codearray[i]);
			Match notmatch = not.Match (codearray[i]);
			Match allofmatch = allof.Match (codearray[i]);
			Match anyofmatch = anyof.Match (codearray[i]);
			Match bothsaemmatch = bothsaem.Match (codearray[i]);
			Match diffrintmatch = diffrint.Match (codearray[i]);
			Match numvarwtfmatch = numvarwtf.Match (codearray [i]);



			Match gimmehmatch = gimmeh.Match (codearray[i]);
			Match assmatch = ass.Match (codearray[i]);


			if (linenumber == 1) {//IF 1ST LINE cotains HAI
				if (startmatch.Success) {
					lex.AppendValues (startmatch.Groups [1].ToString (), library ["HAI"]);
					delimiter.Push ("HAI");
					treeview1.Model = lex;
				} else {
					consoletext.Buffer.InsertAtCursor ("Syntax Error: Expected \"HAI\".\n");
					break;
				}
			} else if (linenumber == len) {	//checks if last line is KYHXBYE
				if (endmatch.Success) { //Matched KTHXBYE token
					lex.AppendValues (endmatch.Groups [1].ToString (), library ["KTHXBYE"]);
					treeview1.Model = lex;
				} else {
					consoletext.Buffer.InsertAtCursor ("Syntax Error: Expected \"KTHXBYE\".\n");
					break;
				}
			} else if (m.Success) { //Matched I HAS A token
				ihasa (m, codearray, i + 1, lex, sym);



			} else if (assmatch.Success && !codearray [i].Contains ("MEBBE")) { //Assignment token
				if (satisfy) {
					assignment (assmatch, codearray, i + 1, lex);
				}

								
			} else if (gimmehmatch.Success) {//Matched GIMMEH token
				if (satisfy) {
					if (varList.ContainsKey (gimmehmatch.Groups [2].ToString ()) == false) {
						consoletext.Buffer.InsertAtCursor ("Syntax Error: Variable Undefined\n");
						treeview1.Model = lex;
						break;
					}
					lexemeHash.Add (gimmehmatch.Groups [1].ToString ());
					lex.AppendValues (gimmehmatch.Groups [1].ToString (), library ["GIMMEH"]);
					treeview1.Model = lex;

					stringholder = gimmehmatch.Groups [2].ToString ();
					sample.inputDialog id = new sample.inputDialog ();

					if (id.Run () == (int)ResponseType.Accept) {

						sym.Clear ();
						symboltreeview.Model = sym;
						varList [gimmehmatch.Groups [2].ToString ()] = id.Text;
						foreach (string key in varList.Keys) {
							sym.AppendValues (key, varList [key]);
						}
						//symboltreeview.Model = sym;
					} 

					id.Destroy ();
				}
				 
			} else if (startmatch.Success) { //Matched HAI token
				if (linenumber != 1) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error: Unexpected \"HAI\".\n");
					break;
				} else {
					lex.AppendValues (startmatch.Groups [1].ToString (), library ["HAI"]);
					delimiter.Push ("HAI");
					treeview1.Model = lex;
				}
			} else if (printmatch.Success) { //Matched VISIBLE token
				if (satisfy) {
					visible (printmatch, codearray, i + 1, lex);
				}




			} else if (commentmatch.Success) { //Matched Comment token
				lex.AppendValues (commentmatch.Groups [1].ToString (), library ["OBTW"]);
				commentline = i;
				try {
					while (!endcommentmatch.Success) {
						endcommentmatch = endcomment.Match (codearray [commentline]);
						commentline++;

					}

				} catch (Exception) {
					syntaxError (commentline);
					break;
				}

				i = commentline;

				lex.AppendValues ("TDLR", library ["TLDR"]);

				treeview1.Model = lex;



			} else if (endmatch.Success) { //Matched KTHXBYE token
				lex.AppendValues (endmatch.Groups [1].ToString (), library ["KTHXBYE"]);
				if (!delimiter.Contains ("HAI")) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error: Program lacks program initialization.\n");
					return;
				}
				treeview1.Model = lex;

			} else if (codearray [i].Contains ("MEBBE")) {//Else if
				if (!satisfy && !mebbe) { //If satisfy and mebbe are false which means previous conditions were denied, then execute
					sym.Clear ();
					symboltreeview.Model = sym;
					varList ["IT"] = perform (codearray [i].ToString ().Replace ("MEBBE ", ""), i + 1, 0); //Evaluate if condition
					if (varList ["IT"].ToString ().Equals ("Error"))
						return;
					foreach (string key in varList.Keys) {
						sym.AppendValues (key, varList [key]);
					}

					if (varList ["IT"].Equals ("WIN") || isNumb (varList ["IT"].ToString (), numb)) {
						satisfy = true;
					} else {
						satisfy = false;
					}
				} else {
					satisfy = false; //Turn the switch off since a mebbe or an if already had a true condition.
					mebbe = true; //Flag to tell NO WAI that a mebbe turned the switch off.
				}

			} else if (addmatch.Success || submatch.Success || mulmatch.Success || divmatch.Success || modmatch.Success || bigmatch.Success || smallmatch.Success || bothmatch.Success || eithermatch.Success || wonmatch.Success || notmatch.Success || allofmatch.Success || anyofmatch.Success || bothsaemmatch.Success || diffrintmatch.Success || codearray [i].Contains ("WIN") || codearray [i].Contains ("FAIL") || codearray [i].Contains ("WTF?")) { //Operations
				if (codearray [i].Contains ("O RLY?")) { //Testing for loop

					sym.Clear ();
					symboltreeview.Model = sym;
					varList ["IT"] = perform (codearray [i].ToString ().Replace (", O RLY?", ""), i + 1, 0); //Evaluate if condition
	
					foreach (string key in varList.Keys) {
						sym.AppendValues (key, varList [key]);
					}

					if (varList ["IT"].Equals ("WIN") || isNumb (varList ["IT"].ToString (), numb)) {
						satisfy = true;
						mebbe = true;
					} else {
						satisfy = false;
					}


				} else if (codearray [i].Contains ("WTF?")) { //Switch-case
					sym.Clear ();
					symboltreeview.Model = sym;
					//Check whether expression is a number.
					Match switchcasematch = switchcase.Match(codearray[i]);
					if (isNumb (switchcasematch.Groups [1].ToString (), numb) && switchcasematch.Success && !isOperation(switchcasematch.Groups [1].ToString ())) {
						varList ["IT"] = switchcasematch.Groups [1].ToString ();
					} else if (switchcasematch.Success && !isOperation(switchcasematch.Groups [1].ToString ())) { //Is variable?
						try {
							varList ["IT"] = varList [switchcasematch.Groups [1].ToString ()];
						} catch (Exception) {
							consoletext.Buffer.InsertAtCursor (library ["noVariable"] + switchcasematch.Groups [1].ToString () + " at line " + linenumber + ".");
						}
					} else { //Is operation?
						varList ["IT"] = perform (codearray [i].ToString ().Replace (", WTF?", ""), i + 1, 0); //Evaluate switch case operation
					}




					if (varList ["IT"].ToString ().Equals ("Error"))
						return;
					foreach (string key in varList.Keys) {
						sym.AppendValues (key, varList [key]);
					}

					satisfy = false; //Make false to find an OMG that will make this true


				} else {
					sym.Clear ();
					symboltreeview.Model = sym;
					varList ["IT"] = perform (codearray [i], i + 1, 0).ToString ();
					if (varList ["IT"].ToString ().Equals ("Error"))
						return;
					foreach (string key in varList.Keys) {
						sym.AppendValues (key, varList [key]);
					}

				}





			} else if (codearray [i].Contains ("OMGWTF")) { //Default Case
				if (!gtfo){
					satisfy = true;

				}
			}


			else if (codearray [i].Contains ("OMG")) {//Cases
				if (codearray [i].Equals ("OMG") || codearray[i].Equals("OMG ")) {
					consoletext.Buffer.InsertAtCursor ("Syntax error at linenumber " + linenumber + ": constant value expected at the end of line\n");
					break;
				} else {
					if (varList ["IT"].ToString ().Equals (codearray [i].Replace ("OMG ", ""))) { //Equal value
						satisfy = true;
					}
				}
				
			} else if (codearray [i].Contains ("GTFO")) { //Break function
				if (satisfy){
					satisfy = false;
					gtfo = true;
				}
			}
			else if (codearray [i].Contains ("NO WAI")) {
				if (satisfy) {
					satisfy = false; //If satisfy is true which means that YA RLY happened, switches off commands
				} else if (!satisfy && !mebbe) { //Neither an if nor a mebbe had a true condition
					satisfy = true; //YA RLY hasn't happened so code under NO WAI has to happen
				} else { //A mebbe had a true condition turned the switch off so it should remain off.
					satisfy = false;
				}
			} else if (codearray [i].Contains ("OIC")) {
				satisfy = true;
			} 


			else { //Syntax error
				if (codearray[i].ToString().Equals("") || codearray[i].ToString().Contains("OIC") || codearray[i].ToString().Contains("YA RLY")){ //Empty lines
					continue;
				}
				syntaxError(linenumber);
				return;
			}

		}



	}

	protected void ihasa(Match m, String[] codearray, int linenumber, ListStore lex, ListStore sym){
		int caller;
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		string stringregex ="(\")(.*)(\")";
		string opsregex = "(SUM OF)|(DIFF OF)|(PRODUKT OF)|(QUOSHUNT OF)|(MOD OF)|(BIGGR OF)|(SMALLR OF)|(AN)";
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		Regex str = new Regex (stringregex);
		Regex ops = new Regex (opsregex);
		string[] splitarray;

		lex.AppendValues (m.Groups [1].ToString (), library ["I HAS A"]);
		treeview1.Model = lex;

		//Check for variable redefinitions
		if (varList.Contains(m.Groups[2].ToString())){
			consoletext.Buffer.InsertAtCursor ("Redefinition of existing variable at: " + m.Groups [2].ToString () + " at line "+linenumber+".");
			return;
		}
		else if (m.Groups [2].ToString ().Equals ("HAI") || m.Groups [2].ToString ().Equals ("AN") || m.Groups [2].ToString ().Equals ("KTHXBYE")) {
			consoletext.Buffer.InsertAtCursor ("Syntax Error at line:"+linenumber+": Invalid variable name\n");
			return;
		}


		if (m.Groups [3].ToString ().Equals ("ITZ")) { //Assignment operation
			if (m.Groups [4].ToString ().Equals ("")) {
				syntaxError (linenumber);
				return;
			}
			else if (isNumb (m.Groups [4].ToString (), numb) && !isVar (m.Groups [4].ToString (), vari) && !isOps (m.Groups [4].ToString (), ops) && !isComparison(m.Groups[4].ToString())) { //Assign a number
				varList.Add (m.Groups [2].ToString (), m.Groups [4].ToString ());

				sym.AppendValues (m.Groups [2].ToString (), m.Groups [4].ToString ());
				symboltreeview.Model = sym;
				lex.AppendValues (m.Groups [2].ToString (), "Variable");
				lex.AppendValues (m.Groups [3].ToString (), "Assignment indicator");
				lex.AppendValues (m.Groups [4].ToString (), "Number Literal");
				treeview1.Model = lex;

			} else if (isVar (m.Groups [4].ToString (), vari) && !isString (m.Groups [4].ToString (), str) && !isOps (m.Groups [4].ToString (), ops) && !m.Groups [4].ToString ().Equals ("WIN") && !m.Groups [4].ToString ().Equals ("FAIL") && !isBool (m.Groups [4].ToString ()) && !isAnyAll (m.Groups [4].ToString ()) && !isComparison (m.Groups [4].ToString ())) { //Assign the value of an existing variable.
				//Check whether the source variable exists
				if (!varList.Contains (m.Groups [4].ToString ())) {//No variable
					consoletext.Buffer.InsertAtCursor (library ["noVariable"] + m.Groups [4].ToString () + " at line " + linenumber + ".");
					return;
				}

				varList.Add (m.Groups [2].ToString (), varList [m.Groups [4].ToString ()].ToString ());
				sym.AppendValues (m.Groups [2].ToString (), varList [m.Groups [4].ToString ()].ToString ());
				lex.AppendValues (m.Groups [2].ToString (), "Variable");
				lex.AppendValues (m.Groups [3].ToString (), "Assignment indicator");
				lex.AppendValues (m.Groups [4].ToString (), "Variable");
				treeview1.Model = lex;
			} else if (isString (m.Groups [4].ToString (), str) && !isOps (m.Groups [4].ToString (), ops)) { //Assigns a string to the destination variable
				Match isStr = str.Match (m.Groups [4].ToString ());

				lex.AppendValues (m.Groups [2].ToString (), "Variable");
				lex.AppendValues (m.Groups [3].ToString (), "Assignment indicator");
				lex.AppendValues (isStr.Groups [1].ToString (), "String Delimiter");
				lex.AppendValues (isStr.Groups [2].ToString (), "String Literal");
				lex.AppendValues (isStr.Groups [3].ToString (), "String Delimiter");
				treeview1.Model = lex;

				varList.Add (m.Groups [2].ToString (), isStr.Groups [2].ToString ());
				sym.AppendValues (m.Groups [2].ToString (), isStr.Groups [2].ToString ());
				symboltreeview.Model = sym;

			} else if (m.Groups [4].ToString ().Equals ("WIN") || m.Groups [4].ToString ().Equals ("FAIL")) { //Boolean
				lex.AppendValues (m.Groups [2].ToString (), "Variable");
				lex.AppendValues (m.Groups [3].ToString (), "Assignment indicator");
				lex.AppendValues (m.Groups [4].ToString (), "Boolean literal");
				varList.Add (m.Groups [2].ToString (), m.Groups [4].ToString ());
				sym.AppendValues (m.Groups [2].ToString (), m.Groups [4].ToString ());
				symboltreeview.Model = sym;
				treeview1.Model = lex;




			} else if (isComparison(m.Groups[4].ToString())) { //Comparisons

				caller = 5;
				varList.Add (m.Groups [2].ToString (), perform (codearray [linenumber - 1], linenumber, caller).ToString ());
				if (varList [m.Groups[2].ToString()].ToString ().Equals ("Error"))
					return;
				sym.AppendValues(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				symboltreeview.Model = sym;

				//Add to the lexemes table
				splitarray = codearray [linenumber - 1].Split (' ');
				lexemeprinter (splitarray, caller, lex);



			} else if (isAnyAll(m.Groups[4].ToString())) { //ANY ALL & AND ALL Boolean operations


				caller = 5;
				varList.Add (m.Groups [2].ToString (), perform (codearray [linenumber - 1], linenumber, caller).ToString ());
				if (varList [m.Groups[2].ToString()].ToString ().Equals ("Error"))
					return;
				sym.AppendValues(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				symboltreeview.Model = sym;

				//Add to the lexemes table
				splitarray = codearray [linenumber - 1].Split (' ');
				lexemeprinter (splitarray, caller, lex);



			} else if (isBool(m.Groups[4].ToString())) { //Boolean operations
				caller = 5;
				varList.Add(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				sym.AppendValues(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				symboltreeview.Model = sym;

				//Add to the lexemes table
				splitarray = codearray [linenumber - 1].Split (' ');
				lexemeprinter (splitarray, caller, lex);



			} else { //Arithmetic Operations

				caller = 5;
				varList.Add(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				sym.AppendValues(m.Groups[2].ToString(), perform(codearray[linenumber-1], linenumber, caller).ToString());
				symboltreeview.Model = sym;

				//Add to the lexemes table
				splitarray = codearray [linenumber - 1].Split (' ');
				lexemeprinter (splitarray, caller, lex);
			}

		} else {
			varList.Add (m.Groups [2].ToString (), "");

			sym.AppendValues (m.Groups [2].ToString (), varList [m.Groups [2].ToString ()].ToString ());
			symboltreeview.Model = sym;
		}

	}

	protected void assignment(Match assmatch, String[] codearray, int linenumber, ListStore lex){
		if (!varList.Contains(assmatch.Groups[1].ToString())){ //No declared variable
			consoletext.Buffer.InsertAtCursor (library ["noVariable"] + assmatch.Groups [1].ToString () + " at line " + linenumber + ".");
			return;
		}
		int caller;
		Gtk.ListStore sym = new Gtk.ListStore (typeof (string), typeof (string));
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		string stringregex ="(\")(.*)(\")";
		string opsregex = "(SUM OF)|(DIFF OF)|(PRODUKT OF)|(QUOSHUNT OF)|(MOD OF)|(BIGGR OF)|(SMALLR OF)|(AN)";
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		Regex str = new Regex (stringregex);
		Regex ops = new Regex (opsregex);
		string[] splitarray;

		if (isNumb (assmatch.Groups [3].ToString (), numb) && !isVar (assmatch.Groups [3].ToString (), vari) && !isOps (assmatch.Groups [3].ToString (), ops) && !isComparison(assmatch.Groups[3].ToString())) { //Assign a number
			sym.Clear ();

			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = assmatch.Groups[3].ToString();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			lex.AppendValues (assmatch.Groups [3].ToString (), "Number Literal");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}
			treeview1.Model = lex;

		} else if (isVar (assmatch.Groups [3].ToString (), vari) && !isString (assmatch.Groups [3].ToString (), str) && !isOps (assmatch.Groups [3].ToString (), ops) && !assmatch.Groups [3].ToString ().Equals ("WIN") && !assmatch.Groups [3].ToString ().Equals ("FAIL") && !isBool (assmatch.Groups [3].ToString ()) && !isAnyAll (assmatch.Groups [3].ToString ()) && !isComparison (assmatch.Groups [3].ToString ())) { //Assign the value of an existing variable.
			//Check whether the source variable exists
			if (!varList.Contains (assmatch.Groups [3].ToString ())) {//No variable
				consoletext.Buffer.InsertAtCursor (library ["noVariable"] + assmatch.Groups [3].ToString () + " at line " + linenumber + ".");
				return;
			}
			//Check whether the destination variable exists
			if (!varList.Contains(assmatch.Groups[1].ToString())){ //No variable
				consoletext.Buffer.InsertAtCursor (library ["noVariable"] + assmatch.Groups [3].ToString () + " at line " + linenumber + ".");
				return;
			}
			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = varList[assmatch.Groups[3].ToString()].ToString();
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}


			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			lex.AppendValues (assmatch.Groups [3].ToString (), "Variable");
			treeview1.Model = lex;
		} else if (isString (assmatch.Groups [3].ToString (), str) && !isOps (assmatch.Groups [3].ToString (), ops)) { //Assigns a string to the destination variable
			//Check whether the destination variable exists
			if (!varList.Contains(assmatch.Groups[1].ToString())){ //No variable
				consoletext.Buffer.InsertAtCursor (library ["noVariable"] + assmatch.Groups [1].ToString () + " at line " + linenumber + ".");
				return;
			}



			Match isStr = str.Match (assmatch.Groups [3].ToString ());
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			lex.AppendValues (isStr.Groups [1].ToString (), "String Delimiter");
			lex.AppendValues (isStr.Groups [2].ToString (), "String Literal");
			lex.AppendValues (isStr.Groups [3].ToString (), "String Delimiter");
			treeview1.Model = lex;

			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = isStr.Groups[2].ToString();
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}

		} else if (assmatch.Groups [3].ToString ().Equals ("WIN") || assmatch.Groups [3].ToString ().Equals ("FAIL")) { //Boolean
			//Check whether the destination variable exists
			if (!varList.Contains(assmatch.Groups[1].ToString())){ //No variable
				consoletext.Buffer.InsertAtCursor (library ["noVariable"] + assmatch.Groups [1].ToString () + " at line " + linenumber + ".");
				return;
			}
			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = assmatch.Groups[3].ToString();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			lex.AppendValues (assmatch.Groups [3].ToString (), "Boolean Literal");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}
			treeview1.Model = lex;




		} else if (isComparison(assmatch.Groups[3].ToString())) { //Comparisons
			caller = 2;
			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = perform (codearray [linenumber - 1], linenumber, caller).ToString ();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);


		} else if (isAnyAll(assmatch.Groups[3].ToString())) { //ANY ALL & AND ALL Boolean operations

			//label1.Text = "isanyall";
			caller = 2;
			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = perform (codearray [linenumber - 1], linenumber, caller).ToString ();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);




		} else if (isBool(assmatch.Groups[3].ToString())) { //Boolean operations
			caller = 2;

			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = perform (codearray [linenumber - 1], linenumber, caller).ToString ();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);

		} else { //Arithmetic Operations

			caller = 2;

			sym.Clear ();
			symboltreeview.Model = sym;
			varList [assmatch.Groups [1].ToString ()] = perform (codearray [linenumber - 1], linenumber, caller).ToString ();
			lex.AppendValues (assmatch.Groups [1].ToString (), "Variable");
			lex.AppendValues (assmatch.Groups [2].ToString (), "Assignment indicator");
			foreach (string key in varList.Keys) {
				sym.AppendValues (key, varList [key]);
			}

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);
		}
	}

	protected void visible (Match printmatch, String[] codearray, int linenumber, ListStore lex){
		int caller;
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		string stringregex ="(\")(.*)(\")";
		string opsregex = "(SUM OF)|(DIFF OF)|(PRODUKT OF)|(QUOSHUNT OF)|(MOD OF)|(BIGGR OF)|(SMALLR OF)|(AN)";
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		Regex str = new Regex (stringregex);
		Regex ops = new Regex (opsregex);
		string[] splitarray;

		if (isNumb (printmatch.Groups [2].ToString (), numb) && !isVar (printmatch.Groups [2].ToString (), vari) && !isOps (printmatch.Groups [2].ToString (), ops) && !isComparison (printmatch.Groups [2].ToString ())) { //Assign a number

			consoletext.Buffer.InsertAtCursor (printmatch.Groups [2].ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Printing Keyword");
			lex.AppendValues (printmatch.Groups [2].ToString (), "Number Literal");

			treeview1.Model = lex;



		} else if (isVar (printmatch.Groups [2].ToString (), vari) && !isString (printmatch.Groups [2].ToString (), str) && !isOps (printmatch.Groups [2].ToString (), ops) && !printmatch.Groups [2].ToString ().Equals ("WIN") && !printmatch.Groups [2].ToString ().Equals ("FAIL") && !isBool (printmatch.Groups [2].ToString ()) && !isAnyAll (printmatch.Groups [2].ToString ()) && !isComparison (printmatch.Groups [2].ToString ())) { //Assign the value of an existing variable.
			//Check whether the source variable exists
			if (!varList.Contains (printmatch.Groups [2].ToString ())) {//No variable
				consoletext.Buffer.InsertAtCursor (library ["noVariable"] + printmatch.Groups [3].ToString () + " at line " + linenumber + ".");
				return;
			}

			consoletext.Buffer.InsertAtCursor (varList[printmatch.Groups [2].ToString ()].ToString()+"\n");

			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");
			lex.AppendValues (printmatch.Groups [2].ToString (), "Variable");
			treeview1.Model = lex;
		} else if (isString (printmatch.Groups [2].ToString (), str) && !isOps (printmatch.Groups [2].ToString (), ops)) { //Assigns a string to the destination variable

			Match isStr = str.Match (printmatch.Groups [2].ToString ());
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print keyword");
			//lex.AppendValues (printmatch.Groups [2].ToString (), "Assignment indicator");
			lex.AppendValues (isStr.Groups [1].ToString (), "String Delimiter");
			lex.AppendValues (isStr.Groups [2].ToString (), "String Literal");
			lex.AppendValues (isStr.Groups [3].ToString (), "String Delimiter");
			consoletext.Buffer.InsertAtCursor (isStr.Groups [2].ToString ()+"\n");
			treeview1.Model = lex;

		} else if (printmatch.Groups [2].ToString ().Equals ("WIN") || printmatch.Groups [2].ToString ().Equals ("FAIL")) { //Boolean

			consoletext.Buffer.InsertAtCursor (printmatch.Groups [2].ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");
			lex.AppendValues (printmatch.Groups [2].ToString (), "Boolean Literal");

			treeview1.Model = lex;
		} else if (isComparison(printmatch.Groups[2].ToString())) { //Comparisons
			caller = 1;
			consoletext.Buffer.InsertAtCursor(perform (codearray [linenumber - 1], linenumber, caller).ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);

		} else if (isAnyAll(printmatch.Groups[2].ToString())) { //ANY ALL & AND ALL Boolean operations

			caller = 1;
			consoletext.Buffer.InsertAtCursor(perform (codearray [linenumber - 1], linenumber, caller).ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");

			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);

		} else if (isBool(printmatch.Groups[2].ToString())) { //Boolean operations
			caller = 1;

			consoletext.Buffer.InsertAtCursor(perform (codearray [linenumber - 1], linenumber, caller).ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");




			//Add to the lexemes table
			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);

		} else { //Arithmetic Operations

			caller = 1;

			consoletext.Buffer.InsertAtCursor(perform (codearray [linenumber - 1], linenumber, caller).ToString ()+"\n");
			lex.AppendValues (printmatch.Groups [1].ToString (), "Print Keyword");

			splitarray = codearray [linenumber - 1].Split (' ');
			lexemeprinter (splitarray, caller, lex);
			treeview1.Model = lex;
		}
	}

	protected void lexemeprinter(string[] splitarray, int caller, ListStore lex){
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		for (int i = caller; i < splitarray.Length; i++) { //Add to lexemes table
			if (splitarray [i].Equals ("BOTH") || splitarray [i].Equals ("EITHER") || splitarray[i].Equals("WON") || splitarray [i].Equals ("SUM") || splitarray [i].Equals ("DIFF") || splitarray [i].Equals ("PRODUKT") || splitarray [i].Equals ("QUOSHUNT") || splitarray [i].Equals ("MOD") || splitarray [i].Equals ("BIGGR") || splitarray [i].Equals ("SMALLR") || splitarray [i].Equals ("ANY") || splitarray [i].Equals ("ALL") && splitarray [i + 1].Equals ("OF")) {
				lex.AppendValues (splitarray [i] + " " + splitarray [i + 1], library [splitarray [i] + " " + splitarray [i + 1]]);
			} else if (splitarray [i].Equals ("NOT")) {

				lex.AppendValues (splitarray [i], "Negation Operator");

			} else if (splitarray [i].Equals ("AN")) {
				lex.AppendValues ("AN", "Value Conjunction");
			} else if (splitarray [i].Equals ("OF") || splitarray[i].Equals("SAEM")) {
			} else if (splitarray [i].Equals ("MKAY")) {
				lex.AppendValues ("MKAY", library ["MKAY"]);
			} else if (splitarray [i].Equals ("BOTH") && splitarray [i + 1].Equals ("SAEM")) {
				lex.AppendValues ("BOTH SAEM", library ["BOTH SAEM"]);
			} else if (splitarray [i].Equals ("DIFFRINT")) {
				lex.AppendValues ("DIFFRINT", library ["DIFFRINT"]);
			} else if (isNumb (splitarray [i], numb) && !isVar (splitarray [i], vari)) {
				lex.AppendValues(splitarray[i], library["number"]);

			} else if (isVar (splitarray [i], vari)) {
				lex.AppendValues(splitarray[i], library["variable"]);

			}

		}
		treeview1.Model = lex;
		
	}


	protected string perform(string expression, int linenumber, int caller){
		string variregex = "([a-zA-Z][a-zA-Z0-9]*)";
		string numregex = @"(-?[0-9]*\.?[0-9]+)";
		string stringregex ="(\")(.*)(\")";
		Regex vari = new Regex (variregex);
		Regex numb =new Regex (numregex);
		Regex str = new Regex (stringregex);
		string[] slice = expression.Split (' ');
		int testcase = 0;
		int trueorfalse = 0;
		Stack checker = new Stack();
		Stack operation = new Stack();
		object object1, object2;
		bool op1, op2;
		double opd1, opd2;
		string changer;
		Array.Reverse (slice);

		for (int i = 0; i<slice.Length-caller; i++) {
			if (slice [i].Equals ("OF") && slice [i + 1].Equals ("BOTH") && operation.Count >1) { //AND
				op1 = Convert.ToBoolean (operation.Pop ());

				op2 = Convert.ToBoolean (operation.Pop ());
				operation.Push (op1 && op2);
				i++; //Skips the following BOTH and disregards it as a keyword not as a variable.

			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("EITHER") && operation.Count > 1) { //OR

				op1 = Convert.ToBoolean (operation.Pop ());
				op2 = Convert.ToBoolean (operation.Pop ());
				operation.Push (op1 || op2);
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("WON") && operation.Count > 1) { //XOR

				op1 = Convert.ToBoolean (operation.Pop ());
				op2 = Convert.ToBoolean (operation.Pop ());
				operation.Push (op1 ^ op2);
				i++;
			} else if (slice [i].Equals ("NOT") && operation.Count > 0) { //NEGATION

				op1 = Convert.ToBoolean (operation.Pop ());
				operation.Push (!op1);

			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("SUM")) {//ADD
		
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (opd1 + opd2);
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}

				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("DIFF")) {//DIFF
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (opd1 - opd2);
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("PRODUKT")) {//MUL
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (opd1 * opd2);
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("QUOSHUNT")) {//DIV
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (opd1 / opd2);
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("MOD")) {//MOD
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (opd1 % opd2);
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("BIGGR")) {//BIGGR
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (Math.Max (opd1, opd2));
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("SMALLR")) {//SMALLR
				try {
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd1 = 1;
					} else if (changer.Equals ("False")) {
						opd1 = 0;
					} else {
						opd1 = Convert.ToDouble (changer);
					}
					changer = operation.Pop ().ToString ();
					if (changer.Equals ("True")) {
						opd2 = 1;
					} else if (changer.Equals ("False")) {
						opd2 = 0;
					} else {
						opd2 = Convert.ToDouble (changer);
					}
					operation.Push (Math.Min (opd1, opd2));
				} catch (Exception) {
					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}
				i++;
			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("ANY") && operation.Count != 0) { //ANY OF
					
				while (!operation.Peek ().Equals ("MKAY")) {
					checker.Push (operation.Pop ());
				}
				operation.Pop (); //Get rid of the dangling MKAY
				
				foreach (object test in checker) {
					if (!Convert.ToBoolean (test)) {
						
						testcase++;
					}
				}
				//consoletext.Buffer.Text = checker.Count.ToString ();
				if (testcase == checker.Count) {

					operation.Push (false);
				} else {
					operation.Push (true);
				}
				i++;
				checker.Clear ();
				testcase = 0;

			} else if (slice [i].Equals ("OF") && slice [i + 1].Equals ("ALL") && operation.Count != 0) { //ALL OF
				while (!operation.Peek ().Equals ("MKAY")) {
					checker.Push (operation.Pop ());
				}

				operation.Pop (); //Get rid of the dangling MKAY

				foreach (object test in checker) {
					if (!Convert.ToBoolean (test)) { //If one of them is false, return FAIL
						operation.Push (false);
						trueorfalse++;

					}
				}
				if (trueorfalse == 0) {
					operation.Push (true);

				} else {
					trueorfalse = 0;
				}
				i++;
				checker.Clear ();

			} else if (slice [i].Equals ("SAEM") && slice [i + 1].Equals ("BOTH") && operation.Count != 0) { //Equality checker

				object1 = operation.Pop ();
				object2 = operation.Pop ();

				if (object1.GetType ().Equals (object2.GetType ())) { //Same types
					if (object1.ToString ().Equals (object2.ToString ())) {
						operation.Push (true);
					} else
						operation.Push (false);
				} else {
					operation.Push (false);
				}


				i++; //Skips the following BOTH and disregards it as a keyword not as a variable.

			} else if (slice [i].Equals ("DIFFRINT") && operation.Count != 0) { //Inequality checker

				object1 = operation.Pop ();
				object2 = operation.Pop ();

				if (object1.GetType ().Equals (object2.GetType ())) { //Same types
					if (object1.ToString ().Equals (object2.ToString ())) {
						operation.Push (false);
					} else
						operation.Push (true);
				} else {
					operation.Push (true);
				}

			} else if (slice [i].Equals ("MKAY")) { //Any and All of delimiter
				operation.Push ("MKAY");
			} else if (slice [i].Equals ("AN") || slice [i].Equals ("OF")) { //Disregard these keywords
			} else if (slice [i].Equals ("WIN") || slice [i].Equals ("FAIL")) { //Boolean literal
				if (slice [i].Equals ("WIN"))
					op1 = true;
				else
					op1 = false;
				operation.Push (op1);



			} else if (isString (slice [i], str)) { //Push string value with ""
				operation.Push (slice [i]);

			} else if (isNumb (slice [i], numb) && !isVar (slice [i], vari)) { //Number literal
				opd1 = Convert.ToDouble (slice [i]);
				operation.Push (opd1);



			} else if (isVar (slice [i], vari)) { //Variable value
				//label1.Text = slice[i].ToString();
				try {
					if (varList [slice [i]].ToString ().Equals ("WIN")) {
						op1 = true;
						operation.Push (op1);
					} else if (varList [slice [i]].ToString ().Equals ("FAIL")) {
						op1 = false;
						operation.Push (op1);
					} else if (isNumb (varList [slice [i]].ToString (), numb)) {
						try {
							opd1 = Convert.ToDouble (varList [slice [i]]);
							operation.Push (opd1);
						} catch (FormatException) {
							consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
							return "Error";
						}
					} else { //String
						operation.Push ("\"" + varList [slice [i]].ToString () + "\"");

					}


				} catch (Exception) {

					consoletext.Buffer.InsertAtCursor ("Syntax Error at linenumber "+linenumber+": Unable to cast value\n");
					return "Error";
				}

			
			}

		}



		if (isOperation(slice[slice.Length - 1 - caller])){
			try {
				return operation.Pop().ToString();
			} catch(Exception){
				syntaxError (linenumber);
			}
			
		}
		try {
			if (operation.Pop ().Equals (true))
				return "WIN";
			else
				return "FAIL";
		} catch(Exception){
			syntaxError (linenumber);
		}

		return "Error";

	}





	protected bool isOperation (string value){
		if (value.Contains ("SUM") || value.Contains ("DIFF") || value.Contains ("PRODUKT") || value.Contains ("QUOSHUNT") || value.Contains ("MOD") || value.Contains ("BIGGR") || value.Contains ("SMALLR")) {
			return true;
		} else
			return false;
	}

	protected bool isComparison (string value){
		if (!value.Contains ("BOTH SAEM") && !value.Contains ("DIFFRINT")) {
			return false;
		} else
			return true;
		
	}

	protected bool isAnyAll (string value){

		if (!value.Contains ("ANY OF") && !value.Contains ("ALL OF")) {
			return false;
		} else
			return true;
	}

	protected bool isBool(string value){
		if (!value.Contains ("BOTH OF") && !value.Contains ("EITHER OF") && !value.Contains ("WON OF") && !value.Contains ("NOT")) {
			return false;
		} else
			return true;
	}

	protected bool isNumb(string value, Regex numb){
		Match isNumber = numb.Match(value);
		if (isNumber.Success) { 
			return true;
		} else
			return false;
	}

	protected bool isVar(string value, Regex vari){
		Match isVariable = vari.Match (value);
		if (isVariable.Success) {
			return true;
		} else
			return false;
	}

	protected bool isString(string value, Regex str){
		Match isStr = str.Match (value);
		if (isStr.Success) {
			return true;
		} else
			return false;
	}

	protected bool isOps(string value, Regex ops){
		Match isOperations = ops.Match (value);
		if (isOperations.Success) {
			return true;
		} else
			return false;
	}

	protected void syntaxError(int linenumber){
		consoletext.Buffer.Clear ();
		consoletext.Buffer.InsertAtCursor ("Syntax error at line " + linenumber + ".\n");


	}


	protected void OnFilechooserbutton1FileActivated (object sender, EventArgs e)
	{
		
	}


	protected void OnButton3Clicked (object sender, EventArgs e)
	{
		waitingForInput = false;

	}



}
