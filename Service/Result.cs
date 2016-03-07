using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class Result : IResult
    {
        private IMapCell _mapCell;

        private int _numberOfVisitedCells;

        private bool _success;

        public Result(IMapCell cell, int numberOfVisitedCells, bool success)
        {
            _mapCell=cell;
            _numberOfVisitedCells=numberOfVisitedCells;
            _success = success;
        }

        public bool success { get { return _success; } }

        public IMapCell mapCell { get { return _mapCell; } }

        public int numberOfVisitedCells { get { return _numberOfVisitedCells; } }
    }
}
