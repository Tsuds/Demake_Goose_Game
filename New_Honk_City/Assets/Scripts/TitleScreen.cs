﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public bool onTitleScreen = true;
    public GameObject titleScreen;
    public GameObject player;
    public GameObject level;
    public GameObject items;

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && onTitleScreen)
        {
            onTitleScreen = false;
            titleScreen.SetActive(false);
            player.SetActive(true);
            level.SetActive(true);
            items.SetActive(true);
        }

        if (onTitleScreen)
        {
            titleScreen.SetActive(true);
            player.SetActive(false);
            level.SetActive(false);
            items.SetActive(false);
        }
    }
}
