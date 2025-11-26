using UnityEngine;
using Cysharp.Threading.Tasks;

public class TargetFruits : MonoBehaviour, IHealtheble
{
    [SerializeField] private Rigidbody rb;
    private Vector3 _toMove;

    public void TakeDamage(float damage)
    {
        rb.isKinematic = false;
        Vector3 basket = QuestTwo.Instance.BasketPos;
        float distance = this.transform.position.y - basket.y;
        float time = Mathf.Sqrt(2 * distance / Mathf.Abs(Physics.gravity.y));
        _toMove = new Vector3(this.transform.position.x, basket.y, this.transform.position.z);
        QuestTwo.Instance.CollectFruit(_toMove, time);
        DestroyFruit().Forget();
    }
    private async UniTaskVoid DestroyFruit()
    {
        await UniTask.WaitUntil(() => this.transform.position.y <= _toMove.y);
        QuestTwo.Instance.OnFruitCollected();
        QuestTwo.Instance.ParticleStart(transform.position);
        Destroy(this.gameObject);
    }
}