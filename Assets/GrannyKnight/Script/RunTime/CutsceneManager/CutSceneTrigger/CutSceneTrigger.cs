using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private string NameCutscene;
    private bool isPlay = false;


    private void OnTriggerEnter(Collider other)
    {
        if (isPlay) return;
        Debug.Log("Start");
        Debug.Log(other.gameObject.name);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, _playerLayer);
        if (colliders.Length > 0 && colliders != null)
        {
            Debug.Log("FindPlayer");
            CutsceneManager.Instance.StartCutscene(NameCutscene);
            isPlay = true;
        }
    }
}