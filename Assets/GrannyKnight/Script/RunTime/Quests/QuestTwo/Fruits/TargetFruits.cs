using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using FMODUnity;

public class TargetFruits : MonoBehaviour, IHealtheble
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private EventReference _fall;
    private Vector3 _toMove;

    private CancellationTokenSource _cancellationTokenSource;

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }

    public void TakeDamage(float damage)
    {
        PlaySound();
        rb.isKinematic = false;
        Vector3 basket = QuestTwo.Instance.BasketPos;
        float distance = this.transform.position.y - basket.y;
        float time = Mathf.Sqrt(2 * distance / Mathf.Abs(Physics.gravity.y));
        _toMove = new Vector3(this.transform.position.x, basket.y, this.transform.position.z);
        QuestTwo.Instance.CollectFruit(_toMove, time);
        _cancellationTokenSource = new CancellationTokenSource();
        DestroyFruit(_cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid DestroyFruit(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => this.transform.position.y <= _toMove.y, cancellationToken: cancellationToken);
        QuestTwo.Instance.OnFruitCollected();
        QuestTwo.Instance.ParticleStart(transform.position);
        Destroy(this.gameObject);
    }

    private void PlaySound()
    {
        if (!_fall.IsNull)
        {
            RuntimeManager.PlayOneShot(_fall);
        }
    }
}