using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //yaptýðýmýzý herkese duyurmak için

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector instance; //Her yerden eriþebilmek için public static kullanýrýz ve instance anahtar kelimesiyle selector içindekilere eriþebiliriz.

    private void Awake()
    {
        instance = this; //Bu sayfaya eriþim saðlandý
    }
    private void Start()
    {
        cam=Camera.main; //Oyun kamerasýný cam'e aktardýk
    }

    //DOKUNDUÐU ZEMÝNÝ TESPÝT ETMEK ÝÇÝN:
    public Vector3 GetCurTilePosition()
    {
        if(EventSystem.current.IsPointerOverGameObject()) //UI'a deðdiyse bloðun içine gir
        {
            return new Vector3(0, -99, 0); //Alakasýz bir yere gitmek için
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);  //2 boyutlu düzlem oluþturuldu
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //mouse ile týkladýðýmýz þeyi ray ile almýþ olduk

        float rayOut = 0.0f;

        if(plane.Raycast(ray,out rayOut))   //Yeþil zemine doknduysa blok içine gir
        {
            Vector3 newPosition = ray.GetPoint(rayOut) - new Vector3(0.05f, 0.0f, 0.05f);//Yeþil zeminde dokunduðu yeri kordinatýný newPosition'e aktardýk

            newPosition = new Vector3(Mathf.CeilToInt(newPosition.x), 0.0f, Mathf.CeilToInt(newPosition.z)); // Elde edilen yeni kordinatlarý tam sayýya yuvarladý
            return newPosition; ;
        }

        return new Vector3(0, -99, 0);  //Alakasýz bir yere gitmek için
    }
}
