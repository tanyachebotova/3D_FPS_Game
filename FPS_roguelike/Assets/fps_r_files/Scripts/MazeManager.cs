using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MazeManager : MonoBehaviour
{
    public GameObject[] mazeBlockPrefabs;
    public GameObject player;
    public Transform currentEntrance;
    public List<GameObject> generatedBlocks = new List<GameObject>();
    private System.Random random = new System.Random();

    void Start()
    {
        if (mazeBlockPrefabs == null || mazeBlockPrefabs.Length == 0 || player == null)
        {
            Debug.LogError("MazeManager: Missing Prefabs or Player!");
            return;
        }
        GenerateNextBlock();
    }

    void GenerateNextBlock()
    {
        int nextLevelIndex = generatedBlocks.Count % mazeBlockPrefabs.Length;
        GameObject newBlock = Instantiate(mazeBlockPrefabs[nextLevelIndex], currentEntrance.position, Quaternion.identity);
        generatedBlocks.Add(newBlock);

        Transform newEntrance = newBlock.transform.Find("Entrance");
        Transform newExit = newBlock.transform.Find("Exit");

        if (newEntrance == null || newExit == null)
        {
            Debug.LogError("MazeBlock prefab is missing 'Entrance' or 'Exit' objects!");
            return;
        }

        Vector3 rotation = new Vector3(0, random.Next(0, 4) * 90, 0);
        newBlock.transform.Rotate(rotation);

        currentEntrance = newExit;
        player.transform.position = newEntrance.position;
    }

    public void OnPlayerExitBlock() { GenerateNextBlock(); }
}
