using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using photoResizer.config;

namespace photoResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigManager();
            var photoManager = new PhotoManager(config);

            photoManager.Resize();
        }
    }

}
