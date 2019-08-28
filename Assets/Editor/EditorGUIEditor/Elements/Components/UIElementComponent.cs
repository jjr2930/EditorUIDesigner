using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    public class UIElementComponent : ScriptableObject
    {
        protected UIElement uiElement= null;
        public void SetUIEl(UIElement el)
        {
            uiElement = el;
        }

        public virtual void Init() { }
        public virtual void Input(Event e) { }
        public virtual void Update() { }
        public virtual void Draw() { }
        public virtual void OnDrag(Vector2 mousePosition, Vector2 delta) { }
        public virtual void OnDragUpdate(Vector2 updatePosition) { }
        public virtual void OnDragEnd(Vector2 endPosition) { }
        public virtual void OnSelected(Vector2 selectedPosition) { }
        public virtual void OnDeselected(Vector2 deselectedPosition) { }
        public virtual void OnRepaint(Vector2 mousePosition, Vector2 delta) { }
        public virtual void OnMouseUp(Vector2 mousePosition) { }
    }
}