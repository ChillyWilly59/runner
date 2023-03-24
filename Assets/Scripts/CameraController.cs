using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _vector3;
    
    void Start()
    {
        _vector3 = transform.position - player.position;
    }

    
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, _vector3.z + player.position.z);
        transform.position = newPosition;
    }
}
