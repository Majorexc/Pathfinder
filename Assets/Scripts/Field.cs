using UnityEngine;

public class Field : MonoBehaviour {
    Node[,] _field;
    Vector2Int _size;
    AStarPathfinder _pathfinder;

    void Awake() {
        CreateFieldModel();
        _pathfinder = new AStarPathfinder(_field, _size);
    }

    void CreateFieldModel() {
        var allCells = GetComponentsInChildren<Cell>();
        var sizeX = allCells[allCells.Length - 1].FieldPosition.x + 1;
        var sizeY = allCells[allCells.Length - 1].FieldPosition.y + 1;

        var cells = new Cell[sizeX, sizeY];

        foreach (var cell in allCells) {
            cells[cell.FieldPosition.x, cell.FieldPosition.y] = cell;
        }
        
        _size = new Vector2Int(sizeX, sizeY);
        _field = new Node[sizeX, sizeY];
        
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                _field[x, y] = new Node(cells[x, y].IsWalkable, x, y);
            }
        }
    }

    public void FindPath(Cell startCell, Cell targetCell) {
        _pathfinder.FindPath(startCell.FieldPosition, targetCell.FieldPosition);
    }
}
