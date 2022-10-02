using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("PickUpWeapon", gameObject.tag);
        }
    }
}
