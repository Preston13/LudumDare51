using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatEnemy : Enemy
{
    BoxCollider2D hitBox;
    bool isAttacking;
    Vector3 prevPos;
    Vector3 lastMoveDir;

    protected override void Start()
    {
        base.Start();

        prevPos = transform.position;
        lastMoveDir = Vector3.zero;
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

        if (transform.position != prevPos)
        {
            lastMoveDir = (transform.position - prevPos).normalized;
            prevPos = transform.position;
        }

        if (lastMoveDir.x > 0)
        {
            facing = Facing.right;
        } 
        else if (lastMoveDir.x < 0)
        {
            facing = Facing.left;
        }
        else if (lastMoveDir.y > 0)
        {
            facing = Facing.up;
        }
        else if (lastMoveDir.y < 0)
        {
            facing = Facing.down;
        }

        anim.SetFloat("Facing", (float)facing);
    }

    private void Attack()
    {
        weapon.QuickAttack();
        StartCoroutine("Attacking");
    }

    private IEnumerator Attacking()
    {
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("IsAttacking", false);
        isAttacking = false;
    }


}
