using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordInfo", menuName = "Weapon/New Sword", order = 51)]
public class Sword : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
}
