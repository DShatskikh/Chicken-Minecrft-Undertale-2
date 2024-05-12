using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DistanceAction : MonoBehaviour
    {
        [SerializeField]
        private float _distance = 3f;

        [SerializeField]
        private UnityEvent _event;

        private void Update()
        {
            if (Vector2.Distance(GameData.Character.transform.position, transform.position) > _distance)
            {
                _event.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}