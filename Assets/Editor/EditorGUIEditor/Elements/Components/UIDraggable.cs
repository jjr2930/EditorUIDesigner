using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIDesigner
{
    [Serializable]
    public class UIDraggable : UIElementComponent
    {
            static UIElement dragObj = null;

        public override void OnMouseUp(Vector2 mousePosition)
        {
            base.OnMouseUp(mousePosition);
            if (dragObj == owner)
                dragObj = null;

        }
        public override void OnMouseDown(Vector2 mousePosition)
        {
            base.OnMouseDown(mousePosition);
            if (owner.WorldRect.Contains(mousePosition))
            {
                dragObj = owner;
            }
        }

        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            //Debug.Log("UI Draggable : OnDrag");
            base.OnDrag(mousePosition, delta);
            if (dragObj == owner)
            {
                owner.localRect.position += delta;
                Event.current.Use();
                owner.RefreshMaxXY();
            }
        }
    }
}