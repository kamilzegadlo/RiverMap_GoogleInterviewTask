using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class MapServiceBFS :AMapServiceEachCelTemplate
    {
        public MapServiceBFS(IMap map):base(map){}

        protected override IResult CheckCell(IMapCell cell)
        {
            Queue<IMapCell> q = new Queue<IMapCell>();
            ICollection<IMapCell> visitedCells = new HashSet<IMapCell>();
            bool pacificConnected = false;
            bool atlanticConnectected = false;
            int startTime = System.Environment.TickCount;
            bool success = false;
            IMapCell startCell = cell;

            q.Enqueue(cell);

            while(q.Count>0)
            {
                visitedCells.Add(cell);

                IEnumerable<IMapCell> possibleNeighbours = GetPossibleNeighbours(cell, visitedCells);
                possibleNeighbours.ToList().ForEach(pn => q.Enqueue(pn));

                if (possibleNeighbours.Contains(_map.GetCell(0, 0)))
                    pacificConnected = true;
                if (possibleNeighbours.Contains(_map.GetCell(_map.SizeY - 1, _map.SizeX - 1)))
                    atlanticConnectected = true;
                if (pacificConnected && atlanticConnectected)
                {
                    success = true;
                    break;
                }

                cell = q.Dequeue();
            }

            return new Result(startCell, visitedCells.Count(), success);
        }
    }
}
