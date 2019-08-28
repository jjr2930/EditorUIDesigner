using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace EditorGUIEditor
{
    public class UIButton : UIElement
    {
        public string labelText;
        Action<UIButton> callback;
        public Action<UIButton> Callback
        {
            get
            {
                return callback;
            }
            set
            {
                callback = value;
            }
        }

        public override void Init()
        {
            base.Init();
            AddComponent<UIResizeable>();
            //AddComponent<UIDraggable>();
        }

        public override void Draw()
        {
            if(GUI.Button(WorldRect, labelText))
            {
                if(null != callback)
                    callback(this);
            }

            base.Draw();
        }

        //public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        //{
        //    base.OnDrag(mousePosition, delta);
        //    localRect.position += delta;
        //}
    }
}