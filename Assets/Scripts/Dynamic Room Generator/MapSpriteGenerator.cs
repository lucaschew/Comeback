using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteGenerator : MonoBehaviour
{
    public Sprite room;
    // TODO: Have all types of sprites for each type of room
    public Color[] roomColours = {
        Color.white,
        Color.green,
        Color.red,
        Color.yellow,
        Color.cyan,
        Color.black,
        Color.gray,
    };

    /*
        0 -> Default Room
        1 -> Starting Room
        2 -> Boss Room
        3 -> Treasure Room
        4 -> Forge Room
        5 -> Grave Room
        6 -> Secret Room
    */
    private int type;

    /*
        0 -> Up
        1 -> Right
        2 -> Down
        3 -> Left
    */
    private bool[] doors = new bool[] {false, false, false, false};
    private SpriteRenderer spriteRend;
    public void setupRoom(int typeInput, bool[] doorsInput){
        spriteRend = GetComponent<SpriteRenderer>();
        this.type = typeInput;
        this.doors = doorsInput;

        spriteRend.color = roomColours[this.type];
        this.gameObject.name = typeInput.ToString();
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
    }

    private void colourPicker(){
        spriteRend.color = roomColours[this.type];
    }


}
