using UnityEngine;
using System.Collections;
using UnityEditor;

namespace EditorGUIDesigner
{
    [CustomEditor(typeof(UIElement))]
    public class EditorElementInspector : Editor
    {
        protected override void OnHeaderGUI()
        {
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                string temp = target.name;
                target.name = EditorGUILayout.TextField("Name", target.name);
                if (temp != target.name)
                {
                    var element = target as UIElement;
                    for (int i = 0; i < element.components.Count; i++)
                    {
                        element.components[i].Rename();
                    }

                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            EditorGUILayout.Space();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Remove"))
            {
                var element = target as UIElement;
                element.RemoveAsset();
                element.parent.childs.Remove(element);
                DestroyImmediate(element, true);
                EditorUtility.SetDirty(EditorGUIEditorWindow.Container);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}