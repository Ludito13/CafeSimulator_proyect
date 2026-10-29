using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player_SM
{

    [System.Serializable]
    public enum PlayerState
    {
        Idle,
        Walking,
        NoMove
    }

    [System.Serializable]
    public class PlayerStateMachine 
    {
        public PlayerState Current {  get; private set; } = PlayerState.Idle;

        public event Action<PlayerState, PlayerState> StateChanged;

        public bool CanEnter(PlayerState s)
        {
            if (s == Current) return false;

            return Current switch
            {
                PlayerState.Idle => s is PlayerState.Walking or
                                         PlayerState.NoMove,
                PlayerState.Walking => s is PlayerState.NoMove or PlayerState.Idle,

                PlayerState.NoMove => s is PlayerState.Idle,

                _ => false
            };
        }

        public bool TryChange(PlayerState s)
        {
            if(!CanEnter(s)) return false;

            PlayerState previous = Current;
            Current = s;

            StateChanged?.Invoke(previous, s);
            return true;
        }
    }
}

