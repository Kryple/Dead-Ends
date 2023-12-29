using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    public class Singleton<T> : MonoBehaviour where T:Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject gameObject = new GameObject();
                        instance = gameObject.AddComponent<T>();
                    }
                }

                return instance;
            }
        }
    }
}