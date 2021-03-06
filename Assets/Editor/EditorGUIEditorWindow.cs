﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorGUIDesigner
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
                    container = UnityEditor.Selection.activeObject as UIElementContainer;
                }
                return container;
            }
        }

        static Rect windowRect;
        public static Rect WindowRect { get { return windowRect; } }

        [MenuItem("Tools/EditorGUIDesigner/EditorWindow")]
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
            if (null == Container)
            {
                GUILayout.Label("Please Select container...");
                return;
            }

            if (null == container.root)
            {
                container.root = CreateInstance<UIRoot>();
                container.root.name = "root";
                container.root.Init();

                AssetDatabase.AddObjectToAsset(container.root, container);
                EditorUtility.SetDirty(container);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorSelection.Selected = container.root;

            windowRect = position;
            
            using (var c = new EditorGUI.ChangeCheckScope())
            {
                container.root.Input(Event.current);
                container.root.Draw();
                if (c.changed)
                {
                    EditorUtility.SetDirty(container);
                }
            }
        }
    }
}