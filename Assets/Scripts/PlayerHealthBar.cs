using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(_camera.transform);
        transform.Rotate(0, 180, 0);
    }
}