using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum Facing
    {
        right,
        left,
        up,
        down
    }

    protected Facing facing = Facing.right;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float health = 100;
    protected float moveSpeed = 3;
    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected Player[] players;
    protected int maxDistance = 3;
    protected int minDistance = 1;
    protected Weapon weapon;

    public Player target;

    protected Animator anim;

    public GameObject dead;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>(false);
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
    }

    public void GetHealed(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void GetAttacked(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(dead, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    protected void Follow(Player target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
}
