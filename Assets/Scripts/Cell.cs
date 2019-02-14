using UnityEngine;

public class Cell : MonoBehaviour {
    public bool IsWalkable;
    public Vector2Int FieldPosition; 
    
    [SerializeField] Renderer _renderer;
    
    static readonly int colorMatParam = Shader.PropertyToID("_Color");
    
    void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        SetVisual(IsWalkable);
    }

    void SetVisual(bool walkable) {
        Color color = walkable ? Color.green : Color.red;
        _renderer.material.SetColor(colorMatParam, color);
    }
}
