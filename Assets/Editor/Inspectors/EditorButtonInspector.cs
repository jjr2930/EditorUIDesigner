using UnityEngine;
using System.Collections;
using UnityEditor;

namespace EditorGUIDesigner
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