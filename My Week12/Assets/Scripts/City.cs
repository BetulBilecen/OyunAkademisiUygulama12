using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    public int money, day, curPopulation, curJobs, curFood, maxPopulation, maxJobs, incomePerJob;
    public TextMeshProUGUI statsText;
    public List<Building>buildings = new List<Building>();
    public static City instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        UpdateStatText();
    }

    public void OnPlaceBuilding(Building building)
    {
        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }

    public void OnRemoveBuilding(Building building)
    {

        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }

    private void UpdateStatText()
    {
        statsText.text = string.Format("Day: {0} Money: {1} Pop: {2}/{3} Jobs: {4}/{5} Food: {6}", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood }); //Oyun ekran�nda en alttaki panelde bilgileri d�zenleyecek
    }

    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopilation();
        CalculateJobs();
        CalculateFood();
        UpdateStatText();
    }

    void CalculateMoney()
    {
        money += curJobs * incomePerJob;
        foreach(Building building in buildings)
        {
            money -= building.preset.costPerTurn;
        }
    }

    void CalculatePopilation()
    {
        if(curFood>=curPopulation && curPopulation<maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulation);
        }
        else if(curFood<curPopulation)
        {
            curPopulation = curFood;
        }

    }

    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    void CalculateFood()
    {
        curFood = 0;
        foreach(Building building in buildings)
        {
            curFood += building.preset.food;
        }
    }
}
