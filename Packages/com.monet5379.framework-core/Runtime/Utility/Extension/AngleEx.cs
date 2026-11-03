using UnityEngine;

namespace Monet5379
{
    public static class AngleEx
    {
        public static float Abs(this float angle)
        {
            float result = angle;

            if (result < 0)
            {
                result += 360;
            }

            if (angle >= 360)
            {
                result %= 360;
            }

            return result;
        }

        public static float ToAngle(this Vector2 from, Vector2 to, bool useAbs = true)
        {
            if (from != null && to != null)
            {
                Vector2 vectorTo = VectorEx.Normalize(to - from);

                return ToAngle(vectorTo, useAbs);
            }

            return 0f;
        }

        public static float ToAngle(this Vector2 vectorTo, bool useAbs = true)
        {
            float angle = Mathf.Atan2(vectorTo.y, vectorTo.x) * Mathf.Rad2Deg;
            if (false == angle.IsNaN())
            {
                if (useAbs)
                {
                    return Abs(angle);
                }
                else
                {
                    return angle;
                }
            }

            return 0f;
        }

        public static float ToAngle90(this Vector2 direction)
        {
            Vector2 referenceVector = Vector2.right;

            float dotProduct = Vector2.Dot(referenceVector, direction.normalized);

            dotProduct = Mathf.Clamp(dotProduct, -1.0f, 1.0f);

            float angleInRadians = Mathf.Acos(dotProduct);

            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            if (angleInDegrees > 90)
            {
                angleInDegrees = 180 - angleInDegrees;
            }

            return angleInDegrees;
        }

        public static Vector3 ToVector3(this float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        }

        public static Vector3 ToVector3(this Quaternion quaternion)
        {
            return quaternion * Vector3.right;
        }

        public static Quaternion ToQuaternion(this Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}