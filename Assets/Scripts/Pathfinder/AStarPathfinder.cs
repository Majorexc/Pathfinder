using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A* (A star) Implementation of pathfinding algorithm.
/// The most common algorithm for Games, Robotics and many others.
/// It's the most flexible/resource-intensive algorithm
/// There is an AStar optimized algorithm names JPS (Jump Point Search).
/// But it fits only for uniform-cost field.
/// Due to heuristic as a part of AStar is better if we want to add some types of ground which changes cost (like mud or smth else).
/// Or if we want to found more smooth path.
///
/// If we speak about this implementation. There are some points which can be optimized.
/// At first sight:
///  * We can use bitmask for field model to decrease memory usage.
///  * We can use heap for openSet. It will greatly reduce time spent to finding a path
///  * More ...
/// </summary>
public class AStarPathfinder : APathfinder {
    public override Node[] FindPath(Vector2Int startPos, Vector2Int targetPos) {
        Node startNode = _field[startPos.x, startPos.y];
        Node targetNode = _field[targetPos.x, targetPos.y];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        
        openSet.Add(startNode);
        
        // Algorithm will be completed when openSet becomes empty
        while (openSet.Count > 0) {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].FCost < node.FCost || Mathf.Approximately(openSet[i].FCost, node.FCost)) {
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);
            
            // Or if we find target node
            if (node == targetNode) {
                var path = RetracePath(startNode, targetNode);
                return path;
            }

            foreach (Node neighbour in GetNeighbours(node)) {
                if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                float neighborCost = node.GCost + GetHeuristic(node, neighbour);
                if (neighborCost < neighbour.GCost || !openSet.Contains(neighbour)) {
                    neighbour.GCost = neighborCost;
                    neighbour.HCost = GetHeuristic(neighbour, targetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return null;
    }

    Node[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        
        path.Add(startNode);
        path.Reverse();
        
        return path.ToArray();
    }
    
    /// <summary>
    /// Just a very simple heuristic supposing for a way between two nodes
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns></returns>
    float GetHeuristic(Node nodeA, Node nodeB) {
        float dstX = Mathf.Pow(nodeB.FieldX - nodeA.FieldX, 2f);
        float dstY = Mathf.Pow(nodeB.FieldY - nodeA.FieldY, 2f);
        return Mathf.Sqrt(dstX + dstY);
    }
}
