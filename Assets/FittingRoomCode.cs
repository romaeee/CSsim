using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FittingRoomCode : MonoBehaviour
{
    [SerializeField] private List<Image> slots = new List<Image>();
    [SerializeField] private List<Sprite> allClothes = new List<Sprite>();
    [SerializeField] private GameObject Btn;
    [SerializeField] private GameObject ftWindow;
    [SerializeField] private SpriteRenderer PlayerFit;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && CartScript.cartId.Count>0)
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
            CheckFittingRoom();

        }
    }

    public void CheckFittingRoom()
    {
        if (Btn.activeSelf)
        {
            for (int i = 0; i < CartScript.cartId.Count; i++)
            {
                slots[i].sprite = allClothes[CartScript.cartId[i]];
            }

            PlayerMovement.isAction = true;
            ftWindow.SetActive(true);
            Btn.SetActive(false);
        }

        else if (ftWindow.activeSelf)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].sprite = null;
            }
            PlayerFit.sprite = null;
            PlayerMovement.isAction = false;
            ftWindow.SetActive(false);
            Btn.SetActive(true);
        }
    }

    public void PuеItOn(int id)
    {
        if(slots[id].sprite!=null)
            PlayerFit.sprite = slots[id].sprite;
    }
}
