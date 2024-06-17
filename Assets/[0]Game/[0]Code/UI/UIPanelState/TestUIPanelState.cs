using System;
using UnityEngine;

namespace Game
{
    public class TestUIPanelState : MonoBehaviour
    {
        [SerializeField]
        private UIPanelState _panel;
        
        private void Start()
        {
            _panel.Activate(true);
        }
    }
}