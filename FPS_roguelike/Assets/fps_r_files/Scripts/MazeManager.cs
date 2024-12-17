using UnityEngine;
using System.Collections.Generic;

public class MazeManager : MonoBehaviour
{
    public GameObject[] mazePrefabs;
    public Transform player;
    public float mazeBlockLength = 10f; // Не используется, но может пригодиться

    private List<GameObject> generatedMazeBlocks = new List<GameObject>();
    private GameObject currentMazeBlock;
    private int currentDifficulty = 0;

    public void GenerateNextMazeBlock()
    {
        int nextBlockIndex;

        if (currentDifficulty < mazePrefabs.Length)
        {
            nextBlockIndex = currentDifficulty;
        }
        else
        {
            nextBlockIndex = Random.Range(1, mazePrefabs.Length);
        }

        GameObject newMazeBlock = Instantiate(mazePrefabs[nextBlockIndex], transform);

        Transform newEntrance = newMazeBlock.transform.Find("Entrance");
        if (newEntrance == null)
        {
            Debug.LogError("В префабе не найден объект 'Entrance'!");
            Destroy(newMazeBlock);
            return;
        }

        if (currentMazeBlock != null)
        {
            Transform lastExit = currentMazeBlock.transform.Find("Exit");
            if (lastExit == null)
            {
                Debug.LogError("В префабе не найден объект 'Exit'!");
                Destroy(newMazeBlock);
                return;
            }

            newMazeBlock.transform.position = lastExit.position - newEntrance.localPosition;
            

            Destroy(currentMazeBlock, 0.5f);
            IncreaseDifficulty();
        }
        

        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);

    }

    

    private void IncreaseDifficulty()
    {
        currentDifficulty++;
        Debug.Log("Уровень сложности: " + currentDifficulty);
    }

    void Start()
    {
        GenerateNextMazeBlock();
        currentDifficulty++;
    }
}

