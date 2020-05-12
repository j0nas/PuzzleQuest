using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEditor.UIElements;
using System;

public class JigsawPieceSpawner : MonoBehaviour
{
    public int BOARD_WIDTH = 3;
    public int BOARD_HEIGHT = 3;

    public GameObject tile;
    public Texture2D puzzleImage;
    public Material material;

    private GameObject[] tiles;
    private bool[] tileCorrectlyPlaced;

    void Explode()
    {
        GameObject.Find("Board").SetActive(false);
        Vector3 explosionPos = new Vector3(-3.17f, 1.37f, 1.62f);
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 25.0f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(250.0f, explosionPos, 50.0f, 3.0f);
            }
        }
    }

    public void OnTilePlace(XRBaseInteractable tile, string socketName)
    {
        try {
            int socketId = int.Parse(socketName.Split('_').Last());
            int tileId = int.Parse(tile.name.Split('_').Last());
            bool tileIsCorrectlyPlaced = (socketId == tileId);
            tileCorrectlyPlaced[tileId - 1] = tileIsCorrectlyPlaced;

            if (tileCorrectlyPlaced.All(x => x))
            {
                Debug.Log("You won!");
                Explode();
            }
        }
        catch (FormatException e) {

        }
    }

    void SetUVs(int x, int y, int currentTile)
    {
        Mesh mesh = tiles[currentTile].GetComponent<MeshFilter>().mesh;
        float uvWidth = 1.0f / BOARD_WIDTH;
        float uvHeight = 1.0f / BOARD_HEIGHT;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        // 0: X=0, Y=1 - TOP-LEFT
        // 1: X=1, Y=1 - TOP-RIGHT
        // 2: X=0, Y=0 - BOTTOM-LEFT
        // 3: X=1, Y=0 - BOTTOM-RIGHT

        // Front: 0-3
        // Top: 4,5,8,9
        // Back: 6,7,10,11
        // Bottom: 12-15
        // Left: 16-19
        // Right: 20-23

        // THIS GETS US A '7' tile
        // UVs[4] = new Vector2(0.0f, uvWidth);
        // UVs[5] = new Vector2(uvHeight, uvHeight);
        // UVs[8] = new Vector2(0.0f, 0.0f);
        // UVs[9] = new Vector2(uvWidth, 0.0f);

        UVs[4] = new Vector2(uvWidth * x, uvWidth * (y + 1));         // TOP-LEFT
        UVs[5] = new Vector2(uvHeight * (x + 1), uvHeight * (y + 1)); // TOP-RIGHT
        UVs[8] = new Vector2(uvHeight * x, uvHeight * y);             // BOTTOM-LEFT
        UVs[9] = new Vector2(uvWidth * (x + 1), uvWidth * y);         // BOTTOM-RIGHT
        mesh.uv = UVs;
    }

    void CreateTile(int currentTile, int x, int y) 
    {
        tiles[currentTile] = Instantiate(tile, transform.position, Quaternion.identity);
        tiles[currentTile].GetComponent<Renderer>().material = material;
        tiles[currentTile].name = "tile_" + (currentTile + 1);
        SetUVs(x, y, currentTile);
    }

    void Start()
    {
        tileCorrectlyPlaced = new bool[BOARD_WIDTH * BOARD_HEIGHT];

        int tileCount = BOARD_WIDTH * BOARD_HEIGHT;
        tiles = new GameObject[tileCount];
        material.mainTexture = puzzleImage;
        int currentTile = 0;

        for (int y = 0; y < BOARD_HEIGHT; y++)
            for (int x = 0; x < BOARD_WIDTH; x++)
                CreateTile(currentTile++, x, y);
    }

    void Update()
    {
        
    }
}
