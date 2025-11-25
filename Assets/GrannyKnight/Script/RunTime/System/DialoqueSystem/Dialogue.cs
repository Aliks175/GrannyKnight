using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] Lines;

}
[System.Serializable]
public struct DialogueLine
{
    public Character Character;
    [TextArea(4,10)]public string Line;
}
public enum Character
{
    ГГ,
    Бабушка,
    Сыч,
    Гвинька,
    Кролик
}
