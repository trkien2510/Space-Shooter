using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelected : MonoBehaviour
{
    [SerializeField] private GameObject mainShip;
    [SerializeField] private Sprite[] ships;
    [SerializeField] private GameObject smallBullet;
    [SerializeField] private GameObject mediumBullet;
    [SerializeField] private GameObject largeBullet;
    [SerializeField] private Sprite[] smallBulletColor;
    [SerializeField] private Sprite[] mediumBulletColor;
    [SerializeField] private Sprite[] largeBulletColor;

    private int shipIndex = 0;
    private int colorIndex = 0;

    private void Awake()
    {
        shipIndex = PlayerPrefs.GetInt("ShipIndex", 0);
        colorIndex = PlayerPrefs.GetInt("BulletColorIndex", 0);
    }

    private void Start()
    {
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
        smallBullet.GetComponent<SpriteRenderer>().sprite = smallBulletColor[colorIndex];
        mediumBullet.GetComponent<SpriteRenderer>().sprite = mediumBulletColor[colorIndex];
        largeBullet.GetComponent<SpriteRenderer>().sprite = largeBulletColor[colorIndex];
    }

    public void PreviousShip()
    {
        shipIndex--;
        if (shipIndex < 0)
        {
            shipIndex += ships.Length;
        }
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
    }

    public void NextShip()
    {
        shipIndex = (shipIndex + 1) % ships.Length;
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
    }

    public void PreviousBulletColor()
    {
        colorIndex--;
        if (colorIndex < 0)
        {
            colorIndex += smallBulletColor.Length;
        }
        smallBullet.GetComponent<SpriteRenderer>().sprite = smallBulletColor[colorIndex];
        mediumBullet.GetComponent<SpriteRenderer>().sprite = mediumBulletColor[colorIndex];
        largeBullet.GetComponent<SpriteRenderer>().sprite = largeBulletColor[colorIndex];
    }

    public void NextBulletColor()
    {
        colorIndex = (colorIndex + 1) % smallBulletColor.Length;
        smallBullet.GetComponent<SpriteRenderer>().sprite = smallBulletColor[colorIndex];
        mediumBullet.GetComponent<SpriteRenderer>().sprite = mediumBulletColor[colorIndex];
        largeBullet.GetComponent<SpriteRenderer>().sprite = largeBulletColor[colorIndex];
    }

    public void SaveChoice()
    {
        PlayerPrefs.SetInt("ShipIndex", shipIndex);
        PlayerPrefs.SetInt("BulletColorIndex", colorIndex);
        PlayerPrefs.Save();
    }
}
