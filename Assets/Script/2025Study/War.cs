using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    public SpawnEntity spawnEntity;

    void Start()
    {
        Debug.Log($"{spawnEntity.itemName}, {spawnEntity.power}");
    }
}
