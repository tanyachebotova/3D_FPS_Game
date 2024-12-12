using UnityEngine;
using System.Collections.Generic;

public class MazeManager : MonoBehaviour
{
    public GameObject[] mazePrefabs;
    public Transform player;

    private List<GameObject> generatedMazeBlocks = new List<GameObject>();
    private GameObject currentMazeBlock;
    private int currentMazeIndex = 0;


    public void GenerateNextMazeBlock()
    {
        if (currentMazeIndex >= mazePrefabs.Length)
        {
            Debug.LogError("Больше нет префабов лабиринта!");
            return;
        }

        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        Transform newEntrance = newMazeBlock.transform.Find("Entrance");
        if (newEntrance == null)
        {
            Debug.LogError("В префабе не найден объект 'Entrance'");
            Destroy(newMazeBlock);
            return;
        }


        if (currentMazeBlock != null)
        {
            Transform lastExit = currentMazeBlock.transform.Find("Exit");
            if (lastExit == null)
            {
                Debug.LogError("В префабе не найден объект 'Exit'");
                Destroy(newMazeBlock);
                return;
            }

            // Позиционируем новый блок так, чтобы его вход совпадал с выходом предыдущего
            newMazeBlock.transform.position = lastExit.position - newEntrance.localPosition;

        }
        else
        {
            // Позиционируем игрока на вход первого блока
            player.position = newEntrance.position;
        }


        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);
        currentMazeIndex++;
    }

    void Start()
    {
        GenerateNextMazeBlock();
    }
}