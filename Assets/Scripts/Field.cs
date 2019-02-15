using System.Linq;

using UnityEngine;

using Zenject;

/// <summary>
/// Field model
/// </summary>
public class Field : MonoBehaviour {
    Node[,] _field;
    Cell[,] _cells;
    Vector2Int _size;
    
    [Inject] APathfinder _pathfinder;
    [Inject] VisualPath _pathVisualizer;

    void Awake() {
        CreateFieldModel();
    }

    void Start() {
        _pathfinder.UpdateState(_field, _size);
    }
    
   /// <summary>
   /// Generates field model from world's model.
   /// It fits only to VisualFieldGenerator from this project
   /// And can't work correct with hand-made field or others generators
   /// </summary>
    void CreateFieldModel() {
        // Find all child cells.  
        // We assume that they form a single field
        var allCells = GetComponentsInChildren<Cell>();
        var sizeX = allCells[allCells.Length - 1].FieldPosition.x + 1;
        var sizeY = allCells[allCells.Length - 1].FieldPosition.y + 1;

        _cells = new Cell[sizeX, sizeY];
        
        // Move cells to two-dimensional array
        foreach (var cell in allCells) {
            _cells[cell.FieldPosition.x, cell.FieldPosition.y] = cell;
        }
        
        _size = new Vector2Int(sizeX, sizeY);
        _field = new Node[sizeX, sizeY];
        
        // Create model for pathfinder
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                _field[x, y] = new Node(_cells[x, y].IsWalkable, x, y);
            }
        }
    }

    public void FindPath(Cell startCell, Cell targetCell) {
        var rawPath = _pathfinder.FindPath(startCell.FieldPosition, targetCell.FieldPosition);
        
        // If path found generate visual line
        if (rawPath != null) {
            var preparedPath = (from node in rawPath
                               select _cells[node.FieldX, node.FieldY].transform.position).ToArray();
            
            _pathVisualizer.ShowPath(preparedPath);
        } else {
            _pathVisualizer.Clear();
            Debug.Log("Path not found");
        }
    }
}
