using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject TeleporterNode;
    public GameObject SpawnRoomPrefab;
    public GameObject RoomType1;
    public GameObject RoomType2;
    public GameObject RoomType3;
    public GameObject EndRoomPrefab;
    public GameObject player;
    public int X;
    public int Z;
    private Node[,] gridArray;
    private Node[,] gridArray2;
    private Vector2Int[] directions = { new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) };
    public float SecondGridHeightOffset;
    private int RandomSpawnRoomX;
    private int RandomSpawnRoomZ;


    private float spawnRoomX;
    private float spawnRoomZ;
    private float endRoomX;
    private float endRoomZ;
    private float roomTypeX;
    private float roomTypeZ;
    private int doorSpawnRoomX = 3;
    private int doorSpawnRoomZ;
    private int doorEndRoomX = 3;
    private int doorEndRoomZ;
    private int doorRoomType1X = 2;
    private int doorRoomType1Z;
    private int doorRoomType2X = 2;
    private int doorRoomType2Z;
    private int doorRoomType3X = 2;
    private int doorRoomType3Z;
    private int doorRoomType4X = 1;
    private int doorRoomType4Z;

    private int doorCoordinateSpawnX;
    private int doorCoordinateSpawnZ;
    private int doorCoordinateEndX;
    private int doorCoordinateEndZ;
    private int doorCoordinateX;
    private int doorCoordinateZ;
    private int doorCoordinateType2X;
    private int doorCoordinateType2Z;
    private int doorCoordinateType3X;
    private int doorCoordinateType3Z;
    private int doorCoordinateType4X;
    private int doorCoordinateType4Z;

    [Header("SpawnRoomPosition")]
    public float SpawnRoomPositionX;
    public float SpawnRoomPositionZ;

    [Header("RoomType1")]
    public float RoomType1PositionX;
    public float RoomType1PositionZ;

    [Header("RoomType2")]
    public float RoomType2PositionX;
    public float RoomType2PositionZ;

    [Header("RoomType3")]
    public float RoomType3PositionX;
    public float RoomType3PositionZ;

    [Header("RoomType4")]
    public float RoomType4PositionX;
    public float RoomType4PositionZ;
    private bool[,] reservedPositions;
    private bool[,] reservedPositions2;

    public float Offset;
    private List<Vector2Int> roomDoorPositions = new List<Vector2Int>();
    private List<Vector2Int> roomDoorPositions2 = new List<Vector2Int>();
    List<Vector2Int> teleporterPositions = new List<Vector2Int>();
    List<Vector2Int> teleporterPositions2 = new List<Vector2Int>();

    private int randomTeleporterX1;
    private int randomTeleporterZ1;
    private int randomTeleporterX2;
    private int randomTeleporterZ2;

    struct Edge
    {
        public Vector2Int start;
        public Vector2Int end;
        public float weight;

        public Edge(Vector2Int s, Vector2Int e, float w)
        {
            start = s;
            end = e;
            weight = w;
        }
    }

    private List<Edge> edges = new List<Edge>();
    private List<Edge> edges2 = new List<Edge>();


    public int GCost;
    public int HCost;

    private void Start()
    {

        reservedPositions = new bool[X, Z];
        reservedPositions2 = new bool[X, Z];

        CreateGrid(0);
        PlaceSpawnRoom();
        PlaceTeleporter1();
        PlaceTeleporter2();
        PlaceRoomType(RoomType1, 2, 2, RoomType1PositionX, RoomType1PositionZ, 0, 1, 1);
        PlaceRoomType(RoomType1, 2, 2, RoomType1PositionX, RoomType1PositionZ, 0, 1, 1);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 0, 2, 1);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 0, 2, 1);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 0, 2, 1);
        PlaceRoomType(RoomType3, 2, 4, RoomType3PositionX, RoomType3PositionZ, 0, 3, 1);
        PlaceRoomType(RoomType3, 2, 4, RoomType3PositionX, RoomType3PositionZ, 0, 3, 1);



        CreateGrid(1);
        PlaceEndRoom();
        PlaceTeleporter3();
        PlaceTeleporter4();
        PlaceRoomType(RoomType1, 2, 2, RoomType1PositionX, RoomType1PositionZ, 100, 1, 2);
        PlaceRoomType(RoomType1, 2, 2, RoomType1PositionX, RoomType1PositionZ, 100, 1, 2);
        PlaceRoomType(RoomType1, 2, 2, RoomType1PositionX, RoomType1PositionZ, 100, 1, 2);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 100, 2, 2);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 100, 2, 2);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 100, 2, 2);
        PlaceRoomType(RoomType2, 2, 3, RoomType2PositionX, RoomType2PositionZ, 100, 2, 2);
        PlaceRoomType(RoomType3, 2, 4, RoomType3PositionX, RoomType3PositionZ, 100, 3, 2);
        PlaceRoomType(RoomType3, 2, 4, RoomType3PositionX, RoomType3PositionZ, 100, 3, 2);


        GenerateEdges();
        var vertices = Prim(roomDoorPositions,edges);
        foreach (var edge in vertices)
        {
            FindPath(edge.start, edge.end, gridArray);
        }
        foreach (var node in gridArray)
        {
            if (node == null || node.delete == false)
            {
                continue;
            }

            Destroy(node.gameObject);
        }

        GenerateEdges2();
        var vertices2 = Prim(roomDoorPositions2, edges2);
        foreach (var edge in vertices2)
        {
            FindPath(edge.start, edge.end, gridArray2);
        }
        foreach (var node in gridArray2)
        {
            if (node == null || node.delete == false)
            {
                continue;
            }
            Destroy(node.gameObject);
        }
    }

    private void Update()
    {
        //Debug.Log(doorCoordinateSpawnX + " " + doorCoordinateSpawnZ);
    }
    void CreateGrid(int gridIndex)
    {
        if (gridIndex == 0)
        {
            gridArray = new Node[X, Z];
            for (int x = 0; x < X; x++)
            {
                for (int z = 0; z < Z; z++)
                {
                    float realX = x * Offset;
                    float realZ = z * Offset;
                    float heightOffset = (gridIndex == 0) ? 0 : SecondGridHeightOffset;
                    Vector3 spawnPosition = new Vector3(realX, heightOffset, realZ);

                    GameObject tile = Instantiate(CellPrefab, spawnPosition, Quaternion.identity);
                    tile.transform.parent = transform;
                    gridArray[x, z] = tile.GetComponent<Node>();
                }
            }
        }
        else if (gridIndex == 1)
        {
            gridArray2 = new Node[X, Z];
            for (int x = 0; x < X; x++)
            {
                for (int z = 0; z < Z; z++)
                {
                    float realX = x * Offset;
                    float realZ = z * Offset;
                    float heightOffset = (gridIndex == 0) ? 0 : SecondGridHeightOffset;
                    Vector3 spawnPosition = new Vector3(realX, heightOffset, realZ);

                    GameObject tile = Instantiate(CellPrefab, spawnPosition, Quaternion.identity);
                    tile.transform.parent = transform;
                    gridArray2[x, z] = tile.GetComponent<Node>();
                }
            }
        }

    }
    void PlaceSpawnRoom()
    {
        int roomSizeX = 3;
        int roomSizeZ = 3;

        bool isOverlapping = true;
        int randomX = 0;
        int randomZ = 0;

        while (isOverlapping)
        {
            randomX = Random.Range(0, X - roomSizeX);
            randomZ = Random.Range(0, Z - roomSizeZ);
            RandomSpawnRoomX = randomX;
            RandomSpawnRoomZ = randomZ;
            isOverlapping = CheckOverlap(randomX, randomZ, roomSizeX, roomSizeZ,1);

        }
        Debug.Log(RandomSpawnRoomX+" "+ RandomSpawnRoomZ);

        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray[x, z].gameObject);
                gridArray[x, z] = null;
            }
        }

        spawnRoomX = (randomX * Offset + Offset / 1.75f) + SpawnRoomPositionX;
        spawnRoomZ = (randomZ * Offset + Offset / 4f) + SpawnRoomPositionZ;

        Vector3 spawnRoomPosition = new Vector3(spawnRoomX, 0, spawnRoomZ);
        Vector3 playerPosition = new Vector3(spawnRoomX + 7, 1.6f, spawnRoomZ+47);
        player.transform.position = playerPosition;
        Instantiate(SpawnRoomPrefab, spawnRoomPosition, Quaternion.identity);
        doorCoordinateSpawnX = randomX + doorSpawnRoomX;
        doorCoordinateSpawnZ = randomZ + doorSpawnRoomZ;
        roomDoorPositions.Add(new Vector2Int(doorCoordinateSpawnX, doorCoordinateSpawnZ));
        reservedPositions[doorCoordinateSpawnX, doorCoordinateSpawnZ] = true;
        gridArray[doorCoordinateSpawnX, doorCoordinateSpawnZ].leftWall.SetActive(false);
        gridArray[doorCoordinateSpawnX, doorCoordinateSpawnZ].leftLight.SetActive(false);
        //Destroy(gridArray[doorCoordinateSpawnX, doorCoordinateSpawnZ]);
    }
    void PlaceEndRoom()
    {
        int roomSizeX = 3;
        int roomSizeZ = 3;
        int randomX = RandomSpawnRoomX;
        int randomZ = RandomSpawnRoomZ;
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions2[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray2[x, z].gameObject);
                gridArray2[x, z] = null;
            }
        }

        endRoomX = (randomX * Offset + Offset / 1.75f) + SpawnRoomPositionX;
        endRoomZ = (randomZ * Offset + Offset / 4f) + SpawnRoomPositionZ;
        Vector3 endRoomPosition = new Vector3(endRoomX, SecondGridHeightOffset, endRoomZ);
        Instantiate(EndRoomPrefab, endRoomPosition, Quaternion.identity);
        doorCoordinateEndX = randomX + doorEndRoomX;
        doorCoordinateEndZ = randomZ + doorEndRoomZ;

        roomDoorPositions2.Add(new Vector2Int(doorCoordinateEndX, doorCoordinateEndZ));
        reservedPositions2[doorCoordinateEndX, doorCoordinateEndZ] = true;
        gridArray2[doorCoordinateEndX, doorCoordinateEndZ].leftWall.SetActive(false);
        gridArray2[doorCoordinateEndX, doorCoordinateEndZ].leftLight.SetActive(false);
        //Destroy(gridArray[doorCoordinateSpawnX, doorCoordinateSpawnZ]);
    }
    

    void PlaceTeleporter1()
    {
        int roomSizeX = 1;
        int roomSizeZ = 1;
        bool isOverlapping = true;
        int randomX = 0;
        int randomZ = 0;
        while (isOverlapping)
        {
            randomX = Random.Range(0, X - roomSizeX);
            randomZ = Random.Range(0, Z - roomSizeZ);
            randomTeleporterX1 = randomX;
            randomTeleporterZ1 = randomZ;
            isOverlapping = CheckOverlap(randomX, randomZ, roomSizeX, roomSizeZ, 1) || IsTeleporterTooClose(randomX, randomZ);
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray[x, z].gameObject);
                gridArray[x, z] = null;
            }
        }
        float teleporterX = (randomX * Offset + Offset / 1.75f) + RoomType4PositionX;
        float teleporterZ = (randomZ * Offset + Offset / 4f) + RoomType4PositionZ;
        Vector3 teleporterPosition = new Vector3(teleporterX, 0, teleporterZ);
        Instantiate(TeleporterNode, teleporterPosition, Quaternion.identity);

        doorCoordinateType4X = randomX + doorRoomType4X;
        doorCoordinateType4Z = randomZ + doorRoomType4Z;

        roomDoorPositions.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        teleporterPositions.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        reservedPositions[doorCoordinateType4X, doorCoordinateType4Z] = true;

        gridArray[doorCoordinateType4X, doorCoordinateType4Z].leftWall.SetActive(false);
        gridArray[doorCoordinateType4X, doorCoordinateType4Z].leftLight.SetActive(false);
    }
    void PlaceTeleporter2()
    {
        int roomSizeX = 1;
        int roomSizeZ = 1;
        bool isOverlapping = true;
        int randomX = 0;
        int randomZ = 0;
        while (isOverlapping)
        {
            randomX = Random.Range(0, X - roomSizeX);
            randomZ = Random.Range(0, Z - roomSizeZ);
            randomTeleporterX2 = randomX;
            randomTeleporterZ2 = randomZ;
            isOverlapping = CheckOverlap(randomX, randomZ, roomSizeX, roomSizeZ, 1) || IsTeleporterTooClose(randomX, randomZ);
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray[x, z].gameObject);
                gridArray[x, z] = null;
            }
        }
        float teleporterX = (randomX * Offset + Offset / 1.75f) + RoomType4PositionX;
        float teleporterZ = (randomZ * Offset + Offset / 4f) + RoomType4PositionZ;
        Vector3 teleporterPosition = new Vector3(teleporterX, 0, teleporterZ);
        Instantiate(TeleporterNode, teleporterPosition, Quaternion.identity);

        doorCoordinateType4X = randomX + doorRoomType4X;
        doorCoordinateType4Z = randomZ + doorRoomType4Z;

        roomDoorPositions.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        teleporterPositions.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        reservedPositions[doorCoordinateType4X, doorCoordinateType4Z] = true;

        gridArray[doorCoordinateType4X, doorCoordinateType4Z].leftWall.SetActive(false);
        gridArray[doorCoordinateType4X, doorCoordinateType4Z].leftLight.SetActive(false);
    }
    void PlaceTeleporter3()
    {
        int roomSizeX = 1;
        int roomSizeZ = 1;
        int randomX = randomTeleporterX1;
        int randomZ = randomTeleporterZ1;

        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions2[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray2[x, z].gameObject);
                gridArray2[x, z] = null;
            }
        }
        float teleporterX = (randomX * Offset + Offset / 1.75f) + RoomType4PositionX;
        float teleporterZ = (randomZ * Offset + Offset / 4f) + RoomType4PositionZ;
        Vector3 teleporterPosition = new Vector3(teleporterX, SecondGridHeightOffset, teleporterZ);
        Instantiate(TeleporterNode, teleporterPosition, Quaternion.identity);

        doorCoordinateType4X = randomX + doorRoomType4X;
        doorCoordinateType4Z = randomZ + doorRoomType4Z;

        roomDoorPositions2.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        teleporterPositions2.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        reservedPositions2[doorCoordinateType4X, doorCoordinateType4Z] = true;

        gridArray2[doorCoordinateType4X, doorCoordinateType4Z].leftWall.SetActive(false);
        gridArray2[doorCoordinateType4X, doorCoordinateType4Z].leftLight.SetActive(false);
    }
    void PlaceTeleporter4()
    {
        int roomSizeX = 1;
        int roomSizeZ = 1;
        int randomX = randomTeleporterX2;
        int randomZ = randomTeleporterZ2;

        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                reservedPositions2[x, z] = true;
            }
        }
        for (int x = randomX; x < randomX + roomSizeX; x++)
        {
            for (int z = randomZ; z < randomZ + roomSizeZ; z++)
            {
                Destroy(gridArray2[x, z].gameObject);
                gridArray2[x, z] = null;
            }
        }
        float teleporterX = (randomX * Offset + Offset / 1.75f) + RoomType4PositionX;
        float teleporterZ = (randomZ * Offset + Offset / 4f) + RoomType4PositionZ;
        Vector3 teleporterPosition = new Vector3(teleporterX, SecondGridHeightOffset, teleporterZ);
        Instantiate(TeleporterNode, teleporterPosition, Quaternion.identity);

        doorCoordinateType4X = randomX + doorRoomType4X;
        doorCoordinateType4Z = randomZ + doorRoomType4Z;

        roomDoorPositions2.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        teleporterPositions2.Add(new Vector2Int(doorCoordinateType4X, doorCoordinateType4Z));
        reservedPositions2[doorCoordinateType4X, doorCoordinateType4Z] = true;

        gridArray2[doorCoordinateType4X, doorCoordinateType4Z].leftWall.SetActive(false);
        gridArray2[doorCoordinateType4X, doorCoordinateType4Z].leftLight.SetActive(false);
    }

    bool IsTeleporterTooClose(int x, int z)
    {
        Vector2Int newTeleporterPos = new Vector2Int(x, z);
        float minDistance = Z/1/2;

        foreach (Vector2Int teleporterPos in teleporterPositions)
        {
            float distance = Vector2Int.Distance(newTeleporterPos, teleporterPos);
            if (distance < minDistance)
            {
                return true;
            }
        }

        return false;
    }
    void PlaceRoomType(GameObject roomType, int roomSizeX, int roomSizeZ, float roomTypePositionAddX, float roomTypePositionAddZ, float gridOffset, int roomTypeNumber, int floor)
    {
        Node[,] currentFloor = floor == 1 ? gridArray : gridArray2;
        bool[,] currentReserved = floor == 1 ? reservedPositions : reservedPositions2;
        List<Vector2Int> currentDoorPositions = floor == 1 ? roomDoorPositions : roomDoorPositions2;

        int randomRoomTypeX;
        int randomRoomTypeZ;
        bool isOverlap = true;

        int currentDoorOffsetX;
        int currentDoorOffsetY;
        if (roomTypeNumber == 1)
        {
            currentDoorOffsetX = doorRoomType1X;
            currentDoorOffsetY = doorRoomType1Z;
        }
        else if (roomTypeNumber == 2)
        {
            currentDoorOffsetX = doorRoomType2X;
            currentDoorOffsetY = doorRoomType2Z;
        }
        else 
        {
            currentDoorOffsetX = doorRoomType3X;
            currentDoorOffsetY = doorRoomType3Z;
        }

        bool tooClose;
        do
        {
            randomRoomTypeX = Random.Range(0, X - roomSizeX);
            randomRoomTypeZ = Random.Range(0, Z - roomSizeZ);
            Vector2Int curr = new(randomRoomTypeX, randomRoomTypeZ);
            tooClose = false;
            foreach(var position in currentDoorPositions)
            {
                var distance = Vector2Int.Distance(curr, position);
                tooClose = distance < 6f;
                if (tooClose)
                {
                    break;
                }
            }
            isOverlap = CheckOverlap(randomRoomTypeX, randomRoomTypeZ, roomSizeX, roomSizeZ, floor);
        }
        while (isOverlap || tooClose);

        for (int x = randomRoomTypeX; x < randomRoomTypeX + roomSizeX; x++)
        {
            for (int z = randomRoomTypeZ; z < randomRoomTypeZ + roomSizeZ; z++)
            {
                Destroy(currentFloor[x, z].gameObject);
                currentFloor[x, z] = null;
                currentReserved[x, z] = true;
            }
        }

        roomTypeX = (randomRoomTypeX * Offset + Offset / 1.75f) + roomTypePositionAddX;
        roomTypeZ = (randomRoomTypeZ * Offset + Offset / 4f) + roomTypePositionAddZ;
        Vector3 roomTypePosition = new Vector3(roomTypeX, gridOffset, roomTypeZ);
        Instantiate(roomType, roomTypePosition, Quaternion.identity);

        doorCoordinateX = randomRoomTypeX + currentDoorOffsetX;
        doorCoordinateZ = randomRoomTypeZ + currentDoorOffsetY;
        currentDoorPositions.Add(new Vector2Int(doorCoordinateX, doorCoordinateZ));
        currentReserved[doorCoordinateX, doorCoordinateZ] = true;
        currentFloor[doorCoordinateX, doorCoordinateZ].leftWall.SetActive(false);
        currentFloor[doorCoordinateX, doorCoordinateZ].leftLight.SetActive(false);
    }

    bool CheckOverlap(int startX, int startZ, int sizeX, int sizeZ, int floor)
    {
        if (floor==1)
        {

        for (int x = startX; x < startX + sizeX; x++)
        {
            for (int z = startZ; z < startZ + sizeZ; z++)
            {
                if (x >= X || z >= Z || reservedPositions[x, z])
                    return true;
            }
        }
        }else if (floor == 2)
        {

            for (int x = startX; x < startX + sizeX; x++)
            {
                for (int z = startZ; z < startZ + sizeZ; z++)
                {
                    if (x >= X || z >= Z || reservedPositions2[x, z])
                        return true;
                }
            }
        }
        return false;
    }
    void GenerateEdges()
    {
        for (int i = 0; i < roomDoorPositions.Count; i++)
        {
            for (int j = i + 1; j < roomDoorPositions.Count; j++)
            {
                Vector2Int start = roomDoorPositions[i];
                Vector2Int end = roomDoorPositions[j];
                float distance = Vector2.Distance(start, end);
                edges.Add(new Edge(start, end, distance));
                Debug.Log("Edge: " + start + " to " + end + ", Weight: " + distance);
            }
            
        }
    }
    void GenerateEdges2()
    {
        for (int i = 0; i < roomDoorPositions2.Count; i++)
        {
            for (int j =  i + 1; j < roomDoorPositions2.Count; j++)
            {
                Vector2Int start = roomDoorPositions2[i];
                Vector2Int end = roomDoorPositions2[j];
                float distance = Vector2.Distance(start, end);
                edges2.Add(new Edge(start, end, distance));
                Debug.Log("Edge: " + start + " to " + end + ", Weight: " + distance);
            }
        }
    }
    List<Edge> Prim(List<Vector2Int> roomDoorPositions, List<Edge>edges)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        List<Edge> mst = new List<Edge>();
        visited.Add(roomDoorPositions[0]);
        while (visited.Count < roomDoorPositions.Count)
        {
            Edge minEdge = new Edge(Vector2Int.zero, Vector2Int.zero, float.MaxValue);
            foreach (Edge edge in edges)
            {
                if ((visited.Contains(edge.start) && !visited.Contains(edge.end)) ||
                    (visited.Contains(edge.end) && !visited.Contains(edge.start)))
                {
                    if (!mst.Contains(edge) && edge.weight < minEdge.weight)
                    {
                        minEdge = edge;
                    }
                }
            }
            mst.Add(minEdge);
            visited.Add(minEdge.start);
            visited.Add(minEdge.end);
            Debug.Log("Added edge to MST: " + minEdge.start + " to " + minEdge.end);
        }
        return mst;
    }

    bool checkBounds(Vector2Int a)
    {
        if (a.y < 0 || a.y >= Z || a.x < 0 || a.x >= X)
        {
            return false;
        }
        return true;
    }

    void FindPath(Vector2Int startPos, Vector2Int targetPos, Node[,] gridArray)
    {
        

        for (int i = 0; i < X; i++)
        {

            for (int z = 0; z < Z; z++)
            {
                if (gridArray[i, z] != null)
                {
                    gridArray[i, z].GridPosition = new Vector2Int(i, z);
                    gridArray[i, z].GCost = int.MaxValue;
                    gridArray[i, z].HCost = float.MaxValue;
                    gridArray[i, z].Parent = null;
                }
            }
        }
        if (!checkBounds(startPos) || !checkBounds(targetPos))
        {
            Debug.LogError("Start or target position is out of bounds!");
            return;
        }
        Node startNode = gridArray[startPos.x, startPos.y];
        Node targetNode = gridArray[targetPos.x, targetPos.y];

        Debug.Log("Start end"+startPos +" "+targetPos); 
        if (startNode == null || targetNode == null)
        {
            
            Debug.LogError("Start node or target node is null!");
            return;
        }
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                {
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                //pulang 
                var curr = node;
                while (curr.GridPosition != startPos)
                {
                    curr.delete = false;
                    curr.Parent.delete = false;
                    curvePath(curr, curr.Parent);
                    curr = curr.Parent;
                }
                return;
            }

            for (int i = 0; i < 4; i++)
            {

                Vector2Int nextPos = node.GridPosition + directions[i];
                Debug.Log(nextPos.ToString());
                if (!checkBounds(nextPos))
                {
                    continue;
                }
                if (closedSet.Contains(gridArray[nextPos.x, nextPos.y]) || gridArray[nextPos.x, nextPos.y] == null)
                {

                    continue;
                }
                int newCostToNeighbour = node.GCost + 1;
                if (newCostToNeighbour < gridArray[nextPos.x, nextPos.y].GCost || !openSet.Contains(gridArray[nextPos.x, nextPos.y]))
                {
                    gridArray[nextPos.x, nextPos.y].GCost = newCostToNeighbour;
                    gridArray[nextPos.x, nextPos.y].HCost = Vector2Int.Distance(nextPos, targetPos);
                    gridArray[nextPos.x, nextPos.y].Parent = node;

                    if (!openSet.Contains(gridArray[nextPos.x, nextPos.y]))
                        openSet.Add(gridArray[nextPos.x, nextPos.y]);
                }
            }
        }
    }
    public void curvePath(Node a, Node b)
    {
        if (a.GridPosition.x < b.GridPosition.x)
        {
            a.rightWall.SetActive(false);
            a.rightLight.SetActive(false);
            b.leftWall.SetActive(false);
            b.leftLight.SetActive(false);
        }
        else if (a.GridPosition.x > b.GridPosition.x)
        {
            a.leftWall.SetActive(false);
            a.leftLight.SetActive(false);
            b.rightWall.SetActive(false);
            b.rightLight.SetActive(false);
        }

        if (a.GridPosition.y < b.GridPosition.y)
        {
            a.topWall.SetActive(false);
            a.topLight.SetActive(false);
            b.bottomWall.SetActive(false);
            b.bottomLight.SetActive(false);
        }
        else if (a.GridPosition.y > b.GridPosition.y)
        {
            b.topWall.SetActive(false);
            b.topLight.SetActive(false);
            a.bottomWall.SetActive(false);
            a.bottomLight.SetActive(false);
        }
    }
}
