using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : APathfinder {
    public override Node[] FindPath(Vector2Int startPos, Vector2Int targetPos) {
        Node startNode = _field[startPos.x, startPos.y];
        Node targetNode = _field[targetPos.x, targetPos.y];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i ++) {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) {
                var path = RetracePath(startNode, targetNode);
                return path;
            }

            foreach (Node neighbour in GetNeighbours(node)) {
                if (!neighbour._walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetHeuristic(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetHeuristic(neighbour, targetNode);
                    neighbour.parent = node;

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
            currentNode = currentNode.parent;
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
    int GetHeuristic(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return dstX + dstY;
    }
}
