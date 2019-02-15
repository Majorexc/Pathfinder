public class Node {
    public bool Walkable;
    public int FieldX;
    public int FieldY;
    
    // Only pathfinder's variables 
    public float GCost;
    public float HCost;
    public Node Parent;
 
    public float FCost {
        get {
            return GCost + HCost;
        }
    }
	
    public Node(bool walkable, int fieldX, int fieldY) {
        Walkable = walkable;
        FieldX = fieldX;
        FieldY = fieldY;
    }
}