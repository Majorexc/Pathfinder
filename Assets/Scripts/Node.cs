using UnityEngine;

public class Node {
    public bool _walkable;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
	
    public Node(bool walkable, int _gridX, int _gridY) {
        _walkable = walkable;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}