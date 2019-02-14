using System;

using UnityEngine;

public static class FieldParserFromText {
    public static Tuple<int[,], Vector2Int> Parse(string text) {
        var field = new int[0, 0];
        var size = Vector2Int.zero;
        for (var i = 0; i < text.Split('\n').Length; i++) {
            var line = text.Split('\n')[i];
            var splitLine = line.Split(' ');
            if (i == 0) {
                size.x = int.TryParse(splitLine[0], out int x) ? x : 0;
                size.y = int.TryParse(splitLine[1], out int y) ? y : 0;
                field = new int[size.x, size.y];
                continue;   
            }

            for (int j = 0; j < size.y; j++) {
                var y = int.TryParse(splitLine[j], out int val) ? val : 0;
                field[i-1, j] = y;
            }
        }
        
        return new Tuple<int[,], Vector2Int>(field, size);
    }
}
