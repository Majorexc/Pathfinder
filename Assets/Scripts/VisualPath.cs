using UnityEngine;

/// <summary>
/// Just shows path 
/// </summary>
public class VisualPath : MonoBehaviour {
    LineRenderer _lineRenderer;
    
    void Awake() {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    /// <summary>
    /// Shows path by income points
    /// </summary>
    /// <param name="path">Income Points array</param>
    public void ShowPath(Vector3[] path) {
        _lineRenderer.positionCount = path.Length;
        for (var index = 0; index < path.Length; index++) {
            var pos = path[index];
            _lineRenderer.SetPosition(index, pos);
        }
    }
    
    /// <summary>
    /// Clears path
    /// </summary>
    public void Clear() {
        _lineRenderer.positionCount = 0;
    }
}
