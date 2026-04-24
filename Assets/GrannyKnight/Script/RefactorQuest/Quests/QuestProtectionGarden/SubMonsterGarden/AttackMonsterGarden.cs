using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class AttackMonsterGarden : IDisposable
{
    private CancellationTokenSource _cancellationToken;
    private bool _isPlay;
    private const float _waitTime = 2f;
    public event Action OnAttack;

    public AttackMonsterGarden()
    {
        _isPlay = true;
        _cancellationToken = new CancellationTokenSource();
    }

    public void Dispose()
    {
        Debug.Log("Disposed!");
        _isPlay = false;
        _cancellationToken?.Cancel();
        _cancellationToken?.Dispose();
        _cancellationToken = null;
    }

    public void Damage(Collider other)
    {
        if (!_isPlay) return;
        if (other.gameObject.TryGetComponent(out PlayerCharacter player))
        {
            Debug.Log("Damage");
            player.TakeDamage(1);
            OnAttack?.Invoke();
            Debug.Log($"CancellationTokenSource = null {_cancellationToken == null}");
            Debug.Log("Before Token");
            if (!_isPlay) return;
            WaitTimeNextAttack(_cancellationToken.Token).Forget();
        }
    }

    private async UniTaskVoid WaitTimeNextAttack(CancellationToken token)
    {
        try
        {
            _isPlay = false;
            token.ThrowIfCancellationRequested();
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
            _isPlay = true;
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Операция отменена ");
        }
    }
}