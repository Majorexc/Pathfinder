using System;

using UnityEngine;

using Airion.Extensions;

using UnityEditor;

// TODO: don't forget to move it to #Editor preprocessor directive
public class VisualFieldGenerator : MonoBehaviour {
    [SerializeField] GameObject _groundBlockPrefab;

    [SerializeField] Vector2Int _fieldSize;
    [SerializeField] float _innerRadius = 1;
    [SerializeField] float _outerRadius = 1;

    int[,] _field = new int[,] {
                                   {0, 0, 0, 0, 1},
                                   {0, 1, 1, 1, 0},
                                   {0, 0, 1, 1, 1},
                                   {0, 1, 1, 1, 0},
                                   {0, 0, 1, 1, 0}
                               }; 
    
    [ContextMenu(nameof(GenerateHG))]
    void GenerateHG() {
        transform.DestroyAllChildren();
        for (int i = 0; i < _fieldSize.x; i++) {
            for (int j = 0; j < _fieldSize.y; j++) {
               CreateBlock(i, j);
            }
        }
    }

    void CreateBlock(int x, int y) {
        var cellPrefab = PrefabUtility.InstantiatePrefab(_groundBlockPrefab) as GameObject;
        cellPrefab.transform.position = GetPosByGrid(x, y);
        cellPrefab.transform.SetParent(transform);
        cellPrefab.GetComponent<Cell>().Walkable = GetWalkable(x, y);
    }
 
    Vector3 GetPosByGrid(int i, int j) {
        Vector3 pos = Vector3.zero;
        pos.x = j * _innerRadius + ((i % 2) * _innerRadius / 2.0f);
        pos.z = -i * _outerRadius;
        return pos;
    }

    bool GetWalkable(int x, int y) {
        try {
            return _field[x, y] > 0;
        }
        catch (Exception e) {
            Debug.LogError($"[{GetType()}] {e.Message}");
            return true;
        }
    }
}

