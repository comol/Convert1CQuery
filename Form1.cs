using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convert1CQuery
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var queryFull = textBox1.Text;

			queryFull = queryFull.Replace("exec sp_executesql N'", "");

			var variablesStartIndex = queryFull.IndexOf(",N'@P1");

			var query = queryFull.Substring(0, variablesStartIndex - 1);

			var queryVariables = queryFull.Substring(variablesStartIndex, queryFull.Length - variablesStartIndex);

			queryVariables = queryVariables.Remove(0, 3);

			var variablesDeclarations = queryVariables.Substring(0, queryVariables.IndexOf("'")).Split(',');

			var variableDeclarationsNames = variablesDeclarations.Select(x => x.Substring(0, x.IndexOf(' '))).ToArray();

			var variablesValues = queryVariables.Substring(queryVariables.IndexOf("'") + 2, queryVariables.Length - queryVariables.IndexOf("'") - 2).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			if (variableDeclarationsNames.Length != variablesValues.Length)
				throw new ArgumentOutOfRangeException("Количество элементов и значений не равно");

			for (int i = variableDeclarationsNames.Length - 1; i >= 0; i--)
			{
				query = query.Replace(variableDeclarationsNames[i], variablesValues[i]);
			}

			textBox2.Text = query/*.Replace("''","'")*/;
		}
	}
}