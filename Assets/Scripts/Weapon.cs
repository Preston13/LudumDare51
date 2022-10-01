using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxQuickDamage = 20;
    public int minQuickDamage = 5;

    public int maxHeavyDamage = 50;
    public int minHeavyDamage = 25;

    public int hitDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void HeavyAttack()
    {
        hitDamage = Random.Range(minHeavyDamage, maxHeavyDamage);
    }

    public virtual void QuickAttack()
    {
        hitDamage = Random.Range(minQuickDamage, maxQuickDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") && this.gameObject.CompareTag("Player")) || (collision.CompareTag("Player") && this.gameObject.CompareTag("Enemy")))
        {
            collision.SendMessage("GetAttacked", hitDamage);
        }
    }
}
