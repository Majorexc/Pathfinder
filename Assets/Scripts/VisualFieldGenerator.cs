using UnityEngine;

using Airion.Extensions;

using UnityEditor;

// TODO: don't forget to move it to #Editor preprocessor directive
public class VisualFieldGenerator : MonoBehaviour {
    [SerializeField] GameObject _groundBlockPrefab;

    [SerializeField] int[][] _field;
    [SerializeField] Vector2Int _fieldSize;
    [SerializeField] float _innerRadius = 1;
    [SerializeField] float _outerRadius = 1;
    
    [ContextMenu(nameof(GenerateHG))]
    void GenerateHG() {
        transform.DestroyAllChildren();
        for (int i = 0; i < _fieldSize.x; i++) {
            for (int j = 0; j < _fieldSize.y; j++) {
                var blockPrefab = PrefabUtility.InstantiatePrefab(_groundBlockPrefab) as GameObject;
                blockPrefab.transform.position = GetPosByGrid(i, j);
                blockPrefab.transform.SetParent(transform);
            }
        }
    }
 
    Vector3 GetPosByGrid(int i, int j) {
        Vector3 pos = Vector3.zero;
        
        pos.x =  (i + j * 0.5f - j / 2) * (_innerRadius * 2f);
        pos.z = j * _outerRadius * 1.5f;
        return pos;
    }
}

