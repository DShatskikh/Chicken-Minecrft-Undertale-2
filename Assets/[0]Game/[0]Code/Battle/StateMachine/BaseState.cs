using UnityEngine;

namespace Game
{
    public abstract class BaseState : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Upgrade();
        public abstract void Exit();
    }
}