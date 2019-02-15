using System;

using UnityEngine;

/// <summary>
/// Simple map parser 
/// </summary>
public static class FieldParserFromText {
    const char NEW_LINE = '\n';
    const char SPACE = ' ';
    
    /// <summary>
    /// Parse text string to map
    /// </summary>
    /// <param name="text">Input string</param>
    /// <returns>Returns parsed field and size tuple. Returns zero size if map didn't parsed</returns>
    public static Tuple<int[,], Vector2Int> Parse(string text) {
        var field = new int[0, 0];
        var size = Vector2Int.zero;
        
        try {
            var splitText = text.Split(NEW_LINE);
            for (var i = 0; i < splitText.Length; i++) {
                var line = splitText[i];
                var splitLine = line.Split(SPACE);
                
                // Get map size
                if (i == 0) {
                    size.x = int.TryParse(splitLine[0], out int x) ? x : 0;
                    size.y = int.TryParse(splitLine[1], out int y) ? y : 0;
                    field = new int[size.x, size.y];
                    continue;
                }

                // Parse map line by line
                for (int j = 0; j < size.y; j++) {
                    var walkable = int.TryParse(splitLine[j], out int val) ? val : 0;
                    field[i - 1, j] = walkable;
                }
            }
        }
        catch (Exception e) {
            Debug.LogError($"[{nameof(FieldParserFromText)}] Map can not be parsed correct.\nReason: {e.Message}");
        }
        
        return new Tuple<int[,], Vector2Int>(field, size);
    }
}
