using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;

        [FormerlySerializedAs("_speed")] [SerializeField] 
        private float _idleSpeed = 3;

        [SerializeField] 
        private float _runSpeed = 5;
        
        public void Move(Vector2 direction, bool isRun)
        {
            _rigidbody.velocity = direction *  (isRun ? _runSpeed : _idleSpeed);
        }
    }
}