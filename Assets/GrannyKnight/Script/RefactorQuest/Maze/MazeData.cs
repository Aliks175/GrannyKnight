using UnityEngine;

public class MazeData : MonoBehaviour
{
    [Header("Настройки лабиринта")]
    [Tooltip("Последовательность")][SerializeField] private int _correctMazeSequence;
    [Tooltip("Точка старта")][SerializeField] private Transform _mazeStart;
    [Header("Рандомизация лабиринта")]
    [Tooltip("Лабиринт рандомный?")][SerializeField] private bool _isRandomSequence;
    [Tooltip("Количество этапов в лабиринте")][SerializeField] private int _mazeSequenceLength;
    [Tooltip("Максимально число вариантов")][SerializeField] private int _mazeDigitCount;
    
    
    private int _currentSequence = 0;
    
    void Awake()
    {
        if (_isRandomSequence) SetRandomMazeSequence();
    }
    
    private void SetRandomMazeSequence()
    {
        string sequence = "";
        for (int i = 0; i < _mazeSequenceLength; i++)
        {
            int digit = Random.Range(1, _mazeDigitCount + 1);
            sequence += digit.ToString();
        }
        _correctMazeSequence = int.Parse(sequence);
    }
    
    public void SetMazeDigit(int digit, int position)
    {
        string current = _currentSequence.ToString().PadLeft(GetDigitCount(_correctMazeSequence), '0');
        char[] digits = current.ToCharArray();
        
        if (position > 0 && position <= digits.Length)
        {
            digits[position - 1] = digit.ToString()[0];
            _currentSequence = int.Parse(new string(digits));
        }
    }
    
    public void ValidateAndResetIfIncorrect(Transform player)
    {
        if (player == null || _mazeStart == null) return;
        
        if (_currentSequence != _correctMazeSequence)
        {
            var controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            player.position = _mazeStart.position;
            controller.enabled = true;
            
            _currentSequence = 0;
        }
    }
    
    private int GetDigitCount(int number) => number.ToString().Length;
}