using UnityEngine;
using System;
using Unity.VisualScripting;

namespace DesignPatterns {
namespace CreationalPatterns{

        public class Singleton<T> : MonoBehaviour where T: Component 
        {
            protected static T instance;
            public static bool HasInstance => instance != null;
            public static T TryGetInstance() => HasInstance ? instance : null;
            public static T Current => instance;


            public static T Instance
            {
                get
                {
                    if(instance != null) return instance;
                    instance = FindAnyObjectByType<T>();
                    if(instance != null) return instance;

                    Debug.LogError("No singleton found");
                    return null;
                }
            }

            protected virtual void Awake() => InitializeSingleton();

            private void InitializeSingleton()
            {
                if (!Application.isPlaying) return;

                if(instance != null && instance != this)
                {
                    Destroy(gameObject);
                    return;
                }

                instance = this as T;
            }
        }


}}