using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string description;
    public bool purchased;
    public int currentLvl = 0;
    public int cost;
    public int upgradeCost;
    public float damage;
    public float upgradeDamage;
    public float coolDown;
    public float distance = 10;
    public float aoeArea;
    public Color orbColor;
    public GameObject prefab;
}
