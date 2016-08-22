using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ConfigModel
    {
        public string InputFolder { get; set; }
        public string OutputFolder { get; set; }
        public string Resolutions { get; set; }
        public int ThreadCount { get; set; }
        public int ThreadCapacity { get; set; }
    }
}