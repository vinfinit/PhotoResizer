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
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private ConfigManager Config { get; }

        private ConcurrentQueue<string> PhotoList { get; set; }

        private CancellationTokenSource _cts;

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

            _cts = new CancellationTokenSource();

            for (var i = 0; i < Config.ThreadCount; i++)
            {
                var threadCapacity = Config.ThreadCapacity;
                int k = i;
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(o =>
                {
                    var abortHandler = (AbortHandler)o;
                    _logger.Info("${0}: start", Thread.CurrentThread.Name);
                    ResizeCallback(abortHandler.Token, abortHandler.Capacity);
                    doneEvents[k].Set();
                    _logger.Info("${0}: end", Thread.CurrentThread.Name);
                }, new AbortHandler(_cts.Token, threadCapacity));
            }
            WaitHandle.WaitAll(doneEvents);
        }

        public void Abort()
        {
            _cts.Cancel();
        }


        private void ResizeCallback(CancellationToken token, int capacity)
        {
            if (capacity <= 0)
            {
                return;
            }

            if (token.IsCancellationRequested)
            {
                _logger.Info("${0}: abort", Thread.CurrentThread.Name);
                return;
            }

            string photo;
            if (PhotoList.TryDequeue(out photo))
            {
                Resize(photo);
                ResizeCallback(token, --capacity);
            }
        }
    }
}