using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public void ZoomOutCamera(float cameraDistance)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - cameraDistance/3, transform.position.z);
        GetComponent<Camera>().orthographicSize += cameraDistance;
    }

    public void MoveCamera(float offset)
    {       
        transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
    }   
}
