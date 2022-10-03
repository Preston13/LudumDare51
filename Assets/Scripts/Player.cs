using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float dodgeSpeed = 10;
    private Vector2 movement;
    [SerializeField]
    private bool isDodging = false;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float health = 100;
    private Animator anim;
    private float attackTimer;
    private bool isAttacking = false;
    public GameObject horWall;
    public GameObject verWall;

    public Manager manager;

    public enum Facing
    {
        right,
        left,
        up,
        down
    }

    [SerializeField]
    private Facing facing = Facing.right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movement * speed;

        if (isDodging)
        {
            rb.velocity = movement * dodgeSpeed;
        }

        if(movement.x > 0)
        {
            facing = Facing.right;
        } else if (movement.x < 0)
        {
            facing = Facing.left;
        } else if (movement.y > 0)
        {
            facing = Facing.up;
        } else if (movement.y < 0)
        {
            facing = Facing.down;
        }

        anim.SetFloat("Facing", (float)facing);
    }

    public void OnBuild()
    {
        if (facing == Facing.up)
        {
            Instantiate(horWall, transform.position+transform.forward * 4, Quaternion.identity); 
        } else if (facing == Facing.right)
        {
            Instantiate(verWall, transform.position + transform.right * 2, Quaternion.identity);
        } else if (facing == Facing.left)
        {
            Instantiate(verWall, transform.position - transform.right * 2, Quaternion.identity);
        } else if (facing == Facing.down)
        {
            Instantiate(horWall, transform.position - transform.forward * 4, Quaternion.identity);
        }
        
    }

    public void PickUpWeapon(string type)
    {
        weapon = GetComponentInChildren<Weapon>(true);
        if (type == "Broom")
        {
            weapon.maxQuickDamage = 20;
            weapon.minQuickDamage = 10;
            weapon.knockbackForce = 15;
            attackTimer = .2f;
        }
        else if (type == "Sword")
        {
            weapon.maxQuickDamage = 70;
            weapon.minQuickDamage = 50;
            weapon.knockbackForce = 5;
            attackTimer = 0;
        }
        else if (type == "Pressure Washer")
        {
            weapon.maxQuickDamage = 50;
            weapon.minQuickDamage = 30;
            weapon.knockbackForce = 20;
            attackTimer = .4f;
        }
        else if (type == "Jai Alai")
        {
            weapon.maxQuickDamage = 30;
            weapon.minQuickDamage = 20;
            weapon.knockbackForce = 10;
            attackTimer = .6f;
        }
        manager.PickedUpWeapon(type);
    }

    public void GetAttacked(Weapon.AttackInfo attackInfo)
    {
        rb.AddForce(attackInfo.knockBack, ForceMode2D.Impulse);
        health -= attackInfo.damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackTimer);
        isAttacking = false;
    }

    private IEnumerator Die()
    {
        // Trigger animation

        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnDodge(InputValue value)
    {
        if (!isDodging)
        {
            isDodging = value.isPressed;
            StartCoroutine("Dodging");
        }
    }

    private IEnumerator Dodging()
    {
        yield return new WaitForSeconds(.1f);
        isDodging = false;
    }

    private void OnQuickAttack()
    {
        if (weapon != null && !isAttacking)
        {
            weapon.QuickAttack();
            StartCoroutine("Attack");
        }
    }

    private void OnInteract()
    {
        Debug.Log("interact");
    }
}
