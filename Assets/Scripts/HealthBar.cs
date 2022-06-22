using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}