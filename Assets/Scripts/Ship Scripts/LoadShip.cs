using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShip : MonoBehaviour
{
    [SerializeField] private Sprite[] ships;
    private SpriteRenderer sr;
    private int selectedShip;

    private void Awake()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        selectedShip = PlayerPrefs.GetInt("ShipIndex", 0);
    }

    void Start()
    {
        sr.sprite = ships[selectedShip];
    }
}
