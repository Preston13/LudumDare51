using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMaterial : MonoBehaviour
{
    public bool isClean = false;
    float speed = 10;
    int cleanAmount = 1;
    SpriteRenderer sprite;
    Manager manager;

    public int dirtiness = 10;
    public Transform dirtyTarget;
    public Transform cleanTarget;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClean && isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, dirtyTarget.position, speed * Time.deltaTime);
        }
        else if (isClean)
        {
            transform.position = Vector3.MoveTowards(transform.position, cleanTarget.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Broom"))
        {
            cleanAmount = 5;
        } 
        else if (collision.CompareTag("Sword"))
        {
            cleanAmount = 1;
        }
        else if (collision.CompareTag("Pressure Washer"))
        {
            cleanAmount = 3;
        }
        else if (collision.CompareTag("Jai Alai"))
        {
            cleanAmount = 2;
        }
        dirtiness -= cleanAmount;
        sprite.color += Color.white / (10 / cleanAmount);
        if (dirtiness <= 0 && !isClean)
        {
            isClean = true;
            manager.cleanedMaterials++;
        }
    }
}
