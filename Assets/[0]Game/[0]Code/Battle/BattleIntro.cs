using System;
using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class BattleIntro : MonoBehaviour
    {
        [SerializeField]
        private GameObject _transitionBackground;

        [SerializeField]
        private GameObject _heart;

        [SerializeField]
        private AudioClip _dangerClip, _noiseClip;

        public void StartBattle()
        {
            StartCoroutine(AwaitIntro());
        }

        private IEnumerator AwaitIntro()
        {
            GameData.MusicAudioSource.Stop();
            
            _heart.transform.position = GameData.Character.transform.position.AddY(0.5f);
            GameData.Character.View.DangerSwitch(true);
            GameData.EffectAudioSource.clip = _dangerClip;
            GameData.EffectAudioSource.Play();
            
            yield return new WaitForSeconds(0.5f);
            GameData.Character.View.DangerSwitch(false);

            float pauseTime = 0.2f;
            
            for (int i = 0; i < 3; i++)
            {
                _transitionBackground.SetActive(true);
                
                yield return new WaitForSeconds(pauseTime);
                _transitionBackground.SetActive(false);
                GameData.EffectAudioSource.clip = _noiseClip;
                GameData.EffectAudioSource.Play();
                
                yield return new WaitForSeconds(pauseTime);

                pauseTime *= 0.75f;
            }
            
            GameData.BattleStateMachine.StartBattle();
        }
    }
}