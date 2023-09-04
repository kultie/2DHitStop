using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.EventSystem
{
    public class EventDispatcher
    {
    }

    public class EventDispatcher<T> : EventDispatcher where T : struct
    {
        List<IEventListener<T>> eventListeners;

        public EventDispatcher()
        {
            eventListeners = new List<IEventListener<T>>();
        }

        public void RegisterListener(IEventListener<T> listener)
        {
            eventListeners.Add(listener);
        }

        public void RemoveListener(IEventListener<T> listener)
        {
            eventListeners.Remove(listener);
        }

        public void Invoke(ref T context)
        {
            var listener = new List<IEventListener<T>>(eventListeners);
            foreach (var li in listener)
            {
                try
                {
                    li.Invoke(ref context);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Format("Removing error listener {0}\n{1}", ex.Message, ex.StackTrace));
                    eventListeners.Remove(li);
                }
            }
        }
    }

    public interface IEventListener<T> where T : struct
    {
        public void Invoke(ref T context);
    }
}