using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class ServiceResult : IServiceResult
    {
        private IEnumerable<IMapCell> _results;

        private int _totalCountrOfVisitedCells;

        public ServiceResult(IEnumerable<IMapCell> results, int totalCountrOfVisitedCells)
        {
            _results = results;
            _totalCountrOfVisitedCells = totalCountrOfVisitedCells;
        }


        public IEnumerable<IMapCell> Sources { get { return _results; } }

        public int TotalCountOfVisitedCells { get { return _totalCountrOfVisitedCells; } }
    }
}
