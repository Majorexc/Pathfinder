using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder {
    Node[,] _field;
    readonly Stack<Vector2Int> _path = new Stack<Vector2Int>();
    Vector2Int _size;

    public AStarPathfinder(Node[,] field, Vector2Int size) {
        _field = field;
        _size = size;
    }
    
    public void FindPath(Vector2Int startPos, Vector2Int targetPos) {
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
                RetracePath(startNode, targetNode);
                Debug.Log($"Start: {startNode.gridX} {startNode.gridY}");
                Debug.Log($"Dest: {targetNode.gridX} {targetNode.gridY}");
                return;
            }

            foreach (Node neighbour in GetNeighbours(node)) {
                if (!neighbour._walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        Debug.Log("Path not found");
    }

    void RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
    }

    Node[] GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        if (node.gridX > 0) {
            neighbours.Add(GetNodeByIndex(node.gridX - 1, node.gridY));
        }
        
        if (node.gridX < _size.x) {
            neighbours.Add(GetNodeByIndex(node.gridX - 1, node.gridY));
        }

        if ((node.gridX > 0 || (node.gridX == 0 && node.gridY % 2 != 0)) && node.gridY > 0) {
            neighbours.Add(GetNodeByIndex(node.gridX, node.gridY-1));
        }
        
        if ((node.gridX > 0 || (node.gridX == 0 && node.gridY % 2 == 0)) && node.gridY > 0) {
            neighbours.Add(GetNodeByIndex(node.gridX, node.gridY-1));
        }
        
        if ((node.gridX < _size.x || (node.gridX == _size.x - 1 && node.gridY % 2 != 0)) && node.gridY < _size.y - 1) {
            neighbours.Add(GetNodeByIndex(node.gridX, node.gridY+1));
        }
        
        if ((node.gridX < _size.x || (node.gridX == _size.x - 1 && node.gridY % 2 == 0)) && node.gridY < _size.y - 1) {
            neighbours.Add(GetNodeByIndex(node.gridX, node.gridY+1));
        }

        return neighbours.ToArray();
    }

    Node GetNodeByIndex(int x, int y) {
        return _field[x, y];
    }
    
    int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return 14*dstX + 10 * (dstY-dstX);
    }
}
