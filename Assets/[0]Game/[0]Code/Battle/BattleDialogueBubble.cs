using System.Collections;
using UnityEngine;

namespace Game
{
    public class BattleDialogueBubble : MonoBehaviour
    {
        [SerializeField]
        private TypingText _typingText;

        public bool IsTyping => _typingText.IsTyping;
        
        public void Open(string text)
        {
            gameObject.SetActive(true);
            _typingText.Write(text);
        }
    }
}