using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField]
    private float speed;
        
    void Update()
    {
        transform.Rotate(Vector3.forward, speed);
    }
}
