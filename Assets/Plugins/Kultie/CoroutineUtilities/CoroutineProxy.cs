using System.Collections;
using UnityEngine;

namespace Kultie
{
    public class CoroutineProxy : MonoBehaviour
    {
        private static CoroutineProxy _instance;

        public static CoroutineProxy Instance
        {
            get
            {
                if (!_instance)
                {
                    GameObject target = new GameObject(nameof(CoroutineProxy));
                    _instance = target.AddComponent<CoroutineProxy>();
                    DontDestroyOnLoad(target);
                    target.hideFlags = HideFlags.HideAndDontSave;
                }

                return _instance;
            }
        }

        public static CoroutineReference Schedule(IEnumerator routine)
        {
            return Instance.Schedule(routine);
        }
    }
}