using UnityEngine;

[CreateAssetMenu(fileName = "BazeItem", menuName = "Create/Item")]
public class BazeItem : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject Model;
    public Sprite IconItem;
    [Header("Settings")]
    public DataItem DataItem;
}