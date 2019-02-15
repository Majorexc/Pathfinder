using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Base class for pathfinding algorithm.
/// Allows fast change pathfinding implementation via dependency injection container
/// </summary>
public abstract class APathfinder {
    Vector2Int _size;
    
    protected Node[,] _field;
    protected readonly Stack<Vector2Int> _path = new Stack<Vector2Int>();

    #region Public Methods    
    
    public abstract Node[] FindPath(Vector2Int startPos, Vector2Int targetPos);
    
    /// <summary>
    /// Updates field's state
    /// Can be use for dynamic cells state
    /// Or just fill on start if uses only static
    /// </summary>
    /// <param name="field">Field model</param>
    /// <param name="size">Field size</param>
    public void UpdateState(Node[,] field, Vector2Int size) {
        _field = field;
        _size = size;
    }
    
    #endregion // Public Methods
    
    #region Protected Methods    
    
    protected APathfinder() {}
    
    /// Get all neighbours of a node
    protected Node[] GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        
        int xOffset = 0;
        if (node.FieldY % 2 != 0)
            xOffset = 1;
            
        // On the same row.
        if (node.FieldX > 0) {
            neighbours.Add(GetNodeFromField(node.FieldX - 1, node.FieldY));
        }

        if (node.FieldX < _size.x - 1) {
            neighbours.Add(GetNodeFromField(node.FieldX + 1, node.FieldY));
        }
 
        // 2 in row below
        if (node.FieldY < _size.y - 1) {
            if (node.FieldX - 1 >= 0)
                neighbours.Add(GetNodeFromField(node.FieldX + xOffset - 1, node.FieldY + 1));
            if (node.FieldX + xOffset < _size.x)
                neighbours.Add(GetNodeFromField(node.FieldX + xOffset, node.FieldY + 1));
        }

        // 2 in row above
        if (node.FieldY > 0) {
            if (node.FieldX - 1 >= 0)
                neighbours.Add(GetNodeFromField(node.FieldX + xOffset - 1, node.FieldY - 1));
            if (node.FieldX + xOffset < _size.x)
                neighbours.Add(GetNodeFromField(node.FieldX + xOffset, node.FieldY - 1));
        }
 
        return neighbours.ToArray();
    }
    
    #endregion // Protected Methods

    #region Private Methods    
    
    Node GetNodeFromField(int x, int y) {
        return _field[x, y];
    }
    
    #endregion // Private Methods    
}
