using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorGUIEditor
{
    public enum E_SelectedPoint
    {
        None,
        LeftTop,
        LeftCenter,
        LeftBottom,
        CenterTop,
        CenterBottom,
        RightTop,
        RightCenter,
        RightBottom,
    }
    [Serializable]
    public class UIResizeable : UIElementComponent
    {
        static UIElement selected = null;
        Vector2 PointerSize
        {
            get
            {
                return new Vector2(10, 10);
            }
        }
        bool resizeStart = false;
        E_SelectedPoint hoveredPoint = E_SelectedPoint.None;

        public override void Draw()
        {
            base.Draw();

            if (selected == owner || resizeStart)
            {
                using (var h = new Handles.DrawingScope(Color.blue))
                {
                    Handles.DrawAAPolyLine(owner.WorldRect.GetLeftBottom(), owner.WorldRect.GetRightBottom(),
                                            owner.WorldRect.GetRightTop(), owner.WorldRect.GetLeftTop(),
                                            owner.WorldRect.GetLeftBottom());

                    Handles.DrawSolidRectangleWithOutline(LeftTopPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(LeftCenterPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(LeftBottomPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(CenterTopPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(CenterBottomPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightTopPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightCenterPoint, new Color(0, 0, 0, 0), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightBottomPoint, new Color(0, 0, 0, 0), Color.black);
                }
            }
        }


        public override void OnMouseDown(Vector2 mousePosition)
        {
            //Debug.Log("mouse down");
            if (owner.WorldRect.Contains(mousePosition))
                selected = owner;


            hoveredPoint = E_SelectedPoint.None;

            ContainsMousePositionAndStartResize(LeftTopPoint, mousePosition, E_SelectedPoint.LeftTop);
            ContainsMousePositionAndStartResize(LeftCenterPoint, mousePosition, E_SelectedPoint.LeftCenter);
            ContainsMousePositionAndStartResize(LeftBottomPoint, mousePosition, E_SelectedPoint.LeftBottom);
            ContainsMousePositionAndStartResize(CenterTopPoint, mousePosition, E_SelectedPoint.CenterTop);
            ContainsMousePositionAndStartResize(CenterBottomPoint, mousePosition, E_SelectedPoint.CenterBottom);
            ContainsMousePositionAndStartResize(RightTopPoint, mousePosition, E_SelectedPoint.RightTop);
            ContainsMousePositionAndStartResize(RightCenterPoint, mousePosition, E_SelectedPoint.RightCenter);
            ContainsMousePositionAndStartResize(RightBottomPoint, mousePosition, E_SelectedPoint.RightBottom);
        }

        public override void OnMouseUp(Vector2 mousePosition)
        {
            base.OnMouseUp(mousePosition);
            resizeStart = false;
        }

        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            base.OnDrag(mousePosition, delta);
            if (resizeStart)
            {
                Debug.Log("UI Resizeable : OnDrag");
                Event.current.Use();
                switch (hoveredPoint)
                {
                    case E_SelectedPoint.None:
                        break;
                    case E_SelectedPoint.LeftTop:
                        {
                            owner.localRect.xMin += delta.x;
                            owner.localRect.yMin += delta.y;
                        }
                        break;
                    case E_SelectedPoint.LeftCenter:
                        {
                            owner.localRect.xMin += delta.x;
                        }
                        break;
                    case E_SelectedPoint.LeftBottom:
                        {
                            owner.localRect.xMin += delta.x;
                            owner.localRect.yMax += delta.y;
                        }
                        break;
                    case E_SelectedPoint.CenterTop:
                        {
                            owner.localRect.yMin += delta.y;
                        }
                        break;
                    case E_SelectedPoint.CenterBottom:
                        {
                            owner.localRect.yMax += delta.y;
                        }
                        break;
                    case E_SelectedPoint.RightTop:
                        {
                            owner.localRect.xMax += delta.x;
                            owner.localRect.yMin += delta.y;
                        }
                        break;
                    case E_SelectedPoint.RightCenter:
                        {
                            owner.localRect.xMax += delta.x;
                        }
                        break;
                    case E_SelectedPoint.RightBottom:
                        {
                            owner.localRect.xMax += delta.x;
                            owner.localRect.yMax += delta.y;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        void ContainsMousePositionAndStartResize(Rect r, Vector2 p, E_SelectedPoint where)
        {
            if (r.Contains(p))
            {
                resizeStart = true;
                hoveredPoint = where;
                selected = owner;
            }
        }

        Rect LeftTopPoint
        {
            get
            {
                Vector3 p = owner.WorldRect.GetLeftTop();
                p.x -= PointerSize.x;
                p.y -= PointerSize.y;

                return new Rect(p, PointerSize);
            }
        }
        Rect LeftCenterPoint
        {
            get
            {
                Vector3 position = owner.WorldRect.GetLeftCenter();
                position.x -= PointerSize.x;
                position.y -= PointerSize.y / 2f;

                return new Rect(position, PointerSize);
            }
        }
        Rect LeftBottomPoint
        {
            get
            {
                Vector3 position = owner.WorldRect.GetLeftBottom();
                position.x -= PointerSize.x;
                return new Rect(position, PointerSize);
            }
        }
        Rect CenterTopPoint
        {
            get
            {
                Vector3 p = owner.WorldRect.GetCenterTop();
                p.x -= PointerSize.x / 2f;
                p.y -= PointerSize.y;

                return new Rect(p, PointerSize);
            }
        }
        Rect CenterBottomPoint
        {
            get
            {
                Vector3 p = owner.WorldRect.GetCenterBottom();
                p.x -= PointerSize.x / 2f;

                return new Rect(p, PointerSize);
            }
        }
        Rect RightTopPoint
        {
            get
            {
                Vector3 p = owner.WorldRect.GetRightTop();
                p.y -= PointerSize.y;

                return new Rect(p, PointerSize);
            }
        }
        Rect RightCenterPoint
        {
            get
            {
                Vector3 p = owner.WorldRect.GetRightCenter();
                p.y -= PointerSize.y / 2f;

                return new Rect(p, PointerSize);
            }
        }
        Rect RightBottomPoint
        {
            get
            {
                return new Rect(owner.WorldRect.GetRightBottom(), PointerSize);
            }
        }
    }
}