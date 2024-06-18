using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class MenuIconStorage : MonoBehaviour
    {
        [SerializeField]
        private List<MenuOptionIconPair> _pair;

        public Sprite GetIcon(MenuOptionType menuOptionType) => 
            _pair.First(x => x.MenuOptionType == menuOptionType).Icon;

        [System.Serializable]
        public struct MenuOptionIconPair
        {
            public MenuOptionType MenuOptionType;
            public Sprite Icon;
        }
    }
}