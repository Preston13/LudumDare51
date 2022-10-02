using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxQuickDamage = 20;
    public int minQuickDamage = 5;
    public int knockbackForce;

    public int hitDamage;

    public struct AttackInfo
    {
        public int damage;
        public Vector2 knockBack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void QuickAttack()
    {
        hitDamage = Random.Range(minQuickDamage, maxQuickDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") && this.gameObject.CompareTag("Player")) || (collision.CompareTag("Player") && this.gameObject.CompareTag("Enemy")))
        {
            AttackInfo attackInfo = new AttackInfo();
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            attackInfo.damage = hitDamage;
            attackInfo.knockBack = knockback;
            collision.SendMessage("GetAttacked", attackInfo);
        }
    }
}
