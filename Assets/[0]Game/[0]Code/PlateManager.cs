using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class PlateManager : MonoBehaviour
    {
        [SerializeField]
        private string _code;

        [SerializeField] 
        private UnityEvent _event;

        [SerializeField] 
        private UnityEvent _resetEvent;
        
        private Plate[] _plates;
        private string _currentCode;
        private Coroutine _coroutine;
        private PlaySound _playSound;

        private void Awake()
        {
            _plates = GetComponentsInChildren<Plate>();
            _playSound = GetComponentInChildren<PlaySound>();
        }

        private void Start()
        {
            foreach (var plate in _plates)
            {
                plate.OnPress += PressPlate;
            }
        }

        public void Activate()
        {
            if (_code == _currentCode)
            {
                StartCoroutine(AwaitSuccess());
            }
            else
            {
                ResetPlates();
            }
        }
        
        private void ResetPlates()
        {
            foreach (var plate in _plates)
            {
                plate.NotPress();
            }

            _currentCode = "";
            _resetEvent.Invoke();
        }

        private void PressPlate(Plate plate)
        {
            _currentCode += plate.Number;
        }

        private IEnumerator AwaitSuccess()
        {
            yield return new WaitForSeconds(0.2f);
            _playSound.Play();
            _event.Invoke();
        }
    }
}