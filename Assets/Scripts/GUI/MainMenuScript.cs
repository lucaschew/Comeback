using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public int currentSceneIndex = 1;

    public int high_btn, counter;
    public Button play, opt, exit;
    public Sprite play_norm, opt_norm, exit_norm;
    public Sprite play_high, opt_high, exit_high;
    public Sprite play_prsd, opt_prsd, exit_prsd;

    public enum Cursor{
        Play = 0,
        Options = 1,
        Exit = 2
    }

    //TODO: Turn into array system to shorten code

    private void Start() {
        high_btn = 0;
    }

    private void Update() {
        // Determine position of cursor
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (high_btn == 0) {
                high_btn = 2;
            } else {
                high_btn -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (high_btn == 2) {
                high_btn = 0;
            } else {
                high_btn += 1;
            }
        }

        //Play Button
        if (high_btn == (int) Cursor.Play) {
            play.image.sprite = play_high;
            } else {
            play.image.sprite = play_norm;
        }
        if (high_btn == (int) Cursor.Play && Input.GetKeyDown(KeyCode.Return)) {
        play.image.sprite = play_prsd;
        SceneManager.LoadScene(currentSceneIndex);
        }
        
        //Options Button
        if (high_btn == (int) Cursor.Options) {
            opt.image.sprite = opt_high;
            } else {
            opt.image.sprite = opt_norm;
        }
        if (high_btn == (int) Cursor.Options && Input.GetKeyDown(KeyCode.Return)) {
            opt.image.sprite = opt_prsd;
            SceneManager.LoadScene("Option Menu");
        }

        //Exit Button
        if (high_btn == (int) Cursor.Exit) {
            exit.image.sprite = exit_high;
        } else {
            exit.image.sprite = exit_norm;
        }
        if (high_btn == (int) Cursor.Exit && Input.GetKeyDown(KeyCode.Return)) {
            exit.image.sprite = exit_prsd;
            Application.Quit();
        }
    }
}
