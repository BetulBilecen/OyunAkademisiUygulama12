using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float minXRotation, maxXRotation, curXRotation;
    public float minZoom, maxZoom, zoomSpeed, rotateSpeed;
    private float curZoom;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;//Þu anki kamera cam'e aktarýldý
        curZoom = cam.transform.localPosition.y;
        curXRotation = -50;
    }

    private void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        cam.transform.localPosition=Vector3.up* curZoom;


        if(Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRotation += -y * rotateSpeed;
            curXRotation = Mathf.Clamp(curXRotation,minXRotation,maxXRotation);

            transform.eulerAngles = new Vector3(curXRotation, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);

        }

        //HAREKET ÝÇÝN:
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = cam.transform.right;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
        dir = dir*moveSpeed*Time.deltaTime;

        transform.position += dir;
    }

}
