using UnityEngine;
using System.Collections;
using UnityEditor;

namespace EditorGUIEditor
{
    [CustomEditor(typeof(UIButton))]
    public class EditorButtonInspector : EditorElementInspector
    {
        protected override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
        }
    }
}