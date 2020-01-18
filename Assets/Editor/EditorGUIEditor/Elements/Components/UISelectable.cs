using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace EditorGUIDesigner
{
    [Serializable]
    public class UISelectable : UIElementComponent
    {
        public static UIElement Selected { get; set; }


        public override void OnMouseDown(Vector2 mousePosition)
        {
            //Debug.Log($"UISelectable : {owner.WorldRect.ToString()}");
            if(owner.WorldRect.Contains(mousePosition))
            {
                Selected = owner;
                Selection.activeObject = owner;
                Event.current.Use();
            }
        }
    }
}