using System;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class TestPlayerInteracteble : ITickable
    {
        private IInteracteble _currentInteracteble;
        private Transform _head;
        private LayerMask _layerInteracteble;
        private float _distance;
        private string _description;

        public event Action<string> OnChangeCurrentInteracteble;

        public TestPlayerInteracteble(SettingsPlayer settingsPlayer, Transform head)
        {
            _layerInteracteble = settingsPlayer.LayerInteracteble;
            _distance = settingsPlayer.DistanceInteracteble;
            _head = head;
        }

        public void Tick()
        {
            Ray ray = new(_head.position, _head.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _distance, _layerInteracteble))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteracteble interacteble))
                {
                    ChangeCurrentInteracteble(interacteble);
                }
                else
                {
                    //Debug.LogError($"Not Found IInteracteble Script this -> {hit.collider.gameObject.name}");
                    ChangeCurrentInteracteble(null);
                }
            }
            else
            {
                ChangeCurrentInteracteble(null);
            }
        }

        public void OnInteracteble()
        {
            if (_currentInteracteble == null) { return; }
            _currentInteracteble.BaseInteract();
        }

        private void ChangeCurrentInteracteble(IInteracteble interacteble)
        {
            if (interacteble == _currentInteracteble) { return; }
            _currentInteracteble = interacteble;
            _description = _currentInteracteble == null ? string.Empty : _currentInteracteble.Description;
            OnChangeCurrentInteracteble?.Invoke(_description);
        }
    }
}