using System;
using UnityEditor;
using UnityEngine;
namespace EditorGUIDesigner
{
    public class EditorGUIDesignerHierachy : EditorWindow
    {
        readonly float INDENT_SIZE = 10f;
        readonly GUILayoutOption GUI_WIDTH = GUILayout.Width(100);

        [MenuItem("Tools/EditorGUIDesigner/Hierachy")]
        static void OpenWindow()
        {
            var window = GetWindow<EditorGUIDesignerHierachy>();
            window.Show();
        }

        UIRoot Root
        {
            get
            {
                if (null == EditorGUIEditorWindow.Container)
                    return null;

                if (root == null || EditorGUIEditorWindow.Container.root != root)
                {
                    var container = Selection.activeObject as UIElementContainer;
                    if (null == container)
                        return null;
                    root = container.root;
                }

                return root;
            }
        }
        UIRoot root;

        private void OnGUI()
        {
            if (null == Root)
            {
                EditorGUILayout.LabelField("Please select uicontainer");
                return;
            }

            using (var v = new EditorGUILayout.VerticalScope())
            {
                TraversalEndDrawUI(root);
            }
        }

        void TraversalEndDrawUI(UIElement parent)
        {
            using (var vertical = new EditorGUILayout.VerticalScope())
            {
                ++EditorGUI.indentLevel;

                parent.fold = EditorGUILayout.Foldout(parent.fold, parent.name);

                for (int i = 0; i < parent.childs.Count; i++)
                {
                    if (parent.fold)
                        TraversalEndDrawUI(parent.childs[i]);
                }
                --EditorGUI.indentLevel;
            }
        }
    }
}
