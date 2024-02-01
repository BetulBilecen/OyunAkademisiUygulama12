using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing,//Se�ilen yerde bir �ey var m� yok mu
        currentlyBuldozering; //Se�ilen silinmi� mi

    private BuildingPreset curBuildingPreset;
    private float indicatorUpdateTime = 0.0f, // Mouse'nin yapt��� her hareketi alg�lamams� i�in 0.05'lik br alg�lama de�eri verildi. Bu olmazsa i�lemciye bo� yere y�klenilirdi.
        lastUpdateTime;

    private Vector3 curIndicatorPos;
    public GameObject placementIndicator, buldozerIndicatoor;

    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        //if (City.instance.money < preset.cost) //Para maliyetten azsa hi�bir �ey yapma
        //    return;

        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);

    }

    //ESC'YE BASTI�INDA OLACAKLAR ���N:
    void CancelBuildingPlacement()
    {
        currentlyPlacing=false;
        placementIndicator.SetActive(false);
    }

    //S�LME ��LEM� ���N:
    public void ToggleBulldoze()
    {
        currentlyBuldozering= !currentlyBuldozering;
        buldozerIndicatoor.SetActive(currentlyBuldozering);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //Escape tu�una bas�ld�ysa blok i�ine gir
        {
            CancelBuildingPlacement();
        }

        if(Time.time-lastUpdateTime > indicatorUpdateTime)
        {
            lastUpdateTime = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            //S�LME YA DA EKLEME ��LEM�NE KARAR VERMEK ���N:
            if(currentlyPlacing)
                placementIndicator.transform.position = curIndicatorPos;
            else if(currentlyBuldozering)
                buldozerIndicatoor.transform.position = curIndicatorPos;

        }

        if(Input.GetMouseButtonDown(0) && currentlyPlacing) //Mouse ile sola t�klad���nda ve currentlyPlacing de�eri1 ise bloka gir
            PlaceBuilding();
        else if(Input.GetMouseButtonDown(0) && currentlyBuldozering)
            Bulldoze();
    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>()); //City dosyas�ndaki fonksiyonlara buraya getiriyor
        CancelBuildingPlacement() ;
    }

    void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == curIndicatorPos);
        if (buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

}
