using UnityEngine;

namespace Kultie
{
    public struct CoroutineReference
    {
        public Coroutine Coroutine { get; private set; }

        public MonoBehaviour MonoBehaviour { get; private set; }

        public CoroutineReference(MonoBehaviour monoBehaviour, Coroutine coroutine)
        {
            MonoBehaviour = monoBehaviour;
            Coroutine = coroutine;
        }

        public void Stop()
        {
            if (Coroutine == null || !MonoBehaviour)
                return;
            MonoBehaviour.StopCoroutine(this.Coroutine);
            Coroutine = null;
        }

        public void Clear() => Coroutine = null;
    }
}