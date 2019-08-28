using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PixelEditor : EditorWindow{
    [MenuItem("Tools/PixelEditor")]
    public static void OpenWindow()
    {
        GetWindow<PixelEditor>();
        PixelEditorPallette.OpenWindow();
    }

    public string fileName = "";
    public Color[,] pixelColor = null;
    public int width = 0;
    public int height = 0;
    public Vector2 cursor = new Vector2();

    public GUILayoutOption buttonWidth = GUILayout.Width(10);
    public GUILayoutOption buttonHeight = GUILayout.Height(10);

    public Stack<int> stacked_x = new Stack<int>();
    public Stack<int> stacked_y = new Stack<int>();
    Rect rect = new Rect();
    private void OnGUI()
    {
        ResetCursor();
        EditorGUI.LabelField(CreateFieldRect(),"FileName");
        IncreaseCursorX();
        fileName = EditorGUI.TextField(CreateFieldRect(), fileName);
        IncreaseCursorY();
        DecreaseCursorX();

        EditorGUI.LabelField(CreateFieldRect(), "Width");   IncreaseCursorX();
        width = EditorGUI.IntField(CreateFieldRect(), width);   IncreaseCursorY(); DecreaseCursorX();

        EditorGUI.LabelField(CreateFieldRect(), "Height"); IncreaseCursorX();
        height = EditorGUI.IntField(CreateFieldRect(), height); IncreaseCursorY(); DecreaseCursorX();
        
        width = (width <= 0) ? 1 : width;
        height = (height <= 0) ? 1 : height;

        
        if (GUI.Button(CreateButtonRect(),"Generate"))
        {
            pixelColor = new Color[width, height];
        }
        
 
        if (null == pixelColor)
            return;

        IncreaseCursorY();

        int pixelSizeX = 10;
        int pixelSizeY = 10;

        var originColor = GUI.color;
        GUI.color = Color.yellow;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (GUI.Button(CreatePixelRect(), ""))
                {

                }
                IncreaseCursorX(pixelSizeX);
            }

            DecreaseCursorX(width * pixelSizeX);
            IncreaseCursorY(pixelSizeY);
        }
        GUI.color = originColor;
    }

    void DecreaseCursorX(int howMuch = 100)
    {
        cursor.x -= howMuch;
    }
    void IncreaseCursorY(int howMuch = 22)
    {
        cursor.y += howMuch;
    }

    void IncreaseCursorX(int howmuch = 100)
    {
        cursor.x += howmuch;
    }

    void ResetCursor()
    {
        cursor.x = 0;
        cursor.y = 0;
    }

    Rect CreatePixelRect(int width = 10, int height = 10)
    {
        return new Rect(cursor.x, cursor.y, width, height);
    }

    Rect CreateButtonRect(int width = 100, int height = 20)
    {
        return new Rect(cursor.x, cursor.y, width, height);
    }
    Rect CreateFieldRect(int width = 50, int height = 20)
    {
        return new Rect(cursor.x, cursor.y, width, height); 
    }
}
