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
        
        private void Start()
        {
            for (int i = 0; i < _config.Data.Count; i++)
            {
                var menuOption = _config.Data[i];
                var slot = Instantiate(_slotPrefab, _root);
                int rowIndex = 0;
                int columnIndex = i;
                var vm = new MenuOptionViewModel(menuOption.Name, i == 0, menuOption.MenuOptionType, menuOption.Icon);
                _viewModels.Add(new Vector2(columnIndex, rowIndex), vm);
                slot.Init(vm);
            }
        }
        
        public override void HandleConfirmButtonClicked()
        {
            var vm = (MenuOptionViewModel)_viewModels[_currentIndex];

            switch (vm.OptionType)
            {
                case MenuOptionType.Ban:
                    GameData.StateController.SetPanelState(UIPanelStateType.Attack);
                    break;
                case MenuOptionType.Act:
                    
                    break;
                case MenuOptionType.Item:
                    
                    break;
                case MenuOptionType.Mercy:
                    
                    break;
            }
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