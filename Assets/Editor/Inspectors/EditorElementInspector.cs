using UnityEngine;
using System.Collections;
using UnityEditor;

namespace EditorGUIEditor
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

            EditorGUILayout.Space(100);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Remove"))
            {
                var element = target as UIElement;
                element.RemoveAsset();
            }
        }
    }
}