using UnityEngine;

public class MazeItem : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private Transform[] _positionItem;

    public void MoveItem()
    {
        if (CheckCorrectPosition())
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].transform.position = _positionItem[i].position;
            }
        }
        else
        {
            Debug.LogError("CheckCorrectPosition False");
        }
    }

    private bool CheckCorrectPosition()
    {
        return _items.Length == _positionItem.Length;
    }
}