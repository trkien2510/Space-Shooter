using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelected : MonoBehaviour
{
    [SerializeField] private GameObject mainShip;
    [SerializeField] private Sprite[] ships;
    private int shipIndex = 0;

    private void Start()
    {
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
    }

    public void PreviousShip()
    {
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
        shipIndex--;
        if (shipIndex < 0)
        {
            shipIndex += ships.Length;
        }
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
    }

    public void NextShip()
    {
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
        shipIndex = (shipIndex + 1) % ships.Length;
        mainShip.GetComponent<SpriteRenderer>().sprite = ships[shipIndex];
    }

    public void SaveChoice()
    {
        PlayerPrefs.SetInt("ShipIndex", shipIndex);
    }
}
