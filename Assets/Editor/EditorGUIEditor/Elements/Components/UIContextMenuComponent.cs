using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace EditorGUIDesigner
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
            Debug.Log($"owner : {owner.name} OnClickAddLabel");
            owner.AddChild<UILabel>();
        }

        private void OnClickAddButton()
        {
            Debug.Log($"owner : {owner.name} OnClickAddButton");
            owner.AddChild<UIButton>();
        }


        public override void OnContextMenu(Vector2 mousPosition)
        {
            base.OnContextMenu(mousPosition);
            if (owner.WorldRect.Contains(mousPosition))
            {
                ContextMenu.ShowAsContext();
                Event.current.Use();
            }
        }
    }
}