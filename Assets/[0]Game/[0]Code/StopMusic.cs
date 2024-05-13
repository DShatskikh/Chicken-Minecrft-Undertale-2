using System.Collections;
using UnityEngine;

namespace Game
{
    public class StopMusic : MonoBehaviour
    {
        private void OnEnable()
        {
            Stop();
        }

        public void Stop()
        {
            StartCoroutine(AwaitStop());
        }
        
        private IEnumerator AwaitStop()
        {
            enabled = true;
            yield return null;
            GameData.MusicAudioSource.Stop();
        }
    }
}