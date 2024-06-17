using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class BanState : BaseState
    {
        [SerializeField]
        private MMF_Player _banFeedback;

        [SerializeField]
        private TypingText _typingText;

        [SerializeField]
        private AudioClip _banClip, _warningClip;
        
        [SerializeField]
        private GameObject _exclamationMark;
        
        public override void Enter()
        {
            StartCoroutine(AwaitBan());
        }

        public override void Upgrade()
        {

        }

        public override void Exit()
        {
            
        }

        private IEnumerator AwaitBan()
        {
            GameData.CurrentPlayerTurn = PlayerTurn.Ban;
            
            yield return new WaitForSeconds(0.5f);
            GameData.EffectAudioSource.clip = _warningClip;
            GameData.EffectAudioSource.Play();
            _exclamationMark.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            _exclamationMark.SetActive(false);
            
            yield return new WaitForSeconds(1);
            GameData.EffectAudioSource.clip = _banClip;
            GameData.EffectAudioSource.Play();
            
            yield return _banFeedback.PlayFeedbacksCoroutine(Vector3.zero);
            GameData.MusicAudioSource.Stop();
            
            yield return new WaitForSeconds(1);
            _typingText.Write($"*Вы забанили игрока {GameData.EnemyData.EnemyConfig.DisplayName}");
            yield return new WaitUntil(() => !_typingText.IsTyping);
            yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
            GameData.BattleStateMachine.CloseBattle();
        }
    }
}