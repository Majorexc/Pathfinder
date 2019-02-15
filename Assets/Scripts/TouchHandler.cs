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
        var screenPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) 
            SelectStart(screenPos);
        
        if (Input.GetMouseButtonDown(1)) 
            SelectTargetAndStartPathFind(screenPos);        
    }
    
    /// <summary>
    /// Selects Start cell
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    void SelectStart(Vector3 screenPos) {
        var cell = GetCellByRaycast(screenPos);
        if (cell != null) {
            // We don't need to find path to/from not walkable cell
            if (!cell.IsWalkable)
                return;
            
            // Just clear start
            if (_startCell != null) {
                _startCell.Clear();
                _startCell = null;
            }
            
            _startCell = cell;
            _startCell.SetAsStartCell();
        }
    }

    /// <summary>
    /// Selects target cell and runs pathfinding
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    void SelectTargetAndStartPathFind(Vector3 screenPos) {
        var cell = GetCellByRaycast(screenPos);
        if (cell != null) {
            // We don't need to find path to/from not walkable cell
            if (!cell.IsWalkable)
                return;
            
            if (cell.Equals(_startCell))
                return;
            
            // Just clear target
            if (_targetCell != null) {
                _targetCell.Clear();
                _targetCell = null;
            }
            
            _targetCell = cell;
            _targetCell.SetAsTargetCell();
            
            if (_startCell != null && _targetCell != null)
                _field.FindPath(_startCell, _targetCell);
        }
    }

    Cell GetCellByRaycast(Vector3 screenPos) {
        var ray = _cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
           return hit.collider.GetComponentInParent<Cell>();
        }

        return null;
    }
}
