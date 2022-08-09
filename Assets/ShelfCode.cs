using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfCode : MonoBehaviour
{
    [SerializeField] private GameObject Btn;
    [SerializeField] public GameObject ShelfWindow;
    [SerializeField] private GameObject rmtBtn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Btn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Btn.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerMovement.isAction)
        {
            ShelfWindow.SetActive(true);
            Btn.SetActive(false);
            PlayerMovement.isTalking = true;
        }

        if(!ShelfWindow.activeSelf)
            PlayerMovement.isTalking = false;
    }

    public void AddToCart(int id)
    {
        if (!CartScript.cartId.Contains(id))
            CartScript.cartId.Add(id);
        else
            Debug.Log("Already in a card");

        
    }
}
