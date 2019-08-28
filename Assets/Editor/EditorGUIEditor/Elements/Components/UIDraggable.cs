using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    public class UIDraggable : UIElementComponent
    {
        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            base.OnDrag(mousePosition, delta);
            if(uiElement != EditorGUIEditorSelection.ResizeSelection)
                uiElement.localRect.position += delta;
        }
    }
}