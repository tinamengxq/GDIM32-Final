using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        DetectObject();
    }

    void DetectObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }



}