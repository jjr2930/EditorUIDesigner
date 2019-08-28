using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIEditor
{
    [CreateAssetMenu(menuName = "Create New UI Coord Data")]
    public class UIElementContainer : ScriptableObject
    {
        public UIElement root;
    }
}