using UnityEngine;

public class Cell : MonoBehaviour {
    public bool Walkable;
    
    [SerializeField] Renderer _renderer;
    
    static readonly int colorMatParam = Shader.PropertyToID("_Color");
    
    void Awake() {
        _renderer = GetComponentInChildren<Renderer>();
        SetVisual(Walkable);
    }

    void SetVisual(bool walkable) {
        Color color = walkable ? Color.green : Color.red;
        _renderer.material.SetColor(colorMatParam, color);
    }
}
