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
    private BoxCollider2D hitBox;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float health = 100;

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
        weapon = GetComponentInChildren<Weapon>();
        hitBox = weapon.gameObject.GetComponent<BoxCollider2D>();
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
    }

    public void GetAttacked(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        // Trigger animation

        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
    }


    private IEnumerator Attack()
    {
        hitBox.enabled = true;

        yield return new WaitForSeconds(.1f);

        hitBox.enabled = false;
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("moving");
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
        weapon.QuickAttack();
        StartCoroutine("Attack");
    }

    private void OnHeavyAttack()
    {
        weapon.HeavyAttack();
        StartCoroutine("Attack");
    }

    private void OnInteract()
    {
        Debug.Log("interact");
    }
}
