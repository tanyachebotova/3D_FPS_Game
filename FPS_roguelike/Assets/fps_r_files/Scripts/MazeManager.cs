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
            Debug.LogError("������ ��� �������� ���������!");
            return;
        }

        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        Transform newEntrance = newMazeBlock.transform.Find("Entrance");
        if (newEntrance == null)
        {
            Debug.LogError("� ������� �� ������ ������ 'Entrance'");
            Destroy(newMazeBlock);
            return;
        }


        if (currentMazeBlock != null)
        {
            Transform lastExit = currentMazeBlock.transform.Find("Exit");
            if (lastExit == null)
            {
                Debug.LogError("� ������� �� ������ ������ 'Exit'");
                Destroy(newMazeBlock);
                return;
            }

            // ������������� ����� ���� ���, ����� ��� ���� �������� � ������� �����������
            newMazeBlock.transform.position = lastExit.position - newEntrance.localPosition;

        }
        else
        {
            // ������������� ������ �� ���� ������� �����
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