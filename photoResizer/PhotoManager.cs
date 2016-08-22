using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using NLog;

namespace photoResizer
{
    public class PhotoManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private ConfigManager Config { get; }

        private ConcurrentQueue<string> PhotoList { get; set; }

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

        public void Resize(string photoLink)
        {
            foreach (var resolution in Config.Resolutions)
            {
                Resize(photoLink,
                    $"{Config.OutputFolder}{Path.GetFileNameWithoutExtension(photoLink)}_{resolution.Width}x{resolution.Height}{Path.GetExtension(photoLink)}",
                    resolution);
            }
        }

        public void Resize()
        {
            var doneEvents = new ManualResetEvent[Config.ThreadCount];
            PhotoList = new ConcurrentQueue<string>(Directory.GetFiles(Config.InputFolder, "*.*").ToList());

            for (var i = 0; i < Config.ThreadCount; i++)
            {
                var threadCapacity = Config.ThreadCapacity;
                int k = i;
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    logger.Info("$0: start", Thread.CurrentThread.Name);
                    ResizeCallback(o);
                    doneEvents[k].Set();
                    logger.Info("$0: end", Thread.CurrentThread.Name);
                }, threadCapacity);
            }
            WaitHandle.WaitAll(doneEvents);
        }


        private void ResizeCallback(object capacity)
        {
            var castCapacity = (int) capacity;
            if (castCapacity <= 0) return;

            string photo;
            if (PhotoList.TryDequeue(out photo))
            {
                Resize(photo);
                ResizeCallback(--castCapacity);
            }
        }
    }
}