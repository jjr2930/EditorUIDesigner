using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorGUIEditor
{
    public class UILabel : UIElement
    {
        public string value = "new label";

        public override bool ShouldEventSkipChild
        {
            get
            {
                return false;
            }
        }

        public override bool ShouldEventSkipSelf
        {
            get
            {
                return false;
            }
        }

        public override void Draw()
        {
            value = GUI.TextField(WorldRect, value);
            
            base.Draw();
        }

        public override void OnSelected(Vector2 selectedPosition)
        {
            base.OnSelected(selectedPosition);
            Selection.activeObject = this;
        }

        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            base.OnDrag(mousePosition, delta);
            localRect.position += delta;
        }
    }
}