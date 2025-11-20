using UnityEngine;

public class FruitBasket : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<TargetFruits>(out TargetFruits fruits))
        {
            QuestTwo.Instance.IsCollected();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
