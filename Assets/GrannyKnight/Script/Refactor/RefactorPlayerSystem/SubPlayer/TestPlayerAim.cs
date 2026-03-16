using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class TestPlayerAim : IDisposable, IInitializable
    {
        private CancellationTokenSource _cancellationToken;
        private CinemachineCamera _cinemachineCamera;
        private float _startFieldOfView;
        private float _endFieldOfView;
        private float _speedChooseView;
        private bool _isActive = false;
        private const int _zoomOn = -1;
        private const int _zoomOFF = 1;
        public event Action<bool> OnAim;

        public TestPlayerAim(SettingsPlayer settingsPlayer, CinemachineCamera cinemachineCamera)
        {
            _startFieldOfView = settingsPlayer.StartFieldOfView;
            _endFieldOfView = settingsPlayer.EndFieldOfView;
            _speedChooseView = settingsPlayer.SpeedChooseView;
            _cinemachineCamera = cinemachineCamera;
            _isActive = false;
        }

        public void Dispose()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }

        public void Initialize()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        public void ProcessAim(bool isActive)
        {
            if (_isActive == isActive) { return; }
            _isActive = isActive;
            OnAim?.Invoke(_isActive);
            ControlAim();
        }

        private void ControlAim()
        {
            int modifier = _isActive ? _zoomOn : _zoomOFF;
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            ChangeFieldOfView(modifier, _cancellationToken.Token).Forget();
        }

        private async UniTaskVoid ChangeFieldOfView(int modifier, CancellationToken token)
        {
            try
            {
                bool _isActive = true;
                float fieldOfView = _cinemachineCamera.Lens.FieldOfView;
                float endPos = modifier > 0 ? _startFieldOfView : _endFieldOfView;
                while (_isActive)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                    fieldOfView += Time.deltaTime * _speedChooseView * modifier;
                    _cinemachineCamera.Lens.FieldOfView = fieldOfView;
                    if (fieldOfView >= endPos && modifier == _zoomOFF)
                    {
                        _isActive = false;
                    }
                    else if (fieldOfView <= endPos && modifier == _zoomOn)
                    {
                        _isActive = false;
                    }
                }
                FinalZoom(endPos);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Операция отменена ");
            }
        }

        private void FinalZoom(float fieldOfView)
        {
            _cinemachineCamera.Lens.FieldOfView = fieldOfView;
        }
    }
}