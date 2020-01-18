using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorGUIDesigner
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


                    Handles.DrawSolidRectangleWithOutline(LeftTopPoint, GetPointColor(E_SelectedPoint.LeftTop), Color.black);
                    Handles.DrawSolidRectangleWithOutline(LeftCenterPoint, GetPointColor(E_SelectedPoint.LeftCenter), Color.black);
                    Handles.DrawSolidRectangleWithOutline(LeftBottomPoint, GetPointColor(E_SelectedPoint.LeftBottom), Color.black);
                    Handles.DrawSolidRectangleWithOutline(CenterTopPoint, GetPointColor(E_SelectedPoint.CenterTop), Color.black);
                    Handles.DrawSolidRectangleWithOutline(CenterBottomPoint, GetPointColor(E_SelectedPoint.CenterBottom), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightTopPoint, GetPointColor(E_SelectedPoint.RightTop), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightCenterPoint, GetPointColor(E_SelectedPoint.RightCenter), Color.black);
                    Handles.DrawSolidRectangleWithOutline(RightBottomPoint, GetPointColor(E_SelectedPoint.RightBottom), Color.black);
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
            hoveredPoint = E_SelectedPoint.None;
        }

        //public override void OnMouseMove(Vector2 mousePosition, Vector2 delta)
        //{
        //    base.OnMouseMove(mousePosition, delta);
        //    if (LeftTopPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.LeftTop;
        //    else if (LeftCenterPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.LeftCenter;
        //    else if (LeftBottomPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.LeftBottom;
        //    else if (CenterTopPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.CenterTop;
        //    else if (CenterBottomPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.CenterBottom;
        //    else if (RightTopPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.RightTop;
        //    else if (RightCenterPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.RightCenter;
        //    else if (RightBottomPoint.Contains(mousePosition))
        //        hoveredPoint = E_SelectedPoint.RightBottom;
        //    else
        //        hoveredPoint = E_SelectedPoint.None;
        //}

        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            base.OnDrag(mousePosition, delta);
            if (resizeStart)
            {
                //Debug.Log("UI Resizeable : OnDrag");
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


                owner.RefreshMaxXY();
            }
        }

        Color GetPointColor(E_SelectedPoint where)
        {
            return where == hoveredPoint ? Color.black : new Color(0f, 0f, 0f, 0f);
        }

        void ContainsMousePositionAndStartResize(Rect r, Vector2 p, E_SelectedPoint where)
        {
            if (r.Contains(p))
            {
                resizeStart = true;
                hoveredPoint = where;
                selected = owner;
                switch (where)
                {
                    case E_SelectedPoint.LeftTop:
                    case E_SelectedPoint.RightBottom:
                        EditorGUIUtility.AddCursorRect(r, MouseCursor.ResizeUpLeft);
                        break;

                    case E_SelectedPoint.CenterTop:
                    case E_SelectedPoint.CenterBottom:
                        EditorGUIUtility.AddCursorRect(r, MouseCursor.ResizeVertical);
                        break;

                    case E_SelectedPoint.RightTop:
                    case E_SelectedPoint.LeftBottom:
                        EditorGUIUtility.AddCursorRect(r, MouseCursor.ResizeUpRight);
                        break;

                    case E_SelectedPoint.RightCenter:
                    case E_SelectedPoint.LeftCenter:
                        EditorGUIUtility.AddCursorRect(r, MouseCursor.ResizeHorizontal);
                        break;
                    default:
                        break;
                }
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