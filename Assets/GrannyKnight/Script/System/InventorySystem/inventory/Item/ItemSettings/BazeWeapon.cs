using UnityEngine;

[CreateAssetMenu(fileName = "BazeWeapon", menuName = "Create/Weapon")]
public class BazeWeapon : BazeItem
{
    [Header("Weapon")]
    public DataWeapon DataWeapon;
}