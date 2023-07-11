using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    //[SerializeField]
    private GameObject roomPrefabs;
    private Dictionary<List<bool>, string> roomDictionary = new Dictionary<List<bool>, string>()
    {
        { new List<bool> {true, false, false, false}, "N" },
        { new List<bool> {true, true, false, false}, "NE" },
        { new List<bool> {true, true, true, false}, "NES" },
        { new List<bool> {true, true, false, true}, "NEW" },
        { new List<bool> {true, false, true, false}, "NS" },
        { new List<bool> {true, false, false, true}, "NW" },
        { new List<bool> {true, false, true, true}, "NSW" },
        { new List<bool> {false, true, false, false}, "E" },
        { new List<bool> {false, true, true, false}, "ES" },
        { new List<bool> {false, true, false, true}, "EW" },
        { new List<bool> {false, true, true, true}, "ESW" },
        { new List<bool> {false, false, true, false}, "S" },
        { new List<bool> {false, false, true, true}, "SW" },
        { new List<bool> {false, false, false, true}, "W" },
        { new List<bool> {true, true, true, true}, "NESW" },
    };

    //20, 12
    public Vector2 roomDimensions = new Vector2(20, 12);
    public Vector2 roomBorders = new Vector2(7, 5);

    public void Setup(Room[,] rooms, List<GameObject> inputRoomPrefabs)
    {
        //Create Rooms
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue;
            }

            foreach(List<bool> doorDict in roomDictionary.Keys){
                if (Enumerable.SequenceEqual(doorDict, room.doors.ToList())){
                    this.roomPrefabs = inputRoomPrefabs.Where(roomObj => roomObj.name == roomDictionary[doorDict]).SingleOrDefault();
                    break;
                }
            }            
            
            roomPrefabs.AddComponent<RoomInstance>();
            Vector3 pos = new Vector3(room.position.x * (roomDimensions.x + roomBorders.x), room.position.y * (roomDimensions.y + roomBorders.y), 0);
            RoomInstance roomInstance = Instantiate(roomPrefabs, pos, Quaternion.identity, this.gameObject.transform).GetComponent<RoomInstance>();
            roomInstance.CreateRoom(room.doors, room.position, room.RoomType);

        }
    }
}
