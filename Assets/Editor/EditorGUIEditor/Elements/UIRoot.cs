using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    [Serializable]
    public class UIRoot : UIElement
    {
        public override Rect WorldRect
        {
            get
            {
                localRect.width = EditorGUIEditorWindow.WindowRect.width;
                localRect.height = EditorGUIEditorWindow.WindowRect.height;
                return localRect;
            }
        }

        public override void Init()
        {
            base.Init();
            AddComponent<UIContextMenuComponent>();
        }

        public override void OnDraw()
        {
            base.OnDraw();
        }
    }
}