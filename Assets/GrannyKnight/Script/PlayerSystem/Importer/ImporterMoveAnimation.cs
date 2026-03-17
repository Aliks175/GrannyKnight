using System;
using Zenject;

namespace Refactor
{
    public class ImporterMoveAnimation : IDisposable, IInitializable
    {
        private PlayerMove _testPlayerMove;
        private TestPlayerControlAnimation _testPlayerControlAnimation;

        public ImporterMoveAnimation(TestPlayerControlAnimation testPlayerControlAnimation, PlayerMove testPlayerMove)
        {
            _testPlayerControlAnimation = testPlayerControlAnimation;
            _testPlayerMove = testPlayerMove;
        }

        public void Dispose()
        {
            _testPlayerMove.OnGrounded -= OnGrounded;
            _testPlayerMove.OnMove -= OnMove;
        }

        public void Initialize()
        {
            _testPlayerMove.OnGrounded += OnGrounded;
            _testPlayerMove.OnMove += OnMove;
        }

        private void OnMove(float speed)
        {
            _testPlayerControlAnimation.SetSpeed(speed);
        }

        private void OnGrounded(bool isGround)
        {
            _testPlayerControlAnimation.SetCheckGround(isGround);
        }
    }
}