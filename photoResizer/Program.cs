using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
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
        private string OutputFolder { get; }
        private string InputFolder { get; }

        public PhotoManager(ConfigManager config)
        {
            OutputFolder = config.OutputFolder;
            InputFolder = config.InputFolder;
        }

        public void Resize(string inputPath, string outputPath)
        {
            var captureImage = new Image<Bgr, byte>(inputPath);
            var resizedImage = captureImage.Resize(600, 600, Inter.Cubic);
            resizedImage.Save(outputPath);
        }

        public void Resize()
        {
            var photosList = Directory.GetFiles(InputFolder, "*.*").ToList();
            foreach (var photo in photosList)
            {
                Resize(photo, $"{OutputFolder}{Path.GetFileNameWithoutExtension(photo)}{"_1"}{Path.GetExtension(photo)}");
            }
        }
       
    }

}
