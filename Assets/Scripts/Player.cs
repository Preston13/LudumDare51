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

    public void PickUpWeapon(string type)
    {
        weapon = GetComponentInChildren<Weapon>(true);
        if (type == "Broom")
        {
            weapon.maxQuickDamage = 20;
            weapon.minQuickDamage = 10;
        }
        else if (type == "Sword")
        {
            weapon.maxQuickDamage = 70;
            weapon.minQuickDamage = 50;
        }
        else if (type == "Pressure Washer")
        {
            weapon.maxQuickDamage = 50;
            weapon.minQuickDamage = 30;
        }
        else if (type == "Jai Alai")
        {
            weapon.maxQuickDamage = 30;
            weapon.minQuickDamage = 20;
        }
        manager.PickedUpWeapon(type);
    }

    public void GetAttacked(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine("Die");
        }
    }

    private IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("IsAttacking", false);
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
        if (weapon != null)
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
