using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace EditorGUIDesigner
{
    public enum E_ElementType
    {
        Label,
        Button,
    }
    [Serializable]
    public class UIElement : ScriptableObject
    {
        public bool fold = false;
        const float DOTTED_VERTICAL_LINE_WIDTH = 1f;
        const float DOTTED_HORIZONTAL_LINE_WIDTH = .3f;

        public static float XMax
        {
            set
            {
                xMax = value;
            }
            get
            {
                return (xMax <= 10f) ? 10 : xMax;
            }
        }
        public static float YMax
        {
            set
            {
                yMax = value;
            }
            get
            {
                return (yMax <= 10f) ? 10 : yMax;
            }
        }

        static float xMax = 0;
        static float yMax = 0;

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


        public Color tintColor;

        public void AddChild<T>() where T : UIElement
        {
            T newChild = CreateInstance<T>();
            newChild.parent = this;
            newChild.Rename();
            newChild.localRect.size = new Vector2(100, 50);
            newChild.localRect.position = new Vector2(100, 100);
            newChild.Init();

            AddChild(newChild);
        }

        public void AddChild<T>(T child) where T : UIElement
        {
            AssetDatabase.AddObjectToAsset(child, EditorGUIEditorWindow.Container);
            if (null == childs)
                childs = new List<UIElement>();

            child.parent = this;
            childs.Add(child);

            EditorUtility.SetDirty(EditorGUIEditorWindow.Container);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }

        public void RemoveChild(UIElement oldChild)
        {
            childs.Remove(oldChild);
        }

        public virtual void Init()
        {
            Rename();
            tintColor = Color.white;

            if (null == childs)
                childs = new List<UIElement>();

            InitComponent();
        }

        public void Rename()
        {
            name = $"new{this.GetType().ToString().GetTypeNameWithOutNamespace()}";
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
            if (null != components)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Update();
                }
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
            com.Owner = this;
            com.Init();
            components.Add(com);

            AssetDatabase.AddObjectToAsset(com, EditorGUIEditorWindow.Container);
            EditorUtility.SetDirty(EditorGUIEditorWindow.Container);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void Input(Event e)
        {
            //Debug.Log("Type : " + e.type.ToString());
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Input(e);
            }

            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].Input(e);
            }

            switch (e.type)
            {
                case EventType.MouseUp: OnMouseUp(e.mousePosition); break;
                case EventType.MouseDrag: OnDrag(e.mousePosition, e.delta); break;
                case EventType.DragUpdated: OnDragUpdate(e.delta); break;
                case EventType.DragExited: OnDragEnd(e.mousePosition); break;
                case EventType.Repaint: OnRepaint(e.mousePosition, e.delta); break;
                case EventType.MouseDown:
                    {
                        switch (e.button)
                        {
                            case 0:
                                OnMouseDown(e.mousePosition);
                                break;

                            case 1:
                                OnContextMenu(e.mousePosition);
                                break;
                        }
                    }
                    break;
                //case EventType.MouseMove: OnMouseMove(e.mousePosition, e.delta); break;
                case EventType.Used: return;
                default:
                    break;
            }
        }

        private void OnMouseMove(Vector2 mousePosition, Vector2 delta)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnMouseMove(mousePosition, delta);
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
        public virtual void OnMouseDown(Vector2 selectedPosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnMouseDown(selectedPosition);
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

        public virtual void OnContextMenu(Vector2 mousePosition)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnContextMenu(mousePosition);
            }
        }

        public virtual void RemoveAsset()
        {
            while(childs.Count > 0)
            {
                childs[0].RemoveAsset();
                DestroyImmediate(childs[0], true);
                childs.RemoveAt(0);
            }


            while(components.Count > 0)
            {
                Debug.Log($"remove component : {components[0].name}");
                DestroyImmediate(components[0], true);
                components.RemoveAt(0);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void Draw()
        {
            var tempColor = GUI.color;
            GUI.color = tintColor;

            OnDraw();

            GUI.color = tempColor;

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

        public virtual void OnDraw() { }

        public void RefreshMaxXY()
        {
            XMax = WorldRect.xMax > XMax ? WorldRect.xMax : XMax;
            YMax = WorldRect.yMax > YMax ? WorldRect.yMax : YMax;
        }

        public virtual void DrawCoord()
        {
            if (null == parent)
                return;

            if (this != UISelectable.Selected)
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

                Handles.DrawDottedLine(left, leftCenter, DOTTED_HORIZONTAL_LINE_WIDTH);
                EditorGUI.LabelField(new Rect(leftLineCenter, labelSize), (leftCenter.x - left.x).ToString());

                Handles.DrawDottedLine(right, rightCenter, DOTTED_HORIZONTAL_LINE_WIDTH);
                EditorGUI.LabelField(new Rect(rightLineCenter, labelSize), (right.x - rightCenter.x).ToString());

                Handles.DrawDottedLine(centerTop, up, DOTTED_VERTICAL_LINE_WIDTH);
                EditorGUI.LabelField(new Rect(upLineCenter, labelSize), (centerTop.y - up.y).ToString());

                Handles.DrawDottedLine(centerBottom, bottom, DOTTED_VERTICAL_LINE_WIDTH);
                EditorGUI.LabelField(new Rect(bottomLineCenter, labelSize), (bottom.y - centerBottom.y).ToString());

                //Draw Width
                EditorGUI.LabelField(new Rect(centerTop, labelSize), localRect.width.ToString());

                rightCenter.x -= 50;
                EditorGUI.LabelField(new Rect(rightCenter, labelSize), localRect.height.ToString());
            }
        }
    }
}
