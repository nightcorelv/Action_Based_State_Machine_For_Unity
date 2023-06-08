using UnityEngine;

namespace Core.Utils
{
    public static class GameObject
    {

        /// <summary>
        /// Returns true if object is prefab instance, prefab asset or model asset
        /// </summary>
        public static bool IsPrefab(UnityEngine.GameObject obj) => obj.scene.rootCount == 0;
        

    }

}