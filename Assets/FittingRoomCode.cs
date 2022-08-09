using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FittingRoomCode : MonoBehaviour
{
    [SerializeField] private List<Image> slots = new List<Image>();
    [SerializeField] private List<Sprite> allClothes = new List<Sprite>();

    void Start()
    {
        for (int i = 0; i < CartScript.cartId.Count; i++)
        {
            slots[i].sprite = allClothes[CartScript.cartId[i]];
        }
    }

    public void PuеItOn(int id)
    {
        Debug.Log("Put it on: " + id);
    }
}
