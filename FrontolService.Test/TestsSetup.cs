using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace FrontolService.Test
{
    class TestsSetup
    {
		public static void SetUp()
		{
			//var backup = new FbRestore("database=localhost:demo.fdb;user=sysdba;password=masterkey");
			
			//FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
		}

		public static string Database()
		{
			string exe_dir = Directory.GetCurrentDirectory();
			string result = Path.Combine(exe_dir, @"Db\MAIN_DEMO.GDB");

			return result;
		}
	}
}
