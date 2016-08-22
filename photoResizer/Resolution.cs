using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoResizer
{
    class Resolution
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Resolution(int width, int height)
        {
            Setup(width, height);
        }

        public Resolution(string resolution)
        {
            var size = Array.ConvertAll(resolution.Split('x'), int.Parse);
            Setup(size[0], size[1]);
        }

        private void Setup(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
