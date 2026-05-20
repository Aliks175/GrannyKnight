using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class FairySensor
{
    private RaycastHit[] _hits;
    private Transform _body;
    private SystemBuss _systemBuss;

    private PlayerCharacter _player;
    private LayerMask _layerPlayer;
    private float _maxDistance;
    private float _timeWaitFindPlayer;

    public Action OnFindPlayer;

    public void Start(Transform body)
    {
        _body = body;
        _hits = new RaycastHit[2];
        WaitPlayer().Forget();
    }

    private async UniTaskVoid WaitPlayer()
    {
        _player = await _systemBuss.GetPlayer();

        WaitFindPlayer().Forget();

    }

    private async UniTaskVoid WaitFindPlayer()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            if (CheckVisiblePlayer())
            {
                OnFindPlayer?.Invoke();
            }
        }
    }

    public bool CheckVisiblePlayer()
    {
        Ray ray = new Ray(_body.position, _player.transform.position - _body.position);
        int countFindPlayer = Physics.RaycastNonAlloc(ray, _hits, _maxDistance, _layerPlayer);
        Debug.Log($"countFindPlayer {countFindPlayer}");
        return countFindPlayer > 0;
    }
}
