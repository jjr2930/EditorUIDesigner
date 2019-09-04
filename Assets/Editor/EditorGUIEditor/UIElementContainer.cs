using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIDesigner
{
    [CreateAssetMenu(menuName = "Create New UI Coord Data")]
    public class UIElementContainer : ScriptableObject
    {
        public UIRoot root;
    }
}