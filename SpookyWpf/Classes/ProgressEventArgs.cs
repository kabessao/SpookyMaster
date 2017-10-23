using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spooky
{
    class ProgressEventArgs : EventArgs
    {
        private double _progress = 0;

        public double Progress
        {
            get { return _progress; }
            set { if (_progress != 100) _progress = value; }
        }


    }
}
