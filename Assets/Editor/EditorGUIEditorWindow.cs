using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorGUIEditor
{
    public class EditorGUIEditorWindow : EditorWindow
    {
        #region STATIC MEMBER
        static UIElementContainer container = null;
        public static UIElementContainer Container
        {
            get
            {
                if (null == container)
                {
                    container = Selection.activeObject as UIElementContainer;
                }
                return container;
            }
        }

        static Rect windowRect;
        public static Rect WindowRect { get { return windowRect; } }

        [MenuItem("Tools/EditorGUIEditor/ElementPalette")]
        public static void OpenWindow()
        {
            GetWindow<EditorGUIEditorWindow>();
        }

        public static void OpenWindowWithParam(UIElementContainer container)
        {
            EditorGUIEditorWindow.container = container;
            var newWindow = GetWindow<EditorGUIEditorWindow>();
            
        } 
        #endregion
        
        private void OnGUI()
        {
            if(null == Container)
            {
                GUILayout.Label("Please Select container...");
                return;
            }

            if (null == EditorGUIEditorSelection.Selected)
            {
                if(null == container.root)
                {
                    container.root = CreateInstance<UIRoot>();
                    container.root.Init();

                    AssetDatabase.AddObjectToAsset(container.root, container);
                }
                EditorGUIEditorSelection.Selected = container.root;
            }

            windowRect = position;

            if (GUILayout.Button("GenerateLabel"))
            {
                var createdLabel = ScriptableObject.CreateInstance<UILabel>();
                createdLabel.localRect.size = new Vector2(100, 50);
                createdLabel.localRect.position = new Vector2(100, 100);
                createdLabel.Init();
                EditorGUIEditorSelection.Selected.AddChild(createdLabel);
            }

            if(GUILayout.Button("Generate Button"))
            {
                var createdButton = CreateInstance<UIButton>();
                createdButton.localRect.size = new Vector2(100, 50);
                createdButton.localRect.position = new Vector2(100, 100);
                createdButton.Init();
                EditorGUIEditorSelection.Selected.AddChild(createdButton);
            }

            using (var c = new EditorGUI.ChangeCheckScope())
            {
                container.root.Input(Event.current);
                container.root.Draw();
                if (c.changed)
                    EditorUtility.SetDirty(container);
            }
        }
    }
}