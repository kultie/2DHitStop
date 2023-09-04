using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.StateMachine.Behavior
{
    public abstract class StateComponent<T> : MonoBehaviour where T : StateContextComponent
    {
        protected T Context;
        protected StateMachineComponent<T> StateMachine;

        private void Awake()
        {
            StateMachine = GetComponent<StateMachineComponent<T>>();
            if (StateMachine == null)
            {
                StateMachine = FindObjectOfType<StateMachineComponent<T>>();
            }

            Context = GetComponent<T>();

            if (Context == null)
            {
                Context = FindObjectOfType<T>();
            }
        }

        public abstract string ID { get; }
        public bool isDefault;
        public abstract void OnUpdate(float dt);

        public virtual void OnFixedUpdate()
        {
        }

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}