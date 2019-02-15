using UnityEngine;

public class Node {
    public bool _walkable;
    public int gridX;
    public int gridY;
    public Vector3 WorldPos;

    public int gCost;
    public int hCost;
    public Node parent;
	
    public Node(bool walkable, int _gridX, int _gridY, Vector3 worldPos) {
        _walkable = walkable;
        gridX = _gridX;
        gridY = _gridY;
        WorldPos = worldPos;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}