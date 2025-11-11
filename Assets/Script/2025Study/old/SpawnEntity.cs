using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Game/ItemData")]
public class SpawnEntity : ScriptableObject
{
    public string itemName;
    public int power;
    public Sprite icon;
}