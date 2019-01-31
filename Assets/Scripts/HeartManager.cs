﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for(int i =0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart; //set image to full heart
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 2; //Accounts for half hearts.
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= tempHealth-1) //Removes 1 since i = 0;
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //Empty Heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //Half Heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
