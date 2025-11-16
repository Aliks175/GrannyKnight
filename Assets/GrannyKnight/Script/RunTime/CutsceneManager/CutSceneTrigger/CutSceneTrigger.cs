using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private string NameCutscene;
    private bool isPlay = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fas");
        if (isPlay) { return; }

        if (other.TryGetComponent(out PlayerCharacter component))
        {
            isPlay = true;
            CutsceneManager.Instance.StartCutscene(NameCutscene);
            gameObject.SetActive(false);
        }
    }
}