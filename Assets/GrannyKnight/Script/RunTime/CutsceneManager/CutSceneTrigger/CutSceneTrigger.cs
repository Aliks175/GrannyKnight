using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private string NameCutscene;
    private bool isPlay = false;
    /// <summary>
    /// Проверяет чтобы тригер отработал 1 раз, Ивызывает катсцену по условию
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlay) { return; }

        if (collision.TryGetComponent(out PlayerCharacter component))
        {
            isPlay = true;
            CutsceneManager.Instance.StartCutscene(NameCutscene);

        }
    }
}
