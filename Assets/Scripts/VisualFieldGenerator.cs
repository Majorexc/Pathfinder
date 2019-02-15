using System;
using System.IO;

using UnityEngine;

using Airion.Extensions;

using UnityEditor;

// TODO: don't forget to move it to #Editor preprocessor directive
public class VisualFieldGenerator : MonoBehaviour {
    [SerializeField] GameObject _groundBlockPrefab;

    [SerializeField] Vector2Int _fieldSize;
    [SerializeField] float _innerRadius = 1;
    [SerializeField] float _outerRadius = 1;

    int[,] _rawfield;

    [ContextMenu(nameof(GenerateHG))]
    void GenerateHG() {
        transform.DestroyAllChildren();

        var path = EditorUtility.OpenFilePanel("Open map file", "Assets/Maps", "txt");
        if (path.Length == 0) 
            return;

        var fileContent = File.ReadAllText(path);
        var fieldAndSize = FieldParserFromText.Parse(fileContent);
        _rawfield = fieldAndSize.Item1;
        _fieldSize = fieldAndSize.Item2;
        
        for (int i = 0; i < _fieldSize.x; i++) {
            for (int j = 0; j < _fieldSize.y; j++) {
               CreateBlock(i, j);
            }
        }
    }

    void CreateBlock(int x, int y) {
        var cell = PrefabUtility.InstantiatePrefab(_groundBlockPrefab) as GameObject;
        cell.transform.position = GetPosByGrid(x, y);
        cell.transform.SetParent(transform);
        var cellScript = cell.GetComponent<Cell>();
        cellScript.IsWalkable = GetWalkable(x, y);
        cellScript.FieldPosition = new Vector2Int(y, x);
        cell.name = $"Cell {y}_{x}";
    }
 
    Vector3 GetPosByGrid(int i, int j) {
        Vector3 pos = Vector3.zero;
        pos.x = j * _innerRadius + ((i % 2) * _innerRadius / 2.0f);
        pos.z = -i * _outerRadius;
        return pos;
    }

    bool GetWalkable(int x, int y) {
        try {
            return _rawfield[x, y] > 0;
        }
        catch (Exception e) {
            Debug.LogError($"[{GetType()}] {e.Message}");
            return true;
        }
    }
}

