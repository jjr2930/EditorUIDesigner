using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    public enum E_CoordType
    {
        
    }

    [Serializable]
    public struct UIElementRect
    {
        public Vector2 leftTop;
        public Vector2 rightBottom;
        
        public Vector2 LeftTop
        {
            get { return leftTop; }
            set
            {
                leftTop = value;
                width = (int)(rightBottom.x - leftTop.x);
                height = (int)(rightBottom.y - leftTop.y);

                width = Mathf.Abs(width);
                height = Mathf.Abs(height);
            }
        }

        public Vector2 RightBottom
        {
            get { return rightBottom; }
            set
            {
                rightBottom = value;
                width = (int)(rightBottom.x - leftTop.x);
                height = (int)(rightBottom.y - leftTop.y);

                width = Mathf.Abs(width);
                height = Mathf.Abs(height);
            }
        }

        public int width;
        public int height;
        public E_CoordType type;
    }
}