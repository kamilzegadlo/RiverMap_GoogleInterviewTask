using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;

namespace RiverMap_googleInterviewQuestion
{
    class Program
    {
        static void Main(string[] args)
        {
            short y = 6;
            short x = 7;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Imagine a matrix of numbers which represent terrain.");
            Console.WriteLine("A number in a cell represent the elevation.");
            Console.WriteLine("The left top edge is the Pacific Ocean.");
            Console.WriteLine("The right bottom edge is the Atlantic Ocean.");
            Console.WriteLine("Find all cells which if we place there source of a river, the water will flow to both oceans.");
            Console.WriteLine("Remember that water flows from higher elements to lower, never opposite.");

            while (true)
            {
                IMap map = new Map(y, x);
                map.GenerateRandomMap();

                Console.WriteLine();
                Console.WriteLine("Generated map:");

                for (short iy = 0; iy < y; ++iy)
                {
                    StringBuilder line = new StringBuilder();

                    for (short ix = 0; ix < x; ++ix)
                        line.Append(map.GetCell(iy, ix).Heigh + "\t");

                    Console.WriteLine(line.ToString());
                }

                Console.WriteLine();
                Console.WriteLine("Sources:");
                IServiceResult DFSresult = DFS(map);
                if (!DFSresult.Sources.Any())
                    Console.WriteLine("No possible sources.");
                else
                {
                    PrintSources(DFSresult.Sources);
                    Console.WriteLine("Total count of visited cells in DFS: "+DFSresult.TotalCountOfVisitedCells);




                    Console.WriteLine("Sources BFS:");
                    IServiceResult BFSresult = BFS(map);
                    PrintSources(BFSresult.Sources);
                    Console.WriteLine("Total count of visited cells in BFS: " + BFSresult.TotalCountOfVisitedCells);

                    if (!DoSourcesMatch(DFSresult.Sources, BFSresult.Sources))
                        throw new Exception("Results DFS are not equal to BFS!");





                    Console.WriteLine("Sources DFS with turning stack upside down after riching one ocean:");
                    IServiceResult DFSTurnStackUpsideDownResult = DFSTurnStackUpsideDown(map);
                    PrintSources(DFSTurnStackUpsideDownResult.Sources);
                    Console.WriteLine("Total count of visited cells in DFSTurnStackUpsideDown: " + DFSTurnStackUpsideDownResult.TotalCountOfVisitedCells);

                    if (!DoSourcesMatch(DFSresult.Sources, DFSTurnStackUpsideDownResult.Sources))
                        throw new Exception("Results DFS are not equal to DFS with turning stack upside down after riching one ocean!");





                    Console.WriteLine("Sources for algorithm when we start traversing from oceans:");
                    IServiceResult TraverseFromOceansResult = TraverseFromOceans(map);
                    PrintSources(TraverseFromOceansResult.Sources);
                    Console.WriteLine("Total count of visited cells in TraverseFromOceans: " + TraverseFromOceansResult.TotalCountOfVisitedCells);

                    if (!DoSourcesMatch(DFSresult.Sources, TraverseFromOceansResult.Sources))
                        throw new Exception("Results DFS are not equal to Traverse From Oceans!");

                    Console.ReadLine();
                }
            }
        }

        private static IServiceResult DFS(IMap map)
        {
            IMapService service = new MapServiceDFS(map);

            return service.FindAllRiverSourcesWhichFlowToBothOceans();
        }

        private static IServiceResult BFS(IMap map)
        {
            IMapService service = new MapServiceBFS(map);

            return service.FindAllRiverSourcesWhichFlowToBothOceans();
        }

        private static IServiceResult DFSTurnStackUpsideDown(IMap map)
        {
            IMapService service = new MapServiceDFSTurnStackUpsideDown(map);

            return service.FindAllRiverSourcesWhichFlowToBothOceans();
        }

        private static IServiceResult TraverseFromOceans(IMap map)
        {
            IMapService service = new MapServiceStartTraversingFromOceans(map);

            return service.FindAllRiverSourcesWhichFlowToBothOceans();
        }

        private static void PrintSources(IEnumerable<IMapCell> sources)
        {
            foreach (IMapCell r in sources)
                Console.WriteLine("x:" + (r.X + 1) + " y:" + (r.Y + 1) + " h:" + r.Heigh);
        }

        private static bool DoSourcesMatch(IEnumerable<IMapCell> sources1, IEnumerable<IMapCell> sources2)
        {
            return sources1.OrderBy(s => s.X).ThenBy(s => s.Y).SequenceEqual(sources2.OrderBy(s => s.X).ThenBy(s => s.Y));
        }
    }
}
