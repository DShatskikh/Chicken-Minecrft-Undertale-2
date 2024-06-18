using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIPanelStateMenu : UIPanelState
    {
        [SerializeField]
        private MenuOptionSlotView _slotPrefab;
        
        [SerializeField]
        private Transform _root;
        
        [SerializeField]
        private MenuOptionConfig _config;

        private List<MenuOptionSlotView> _slots = new List<MenuOptionSlotView>();

        private void Start()
        {
            for (int i = 0; i < _config.Data.Count; i++)
            {
                var menuOption = _config.Data[i];
                var slot = Instantiate(_slotPrefab, _root);
                _slots.Add(slot);
                int rowIndex = 0;
                int columnIndex = i;
                var vm = new MenuOptionViewModel(menuOption.Name, i == 0, menuOption.MenuOptionType);
                _viewModels.Add(new Vector2(columnIndex, rowIndex), vm);
                slot.Init(vm);
            }
        }

        private void OnEnable()
        {
            _viewModels[_currentIndex].SetSelected(true);
        }

        public override void HandleConfirmButtonClicked()
        {
            var vm = (MenuOptionViewModel)_viewModels[_currentIndex];

            switch (vm.OptionType)
            {
                case MenuOptionType.Ban:
                    GameData.StateController.SetPanelState(UIPanelStateType.Ban);
                    break;
                case MenuOptionType.Act:
                    
                    break;
                case MenuOptionType.Item:
                    
                    break;
                case MenuOptionType.Mercy:
                    
                    break;
            }
            
            _viewModels[_currentIndex].SetSelected(false);
        }

        public override void HandleBackButtonClicked()
        {
            throw new System.NotImplementedException();
        }

        public override void HandleSelectedSlotChanged(Vector2 vectorDelta)
        {
            var newIndex = _currentIndex + vectorDelta;
            
            if (_viewModels.ContainsKey(newIndex))
            {
                var newVM = _viewModels[newIndex];
                if (newVM != null)
                {
                    newVM.SetSelected(true);

                    var oldVM = _viewModels[_currentIndex];
                    oldVM.SetSelected(false);
                    _currentIndex = newIndex;
                }
            }
        }

        public override UIPanelStateType GetUIPanelStateType()
        {
            return UIPanelStateType.Menu;
        }
    }
}