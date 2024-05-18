using System.Collections;
using MoreMountains.Feedbacks;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class AttackState : BaseState
    {
        [SerializeField]
        private Transform _attackLine;

        [SerializeField]
        private GameObject _background;
        
        [SerializeField]
        private MMF_Player _openFeedback;

        [SerializeField]
        private MMF_Player _closeFeedback;

        private float _progress;
        private bool _isHit;
        private Coroutine _aimCoroutine;

        public override void Enter()
        {
            _aimCoroutine = StartCoroutine(AwaitAim());
        }

        public override void Upgrade()
        {
            if (Input.GetMouseButtonDown(0) && _isHit)
            {
                StopCoroutine(_aimCoroutine);
                _isHit = false;
                StartCoroutine(AwaitHit());
            }
        }

        public override void Exit()
        {
            
        }
        
        private IEnumerator AwaitAim()
        {
            yield return new WaitForSeconds(2);
            _background.SetActive(true);
            yield return _openFeedback.PlayFeedbacksCoroutine(Vector3.zero);
            _attackLine.gameObject.SetActive(true);

            _progress = 0f;
            _isHit = true;
            
            while (_progress <= 1)
            {
                _progress += Time.deltaTime / 3;
                _attackLine.position = _attackLine.position.SetX(Mathf.Lerp(-6, 6, _progress));
                yield return null;
            }

            _attackLine.gameObject.SetActive(false);
            yield return AwaitClose();
        }
        
        private IEnumerator AwaitHit()
        {
            print("Hit");
            yield return new WaitForSeconds(1);
            yield return AwaitClose();
        }

        private IEnumerator AwaitClose()
        {
            yield return _closeFeedback.PlayFeedbacksCoroutine(Vector3.zero);
            GameData.BattleStateMachine.ChangeState(GameData.BattleStateMachine.EnemyTurnState);
        }
    }
}