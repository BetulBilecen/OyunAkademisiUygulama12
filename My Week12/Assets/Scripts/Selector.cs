using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //yapt���m�z� herkese duyurmak i�in

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector instance; //Her yerden eri�ebilmek i�in public static kullan�r�z ve instance anahtar kelimesiyle selector i�indekilere eri�ebiliriz.

    private void Awake()
    {
        instance = this; //Bu sayfaya eri�im sa�land�
    }
    private void Start()
    {
        cam=Camera.main; //Oyun kameras�n� cam'e aktard�k
    }

    //DOKUNDU�U ZEM�N� TESP�T ETMEK ���N:
    public Vector3 GetCurTilePosition()
    {
        if(EventSystem.current.IsPointerOverGameObject()) //UI'a de�diyse blo�un i�ine gir
        {
            return new Vector3(0, -99, 0); //Alakas�z bir yere gitmek i�in
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);  //2 boyutlu d�zlem olu�turuldu
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //mouse ile t�klad���m�z �eyi ray ile alm�� olduk

        float rayOut = 0.0f;

        if(plane.Raycast(ray,out rayOut))   //Ye�il zemine doknduysa blok i�ine gir
        {
            Vector3 newPosition = ray.GetPoint(rayOut) - new Vector3(0.05f, 0.0f, 0.05f);//Ye�il zeminde dokundu�u yeri kordinat�n� newPosition'e aktard�k

            newPosition = new Vector3(Mathf.CeilToInt(newPosition.x), 0.0f, Mathf.CeilToInt(newPosition.z)); // Elde edilen yeni kordinatlar� tam say�ya yuvarlad�
            return newPosition; ;
        }

        return new Vector3(0, -99, 0);  //Alakas�z bir yere gitmek i�in
    }
}
