using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorGUIDesigner
{
    public static class RectUtility
    {
        public static Vector3 GetLeftTop(this Rect r)
        {
            return new Vector3(r.xMin, r.yMin);
        }

        public static Vector3 GetRightTop(this Rect r)
        {
            return new Vector3(r.xMax, r.yMin);
        }

        public static Vector3 GetLeftBottom(this Rect r)
        {
            return new Vector3(r.xMin, r.yMax);
        }

        public static Vector3 GetRightBottom(this Rect r)
        {
            return new Vector3(r.xMax, r.yMax);
        }

        public static Vector3 GetCenterTop(this Rect r)
        {
            return new Vector3(r.xMax - r.width / 2f, r.yMin);
        }

        public static Vector3 GetCenterBottom(this Rect r)
        {
            return new Vector3(r.xMax - r.width / 2f, r.yMax);
        }

        public static Vector3 GetLeftCenter(this Rect r)
        {
            return new Vector3(r.xMin, r.yMax - r.height / 2f);
        }

        public static Vector3 GetRightCenter(this Rect r)
        {
            return new Vector3(r.xMax, r.yMax - r.height / 2f);
        }

        public static Rect AddPosition(this Rect r1, Rect r2)
        {
            return new Rect(r1.position + r2.position, r1.size);
        }
    }

    public static class LineUtil
    {
        public static void DrawLineWithCap(int sizeOfEdge, Vector2 point1, Vector2 point2, Color color)
        {
            throw new System.NotImplementedException();
        }
    }

    public static class Math
    {
        /// <summary>
        /// 수선의 발을 구한다.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Vector3 GetFootOfPerpendicular(Vector2 point, Vector2 dir, Vector2 targetLinePos1, Vector2 targetLinePos2)
        {
            /*
             * 공식을 찾은 곳
             * https://blog.naver.com/PostView.nhn?blogId=stkov&logNo=90019263178&parentCategoryNo=&categoryNo=25&viewDate=&isShowPopularPosts=true&from=search
             */
            Vector3 p = targetLinePos1;
            Vector3 d = targetLinePos2 - targetLinePos1;
            Vector3 a = new Vector3(point.x, point.y, 0);
            Vector3 foundedPoint = p + (Vector3.Dot((a - p), d) / d.sqrMagnitude) * d;

            return foundedPoint;
        }
    }

    public static class StringExtension
    {
        public static string GetTypeNameWithOutNamespace(this string my)
        {
            var splits = my.Split('.');
            return splits[splits.Length - 1];
        }
    }
}