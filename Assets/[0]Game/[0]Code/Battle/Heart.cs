using System;
using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] 
        private Vector2 _sizeField;

        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private Shield _shield;

        [SerializeField] 
        private AudioSource _damageSource;

        [SerializeField]
        private AnimationCurve _jumpCurve;

        [SerializeField]
        private float _jumpForce;
        
        [SerializeField]
        private float _gravity;

        [SerializeField]
        private float _maxVelocityY;

        [SerializeField]
        private float _maxJumpDuration;
        
        private bool _isInvulnerability;

        float _velocitySpeed = 0f;
        float _jumpDuration = 0f;
        bool _isGround;
        
        [SerializeField]
        private float _planingSpeed;

        private void Update()
        {
            var position = (Vector2)transform.position;
            
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
            {
                direction = GameData.Joystick.Direction.normalized; 
            }

            if (direction.y < 0)
                direction.y = 0;

            if (_isGround && direction.y > 0 && _jumpDuration < _maxJumpDuration)
                _velocitySpeed = _jumpForce;

            //if (direction.y == 0 && _velocitySpeed > 0)
            //    _velocitySpeed = 0;
            
            if (!_isGround)
            {
                _jumpDuration += Time.deltaTime;
                
                if (_velocitySpeed > _maxVelocityY)
                    _velocitySpeed = 0;
                else if (_velocitySpeed > 0 && _jumpDuration < _maxJumpDuration)
                    _velocitySpeed += direction.y * _planingSpeed;
                
                _velocitySpeed -= _gravity;
                position.y += _velocitySpeed * Time.deltaTime;
            }

            if (_jumpDuration > _maxJumpDuration)
                direction.y = 0;
            
            position += direction * _speed * Time.deltaTime;
            
            if (GameData.Arena.activeSelf)
            {
                var limitX = _sizeField.x / 2;
                var limitY = _sizeField.y / 2;

                if (position.y <= -limitY + GameData.Arena.transform.position.y)
                {
                    _velocitySpeed = 0;
                    _isGround = true;
                    _jumpDuration = 0;
                }
                else
                {
                    _isGround = false;
                }

                position = new Vector2(
                    Mathf.Clamp(position.x, -limitX + GameData.Arena.transform.position.x, limitX + GameData.Arena.transform.position.x), 
                    Mathf.Clamp(position.y, -limitY + GameData.Arena.transform.position.y, limitY + GameData.Arena.transform.position.y));
            }

            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Attack attack))
            {
                if (!_isInvulnerability)
                {
                    _isInvulnerability = true;
                    StartCoroutine(TakeDamage());
                }

                Destroy(attack.gameObject);
            }
        }

        private IEnumerator TakeDamage()
        {
            GameData.Health -= GameData.EnemyData.EnemyConfig.Damage;
            SignalBus.OnDamage?.Invoke(1);
            SignalBus.OnHealthChange?.Invoke(GameData.MaxHealth, GameData.Health);
            _damageSource.Play();
            _shield.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            _shield.gameObject.SetActive(true);
            _isInvulnerability = false;
            
            if (GameData.Health <= 0 && !GameData.IsCheat)
                Death();
        }

        private void Death()
        {
            SignalBus.OnDeath?.Invoke();
            GameData.GameOver.SetActive(true);

            Analytics.CustomEvent("Death " + GameData.EnemyData.EnemyConfig.name);
        }
    }
}