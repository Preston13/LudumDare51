using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sweeper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, 0, 0));
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -mousePos - transform.position);
    }
}
