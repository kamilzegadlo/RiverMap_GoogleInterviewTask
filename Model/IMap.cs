using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IMap
    {
        IMapCell GetCell(int y, int x);

        void AddCell(IMapCell c);

        void GenerateRandomMap();

        int SizeX { get; }

        int SizeY { get; }
    }
}
