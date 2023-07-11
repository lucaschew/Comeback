using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    /*
        0 -> Default Room
        1 -> Starting Room
        2 -> Boss Room
        3 -> Treasure Room
        4 -> Forge Room
        5 -> Grave Room
        6 -> Secret Room
    */
    public int RoomType; 
    public Vector2 position;

    /*
        0 -> Up
        1 -> Right
        2 -> Down
        3 -> Left
    */
    public bool[] doors = new bool[] {false, false, false, false};
    public Room(Vector2 pos, int type)
    {
        this.position = pos;
        this.RoomType = type;
    }

}
