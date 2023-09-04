using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie
{
    public abstract class TMPSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                /*if (applicationIsQuitting) {
                    Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }*/

                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T), true);

                    if (_instance == null)
                    {
                        Debug.LogError($"No {typeof(T).Name} found on loaded scenes");
                    }

                    return _instance;
                }

                return _instance;
            }
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            //applicationIsQuitting = true;
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}