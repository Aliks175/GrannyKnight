using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SpawnBubles : MonoBehaviour
{
    [SerializeField] private BubblePool _prefPoolBubles;
    private TargetBubble[] _targets;
    private ControlTarget _controlTarget;
    private SystemBuss _systemBuss;
    private PlayerCharacter _playerCharacter;

    [Inject]
    public void Construct(ControlTarget controlBubbles, SystemBuss systemBuss)
    {
        _controlTarget = controlBubbles;
        _systemBuss = systemBuss;
    }

    private async UniTaskVoid WaitPlayer()
    {
        _playerCharacter = await _systemBuss.GetPlayer();
        BubblePool poolBubles = GameObject.Instantiate(_prefPoolBubles, transform);
        Initialization(poolBubles);
    }

    public void Spawn()
    {
        if (_systemBuss == null) { return; }
        WaitPlayer().Forget();
    }

    private void Initialization(BubblePool poolBubles)
    {
        _targets = poolBubles.TargetsBubble;
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].Construct(_controlTarget, _playerCharacter.transform);
        }
    }
}