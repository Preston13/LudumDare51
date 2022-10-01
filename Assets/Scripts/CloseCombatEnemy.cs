using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatEnemy : Enemy
{
    BoxCollider2D hitBox;
    bool isAttacking;

    protected override void Start()
    {
        base.Start();

        hitBox = weapon.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        target = null;
        foreach (Player player in players)
        {
            float playerDistance = Vector2.Distance(transform.position, player.transform.position);
            if (playerDistance < maxDistance)
            {
                if (target != null)
                {
                    if (playerDistance < Vector2.Distance(transform.position, target.transform.position))
                    {
                        target = player;
                    }
                }
                else
                {
                    target = player;
                }
            }
        }

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > minDistance)
            {
                Follow(target);
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                Invoke("Attack", 1f);
            }
        }
    }

    private void Attack()
    {
        weapon.QuickAttack();
        StartCoroutine("Attacking");
    }

    private IEnumerator Attacking()
    {
        hitBox.enabled = true;

        yield return new WaitForSeconds(.1f);

        hitBox.enabled = false;
        isAttacking = false;
    }


}
