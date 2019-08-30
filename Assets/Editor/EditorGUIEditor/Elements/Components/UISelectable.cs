using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace EditorGUIEditor
{
    [Serializable]
    public class UISelectable : UIElementComponent
    {
        public static UIElement Selected { get; set; }


        public override void OnMouseDown(Vector2 mousePosition)
        {
            if(owner.WorldRect.Contains(mousePosition))
            {
                Selected = owner;
                Selection.activeObject = owner;
                Event.current.Use();
            }
        }
    }
}