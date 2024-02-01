using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //yeni k�t�phanelerden biri


[CreateAssetMenu(fileName ="Building Preset",menuName ="New Building Preset")]
public class BuildingPreset : ScriptableObject //i�inde baz� �zellikler olan script k�t�phanesi burada ev falan ekleyece�iz
{
    public int cost, costPerTurn, population, jobs, food;
    public GameObject prefab;
}
