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

    class PhotoManager
    {
        private ConfigManager Config { get; }

        public PhotoManager(ConfigManager config)
        {
            Config = config;
        }

        public void Resize(string inputPath, string outputPath, Resolution resolution)
        {
            var captureImage = new Image<Bgr, byte>(inputPath);
            var resizedImage = captureImage.Resize(resolution.Width, resolution.Height, Inter.Cubic);
            resizedImage.Save(outputPath);
        }

        public void Resize()
        {
            var photosList = Directory.GetFiles(Config.InputFolder, "*.*").ToList();
            foreach (var photo in photosList)
            {
                foreach (var resolution in Config.Resolutions)
                {
                    Resize(photo, 
                        $"{Config.OutputFolder}{Path.GetFileNameWithoutExtension(photo)}_{resolution.Width}x{resolution.Height}{Path.GetExtension(photo)}",
                        resolution);
                }
            }
        }
       
    }

}
