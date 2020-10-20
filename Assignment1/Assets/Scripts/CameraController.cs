using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        offset = target.position - this.transform.position;
      
    }

    // Update is called once per frame
    void LateUpdate()
    {

        this.transform.position = target.position - offset;
        this.transform.LookAt(target);
    }
}
