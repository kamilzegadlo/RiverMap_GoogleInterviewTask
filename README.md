# RiverMap_GoogleInterviewTask

It's my solution for a Google interview task.

Imagine a matrix where left top corner is the Pacific Ocean and right bottom is the Atlantic Ocean. 
Find all cells where if you put a source of a river, the water will flow to both oceans.
Remember that water flows only to lower cells, never oposite.

Sounds easy - from the first glance it is a task for DFS/BFS and its obvious that DFS should be better than BFS in this case. 
And turning stack up side down after reaching one Ocean is a cool trick which performs here absolutly great.

But it's a trap. If we check each cell with BDS/DFS the compexity will be O(N^2). Very slow...
Much better will be if we change the condition for water flow to the opposite one and start from the Oceans. 
Lets find all fields reachable from each Ocean. 
The solution is the intersect of these two sets.
The compexity od the latter solution is O(N).



