using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Public Variables
    public int NumberOfFloors = 7;
    public int WorldSize = 7;
    // 5 35
    public int[,] RoomRange = new int[,] {
        {5, 10},
        {10, 15},
        {10, 20},
        {15, 20},
        {20, 25},
        {25, 30},
        {30, 35},
    };
    public int[,] TreasureRoomRange = new int[,] {
        {1, 1},
        {1, 1},
        {1, 2},
        {1, 3},
        {1, 3},
        {1, 4},
        {1, 4},
    };
    public int[] SecretRoomRange = new int[] { 1, 2 };
    public int[] ForgeRoomSpawnChance = new int[] { 60, 100 };
    public List<int> GraveRoomFloorSpawns = new List<int> { 3, 5, 7 };
    public Transform mapParent, roomParent;
    public GameObject mapSprite;
    public List<GameObject> roomPrefabs;

    //Private Variables
    private int currentFloor = 0;
    private Room[,] Rooms;
    private List<Vector2> CreatedRooms = new List<Vector2>();

    void Start()
    {
        IncrementMapFloor();
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            CreatedRooms = new List<Vector2>();
            if (mapParent != null && roomParent != null)
            {
                Destroy(mapParent.gameObject);
                Destroy(roomParent.gameObject);

                mapParent = null;
                roomParent = null;
            }

            if (currentFloor <= 7)
            {
                CreateMap();
                SetRoomConnections();
                GenerateMap();
                GenerateRoomsObjects();
            }
        }

        if (Input.GetKeyDown("1"))
        {
            currentFloor = 1;
        }
        if (Input.GetKeyDown("2"))
        {
            currentFloor = 2;
        }
        if (Input.GetKeyDown("3"))
        {
            currentFloor = 3;
        }
        if (Input.GetKeyDown("4"))
        {
            currentFloor = 4;
        }
        if (Input.GetKeyDown("5"))
        {
            currentFloor = 5;
        }
        if (Input.GetKeyDown("6"))
        {
            currentFloor = 6;
        }
        if (Input.GetKeyDown("7"))
        {
            currentFloor = 7;
        }


    }

    public void IncrementMapFloor()
    {
        currentFloor++;
        if (mapParent != null && roomParent != null)
        {
            Destroy(mapParent.gameObject);
            Destroy(roomParent.gameObject);

            mapParent = null;
            roomParent = null;
        }

        if (currentFloor <= 7)
        {
            CreatedRooms.Clear();
            CreateMap();
            SetRoomConnections();
            GenerateMap();
            GenerateRoomsObjects();
        }
    }

    private void CreateMap()
    {
        //Create initial map
        Rooms = new Room[WorldSize * 2, WorldSize * 2];

        //Instantiate starting room and Add to visited list
        Rooms[WorldSize, WorldSize] = new Room(Vector2.zero, 1);
        CreatedRooms.Add(Vector2.zero);

        Vector2 currentPosition = Vector2.zero;

        //Generate Default Rooms
        float randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        int numOfRoomsInFloor = Random.Range(RoomRange[currentFloor - 1, 0], RoomRange[currentFloor - 1, 1]);
        for (int i = 0; i < numOfRoomsInFloor; i++)
        {
            //Create randomness and less connected rooms
            float randomInverse = ((float)i) / ((float)numOfRoomsInFloor);
            float randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomInverse);

            currentPosition = GetNewPosition(false);

            //As the remaining rooms are less, create more single-connected rooms
            if (Random.value > randomCompare && !OnlyOneConnection(currentPosition))
            {
                while (!OnlyOneConnection(currentPosition))
                {

                    currentPosition = GetNewPosition(true);

                }
            }

            //Add Position as Room and Store
            Rooms[(int)currentPosition.x + WorldSize, (int)currentPosition.y + WorldSize] = new Room(currentPosition, 0);
            CreatedRooms.Add(currentPosition);
        }

        //Treasure Room
        int randomCounter = (int)Random.Range(TreasureRoomRange[currentFloor - 1, 0], TreasureRoomRange[currentFloor - 1, 1]);
        for (int i = 0; i < randomCounter; i++)
        {
            GenerateSpecialRoom(3);
        }

        //Forge Room
        randomCounter = (int)Random.Range(0, ForgeRoomSpawnChance[1]);
        if ((Random.value * 100) >= ForgeRoomSpawnChance[1] - ForgeRoomSpawnChance[0])
        {
            GenerateSpecialRoom(4);
        }

        //Secret Room
        randomCounter = Random.Range(SecretRoomRange[0], SecretRoomRange[1]);
        for (int i = 0; i < randomCounter; i++)
        {
            GenerateSpecialRoom(6);
        }

        //Boss Room
        GenerateSpecialRoom(2);

        //Grave Room
        if (GraveRoomFloorSpawns.Contains(currentFloor))
        {
            GenerateSpecialRoom(5);
        }


    }
    private Vector2 GetNewPosition(bool getOnlyOneNeighbour, int numberOfTries = 100)
    {
        int x = 0, y = 0, singleNeighbourCounter = 0;
        Vector2 currentPosition = Vector2.zero;

        while (CreatedRooms.Contains(currentPosition) || x >= WorldSize || x < -WorldSize || y >= WorldSize || y < -WorldSize)
        {
            //Pick a random room from the list
            int index = Mathf.RoundToInt(Random.value * (CreatedRooms.Count - 1));

            //Get Coordinates
            x = (int)CreatedRooms[index].x;
            y = (int)CreatedRooms[index].y;

            //Get Random Direction Values
            /*
            0 -> X-Axis
            1 -> Y-Axis
            */
            bool WhichAxis = (Random.value >= 0.5f);
            bool positiveDirection = (Random.value >= 0.5f);

            if (WhichAxis && positiveDirection)
            {
                y++;
            }
            else if (WhichAxis && !positiveDirection)
            {
                y--;
            }
            else if (!WhichAxis && positiveDirection)
            {
                x++;
            }
            else if (!WhichAxis && !positiveDirection)
            {
                x--;
            }

            //Set currentPosition based on results
            currentPosition = new Vector2(x, y);

            // If single room cannot be found after # of tries, allow connection
            if (getOnlyOneNeighbour && singleNeighbourCounter <= numberOfTries && !OnlyOneConnection(currentPosition))
            {
                singleNeighbourCounter++;
                continue;
            }

        }

        return currentPosition;
    }

    private bool OnlyOneConnection(Vector2 checkPos)
    {
        int connections = 0;
        Vector2[] cardinalDirections = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        foreach (Vector2 direction in cardinalDirections)
        {
            Vector2 newPos = checkPos + direction;
            //If beside special room, return false
            if (CreatedRooms.Contains(newPos) && Rooms[(int)newPos.x + WorldSize, (int)newPos.y + WorldSize].RoomType > 1)
            {
                return false;
            }
            else if (CreatedRooms.Contains(newPos))
            {
                connections++;
            }
        }

        return connections <= 1;
    }

    private void GenerateSpecialRoom(int type)
    {
        Vector2 specialRoom;
        if (type >= 2 && type <= 5)
        {
            specialRoom = GetNewPosition(true, -1);
            while (!OnlyOneConnection(specialRoom))
            {
                specialRoom = GetNewPosition(true, -1);
            }
        }
        else
        {
            specialRoom = GetNewPosition(true, 300);
        }

        Rooms[(int)specialRoom.x + WorldSize, (int)specialRoom.y + WorldSize] = new Room(specialRoom, type);
        CreatedRooms.Add(specialRoom);
    }

    private Vector2 GuaranteeUniqueRoom()
    {
        Vector2[] cardinalDirections = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        foreach (Room room in Rooms)
        {

            if (room == null || room.RoomType > 0)
            {
                continue;
            }

            Vector2 actualPosition = new Vector2(room.position.x, room.position.y);

            foreach (Vector2 direction in cardinalDirections)
            {
                if (!CreatedRooms.Contains(actualPosition + direction) && OnlyOneConnection(actualPosition + direction))
                {
                    int x = (int)(actualPosition + direction).x, y = (int)(actualPosition + direction).y;

                    if (!(x >= WorldSize || x < -WorldSize || y >= WorldSize || y < -WorldSize))
                    {
                        return (actualPosition + direction);
                    }
                }
            }

        }

        return Vector2.zero;
    }

    private void SetRoomConnections()
    {
        foreach (Room _room in Rooms)
        {
            if (_room == null)
                continue;

            if (CreatedRooms.Contains(_room.position + Vector2.up))
            {
                _room.doors[0] = true;
            }
            if (CreatedRooms.Contains(_room.position + Vector2.right))
            {
                _room.doors[1] = true;
            }
            if (CreatedRooms.Contains(_room.position + Vector2.down))
            {
                _room.doors[2] = true;
            }
            if (CreatedRooms.Contains(_room.position + Vector2.left))
            {
                _room.doors[3] = true;
            }
        }
    }

    private void GenerateMap()
    {

        //If MapParent is Null, Create one
        if (mapParent == null)
        {
            var temp = new GameObject();
            temp.name = "MapParent";
            temp.transform.position = new Vector3(1000, 1000, 50);

            mapParent = temp.transform;
        }
        mapSprite.gameObject.transform.position = new Vector3(0, 0, 0);
        const int scaleAspect = 5;

        foreach (Room _room in Rooms)
        {
            if (_room == null)
            {
                continue;
            }

            //14, 10

            mapSprite.gameObject.transform.localScale = new Vector3(scaleAspect, scaleAspect, scaleAspect);
            Vector2 aspectRatio = new Vector2(1, 1);
            Vector3 drawPosition = _room.position * aspectRatio;


            MapSpriteGenerator mapper = Object.Instantiate(mapSprite, drawPosition, Quaternion.identity, mapParent).GetComponent<MapSpriteGenerator>();
            mapper.setupRoom(_room.RoomType, _room.doors);

        }
    }

    private void GenerateRoomsObjects()
    {
        if (roomParent == null)
        {
            var temp = new GameObject();
            temp.name = "RoomParent";
            temp.AddComponent<RoomAssigner>();

            roomParent = temp.transform;
        }

        roomParent.GetComponent<RoomAssigner>().Setup(Rooms, roomPrefabs);
    }

}