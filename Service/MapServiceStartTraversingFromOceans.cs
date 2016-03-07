using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class MapServiceStartTraversingFromOceans : AMapServiceTemplate
    {
        //mapServiceDFS, BFS and DFSTurnStackUpsideDown are slow they have O(n^2) complexity.
        //so let's try doing it bettr. We will start traversing from oceans with opposite condition - water can flow only to higher fields.
        //The set of sources is the intersec of both sets.
        //In this case it doesn't matter that we will use DFS or BFS for traverse. 

        public MapServiceStartTraversingFromOceans(IMap map):base(map){}

        protected override bool CheckFlowBetweenCells(IMapCell from, IMapCell to)
        {
            return to.Heigh >= from.Heigh;
        }

        public override IServiceResult FindAllRiverSourcesWhichFlowToBothOceans()
        {
            IEnumerable<IMapCell> cellsReachablefromPacific=GetAllReachableCells(_map.GetCell(0, 0));
            IEnumerable<IMapCell> cellsReachablefromAtlantic=GetAllReachableCells(_map.GetCell(_map.SizeY - 1, _map.SizeX - 1));

            return new ServiceResult(cellsReachablefromPacific.Intersect(cellsReachablefromAtlantic),cellsReachablefromPacific.Count()+cellsReachablefromAtlantic.Count());
        }

        private IEnumerable<IMapCell> GetAllReachableCells(IMapCell fromCell)
        {
            Queue<IMapCell> q = new Queue<IMapCell>();
            ICollection<IMapCell> visitedCells = new HashSet<IMapCell>();

            IMapCell cell = fromCell;

            q.Enqueue(cell);

            while (q.Count > 0)
            {
                visitedCells.Add(cell);

                IEnumerable<IMapCell> possibleNeighbours = GetPossibleNeighbours(cell, visitedCells);
                possibleNeighbours.ToList().ForEach(pn => q.Enqueue(pn));
                cell = q.Dequeue();
            }

            return visitedCells;
        }
    }
}
