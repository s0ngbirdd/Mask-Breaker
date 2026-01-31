using UnityEngine;

public static class DebugColor
{
    public static void DebugLog(string message, Color color)
    {
#if UNITY_EDITOR
        string colorName = "white";

        if (color == Color.green) colorName = "green";
        else if (color == Color.red) colorName = "red";
        else if (color == Color.black) colorName = "black";
        else if (color == Color.blue) colorName = "blue";
        else if (color == Color.cyan) colorName = "cyan";
        else if (color == Color.clear) colorName = "clear";
        else if (color == Color.gray) colorName = "gray";
        else if (color == Color.grey) colorName = "grey";
        else if (color == Color.magenta) colorName = "magenta";
        else if (color == Color.white) colorName = "white";
        else if (color == Color.yellow) colorName = "yellow";
        
        Debug.Log($"<color={colorName}>{message}</color>");
#endif
    }
}
