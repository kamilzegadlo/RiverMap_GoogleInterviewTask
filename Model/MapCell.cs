using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MapCell : IMapCell
    {
        private int _heigh;
        private int _x;
        private int _y;

        public MapCell(int heigh, int y, int x)
        {
            _heigh = heigh;
            _x = x;
            _y = y;
        }

        public int Heigh { get { return _heigh; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } } 


    }
}
