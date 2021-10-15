using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMinimap();
    }

    private void MoveMinimap(){
        transform.position = new Vector3(transform.position.x + Input.GetAxis("MinimapX")/10,
                                        transform.position.y + Input.GetAxis("MinimapY")/10,
                                        transform.position.z);
    }
}
