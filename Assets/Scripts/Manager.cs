using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public PickUp[] weapons;
    public string weaponChoice;
    public GameObject[] sweepers;
    public CleanMaterial[] materials;
    public int cleanedMaterials = 0;

    private Sweeper sweeper;
    private bool sweeperExists;
    private bool activeMaterial = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        materials = FindObjectsOfType<CleanMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsOfType<Manager>().Length > 1)
        {
            if (weaponChoice != "")
            {
                Destroy(this.gameObject);
            }
        }

        if (sweeper == null && !sweeperExists)
        {
            sweeper = FindObjectOfType<Sweeper>();
        }
        else if (sweeper != null && !sweeperExists)
        {
            foreach (GameObject sweep in sweepers)
            {
                if (sweep.name == weaponChoice)
                {
                    Instantiate(sweep, sweeper.transform);
                    sweeperExists = true;
                }
            }
        }

        if (materials.Length == 0)
        {
            materials = FindObjectsOfType<CleanMaterial>();
        }
        else
        {
            foreach (CleanMaterial material in materials)
            {
                if (!material.isActive && !material.isClean && !activeMaterial)
                {
                    material.isActive = true;
                    activeMaterial = true;
                }
                else if (material.isActive && material.isClean && activeMaterial)
                {
                    material.isActive = false;
                    activeMaterial = false;
                }
            }
        }
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
