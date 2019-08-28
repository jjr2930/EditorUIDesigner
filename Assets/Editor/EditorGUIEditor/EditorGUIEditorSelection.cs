using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    public static class EditorGUIEditorSelection
    {
        static UIElement selected = null;
        static UIElement dragSelection = null;
        static UIElement resizeSelection = null;
        public static Action<UIElement> onSelectionChanged;
        public static Action<UIElement> onDragSelectionChanged;
        public static Action<UIElement> onResizeSelectionChanged;

        public static UIElement Selected
        {
            get
            {
                return selected;
            }
            set
            {
                Debug.Log("Selection : " + value.GetInstanceID());
                selected = value;
                if(null != onSelectionChanged)
                    onSelectionChanged(value);
            }
        }

        public static UIElement DragSelection
        {
            get { return dragSelection; }
            set
            {
                //Debug.Log("DragSelection : " + value.name + " Type :" + value.GetType().ToString());
                dragSelection = value;
                if (null != onDragSelectionChanged)
                    onDragSelectionChanged(value);
            }
        }

        public static UIElement ResizeSelection
        {
            get { return resizeSelection; }
            set
            {
                //Debug.Log("ResizeSelection");
                resizeSelection = value;
                if (null != onResizeSelectionChanged)
                    onResizeSelectionChanged(value);
            }
        }
    }
}