using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    public Tilemaps map;

    List<Node> visited = new List<Node>();
    List<Node> unvisited = new List<Node>();

    Dictionary<Node, Node> predecessorDict = new Dictionary<Node, Node>();
    Dictionary<Node, float> distanceDict = new Dictionary<Node, float>();
    Dictionary<Node, float> actualDistanceDict = new Dictionary<Node, float>();

    public void Init(Tilemaps tileMap)
    {
        map = tileMap;

        List<Node> nodes = map.GetAllNodes();
        foreach (Node node in nodes)
        {
            distanceDict[node] = float.MaxValue;
            actualDistanceDict[node] = float.MaxValue;//
        }
    }

    public List<Node> Search(Node start, Node goal)
    {
        // 1. dist[s] = 0
        // 2. set all other distances to infinity
        List<Node> nodes = map.GetAllNodes();
        foreach (Node node in nodes)
        {
            distanceDict[node] = float.MaxValue;
            actualDistanceDict[node] = float.MaxValue;
        }
        distanceDict[start] = 0;
        actualDistanceDict[start] = 0;

        // 3. Initialize S(visited) and Q(unvisited)
        //    S, the set of visited nodes is initially empty
        //    Q, the queue initially conatains all nodes
        visited.Clear();
        unvisited.Clear();
        foreach (Node n in map.GetAllNodes())
        {
            unvisited.Add(n);
        }

        predecessorDict.Clear(); // to generate the result path

        while (unvisited.Count > 0)
        {
            // 4. select element of Q with the minimum distance
            Node u = GetClosestFromUnvisited();

            // Check if the node u is the goal.            
            if (u == goal) break;

            // 5. add u to list of S(visited)            
            visited.Add(u);

            foreach (Node v in map.GetNeighbors(u))
            {
                if (visited.Contains(v))
                    continue;

                // 6. If new shortest path found then set new value of shortest path                
                if (distanceDict[v] > actualDistanceDict[u] + map.GetNeighborDistance(u, v) + map.GetEstimatedDistance(v, goal))
                {
                    distanceDict[v] = actualDistanceDict[u] + map.GetNeighborDistance(u, v) + map.GetEstimatedDistance(v, goal);
                }

                if (actualDistanceDict[v] > actualDistanceDict[u] + map.GetNeighborDistance(u, v))
                {
                    actualDistanceDict[v] = actualDistanceDict[u] + map.GetNeighborDistance(u, v);
                    // update predecessorDict to build the result path
                    predecessorDict[v] = u;
                }
            }
        }

        List<Node> path = new List<Node>();

        // Generate the shortest path
        path.Add(goal);
        Node p = predecessorDict[goal];

        while (p != start)
        {
            path.Add(p);
            p = predecessorDict[p];
        }
        path.Add(p);
        path.Reverse();

        List<Node> smoothedPath = GetSmoothedPath(path);
        return smoothedPath;
    }

    List<Node> GetSmoothedPath(List<Node> inputPath)
    {
        // To do: Comment out the following line: "return inputPath;"
        //return inputPath;

        if (inputPath.Count == 2)
        {
            // If the path is only two nodes long, then we can’t smooth it, so return
            return inputPath;
        }

        List<Node> outputPath = new List<Node>();
        outputPath.Add(inputPath[0]);

        // Keep track of where we are in the input path. 
        // We start at 2, because we assume two adjacent nodes will pass the raycast
        int inputIndex = 2;

        while (inputIndex < inputPath.Count - 1) // Loop until we find the last item in the input
        {
            // To do: Complete this while loop
            // use raycast to check if there is an obstacle between two nodes
            // if the ray test failed, then add the last node that passed to the output list
            // For more detailed information, refer to Path Smoothing Algorithm in the lecture slide
            Vector2 outputVector = new Vector2(outputPath[outputPath.Count - 1].x, outputPath[outputPath.Count - 1].y);
            Vector2 inputVector = new Vector2(inputPath[inputIndex].x, inputPath[inputIndex].y);
            Vector2 dir = inputVector - outputVector;

            // Raycast needs origin Vector and Direction
            RaycastHit2D rayCast = Physics2D.Raycast(inputVector, dir, dir.magnitude);

            if (!rayCast)
            {
                outputPath.Add(inputPath[inputIndex - 1]);
            }
            inputIndex++;
        }
        // We’ve reached the end of the input path, add the end node to the output and return it
        outputPath.Add(inputPath[inputPath.Count - 1]);
        return outputPath;
    }

    Node GetClosestFromUnvisited()
    {
        float shortest = float.MaxValue;
        Node shortestNode = null;
        foreach (var node in unvisited)
        {
            if (shortest > distanceDict[node])
            {
                shortest = distanceDict[node];
                shortestNode = node;
            }
        }

        unvisited.Remove(shortestNode);
        return shortestNode;
    }
}
