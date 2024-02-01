using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing,//Seçilen yerde bir þey var mý yok mu
        currentlyBuldozering; //Seçilen silinmiþ mi

    private BuildingPreset curBuildingPreset;
    private float indicatorUpdateTime = 0.0f, // Mouse'nin yaptýðý her hareketi algýlamamsý için 0.05'lik br algýlama deðeri verildi. Bu olmazsa iþlemciye boþ yere yüklenilirdi.
        lastUpdateTime;

    private Vector3 curIndicatorPos;
    public GameObject placementIndicator, buldozerIndicatoor;

    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        //if (City.instance.money < preset.cost) //Para maliyetten azsa hiçbir þey yapma
        //    return;

        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);

    }

    //ESC'YE BASTIÐINDA OLACAKLAR ÝÇÝN:
    void CancelBuildingPlacement()
    {
        currentlyPlacing=false;
        placementIndicator.SetActive(false);
    }

    //SÝLME ÝÞLEMÝ ÝÇÝN:
    public void ToggleBulldoze()
    {
        currentlyBuldozering= !currentlyBuldozering;
        buldozerIndicatoor.SetActive(currentlyBuldozering);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //Escape tuþuna basýldýysa blok içine gir
        {
            CancelBuildingPlacement();
        }

        if(Time.time-lastUpdateTime > indicatorUpdateTime)
        {
            lastUpdateTime = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            //SÝLME YA DA EKLEME ÝÞLEMÝNE KARAR VERMEK ÝÇÝN:
            if(currentlyPlacing)
                placementIndicator.transform.position = curIndicatorPos;
            else if(currentlyBuldozering)
                buldozerIndicatoor.transform.position = curIndicatorPos;

        }

        if(Input.GetMouseButtonDown(0) && currentlyPlacing) //Mouse ile sola týkladýðýnda ve currentlyPlacing deðeri1 ise bloka gir
            PlaceBuilding();
        else if(Input.GetMouseButtonDown(0) && currentlyBuldozering)
            Bulldoze();
    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>()); //City dosyasýndaki fonksiyonlara buraya getiriyor
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
