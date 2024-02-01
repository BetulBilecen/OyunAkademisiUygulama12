using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //yeni kütüphanelerden biri


[CreateAssetMenu(fileName ="Building Preset",menuName ="New Building Preset")]
public class BuildingPreset : ScriptableObject //içinde bazı özellikler olan script kütüphanesi burada ev falan ekleyeceğiz
{
    public int cost, costPerTurn, population, jobs, food;
    public GameObject prefab;
}
