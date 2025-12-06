using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ColliderExtensions 
    {
        public static bool TagIs(this Collider c, string tag) => (c.tag == tag);
    }
}
