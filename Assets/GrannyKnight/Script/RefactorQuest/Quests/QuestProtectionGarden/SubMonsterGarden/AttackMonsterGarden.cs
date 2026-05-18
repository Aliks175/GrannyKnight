using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class AttackMonsterGarden : IDisposable
{
    private CancellationTokenSource _cancellationToken;
    private DangerZone _dangerZone;
    private Transform _body;
    private bool _isPlay;
    private float _damage;
    private const float _waitTime = 2f;

    public event Action OnPrepareAttack;
    public event Action OnAttack;

    public AttackMonsterGarden(DangerZone dangerZone, Transform body,float damage)
    {
        _isPlay = true;
        _cancellationToken = new CancellationTokenSource();
        _dangerZone = dangerZone;
        _damage = damage;
        _body = body;
    }

    public void Initialization()
    {
        _dangerZone.OnEnter += Damage;
    }

    public void Dispose()
    {
        _dangerZone.OnEnter -= Damage;
        _isPlay = false;
        _cancellationToken?.Cancel();
        _cancellationToken?.Dispose();
        _cancellationToken = null;
    }

    public void Damage(Collider other)
    {
        if (!_isPlay) return;
        OnPrepareAttack?.Invoke();
        WaitTimeNextAttack(_cancellationToken.Token).Forget();
    }

    public void AttackPlayer()
    {
        if (_dangerZone.CheckPlayer(_body.position, 1.6f,out PlayerCharacter playerCharacter))
        {
            playerCharacter.TakeDamage(_damage);
        }
    }

    private async UniTaskVoid WaitTimeNextAttack(CancellationToken token)
    {
        try
        {
            Debug.Log("WaitTimeNextAttack");
            _isPlay = false;
            token.ThrowIfCancellationRequested();
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
            Debug.Log("DelayAttack");
            OnAttack?.Invoke();
            _isPlay = true;
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Операция отменена ");
        }
    }
}