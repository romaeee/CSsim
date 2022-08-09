using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartScript : MonoBehaviour
{
    public static List<int> cartId = new List<int>();
    [SerializeField] private List<Image> slots = new List<Image>();
    [SerializeField] private List<Sprite> allClothes = new List<Sprite>();
    [SerializeField] private List<GameObject> rmBtn = new List<GameObject>();

    private void Start()
    {
        UbdCard();
    }

    private void OnEnable()
    {
        Debug.Log("cartId.Count: " + cartId.Count + ", slots: " + slots.Count);
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].sprite = null;
        }
        UbdCard();
    }

    private void Update()
    {
        //CheckCart();

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckSCartStat();
        }
    }

    public void RemoveItem(int slotIndex)
    {
        if (cartId.Count > 0)
            cartId.RemoveAt(cartId.IndexOf(slotIndex)+1);
        //UbdCard();
        if (slots[slotIndex].sprite!=null)
            slots[slotIndex].sprite = null;
        CheckCart();

    }

    void UbdCard()
    {
        for (int i = 0; i < cartId.Count; i++)
        {
            slots[i].sprite = allClothes[cartId[i]];
        }
        CheckCart();
    }


    private void CheckCart()
    {
            for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].sprite != null)
                rmBtn[i].SetActive(true);
            else
                rmBtn[i].SetActive(false);
        }
    }

    public void CheckSCartStat()
    {
        gameObject.SetActive(false);

    }
}
