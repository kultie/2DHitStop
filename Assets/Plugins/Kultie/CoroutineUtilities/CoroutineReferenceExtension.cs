using System.Collections;
using UnityEngine;

namespace Kultie
{
    public static class CoroutineReferenceExtension
    {
        public static CoroutineReference Schedule(this MonoBehaviour monoBehaviour,
            IEnumerator routine)
        {
            return new CoroutineReference(monoBehaviour, monoBehaviour.StartCoroutine(routine));
        }
    }
}