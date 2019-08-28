using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

public class PixelEditorPallette : EditorWindow
{
    public static void OpenWindow()
    {
        GetWindow<PixelEditorPallette>();
    }

    Color[] colorDefaultPallette = new Color[10]
    {
        Color.black,
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.grey,
        Color.magenta,
        Color.red,
        Color.white,
        Color.yellow,
    };

    public Color CurrentSelectedPallette
    {
        get;set;
    }
    private void OnGUI()
    {
        Color originColor = GUI.color;
        for (int i = 0; i < colorDefaultPallette.Length; i++)
        {
            GUI.color = colorDefaultPallette[i];
            if (GUILayout.Button(""))
            {
                CurrentSelectedPallette = colorDefaultPallette[i];
            }
        }
        GUI.color = originColor;

        GUI.color = CurrentSelectedPallette;
        GUILayout.Button("");
        GUI.color = originColor;
    }
}

