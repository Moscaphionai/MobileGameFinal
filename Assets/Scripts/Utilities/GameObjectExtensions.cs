using UnityEngine;

namespace Utilities
{
    public static class GameObjectExtensions
    {
        public static void ClearChildren(this GameObject gameObject)
        {
            var transform = gameObject.transform;

            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
