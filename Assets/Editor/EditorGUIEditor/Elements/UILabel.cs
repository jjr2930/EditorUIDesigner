using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace EditorGUIEditor
{
    [Serializable]
    public class UILabel : UIElement
    {
        public string value = "new label";

        public override void Init()
        {
            base.Init();
            AddComponent<UIDraggable>();
            AddComponent<UIResizeable>();
            AddComponent<UISelectable>();
        }


        public override void OnDraw()
        {
            value = GUI.TextField(WorldRect, value);
            
            base.OnDraw();
        }

        public override void OnMouseDown(Vector2 selectedPosition)
        {
            base.OnMouseDown(selectedPosition);
            UnityEditor.Selection.activeObject = this;
        }
    }
}