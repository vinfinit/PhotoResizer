using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoResizer
{
    class ConfigManager
    {
        public string OutputFolder { get; private set; }
        public string InputFolder { get; private set; }
        public int ThreadCount { get; private set; }
        public int ThreadCapacity { get; private set; }
        public List<Resolution> Resolutions { get; private set; }

        public ConfigManager()
        {
            int threadCount = 4,
                threadCapacity = 2;

            OutputFolder = ConfigurationManager.AppSettings["outputFolder"];
            InputFolder = ConfigurationManager.AppSettings["inputFolder"];

            int.TryParse(ConfigurationManager.AppSettings["threadCount"], out threadCount);
            ThreadCount = threadCount;

            int.TryParse(ConfigurationManager.AppSettings["threadCapacity"], out threadCapacity);
            ThreadCapacity = threadCapacity;

            var resolutions = ConfigurationManager.AppSettings["resolutions"].Split(';');
            Resolutions = Array.ConvertAll(resolutions, (i) => new Resolution(i)).ToList();
        }
    }
}
