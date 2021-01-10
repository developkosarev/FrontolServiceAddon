using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontolServiceAddon
{
    public class FtpServerSettings
    {
        public string Url { get; set; } = "ftp://localhost/";
        public string Login { get; set; } = "anonymous";
        public string Password { get; set; } = "anonymous";        
        public string Directory { get; set; } = "shop001";
        public string FileName { get; set; } = "sprt";
    }
}
