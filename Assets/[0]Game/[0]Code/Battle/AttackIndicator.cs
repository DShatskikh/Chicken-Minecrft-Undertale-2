using System;
using System.Collections;
using MoreMountains.Feedbacks;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class AttackIndicator : MonoBehaviour
    {
        [SerializeField]
        private MMF_Player _openFeedback;

        [SerializeField]
        private MMF_Player _closeFeedback;
        
        [SerializeField]
        private Transform _attackLine;

        private float _progress;
        private bool _isHit;
        private Coroutine _aimCoroutine;

        private void OnEnable()
        {
            _aimCoroutine = StartCoroutine(AwaitAim());
        }

        private IEnumerator AwaitAim()
        {
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

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _isHit)
            {
                StopCoroutine(_aimCoroutine);
                _isHit = false;
                StartCoroutine(AwaitHit());
            }
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
            gameObject.SetActive(false);
        }
    }
}