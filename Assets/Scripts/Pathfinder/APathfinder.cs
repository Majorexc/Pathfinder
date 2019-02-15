using System.Collections.Generic;

using UnityEngine;

public abstract class APathfinder {
    protected Node[,] _field;
    protected Vector2Int _size;
    
    protected readonly Stack<Vector2Int> _path = new Stack<Vector2Int>();

    public APathfinder() {}
    
    public abstract Node[] FindPath(Vector2Int startPos, Vector2Int targetPos);
    
    public void UpdateState(Node[,] field, Vector2Int size) {
        _field = field;
        _size = size;
    }
    
    protected Node[] GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        
        int xOffset = 0;
        if (node.gridY % 2 != 0)
            xOffset = 1;
            
        // On the same row.
        if (node.gridX > 0) {
            neighbours.Add(GetNodeByIndex(node.gridX - 1, node.gridY));
        }

        if (node.gridX < _size.x - 1) {
            neighbours.Add(GetNodeByIndex(node.gridX + 1, node.gridY));
        }
 
        // 2 in row below
        if (node.gridY < _size.y - 1) {
            if (node.gridX - 1 >= 0)
                neighbours.Add(GetNodeByIndex(node.gridX + xOffset - 1, node.gridY + 1));
            if (node.gridX + xOffset < _size.x)
                neighbours.Add(GetNodeByIndex(node.gridX + xOffset, node.gridY + 1));
        }

        // 2 in row above
        if (node.gridY > 0) {
            if (node.gridX - 1 >= 0)
                neighbours.Add(GetNodeByIndex(node.gridX + xOffset - 1, node.gridY - 1));
            if (node.gridX + xOffset < _size.x)
                neighbours.Add(GetNodeByIndex(node.gridX + xOffset, node.gridY - 1));
        }
 
        return neighbours.ToArray();
    }
    
    Node GetNodeByIndex(int x, int y) {
        return _field[x, y];
    }
}
