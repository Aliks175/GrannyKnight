using System;
using Zenject;

namespace Refactor
{
    public class ImporterAimSensitivity : IDisposable, IInitializable
    {
        private PlayerAim _testPlayerAim;
        private PlayerLook _testPlayerLook;
        private float _coefficientSensitivityAim;
        private readonly float _normalCoefficientSensitivityAim;

        public ImporterAimSensitivity(PlayerAim testPlayerAim, PlayerLook testPlayerLook, SettingsPlayer settingsPlayer)
        {
            _testPlayerAim = testPlayerAim;
            _testPlayerLook = testPlayerLook;
            _normalCoefficientSensitivityAim = settingsPlayer.CoefficientSensitivityAim;
        }

        public void Dispose()
        {
            _testPlayerAim.OnAim -= OnAim;
        }

        public void Initialize()
        {
            _testPlayerAim.OnAim += OnAim;
        }

        private void OnAim(bool isAim)
        {
            _coefficientSensitivityAim = isAim ? _normalCoefficientSensitivityAim : 1;
            _testPlayerLook.ChangeCoefficientSensitivityForAim(_coefficientSensitivityAim);
        }
    }
}