using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class MapServiceDFS : AMapServiceEachCelTemplate
    {
        public MapServiceDFS(IMap map):base(map){}

        protected override IResult CheckCell(IMapCell cell)
        {
            ICollection<IMapCell> visitedCells = new HashSet<IMapCell>();
            bool pacificConnected = false;
            bool atlanticConnectected = false;
            bool success=CheckCellRekursion(cell, visitedCells, ref pacificConnected, ref atlanticConnectected);

            IResult result = new Result(cell, visitedCells.Count(), success);

            return result;
        }

        private bool CheckCellRekursion(IMapCell cell, ICollection<IMapCell> visitedCells, ref bool pacificConnected, ref bool atlanticConnectected)
        {
            visitedCells.Add(cell);

            IEnumerable<IMapCell> possibleNeighbours = GetPossibleNeighbours(cell, visitedCells);

            if (possibleNeighbours.Contains(_map.GetCell(0, 0)))
                pacificConnected = true;
            if (possibleNeighbours.Contains(_map.GetCell(_map.SizeY - 1, _map.SizeX - 1)))
                atlanticConnectected = true;
            if (pacificConnected && atlanticConnectected)
                return true;

            //There is more likely that higher neigbourhood will not be a dead end
            foreach (IMapCell pn in possibleNeighbours.OrderByDescending(n => n.Heigh))
                if (CheckCellRekursion(pn, visitedCells, ref pacificConnected, ref atlanticConnectected))
                    return true;

            return false;
        }

    }
}
