namespace Airion.Extensions {
    
    using UnityEngine;

    public static class TransformExtensions {
        //Breadth-first search
        /// <summary>
        /// Breadth-first search for first equal. Don't use with huge child amount.
        /// </summary>
        /// <param name="aParent">Parent for search</param>
        /// <param name="aName">Name of Transform you searching</param>
        /// <returns>Found Transform</returns>
        public static Transform FindDeepChild(this Transform aParent, string aName) {
            var result = aParent.Find(aName);
            if (result != null)
                return result;

            foreach (Transform child in aParent) {
                result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Destroys all transform's children
        /// </summary>
        public static void DestroyAllChildren(this Transform transform) {
            foreach (Transform child in transform) {
                if (Application.isPlaying) GameObject.Destroy(child.gameObject);
                else {
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.delayCall += () => GameObject.DestroyImmediate(child.gameObject);
                    #endif
                }
            }
        }

        /// <summary>
        /// SetActive equivalent for all transform's children by one call
        /// </summary>
        public static void SetActiveAllChildren(this Transform transform, bool isActive) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(isActive);
            }
        }

        /// <summary>
        /// Transfer to scene root
        /// </summary>
        public static void Unchild(this Transform transform) {
            transform.SetParent(null);
        }
    }
}