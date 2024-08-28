using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Types
{
	public class TestTable
	{
		public object _id = Guid.NewGuid().ToString();
		public string Naam = "Test";
		public DateTime Datum = DateTime.Now;
		public int Age = 36;
		public bool InsertTrialDataMongoDb = true;
		public ExtraValues Waarden = new ();
	}

	public class ExtraValues
	{
		public string _id { get; set; } = String.Empty;
		public string name { get; set; } = String.Empty;
	}
}
