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
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckShelf();
        }
    }

    public void AddToCart(int id)
    {
        if (!CartScript.cartId.Contains(id))
        {
            CartScript.cartId.Add(id);
            //CartScript.totalPrice += price;
        }
            
        else
            Debug.Log("Already in a card");
    }



    public void CheckShelf()
    {
        if (Btn.activeSelf)// && !PlayerMovement.isTalking)
        {
            PlayerMovement.isAction = true;
            ShelfWindow.SetActive(true);
            Btn.SetActive(false);
            //PlayerMovement.isTalking = true;
        }

        else if (ShelfWindow.activeSelf)
        {
            PlayerMovement.isAction = false;
            Debug.Log("Open Shelf");
            ShelfWindow.SetActive(false);
            Btn.SetActive(true);
            //PlayerMovement.isTalking = false;

        }
    }
}
