using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Player;
    public Vector3 betweenValue; 
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.position = betweenValue + Player.transform.position;
        transform.LookAt(Player.transform.position);
    }
}
