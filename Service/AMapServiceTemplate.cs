using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public abstract class AMapServiceTemplate : IMapService
    {
        protected IMap _map;

        public AMapServiceTemplate(IMap map)
        {
            _map = map;
        }

        public abstract IServiceResult FindAllRiverSourcesWhichFlowToBothOceans();

        protected virtual IEnumerable<IMapCell> GetPossibleNeighbours(IMapCell from, ICollection<IMapCell> visitedCells)
        {
            ICollection<IMapCell> possibleNeighbours = new HashSet<IMapCell>();
            IMapCell neighbour;

            if (from.Y > 0)
            {
                neighbour = _map.GetCell(from.Y - 1, from.X);
                if (!visitedCells.Contains(neighbour) && CheckFlowBetweenCells(from, neighbour))
                    possibleNeighbours.Add(neighbour);
            }

            if (from.Y < _map.SizeY - 1)
            {
                neighbour = _map.GetCell(from.Y + 1, from.X);
                if (!visitedCells.Contains(neighbour) && CheckFlowBetweenCells(from, neighbour))
                    possibleNeighbours.Add(neighbour);
            }

            if (from.X > 0)
            {
                neighbour = _map.GetCell(from.Y, from.X - 1);
                if (!visitedCells.Contains(neighbour) && CheckFlowBetweenCells(from, neighbour))
                    possibleNeighbours.Add(neighbour);
            }

            if (from.X < _map.SizeX - 1)
            {
                neighbour = _map.GetCell(from.Y, from.X + 1);
                if (!visitedCells.Contains(neighbour) && CheckFlowBetweenCells(from, neighbour))
                    possibleNeighbours.Add(neighbour);
            }

            return possibleNeighbours;
        }

        protected virtual bool CheckFlowBetweenCells(IMapCell from, IMapCell to)
        {
            return to.Heigh <= from.Heigh;
        }
    }
}
