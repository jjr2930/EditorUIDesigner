using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    public class UIRoot : UIElement
    {
        public override bool ShouldEventSkipSelf
        {
            get
            {
                return true;
            }

            set
            {
                return;
            }
        }
        public override Rect WorldRect
        {
            get
            {
                localRect.width = EditorGUIEditorWindow.WindowRect.width;
                localRect.height = EditorGUIEditorWindow.WindowRect.height;
                return localRect;
            }
        }
        
        public override void Draw()
        {
            base.Draw();
        }
    }
}