using System;
using System.Collections;
using System.Collections.Generic;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using YG;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace Game
{
    public class BattleStateMachine : MonoBehaviour
    {
        [SerializeField] 
        private float _speedPlacement;

        [SerializeField]
        private PlaySound _startBattlePlaySound;

        [SerializeField]
        private PlaySound _levelUpPlaySound;
        
        [SerializeField]
        private PlaySound _sparePlaySound;

        private Label _healthLabel;
        private Label _enemyHealthLabel;
        private Vector2 _normalWorldCharacterPosition;
        private Coroutine _coroutine;
        private Vector2 _enemyStartPosition;
        private GameObject _attack;

        [HideInInspector]
        public BaseState CurrentState;
        
        public int AttackIndex;
        public AudioClip PreviousSound;
        public Button[] Buttons;
        public CanvasGroup CanvasGroup;
        public Arena Arena;
        
        [Header("StateMachine")]
        public StartBattleState StartBattleState;
        public PlayerTurnState PlayerTurnState;
        [FormerlySerializedAs("AttackState")] public BanState banState;
        public EnemyTurnState EnemyTurnState;

        private void Update()
        {
            CurrentState.Upgrade();
        }

        public void StartBattle()
        {
            gameObject.SetActive(true);
            Initialize(StartBattleState);
        }

        public void CloseBattle()
        {
            gameObject.SetActive(false);
        }

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

        private void OnDisable()
        {
            SignalBus.OnDeath = null;
            SignalBus.OnDamage = null;
            
            if (_attack)
                Destroy(_attack.gameObject);

            if (GameData.EnemyData != null)
            {
                if (GameData.EnemyData.GameObject != null && GameData.EnemyData.StartBattleTrigger != null)
                {
                    GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyData.StartBattleTrigger.transform);
                }
                
                GameData.EnemyData.StartBattleTrigger = null;
            }
        }

        private IEnumerator AwaitBattle()
        {
            SignalBus.OnDamage += OnDamage;
            SignalBus.OnDeath += OnDeath;
            yield return Intro();

            var _attacks = GameData.EnemyData.EnemyConfig.Attacks;
            
            yield return new WaitForSeconds(1);
            
            while (GameData.BattleProgress < 100)
            {
                GameData.Arena.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                _attack = Instantiate(_attacks[AttackIndex], transform);
                yield return new WaitForSeconds(10);
                Destroy(_attack.gameObject);
                AttackIndex++;

                if (AttackIndex >= _attacks.Length)
                {
                    AttackIndex = Random.Range(0, _attacks.Length);
                }
                
                if (GameData.BattleProgress > 100)
                    GameData.BattleProgress = 100;
                
                SignalBus.OnBattleProgressChange?.Invoke(GameData.BattleProgress);
            }
            
            yield return new WaitForSeconds(1);
            GameData.Character.StartCoroutine(Exit());
        }

        private IEnumerator Intro()
        {
            _startBattlePlaySound.Play();
            GameData.Character.View.Idle();
            
            var characterTransform = GameData.Character.transform;
            var enemyTransform = GameData.EnemyData.GameObject.transform;
            
            _enemyStartPosition = enemyTransform.position;
            _normalWorldCharacterPosition = characterTransform.position;

            yield return null;
        }

        private void OnDeath()
        {
            StopCoroutine(_coroutine);
        }
        
        private IEnumerator Exit()
        {
            gameObject.SetActive(false);

            var disapperance = GameData.EnemyData.GameObject.AddComponent<HideSmooth>();
            disapperance.SetDuration(0.5f);
            _sparePlaySound.Play();
            yield return new WaitForSeconds(0.5f);

            var characterTransform = GameData.Character.transform;

            while ((Vector2)characterTransform.position != _normalWorldCharacterPosition)
            {
                characterTransform.position = Vector2.MoveTowards(characterTransform.position, _normalWorldCharacterPosition, Time.deltaTime * _speedPlacement);
                yield return null;
            }
            
            GameData.Character.enabled = true;
            GameData.Character.GetComponent<Collider2D>().isTrigger = false;

            GameData.MusicAudioSource.clip = PreviousSound;
            GameData.MusicAudioSource.Play();

            var eventParams = new Dictionary<string, string>
            {
                { "Wins", GameData.EnemyData.EnemyConfig.name }
            };
            
            YandexMetrica.Send("Wins", eventParams);
            
            GameData.Monolog.Show(new []{$"*Вы победили!\n*Ваше максимальное здоровье увеличилось\nна {GameData.EnemyData.EnemyConfig.HealthPrize}"});
            SignalBus.OnCloseMonolog += () =>
            {
                _levelUpPlaySound.Play();
                
                GameData.MaxHealth += GameData.EnemyData.EnemyConfig.HealthPrize;
                SignalBus.OnPlayerWin.Invoke(GameData.EnemyData.EnemyConfig);
                SignalBus.OnPlayerWin = null;
                GameData.Saver.Save();
                GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            };
        }

        private void OnDamage(int value)
        {
            GameData.Character.View.Damage();
        }
    }
}