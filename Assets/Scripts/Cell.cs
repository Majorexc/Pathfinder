using UnityEngine;

public class Cell : MonoBehaviour {
    public bool IsWalkable;
    public Vector2Int FieldPosition; 
    
    [SerializeField] Renderer _renderer;
    
    static readonly int colorMatParam = Shader.PropertyToID("_Color");
    static readonly Color walkableColor = Color.green;
    static readonly Color notWalkableColor = Color.red;
    static readonly Color startPathColor = Color.magenta;
    static readonly Color targetPathColor = Color.blue;

    void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        SetVisualColor(IsWalkable ? walkableColor : notWalkableColor);
    }

    void SetVisualColor(Color color) {
        _renderer.material.SetColor(colorMatParam, color);
    }

    public void SetAsStartCell() {
        SetVisualColor(startPathColor);
    }
    
    public void SetAsTargetCell() {
        SetVisualColor(targetPathColor);
    }

    public void Clear() {
        SetVisualColor(IsWalkable ? walkableColor : notWalkableColor);
    }
}
