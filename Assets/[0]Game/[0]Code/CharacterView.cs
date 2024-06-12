using UnityEngine;

namespace Game
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private GameObject _danger;
        
        [SerializeField] 
        private Animator _animator;
        
        public void Flip(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }

        public void Damage()
        {
            _animator.SetTrigger("Damage");
        }

        public void Step()
        {
            _animator.SetFloat("Speed", 0.5f);
        }

        public void Idle()
        {
            _animator.SetFloat("Speed", 0);
        }

        public void DangerSwitch(bool isActive) => 
            _danger.SetActive(isActive);

        public void Run()
        {
            _animator.SetFloat("Speed", 1);
        }
    }
}