using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace EditorGUIEditor
{
    public class UIContextMenuComponent : UIElementComponent
    {
        GenericMenu contextMenu;
        GenericMenu ContextMenu
        {
            get
            {
                if (null == contextMenu)
                {
                    contextMenu = new GenericMenu();
                    var addButton = new GUIContent("Add Button");
                    var addLabel = new GUIContent("Add Label");

                    contextMenu.AddItem(addButton, false, OnClickAddButton);
                    contextMenu.AddItem(addLabel, false, OnClickAddLabel);
                }

                return contextMenu;
            }
        }

        private void OnClickAddLabel()
        {
            owner.AddChild<UILabel>();
        }

        private void OnClickAddButton()
        {
            owner.AddChild<UIButton>();
        }


        public override void OnContextMenu(Vector2 mousePosition)
        {
            base.OnContextMenu(mousePosition);
            if (owner.WorldRect.Contains(mousePosition))
            {
                ContextMenu.ShowAsContext();
                Event.current.Use();
            }
        }
    }
}