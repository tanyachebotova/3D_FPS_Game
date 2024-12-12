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

        currentMazeIndex++;
        if (currentMazeIndex >= mazePrefabs.Length)
        {
            currentMazeIndex = mazePrefabs.Length -1; // ����������� �� ��������� ���� �����, ���� �� ������ ���
            Debug.Log("���� ������ �����������, �����! ������ ����� �����������.");


        }


        // ������� ����� ����
        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        // ������������� ����� ���� ����� � �������
        if (currentMazeBlock != null)
        {
            Transform currentExit = currentMazeBlock.transform.Find("Exit");
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");

            //Debug.Log($"currentExit {currentExit}, newEntrance {newEntrance}");



            newMazeBlock.transform.position = currentExit.position - newEntrance.localPosition; // ��������� �������
            //Debug.Log($"newMazeBlock.transform.position{newMazeBlock.transform.position}");

            // ���������� ���� ������ ����� (�����������)
            Destroy(newEntrance.gameObject);


        }
        else
        {
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");
            player.transform.position = newEntrance.position;
        }


        // ��������� ����� ���� ��� �������
        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);




    }

    void Start()
    {

        GenerateNextMazeBlock();




    }


}
