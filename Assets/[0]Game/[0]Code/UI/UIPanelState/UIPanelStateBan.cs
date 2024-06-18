using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIPanelStateBan : UIPanelState
    {
        [SerializeField]
        private DefaultOptionSlotView _slotPrefab;
        
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private List<EnemyConfig> _configs;

        private List<DefaultOptionSlotView> _slots = new List<DefaultOptionSlotView>();

        public override void Activate(bool active)
        {
            base.Activate(active);

            if (active)
                CreateButton();
            else
                DestroyButton();
        }

        public override void HandleConfirmButtonClicked()
        {
            var vm = _viewModels[_currentIndex];
            
            print(vm.Name + " Забанен");
            
            
        }

        public override void HandleBackButtonClicked()
        {
            _viewModels[_currentIndex].SetSelected(false);
            GameData.StateController.SetPanelState(UIPanelStateType.Menu);
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
            return UIPanelStateType.Ban;
        }

        private void CreateButton()
        {
            for (int i = 0; i < _configs.Count; i++)
            {
                var menuOption = _configs[i];
                var slot = Instantiate(_slotPrefab, _root);
                _slots.Add(slot);
                int rowIndex = i;
                int columnIndex = 0;
                var vm = new UIPanelOptionViewModel(menuOption.DisplayName, i == 0);
                _viewModels.Add(new Vector2(columnIndex, rowIndex), vm);
                slot.Init(vm);
            }
        }

        private void DestroyButton()
        {
            foreach (var slot in _slots) 
                Destroy(slot.gameObject);

            _slots = new List<DefaultOptionSlotView>();
            _viewModels = new ();
        }
    }
}