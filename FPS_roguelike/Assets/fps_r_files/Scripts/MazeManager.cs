using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public GameObject[] mazePrefabs; // ������ �������� ������
    public Transform player;
    public int currentMazeIndex = 0;
    private List<GameObject> generatedMazeBlocks = new List<GameObject>();
    private GameObject currentMazeBlock;

    public void GenerateNextMazeBlock()
    {
        //�������� �� ���������� ��������� (��������� �������)
        if (currentMazeIndex >= mazePrefabs.Length)
        {
            Debug.Log("��� ���� ������ �����������. ���� ��������.");
            return; // ������� �� �������, ���� ������ ������ ���
        }


        currentMazeIndex++;
        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        // ���������������� ������������ ���������� ������
        if (currentMazeBlock != null)
        {
            Transform lastExit = currentMazeBlock.transform.Find("Exit"); // ������� ����� ����������� �����

            if (lastExit == null)
            {
                Debug.LogError($"�� ������ ����� � ����� {currentMazeIndex}. ���������, ��������� �� ������ ������ � Exit � �������.");
                return; // ���� ������ ���, �� ����������
            }

            Transform newEntrance = newMazeBlock.transform.Find("Entrance");

            if (newEntrance == null)
            {
                Debug.LogError($"�� ������ ���� � ����� {currentMazeIndex}. ���������, ��������� �� ������ ������ � Entrance � �������.");
                Destroy(newMazeBlock);
                return; // ���� ����� ���, ���������� ���� � �� ����������
            }


            newMazeBlock.transform.position = lastExit.position;

            // ������������� ��������, ����� ���� ������ ����� ��� � ���������� �����
            newMazeBlock.transform.position += newEntrance.localPosition;
        }
        else
        {
            // ���� ��� ������ ����, ������������� ������������ �����
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");
            if (newEntrance == null)
            {
                Debug.LogError($"�� ������ ���� � ����� 0. ���������, ��������� �� ������ ������ � Entrance � �������.");
                Destroy(newMazeBlock);
                return;
            }
            newMazeBlock.transform.position = newEntrance.position;
            player.position = newEntrance.position;
        }


        //��������� ������� ����
        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);

    }

    void Start()
    {
        GenerateNextMazeBlock();
    }
}
