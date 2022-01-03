using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch
{
    public class CountdownEventArgs : EventArgs
    {
        public TimeSpan RemainingCountdownTime { get; }
        public CountdownEventArgs(TimeSpan rct)
        {
            RemainingCountdownTime = rct;
        }
    }
}
