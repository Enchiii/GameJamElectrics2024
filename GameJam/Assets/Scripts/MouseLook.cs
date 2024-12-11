using System;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float mouseSensitivy = 200f;
    public Transform playerBody;

    float xRotation = 0f;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
