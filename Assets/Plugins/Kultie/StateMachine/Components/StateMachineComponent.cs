using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.StateMachine.Behavior
{
    [DefaultExecutionOrder(-1)]
    public abstract class StateMachineComponent : MonoBehaviour
    {
        public abstract void ChangeState(string id);
        public abstract string GetCurrentState();
        public abstract void ManualUpdate(float dt);
    }

    public class StateMachineComponent<T> : StateMachineComponent where T : StateContextComponent
    {
        public T context { private set; get; }
        private Dictionary<string, StateComponent<T>> _states = new Dictionary<string, StateComponent<T>>();
        protected StateComponent<T> currentState;
        public bool manualUpdate;

        protected virtual void Start()
        {
            context = GetComponent<T>();
            StateComponent<T>[] states = GetComponentsInChildren<StateComponent<T>>(true);
            for (int i = 0; i < states.Length; i++)
            {
                _states[states[i].ID] = states[i];
            }

            var defaultState = states.FirstOrDefault(t => t.isDefault);
            if (defaultState != null)
            {
                ChangeState(defaultState.ID);
            }
        }

        private void Update()
        {
            if (manualUpdate) return;
            if (currentState)
            {
                currentState.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (manualUpdate) return;
            if (currentState)
            {
                currentState.OnFixedUpdate();
            }
        }

        public override void ManualUpdate(float dt)
        {
            if (!manualUpdate) return;
            if (currentState)
            {
                currentState.OnUpdate(dt);
            }
        }

        [Button]
        public override void ChangeState(string id)
        {
            if (currentState)
            {
                currentState.OnExit();
            }

            currentState = null;
            if (_states.TryGetValue(id, out currentState))
            {
                currentState.OnEnter();
                return;
            }

            Debug.LogError("State with " + id + " is not existed");
        }

        public override string GetCurrentState()
        {
            return currentState.ID;
        }
    }
}