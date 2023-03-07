using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct PlayerHotKeyData
    {
        public PlayerActionType Action;
        public KeyCode KeyCode;
    }
}