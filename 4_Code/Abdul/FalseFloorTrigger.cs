using UnityEngine;

public class FalseFloorTrigger : MonoBehaviour
{
    public Terrain terrain;
    private TerrainData terrainData;

    private float originalFloorDepth = 0.166667f;
    private float desiredFloorDepth = 0f;
    private int startX = 125;
    private int startY = 316;
    private int EndX = 145;
    private int EndY = 335;

    // Start is called before the first frame update
    void Start()
    {
        // searches for terrain object
        GameObject temp = GameObject.Find("Terrain");
        if (temp != null)
        {
            // gets Terrain
            terrain = temp.GetComponent<Terrain>();
            terrainData = terrain.terrainData;
        }
        else
        {
            Debug.Log("Terrain not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivateFalseFloor();
            // DeactivateFalseFloor();
        }
    }

    void ActivateFalseFloor()
    {
        int heightmapWidth = terrainData.heightmapWidth;
        int heightmapHeight = terrainData.heightmapHeight;
        float[,] heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);

        for (int x = startX; x < EndX; x++)
        {
            for (int y = startY; y < EndY; y++)
            {
                heights[x, y] = desiredFloorDepth;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    public void DeactivateFalseFloor()
    {
        int heightmapWidth = terrainData.heightmapWidth;
        int heightmapHeight = terrainData.heightmapHeight;
        float[,] heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);

        for (int x = startX; x < EndX; x++)
        {
            for (int y = startY; y < EndY; y++)
            {
                heights[x, y] = originalFloorDepth;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}
