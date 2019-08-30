using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace EditorGUIEditor
{
    [Serializable]
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
            AddComponent<UIDraggable>();
            AddComponent<UISelectable>();
            AddComponent<UIContextMenuComponent>();
        }

        public override void OnDraw()
        {
            base.OnDraw();
            if (GUI.Button(WorldRect, labelText))
            {
                if(null != callback)
                    callback(this);
            }            
        }
    }
}