using UnityEngine;

public class MazeCheck : MonoBehaviour
{
    [SerializeField] private MazeData _maze;
    [Tooltip("Порядоковый номер")][SerializeField] private int _number;
    [Tooltip("Номер порядка стены")][SerializeField] private int _order;
    [Tooltip("Последний ряд лабиринта?")][SerializeField] private bool _isEnd = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _maze.SetMazeDigit(_number, _order);
            if (_isEnd)
            {
                _maze.ValidateAndResetIfIncorrect(other.transform);
            }
        }
    }
}
