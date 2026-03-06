using System;
using Zenject;

namespace Refactor
{

    public class ImporterAimMove : IDisposable, IInitializable
    {
        private TestPlayerAim _testPlayerAim;
        private TestPlayerMove _testPlayerMove;
        private float coefficientSpeedForAim;
        //ImporterAimSensitivity
        private readonly float _normalCoefficientSpeedForAim;

        public ImporterAimMove(TestPlayerAim testPlayerAim, TestPlayerMove testPlayerMove, SettingsPlayer settingsPlayer)
        {
            _testPlayerAim = testPlayerAim;
            _testPlayerMove = testPlayerMove;
            _normalCoefficientSpeedForAim = settingsPlayer.CoefficientSpeedForAim;
        }

        public void Dispose()
        {
            _testPlayerAim.OnAim -= OnAim;
        }

        public void Initialize()
        {
            coefficientSpeedForAim = 1;
            _testPlayerAim.OnAim += OnAim;
        }

        private void OnAim(bool isAim)
        {
            coefficientSpeedForAim = isAim ? _normalCoefficientSpeedForAim : 1;
            _testPlayerMove.ChangeCoefficientSpeedForAim(coefficientSpeedForAim);
        }
    }

}