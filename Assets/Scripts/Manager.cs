using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public PickUp[] weapons;
    public string weaponChoice;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUpWeapon(string choice)
    {
        foreach (PickUp weapon in weapons)
        {
            Destroy(weapon.gameObject);
        }
        weaponChoice = choice;
    }
}
