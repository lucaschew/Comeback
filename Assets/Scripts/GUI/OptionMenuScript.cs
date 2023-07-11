using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenuScript : MonoBehaviour
{
    public enum Cursor {
        Volume = 0,
        Fullscreen = 1,
        Back = 2
    }
    public int selected, volume;
    // Objects
    public Image Background, Fullscreen, Volume, Button;
    // Fullscreen Sprites
    public Sprite f_sel_on, f_sel_off, f_un_on, f_un_off;
    //Button Sprites
    public Sprite butt_norm, butt_sel, butt_press;
    //Array for Unselected Volume
    public Sprite[] UV;
    //Array for Selected Volume
    public Sprite[] SV;


    private void Start() {
        selected = 0;
        volume = 5;
        }

    private void Update() {
        // Determine position of cursor
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (selected == (int) Cursor.Volume) {
                selected = (int) Cursor.Back;
            } else {
                selected -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (selected == (int) Cursor.Back) {
                selected = (int) Cursor.Volume;
            } else {
                selected += 1;
            }
        }

        //Volume Sprites
        if (selected == (int) Cursor.Volume) {
            Volume.sprite = SV[volume];
        } else {
            Volume.sprite = UV[volume];
        }

        //Volume Implementation
        if (selected == (int) Cursor.Volume && Input.GetKeyDown(KeyCode.RightArrow)) {
            volume++;
            if (volume >= 9) {
                volume = 9;
            }
        }
        if (selected == (int) Cursor.Volume && Input.GetKeyDown(KeyCode.LeftArrow)) {
            volume--;
            if (volume <= 0) {
                volume = 0;
            }
        }
        AudioListener.volume = volume / 10;

        //Fullscreen Sprite
        if (selected == (int) Cursor.Fullscreen) {
            if (Screen.fullScreen == true) {
                Fullscreen.sprite = f_sel_on;
            } 
            if (Screen.fullScreen == false) {
                Fullscreen.sprite = f_sel_off;
            }
        } else {
            if (Screen.fullScreen == true) {
                Fullscreen.sprite = f_un_on;
            }
            if (Screen.fullScreen == false) {
                Fullscreen.sprite = f_un_off;
            }
        }
        //Fullscreen Implementation
        if (selected == (int) Cursor.Fullscreen && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))) {
            Screen.fullScreen = !Screen.fullScreen;
        }


        //Back Button 
        if (selected == (int) Cursor.Back) {
            Button.sprite = butt_sel;
        } else {
            Button.sprite = butt_norm;
        }
        if (selected == (int) Cursor.Back && (Input.GetKeyDown(KeyCode.Return))) {
            Button.sprite = butt_press;
            SceneManager.LoadScene("Start Screen");
        }
    }
}
