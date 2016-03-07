using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Map: IMap
    {
        private IMapCell[,] _map;

        private void CheckMapConditions(int sizeY,int sizeX)
        {
            if (sizeX < 3 || sizeY < 3)
                throw new InvalidMapSizeException("Minimum size of map is 3x3");
        }

        public Map(int sizeY,int sizeX)
        {
            CheckMapConditions(sizeY, sizeX);

            _map = new IMapCell[sizeY, sizeX];
        }

        public Map(int sizeY, int sizeX, params int[] values)
        {
            CheckMapConditions(sizeY, sizeX);

            _map = new IMapCell[sizeY, sizeX];

            if(values.Length!=SizeX*sizeY)
                throw new InvalidMapSizeException("Map size and the number of passed values do not match.");

            for (int iy = 0; iy < SizeY; ++iy)
                for (int ix = 0; ix < SizeX; ++ix)
                    _map[iy, ix] = new MapCell(values[iy * SizeX + ix], iy, ix);
        }

        public void GenerateRandomMap()
        {
            Random r = new Random();

            for (int iy = 0; iy < SizeY; ++iy)
                for (int ix = 0; ix < SizeX; ++ix)
                    _map[iy, ix] = new MapCell(r.Next(255), iy, ix);

            _map[0, 0] = new MapCell(0, 0, 0);
            _map[SizeY - 1, SizeX - 1] = new MapCell(0, SizeY - 1, SizeX - 1);
        }

        public IMapCell GetCell(int y, int x)
        {
            return _map[y, x];
        }

        public void AddCell(IMapCell c)
        {
            _map[c.Y, c.X]=c;
        }

        //always get sizes by GetLength or add private fields which would be initialised in constructor?
        //having these fields would be faster than runing getlength each time, but it is also keeping data in more than one place.
        public int SizeY { get { return _map.GetLength(0); } }

        public int SizeX { get { return _map.GetLength(1); } }

    }
}
