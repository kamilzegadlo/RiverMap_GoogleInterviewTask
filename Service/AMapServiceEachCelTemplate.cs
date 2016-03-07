using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public abstract class AMapServiceEachCelTemplate : AMapServiceTemplate
    {
        public AMapServiceEachCelTemplate(IMap map) : base(map) { }

        public override IServiceResult FindAllRiverSourcesWhichFlowToBothOceans()
        {
            ICollection<IMapCell> sources = new HashSet<IMapCell>();
            int totalCountOfVisitedSources=0;


            for (short iy = 0; iy < _map.SizeY; ++iy)
                for (short ix = 0; ix < _map.SizeX; ++ix)
                    if ((ix > 0 || iy > 0) && (ix < _map.SizeX - 1 || iy < _map.SizeY - 1))
                    {
                        IResult result = CheckCell(_map.GetCell(iy, ix));
                        if (result.success)
                            sources.Add(result.mapCell);
                        totalCountOfVisitedSources+=result.numberOfVisitedCells;
                    }

            return new ServiceResult(sources, totalCountOfVisitedSources);
        }

        protected abstract IResult CheckCell(IMapCell cell);

    }
}
