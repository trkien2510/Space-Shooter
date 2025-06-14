using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollowMouse : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(cam.transform.position.z);

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
}
