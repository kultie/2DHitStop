using UnityEngine;

namespace Kultie.StateMachine.Behavior
{
    public abstract class StateContextComponent : MonoBehaviour
    {
        public StateMachineComponent StateMachine { private set; get; }

        private void Awake()
        {
            StateMachine = GetComponent<StateMachineComponent>();
            OnAwake();
        }

        protected abstract void OnAwake();

        public void ChangeState(string id)
        {
            StateMachine.ChangeState(id);
        }
    }
}