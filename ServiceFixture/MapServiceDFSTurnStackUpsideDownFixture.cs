using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;
using NUnit.Framework;

namespace ServiceFixture
{
    [TestFixture]
    class MapServiceDFSTurnStackUpsideDownFixture
    {


        [Test]
        public void testDFSTurnStackUpsideDownNoHits()
        {
            //0     101       199       139     168     210     45   
            //107   104       246       183     115     113     171
            //122   209       42        12      15      7       219
            //41    146       210       1       53      175     210
            //203   62        182       52      163     224     242
            //151   28        105       222     140     196     0

            IMap map = new Map(6, 7, 0, 101, 199, 139, 168, 210, 45, 107, 104, 246, 183, 115, 113, 171, 122, 209, 42, 12, 15, 7, 219, 41, 146, 210, 1, 53, 175, 210,
                203, 62, 182, 52, 163, 224, 242, 151, 28, 105, 222, 140, 196, 0);

            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);

            IEnumerable<IMapCell> sources = service.FindAllRiverSourcesWhichFlowToBothOceans().Sources;

            Assert.AreEqual(0, sources.Count(), "Wrong number of sources.");
        }

        [Test]
        public void testDFSTurnStackUpsideDownOneHit()
        {
            //0     23       246       245     225     205     88   
            //114   98       220       129     241     112     254
            //173   207      231       246     125     57      251
            //22    70       105       146     127     113     188
            //55   33        158       107     19      71      137
            //73   92        80        155     161     13      0

            IMap map = new Map(6, 7, 0, 23, 246, 245, 225, 205, 88, 114, 98, 220, 129, 241, 112, 254, 173, 207, 231, 246, 125, 57, 251,
                22, 70, 105, 146, 127, 113, 188, 55, 33, 158, 107, 19, 71, 137, 73, 92, 80, 155, 161, 13, 0);

            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);
            IEnumerable<IMapCell> sources = service.FindAllRiverSourcesWhichFlowToBothOceans().Sources;

            Assert.AreEqual(1, sources.Count(), "Wrong number of sources.");
            Assert.IsTrue(sources.Contains(map.GetCell(2, 3)), "Wrong source.");
        }

        [Test]
        public void testBFSMultipleHits()
        {
            //0     1       10   
            //10    8       10
            //10    1       0
            IMap map = new Map(3, 3, 0, 1, 10, 10, 8, 10, 10, 1, 0);

            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);

            IEnumerable<IMapCell> sources = service.FindAllRiverSourcesWhichFlowToBothOceans().Sources;

            Assert.AreEqual(5, sources.Count(), "Wrong number of sources.");

            Assert.IsTrue(sources.Contains(map.GetCell(1, 1)), "Missing source.");
            Assert.IsTrue(sources.Contains(map.GetCell(1, 0)), "Missing source.");
            Assert.IsTrue(sources.Contains(map.GetCell(2, 0)), "Missing source.");
            Assert.IsTrue(sources.Contains(map.GetCell(0, 2)), "Missing source.");
            Assert.IsTrue(sources.Contains(map.GetCell(1, 2)), "Missing source.");
        }

        [Test]
        public void testDFSTurnStackUpsideDownNumberOfVisitedCells1()
        {
            //0     23       246       245     225     205     88   
            //114   98       220       129     241     112     254
            //173   207      231       246     125     57      251
            //22    70       105       146     127     113     188
            //55   33        158       107     19      71      137
            //73   92        80        155     161     13      0

            IMap map = new Map(6, 7, 0, 23, 246, 245, 225, 205, 88, 114, 98, 220, 129, 241, 112, 254, 173, 207, 231, 246, 125, 57, 251,
                22, 70, 105, 146, 127, 113, 188, 55, 33, 158, 107, 19, 71, 137, 73, 92, 80, 155, 161, 13, 0);

            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);
            IServiceResult result = service.FindAllRiverSourcesWhichFlowToBothOceans();

            Assert.AreEqual(203, result.TotalCountOfVisitedCells, "Wrong number of visited cells for DFSTurnStackUpsideDown.");
        }

        [Test]
        public void testDFSTurnStackUpsideDownNumberOfVisitedCells2()
        {
            //0     1       10   
            //10    8       10
            //10    1       0
            IMap map = new Map(3, 3, 0, 1, 10, 10, 8, 10, 10, 1, 0);

            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);

            IServiceResult result = service.FindAllRiverSourcesWhichFlowToBothOceans();

            Assert.AreEqual(19, result.TotalCountOfVisitedCells, "Wrong number of visited cells for DFSTurnStackUpsideDown.");
        }

    }
}
