using UnityEngine;

namespace Game
{
    public class UseSave : MonoBehaviour
    {
        public void Use()
        {
            GameData.Saver.Save();
        }
    }
}