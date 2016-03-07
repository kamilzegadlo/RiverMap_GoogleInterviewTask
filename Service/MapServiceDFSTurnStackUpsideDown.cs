using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class MapServiceDFSTurnStackUpsideDown : AMapServiceEachCelTemplate
    {
        public MapServiceDFSTurnStackUpsideDown(IMap map) : base(map) { }

        protected override IResult CheckCell(IMapCell cell)
        {
            Stack<IMapCell> stack = new Stack<IMapCell>();
            ICollection<IMapCell> visitedCells = new HashSet<IMapCell>();
            bool pacificConnected = false;
            bool atlanticConnectected = false;
            bool success = false;
            IMapCell startCell = cell;

            stack.Push(cell);

            while(stack.Count>0)
            {
                visitedCells.Add(cell);
                IEnumerable<IMapCell> possibleNeighbours = GetPossibleNeighbours(cell, visitedCells);
                //There is more likely that higher neigbourhood will not be a dead end
                possibleNeighbours.OrderBy(n=>n.Heigh).ToList().ForEach(pn => stack.Push(pn));

                if (possibleNeighbours.Contains(_map.GetCell(0, 0)))
                {
                    pacificConnected = true;
                    TurnStackUpsideDown(ref stack);
                }
                if (possibleNeighbours.Contains(_map.GetCell(_map.SizeY - 1, _map.SizeX - 1)))
                { 
                    atlanticConnectected = true;
                    TurnStackUpsideDown(ref stack);
                }
                if (pacificConnected && atlanticConnectected)
                {
                    success = true;
                    break;
                }

                cell = stack.Pop();
            }

            return new Result(startCell, visitedCells.Count(), success);
        }

        private void TurnStackUpsideDown(ref Stack<IMapCell> stack)
        {
            Stack<IMapCell> temporaryStack = new Stack<IMapCell>();

            while (stack.Count > 0)
                temporaryStack.Push(stack.Pop());

            stack = temporaryStack;
        }

    }
}
