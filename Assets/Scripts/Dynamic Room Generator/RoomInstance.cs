using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public Vector2 position;
    public int type;

    //Top, Right, Bottom, Left
    public bool[] doors;

    //Top, Right, Bottom, Left, Wall?
    public GameObject[] doorObjects;

    //------------------------------------------------------------------------//

    private Vector2 aspectSize;

    public void CreateRoom(bool[] doorsInput, Vector2 positionInput, int typeInput) {
        this.doors = doorsInput;
        this.position = positionInput;
        this.type = typeInput;
    }
    
}
