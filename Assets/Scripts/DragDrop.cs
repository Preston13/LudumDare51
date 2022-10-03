using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour
{
    Vector2 mousePos;
    bool isDragging = false;
    [SerializeField]
    InputAction mouseClick;
    Manager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<Manager>();
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += Grabbing;
    }

    private void OnDisable()
    {
        mouseClick.performed -= Grabbing;
        mouseClick.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0));
        if (isDragging && mouseClick.ReadValue<float>() != 0)
        {
            transform.position = mousePos;
        }
    }

    private void Grabbing(InputAction.CallbackContext context)
    {
        Debug.Log("Drag");
        if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(mousePos) && context.performed)
        {
            isDragging = true;
        }
        else if (GetComponent<BoxCollider2D>() != Physics2D.OverlapPoint(mousePos) || context.canceled)
        {
            isDragging = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Oven"))
        {
            gameManager.AddIngredient(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
