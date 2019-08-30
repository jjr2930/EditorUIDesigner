using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorGUIEditor
{
    [Serializable]
    public class UIElementComponent : ScriptableObject
    {
        [SerializeField] protected UIElement owner;
        public UIElement Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public virtual void Init()
        {
            Rename();
        }

        public void Rename()
        {
            name = $"{owner.name}_new{this.GetType().ToString().GetTypeNameWithOutNamespace()}";
        }

        public virtual void Input(Event e) { }
        public virtual void Update() { }
        public virtual void Draw() { }
        public virtual void OnDrag(Vector2 mousePosition, Vector2 delta) { }
        public virtual void OnDragUpdate(Vector2 updatePosition) { }
        public virtual void OnDragEnd(Vector2 endPosition) { }
        public virtual void OnDeselected(Vector2 deselectedPosition) { }
        public virtual void OnRepaint(Vector2 mousePosition, Vector2 delta) { }
        public virtual void OnMouseUp(Vector2 mousePosition) { }
        public virtual void OnMouseDown(Vector2 mousePosition) { }
        public virtual void OnContextMenu(Vector2 mousPosition) { }
        public virtual void RemovedAsset()
        {
            AssetDatabase.RemoveObjectFromAsset(this);
        }
    }
}