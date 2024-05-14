using UnityEngine;

namespace Game
{
    public class StateMachine : MonoBehaviour
    {
        [HideInInspector]
        public BaseState CurrentState;

        public void Initialize(BaseState state)
        {
            CurrentState = state;
            state.Enter();
        }

        public void ChangeState(BaseState state)
        {
            CurrentState.Exit();

            CurrentState = state;
            state.Enter();
        }
    }
}