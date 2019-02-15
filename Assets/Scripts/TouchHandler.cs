using UnityEngine;

using Zenject;

/// <summary>
/// Just simple user's input controller.
/// Handles touch to start cell and destination cell
/// And runs pathfinding process
/// </summary>
public class TouchHandler : MonoBehaviour {
    Cell _startCell;
    Cell _targetCell;

    [Inject] Camera _cam;
    [Inject] Field _field;
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var screenPos = Input.mousePosition;
            if (SelectStartAndTargetCells(screenPos)) {
                _field.FindPath(_startCell, _targetCell);
            }
        }
    }
    
    /// <summary>
    /// Selects Start and target cells.
    /// Returns true if found both otherwise false
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    bool SelectStartAndTargetCells(Vector3 screenPos) {
        var ray = _cam.ScreenPointToRay(screenPos);
        
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            var cell = hit.collider.GetComponentInParent<Cell>();
            if (cell != null) {
                // We don't need to find path to/from not walkable cell
                if (!cell.IsWalkable)
                    return false;
                
                // Just clear start and dest cells if target cell already exist
                if (_targetCell != null) {
                    _targetCell.Clear();
                    _startCell.Clear();
                    _targetCell = null;
                    _startCell = null;
                }
                
                if (_startCell == null) {
                    _startCell = cell;
                    _startCell.SetAsStartCell();
                } else {
                    // We've found target cell
                    _targetCell = cell;
                    _targetCell.SetAsTargetCell();
                    return true;
                }
            }
        }

        return false;
    }
}
