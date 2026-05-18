using UnityEngine;

public class FirePlace : MonoBehaviour
{
    private Pot _pot;
    void Awake()
    {
        _pot = FindAnyObjectByType<Pot>();
    }
    void OnMouseDown()
    {
        _pot.CleareIng();
    }
}
