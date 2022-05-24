using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLib.Abstract
{
    public interface IGraph
    {
        public int Size { get; }

        bool HasPath(int from, int to);

        void SetPath(int from, int to);

        void RemovePath(int from, int to);
    }
}
