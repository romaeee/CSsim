using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableAccessories : MonoBehaviour
{
    // This script is for bonus accessories that you want to add to your character
    // Add this script to any GameObject that is the child component of the main character
    // This main character parent needs to have the script CustomizableCharacter.cs
    // Just like the parent object, this object needs a Sprite Renderer to work
    // Make sure the spritesheet you add is similar in size and animations of your main character spritesheet


    public int accessoryNr;

    // This is where your spritesheets go
    // In the inspector, set the size to, for example 5, if you have 5 spritesheets
    // Then open each individual element and add the individual sprites from the spritesheets in here
    // This means if your spritesheet has 10 frames, the Sprites element in the inspector needs to contain these 10 sprites
    public Accessories[] accessories;
    SpriteRenderer spriteRenderer;
    SpriteRenderer parentSpriteRenderer;
    CustomizableCharacter customizableCharacter;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        customizableCharacter = GetComponentInParent<CustomizableCharacter>();
    }

    void Update()
    {
        if (accessoryNr > accessories.Length - 1) accessoryNr = 0;
        else if (accessoryNr < 0) accessoryNr = accessories.Length - 1;
    }

    void LateUpdate()
    {
        AccessoryChoice();
    }

    void AccessoryChoice()
    {
        spriteRenderer.sprite = accessories[accessoryNr].sprites[customizableCharacter.spriteNr];
    }

    // UI Element - Link to a button to select the next skin
    public void AccessoryPlus()
    {
        accessoryNr++;
    }

    // UI Element - Link to a button to select the previous skin
    public void AccessoryMin()
    {
        accessoryNr--;
    }
}


[System.Serializable]
public struct Accessories
{
    public Sprite[] sprites;
}