using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace photoResizer
{
    class AbortHandler
    {
        public CancellationToken Token { get; private set; }
        public int Capacity { get; private set; }

        public AbortHandler(CancellationToken token, int capacity)
        {
            Token = token;
            Capacity = capacity;
        }
    }
}
