using UnityEngine;

public class TargetFruits : MonoBehaviour, IHealtheble
{
    [SerializeField] private Rigidbody rb;

    public void TakeDamage(float damage)
    {
        rb.isKinematic = false;
        Vector3 basket = QuestTwo.Instance.BasketPos;
        float distance = this.transform.position.y - basket.y;
        float time = Mathf.Sqrt(2 * distance / Mathf.Abs(Physics.gravity.y));
        Vector3 toMove = new Vector3(this.transform.position.x, basket.y, this.transform.position.z);
        QuestTwo.Instance.CollectFruit(toMove, time);
    }
}