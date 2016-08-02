using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
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
            PhotoManager.Resize("C:\\Users\\uladzimir_artsemenka\\Downloads\\vovchik.jpg", "C:\\Users\\uladzimir_artsemenka\\Downloads\\vovchik_resized1.jpg");
            Console.ReadLine();
        }
    }

    static class PhotoManager
    {
        public static void Resize(string inputPath, string outputPath)
        {
            var captureImage = new Image<Bgr, byte>(inputPath);
            var resizedImage = captureImage.Resize(600, 600, Inter.Cubic);
            resizedImage.Save(outputPath);
        }
    }

}
