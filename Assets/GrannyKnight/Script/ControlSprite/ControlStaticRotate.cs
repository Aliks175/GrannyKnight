using UnityEngine;

public class ControlStaticRotate : MonoBehaviour
{
    [SerializeField] private StaticRotate[] _staticRotates;

    private void Update()
    {
        CheckEneble();
    }

    private void CheckEneble()
    {
        for (int i = 0; i < _staticRotates.Length; i++)
        {
            StaticRotate staticRotate = _staticRotates[i];
            if (staticRotate != null)
            {
                if (staticRotate.gameObject.activeSelf)
                {
                    staticRotate.OnUpdate();
                }
            }
        }
    }
}