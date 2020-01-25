using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrontolServiceAddon
{
    public class FirebirdSettings
    {
        public string Database { get; set; } = string.Empty;
        public string Login { get; set; } = "SYSDBA";
        public string Password { get; set; } = "masterkey";

        public FirebirdSettings() 
        {
            string[] drives = {"C","D","E","F" };

            foreach (string drive in drives)
            {
                string patch = drive + @":\DBFrontol\MAIN.GDB";
                if (File.Exists(patch)) 
                {
                    Database = patch;                    
                    break;
                }                                    
            }
            
        }
    }
}
