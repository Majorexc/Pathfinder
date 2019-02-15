using UnityEngine;

public class TouchHandler : MonoBehaviour {

    Cell _startCell;
    Cell _targetCell;
    Camera _cam;
    Field _field;
    
    void Awake() {
        _cam = Camera.main;
        // TODO: zenject
        _field = FindObjectOfType<Field>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var screenPos = Input.mousePosition;
            if (SelectStartAndTargetCells(screenPos)) {
                _field.FindPath(_startCell, _targetCell);
            }
        }
    }

    bool SelectStartAndTargetCells(Vector3 screenPos) {
        var ray = _cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            var cell = hit.collider.GetComponentInParent<Cell>();
            if (cell != null) {

                if (!cell.IsWalkable)
                    return false;
                
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
                    _targetCell = cell;
                    _targetCell.SetAsTargetCell();
                    return true;
                }
            }
        }

        return false;
    }
}
