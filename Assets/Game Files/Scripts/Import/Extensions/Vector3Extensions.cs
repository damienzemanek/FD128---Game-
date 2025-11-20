using UnityEngine;

namespace Extensions
{
    public static class Vector3EX
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            => new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);

        public static Vector3 WithScale(this Vector3 v, Vector3 scale)
            => Vector3.Scale(v, scale);
    }
}