using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation;
    float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.rotation.eulerAngles.x;
        yRotation = transform.rotation.eulerAngles.y;
        //Debug.Log("Xrot: " + xRotation + " Yrot: " + yRotation);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //Debug.Log("Mx: " + mouseX + " My" + mouseY);

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }
}