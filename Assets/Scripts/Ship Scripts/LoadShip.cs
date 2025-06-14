using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShip : MonoBehaviour
{
    [SerializeField] private Sprite[] ships;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int selectedShip = PlayerPrefs.GetInt("ShipIndex");
        sr.sprite = ships[selectedShip];
    }
}
