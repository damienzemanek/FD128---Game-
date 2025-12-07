using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Extensions;
using static Extensions.DelegateEX;
using Sirenix.OdinInspector;

public static class  SignalUtility
{
    //TRANSFORM, FILTER, MODIFY, BEFORE STORAGE
    [Serializable]
    public struct Interceptable<T> where T : IEquatable<T>
    {
        //    First T is the TYPE we are using: int, float, bool etc
        //   Second T is the VALUE its changed to to use wherever else
        public Func<T, T> Intercept;

        [SerializeField] T _value;
        public T value
        {
            get => _value;
            set
            {
                // Compares generics of type T.
                // doesnt box
                //.Default returns the best equality comparator for the exact type T
                //.Equals calls the logic
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;

                _value = (Intercept != null)
                    ? Intercept(value)  // Func decides final value
                    : value;
            }
        }


    }

    //OBSERVE, NOTIFY AFTER STORAGE
    [Serializable]
    public struct Reactable<T> where T : IEquatable<T>
    {
        public Action<T> OnChanged;

        [SerializeField] T _value;
        public T value
        {
            get => _value;

            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;
                _value = value;
                OnChanged?.Invoke(value);
            }
        }
    }

    public static Reactable<T> Sub<T>(ref this Reactable<T> reactable, Action<T> method) where T : IEquatable<T>
    {
        if(!reactable.OnChanged.Contains(method))
            reactable.OnChanged += method;
        return reactable;
    }

}
