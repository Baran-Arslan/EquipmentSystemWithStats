using System.Collections.Generic;

namespace _Common.iCare.Extensions {
    public static class ListExtensions {
        
        public static void DestroyAndClear<T>(this ICollection<T> collection) where T : UnityEngine.Object {
            foreach (var item in collection) {
                UnityEngine.Object.Destroy(item);
            }
            collection.Clear();
        }
    }
}