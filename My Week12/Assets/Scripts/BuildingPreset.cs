using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //yeni kütüphanelerden biri


[CreateAssetMenu(fileName ="Building Preset",menuName ="New Building Preset")]
public class BuildingPreset : ScriptableObject //içinde bazý özellikler olan script kütüphanesi burada ev falan ekleyeceðiz
{
    public int cost, costPerTurn, population, jobs, food;
    public GameObject prefab;
}
