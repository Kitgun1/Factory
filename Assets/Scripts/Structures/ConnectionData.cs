using System;
using UnityEngine;

namespace Shoping
{
    [Serializable]
    public struct ConnectionData<T1, T2>
    {
        public T1 TValue1;
        public T2 TValue2;

        public ConnectionData(T1 value1, T2 value2)
        {
            TValue1 = value1;
            TValue2 = value2;
        }
    }
}