using System.IO;

using UnityEngine;

using Airion.Extensions;

#if UNITY_EDITOR

using UnityEditor;

#endif // UNITY_EDITOR

/// <summary>
/// Generates field by map file.
/// Just call "GenerateField" from context menu on field GameObject in the scene to create brand-new field from map file
/// \note Not working in runtime
/// </summary>
public class VisualFieldGenerator : MonoBehaviour {
    [SerializeField] GameObject _groundBlockPrefab;

    [SerializeField] float _innerRadius = 1;
    [SerializeField] float _outerRadius = 1;

    #if UNITY_EDITOR
    
    [ContextMenu(nameof(GenerateField))]
    void GenerateField() {
        transform.DestroyAllChildren();

        var path = EditorUtility.OpenFilePanel("Open map file", "Assets/Maps", "txt");
        if (path.Length == 0) 
            return;
        
        var fileContent = File.ReadAllText(path);
        var fieldAndSize = FieldParserFromText.Parse(fileContent);
        var rawField = fieldAndSize.Item1;
        var fieldSize = fieldAndSize.Item2;
        
        /// Just creates cell in world
        void CreateCell(int x, int y) {
            var cell = PrefabUtility.InstantiatePrefab(_groundBlockPrefab) as GameObject;
            cell.transform.position = GetPosByGrid(x, y);
            cell.transform.SetParent(transform);
            var cellScript = cell.GetComponent<Cell>();
            // Cell is walkable if value from field not equals to zero
            cellScript.IsWalkable = rawField[x, y] > 0;
            cellScript.FieldPosition = new Vector2Int(y, x);
            cell.name = $"Cell {y}_{x}";
        }
        
        for (int i = 0; i < fieldSize.x; i++) {
            for (int j = 0; j < fieldSize.y; j++) {
               CreateCell(i, j);
            }
        }
    }
    
    #endif // UNITY_EDITOR
 
    Vector3 GetPosByGrid(int i, int j) {
        Vector3 pos = Vector3.zero;
        pos.x = j * _innerRadius + ((i % 2) * _innerRadius / 2.0f);
        pos.z = -i * _outerRadius;
        return pos;
    }
}

