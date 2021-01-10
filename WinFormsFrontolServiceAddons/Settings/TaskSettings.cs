using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontolServiceAddon
{
    public class TaskSettings
    {
        public int Interval { get; set; } = 6;
        public bool CollapseRemaind { get; set; } = true;
        public bool DeleteRemaindCollapsed { get; set; } = true;
        public bool SendToFtp { get; set; } = true;
    }
}
