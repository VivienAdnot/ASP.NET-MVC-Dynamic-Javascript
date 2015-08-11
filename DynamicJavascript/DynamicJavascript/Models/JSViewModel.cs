using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicJavascript.MVCUtilities;

namespace DynamicJavascript.Models
{
    public class JSViewModel
    {
        public int PartnerId { get; set; }
        public List<string> AllowedModuleIds { get; set; }
    }
}