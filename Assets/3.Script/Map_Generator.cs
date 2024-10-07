using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_Generator : MonoBehaviour
{

    [Header("타일맵 관련")]
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase the_ocean_warm, the_ocean_cold, the_ocean_deep;
    [Space]
    [Header("값 관련")]
    [SerializeField] private float mapScale = 0.01f;
    [SerializeField] public int mapSize;

    private float seed;

    private async void Start()
    {
        seed = Random.Range(0, 10000f);
        var noiseArr = await Task.Run(GenerateNoise);
        SettingTileMap(noiseArr);
    }


    private float[,] GenerateNoise()
    {
        float[,] noiseArr = new float[mapSize * 5, mapSize * 5];
        for (int x = 0; x < mapSize * 5f; x++)
        {
            for (int y = 0; y < mapSize * 5f; y++)
            {
                noiseArr[x, y] = Mathf.PerlinNoise(
                    x * mapScale + seed,
                    y * mapScale + seed);
            }
        }
        return noiseArr;
    }

    private void SettingTileMap(float[,] noiseArr)
    {
        Vector3Int point = new Vector3Int(-500, -500, 0);

        for (int x = 0; x < mapSize * 5f; x++)
        {

            for (int y = 0; y < mapSize * 5f; y++)
            {
                point.Set(-(mapSize/2 * 5) + x, -(mapSize/2 * 5) + y, 0);
                tileMap.SetTile(point, GetTileByHight(noiseArr[x, y]));
            }
        }
    }

    private TileBase GetTileByHight(float hight)
    {
        if (hight <= 0.45f) return the_ocean_warm;
        else if (0.45f <= hight && hight <= 0.6f) return the_ocean_cold;
        else return the_ocean_deep;
    }
}