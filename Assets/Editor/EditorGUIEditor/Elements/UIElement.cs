using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace EditorGUIEditor
{
    public enum E_ElementType
    {
        Label,
        Button,
    }
    [Serializable]
    public class UIElement : ScriptableObject
    {
        public UIElement parent;
        public List<UIElement> childs;
        public List<UIElementComponent> components;
        public Rect localRect;
        public virtual Rect WorldRect
        {
            get
            {
                if (null != parent)
                    return localRect.AddPosition(parent.WorldRect);
                else
                    return localRect;
            }
        }

        public E_ElementType type;
        public string elementData;
        public virtual bool ShouldEventSkipSelf { get { return false; } set { return; } }
        public virtual bool ShouldEventSkipChild { get { return false; } set { return; } }

        public void AddChild<T>( T child) where T : UIElement
        {
            AssetDatabase.AddObjectToAsset(child, EditorGUIEditorWindow.Container);
            if (null == childs)
                childs = new List<UIElement>();

            child.parent = this;
            childs.Add(child);

            //AssetDatabase.SaveAssets();
        }

        public virtual void Init()
        {
            if (null == childs)
                childs = new List<UIElement>();

            InitComponent();
        }

        public void InitComponent()
        {
            if (null == components)
                components = new List<UIElementComponent>();

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Init();
            }

            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].InitComponent();
            }
        }

        public void UpdateComponent()
        {
            if (null == components)
                return;

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }

            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].UpdateComponent();
            }
        }

        public void AddComponent<T>() where T : UIElementComponent
        {
            if (null == components)
                components = new List<UIElementComponent>();

            var com = CreateInstance<T>();
            components.Add(com);
            com.SetUIEl(this);
            com.Init();

            AssetDatabase.AddObjectToAsset(com, EditorGUIEditorWindow.Container);
            EditorUtility.SetDirty(this);
        }

        public void Input(Event e)
        {
            //Debug.Log("Type : " + e.type.ToString());
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Input(e);
            }

            if (!ShouldEventSkipChild && null != childs)
            {
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].Input(e);
                }
            }

            if (!ShouldEventSkipSelf )
            {
                switch (e.type)
                {
                    case EventType.MouseUp:
                        {
                            OnMouseUp(e.mousePosition);
                            EditorGUIEditorSelection.DragSelection = null;
                            e.Use();
                        }
                        break;

                    case EventType.MouseDown:
                        if (WorldRect.Contains(e.mousePosition))
                        {                            
                            if (null != EditorGUIEditorSelection.Selected)
                                EditorGUIEditorSelection.Selected.OnDeselected(e.mousePosition);

                            Selection.activeObject = this;
                            EditorGUIEditorSelection.Selected = this;
                            OnSelected(e.mousePosition);
                            e.Use();
                        }
                        break;
                    
                    case EventType.MouseDrag:
                        {
                            if (WorldRect.Contains(e.mousePosition)
                                || this == EditorGUIEditorSelection.Selected)
                            {
                                EditorGUIEditorSelection.DragSelection = this;
                                OnDrag(e.mousePosition, e.delta);
                                e.Use();
                            }
                        }
                        break;
                    case EventType.DragUpdated:
                        {
                            Debug.Log("Drag Update");
                            OnDragUpdate(e.delta);
                            e.Use();
                        }
                        break;
                    case EventType.DragExited:
                        {
                            Debug.Log("Drag Exit");
                            OnDragEnd(e.mousePosition);
                            EditorGUIEditorSelection.DragSelection = null;
                            e.Use();
                        }
                        break;
                    case EventType.Repaint:
                        {
                            OnRepaint(e.mousePosition, e.delta);
                            //e.Use();
                        }
                        break;
                    case EventType.Used:
                        return;

                    default:
                        break;
                }
            }
        }
        public virtual void OnRepaint(Vector2 mousePosition, Vector2 delta)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnRepaint(mousePosition, delta);
            }
        }

        public virtual void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnDrag(mousePosition, delta);
            }
        }
        public virtual void OnDragUpdate(Vector2 updatePosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnDragUpdate(updatePosition);
            }
        }
        public virtual void OnDragEnd(Vector2 endPosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnDragEnd(endPosition);
            }
        }
        public virtual void OnSelected(Vector2 selectedPosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnSelected(selectedPosition);
            }
        }
        public virtual void OnDeselected(Vector2 deselectedPosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnDeselected(deselectedPosition);
            }
        }

        public virtual void OnMouseUp(Vector2 mousePosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnMouseUp(mousePosition);
            }
        }

        public virtual void Draw()
        {
            DrawCoord();

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw();
            }
            
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].Draw();
            }
        }

        public virtual void DrawCoord()
        {
            if (null == parent)
                return;

            using (var g = new Handles.DrawingScope(Color.black))
            {
                Vector3 leftCenter = WorldRect.GetLeftCenter();
                Vector3 rightCenter = WorldRect.GetRightCenter();
                Vector3 centerTop = WorldRect.GetCenterTop();
                Vector3 centerBottom = WorldRect.GetCenterBottom();

                Vector3 left = new Vector3(parent.WorldRect.xMin, WorldRect.center.y);
                Vector3 right = new Vector3(parent.WorldRect.xMax, WorldRect.center.y);
                Vector3 up = new Vector3(WorldRect.center.x, parent.WorldRect.yMin);
                Vector3 bottom = new Vector3(WorldRect.center.x, parent.WorldRect.yMax);

                Vector3 leftLineCenter = left + (leftCenter - left) / 2f;
                Vector3 rightLineCenter = rightCenter + (right - rightCenter) / 2f;
                Vector3 upLineCenter = centerTop + (up - centerTop) / 2f;
                Vector3 bottomLineCenter = centerBottom + (bottom - centerBottom) / 2f;

                Vector2 labelSize = new Vector2(50, 30);

                Handles.DrawLine(left, leftCenter);
                EditorGUI.LabelField(new Rect(leftLineCenter, labelSize), (leftCenter.x - left.x).ToString());

                Handles.DrawLine(right, rightCenter);
                EditorGUI.LabelField(new Rect(rightLineCenter, labelSize), (right.x - rightCenter.x).ToString());

                Handles.DrawLine(centerTop, up);
                EditorGUI.LabelField(new Rect(upLineCenter, labelSize), (centerTop.y - up.y).ToString());

                Handles.DrawLine(centerBottom, bottom);
                EditorGUI.LabelField(new Rect(bottomLineCenter, labelSize), (bottom.y - centerBottom.y).ToString());

                //Draw Width
                EditorGUI.LabelField(new Rect(centerTop, labelSize), localRect.width.ToString());

                rightCenter.x -= 50;
                EditorGUI.LabelField(new Rect(rightCenter, labelSize), localRect.height.ToString());
            }
        }
    }
}