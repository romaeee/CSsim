using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{
    // This script should be added to your main character and won't be used for accessories


    public int skinNr;

    // This is where your spritesheets go
    // In the inspector, set the size to, for example 5, if you have 5 spritesheets
    // Then open each individual element and add the individual sprites from the spritesheets in here
    // This means if your spritesheet has 10 frames, the Sprites element in the inspector needs to contain these 10 sprites
    public Skins[] skins;
    SpriteRenderer spriteRenderer;

    // This spriteNr is helpful to easily add more accessories using the CustomizableAccessories.cs script
    public int spriteNr;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (skinNr > skins.Length - 1) skinNr = 0;
        else if (skinNr < 0) skinNr = skins.Length - 1;
    }

    void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("Player"))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("Player_", "");
            spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
    }

    // UI Element - Link to a button to select the next skin
    public void SkinPlus()
    {
        skinNr++;
    }

    // UI Element - Link to a button to select the previous skin
    public void SkinMin()
    {
        skinNr--;
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}
