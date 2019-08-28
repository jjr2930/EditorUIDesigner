using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace EditorGUIEditor
{
    public class UIResizeable : UIElementComponent
    {
        //Rect leftTop;
        //Rect rightTop;
        //Rect leftBottom;
        //Rect rightBottom;

        public enum E_SelectedPoint
        {
            None,
            LeftTop,
            CenterTop,
            RightTop,
            LeftCenter,
            Center,
            RightCenter,
            LeftBottom,
            CenterBottom,
            RightBottom
        }

        const float CAN_CANTOROL_DISTANCE = 10f;
        const float ADD_CUSOR_RECT_WIDTH = 10f;
        E_SelectedPoint selectedPoint;

        public override void Init()
        {
            base.Init();

            selectedPoint = E_SelectedPoint.None;

            //leftTop.position = uiEl.WorldRect.GetLeftTop();
            //leftTop.size = new Vector2(10, 10);

            //rightTop.position = uiEl.WorldRect.GetRightTop();
            //rightTop.size = new Vector2(10, 10);

            //leftBottom.position = uiEl.WorldRect.GetLeftBottom();
            //leftBottom.size = new Vector2(10, 10);

            //rightBottom.position = uiEl.WorldRect.GetRightBottom();
            //rightBottom.size = new Vector2(10, 10);
        }

        void Resize()
        {
            //uiEl.localRect.xMin = leftTop.x;
            //uiEl.localRect.xMax = rightBottom.x;
            //uiEl.localRect.yMin = rightBottom.y;
            //uiEl.localRect.yMax = leftTop.y;
        }

        public override void Draw()
        {
            base.Draw();
            //if (GUI.Button(leftTop, "")) { }
            //if (GUI.Button(leftBottom, "")) { }
            //if (GUI.Button(rightTop, "")) { }
            //if (GUI.Button(rightBottom, "")) { }

            if (EditorGUIEditorSelection.Selected == uiElement)
            {
                using (var h = new Handles.DrawingScope(Color.blue))
                {
                    Handles.DrawAAPolyLine(CAN_CANTOROL_DISTANCE, uiElement.WorldRect.GetLeftTop(), uiElement.WorldRect.GetRightTop(),
                                                                    uiElement.WorldRect.GetRightBottom(), uiElement.WorldRect.GetLeftBottom(), uiElement.WorldRect.GetLeftTop());

                }
            }
        }

        public override void OnMouseUp(Vector2 mousePosition)
        {
            base.OnMouseUp(mousePosition);
            selectedPoint = E_SelectedPoint.None;
            EditorGUIEditorSelection.ResizeSelection = null;
        }



        public override void OnRepaint(Vector2 mousePosition, Vector2 delta)
        {
            base.OnRepaint(mousePosition, delta);
            if (EditorGUIEditorSelection.ResizeSelection == uiElement)
            {
                DrawCursor();
                return;
            }

            selectedPoint = DragOverPoint(mousePosition);
            //Debug.Log("SelectPoint : " + selectedPoint.ToString());
            DrawCursor();
        }

        private void DrawCursor()
        {
            switch (selectedPoint)
            {
                case E_SelectedPoint.LeftTop:
                    {
                        Vector2 leftTop = uiElement.WorldRect.GetLeftTop();
                        Rect mouseRect = new Rect(leftTop.x, leftTop.y, ADD_CUSOR_RECT_WIDTH, ADD_CUSOR_RECT_WIDTH);
                        EditorGUIUtility.AddCursorRect(mouseRect, MouseCursor.ResizeUpLeft);
                    }
                    break;

                case E_SelectedPoint.CenterTop:
                    {
                        Rect mouseRect = new Rect(uiElement.WorldRect.xMin, uiElement.WorldRect.yMin, uiElement.WorldRect.width, ADD_CUSOR_RECT_WIDTH);
                        EditorGUIUtility.AddCursorRect(mouseRect, MouseCursor.ResizeVertical);
                    }
                    break;

                case E_SelectedPoint.None:
                    EditorGUIEditorSelection.ResizeSelection = null;
                    break;

                default:
                    break;
            }
        }

        public override void OnSelected(Vector2 selectedPosition)
        {
            base.OnSelected(selectedPosition);

            if (E_SelectedPoint.None != selectedPoint)
                EditorGUIEditorSelection.ResizeSelection = uiElement;
        }



        public override void OnDrag(Vector2 mousePosition, Vector2 delta)
        {
            base.OnDrag(mousePosition, delta);
            selectedPoint = DragOverPoint(mousePosition);
            if(selectedPoint != E_SelectedPoint.None)
            {
                Event.current.Use();
                EditorGUIEditorSelection.ResizeSelection = uiElement;
                switch (selectedPoint)
                {
                    case E_SelectedPoint.LeftTop:
                            uiElement.localRect.xMin += delta.x;
                            uiElement.localRect.yMin += delta.y;
                        break;

                    case E_SelectedPoint.CenterTop:
                            uiElement.localRect.yMin += delta.y;
                        break;

                    case E_SelectedPoint.RightTop:
                            uiElement.localRect.xMax += delta.x;
                            uiElement.localRect.yMin += delta.y;
                        break;

                    case E_SelectedPoint.LeftCenter:
                            uiElement.localRect.xMin += delta.x;
                        break;

                    case E_SelectedPoint.RightCenter:
                        uiElement.localRect.xMax += delta.x;
                        break;

                    case E_SelectedPoint.LeftBottom:
                        uiElement.localRect.xMin += delta.x;
                        uiElement.localRect.yMax += delta.y;
                        break;

                    case E_SelectedPoint.CenterBottom:
                        uiElement.localRect.yMax += delta.y;
                        break;

                    case E_SelectedPoint.RightBottom:
                        uiElement.localRect.xMax += delta.x;
                        uiElement.localRect.yMax += delta.y;
                        break;

                    default:
                        break;
                }
            }
        }

        E_SelectedPoint DragOverPoint(Vector2 mousePosition)
        {
            Rect wr = uiElement.WorldRect;
            if (IsEnteredPoint(wr.GetLeftTop(), mousePosition))
            {
                return E_SelectedPoint.LeftTop;
            }
            else if (IsEnteredPoint(wr.GetRightTop(), mousePosition))
            {
                return E_SelectedPoint.RightTop;
            }
            else if (IsEnteredPoint(wr.GetLeftCenter(), mousePosition))
            {
                return E_SelectedPoint.LeftCenter;
            }
            else if (IsEnteredPoint(wr.GetRightCenter(), mousePosition))
            {
                return E_SelectedPoint.RightCenter;
            }
            else if (IsEnteredPoint(wr.GetLeftBottom(), mousePosition))
            {
                return E_SelectedPoint.LeftBottom;
            }
            else if (IsEnteredPoint(wr.GetRightBottom(), mousePosition))
            {
                return E_SelectedPoint.RightBottom;
            }
            else if (IsEnteredPoint(wr.center, mousePosition))
            {
                return E_SelectedPoint.Center;
            }
            else if (IsEnteredPoint(wr.GetCenterBottom(), mousePosition))
            {
                return E_SelectedPoint.CenterBottom;
            }
            else if (IsEnteredPoint(wr.GetCenterTop(), mousePosition))
            {
                return E_SelectedPoint.CenterTop;
            }

            return E_SelectedPoint.None;
        }

        public bool IsEnteredPoint(Vector2 point, Vector2 mousePosition)
        {
            return (point - mousePosition).magnitude <= CAN_CANTOROL_DISTANCE;
        }


        //public override void Input(Event e)
        //{
        //    base.Input(e);
        //    switch (e.type)
        //    {
        //        case EventType.MouseDown:
        //            {
        //                if (EditorGUIEditorSelection.Selected == uiEl)
        //                {
        //                    if (leftTop.Contains(e.mousePosition))
        //                    {
        //                        leftTop.position += e.delta;

        //                        var temp = leftBottom.position;
        //                        temp.x += e.delta.x;
        //                        leftBottom.position = temp;

        //                        temp = rightTop.position;
        //                        temp.y += e.delta.y;
        //                        rightTop.position = temp;

        //                        Resize();

        //                        e.Use();
        //                    }

        //                    else if (leftBottom.Contains(e.mousePosition))
        //                    {
        //                        leftBottom.position += e.delta;

        //                        var temp = leftTop.position;
        //                        temp.x += e.delta.x;
        //                        leftTop.position = temp;

        //                        temp = rightBottom.position;
        //                        temp.y += e.delta.y;

        //                        rightBottom.position = temp;

        //                        Resize();

        //                        e.Use();
        //                    }

        //                    else if (rightTop.Contains(e.mousePosition))
        //                    {
        //                        rightTop.position += e.delta;

        //                        var temp = rightBottom.position;
        //                        temp.x += e.delta.x;
        //                        rightBottom.position = temp;

        //                        temp = leftTop.position;
        //                        temp.y += e.delta.y;
        //                        leftTop.position = temp;

        //                        Resize();

        //                        e.Use();
        //                    }

        //                    else if (rightBottom.Contains(e.mousePosition))
        //                    {
        //                        rightBottom.position += e.delta;

        //                        var temp = rightTop.position;
        //                        temp.x += e.delta.x;
        //                        rightTop.position = temp;

        //                        temp = leftTop.position;
        //                        temp.y += e.delta.y;
        //                        leftTop.position = temp;

        //                        Resize();

        //                        e.Use();
        //                    }
        //                }

        //                if (EditorGUIEditorSelection.DragSelection == uiEl)
        //                {
        //                    leftTop.position += e.delta;
        //                    leftBottom.position += e.delta;
        //                    rightTop.position += e.delta;
        //                    rightBottom.position += e.delta;
        //                }
        //            }
        //            break;
        //    }
        //}
    }
}