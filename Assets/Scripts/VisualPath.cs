using UnityEngine;

public class VisualPath : MonoBehaviour {
    LineRenderer _lineRenderer;
    void Awake() {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public void ShowPath(Vector3[] path) {
        _lineRenderer.positionCount = path.Length;
        for (var index = 0; index < path.Length; index++) {
            var pos = path[index];
            _lineRenderer.SetPosition(index, pos);
        }
    }

    public void Clear() {
        _lineRenderer.positionCount = 0;
    }
}
