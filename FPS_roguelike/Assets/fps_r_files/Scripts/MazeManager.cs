
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public int width = 10;
    public int depth = 10;
    public GameObject[] mazePrefabs; // ������ �������� ����������
    public Transform player;
    private int currentMazeIndex = 0;
    public int lastMaze = 4;


    private List<GameObject> currentMazeBlocks = new List<GameObject>();

    public void GenerateMaze()
    {

        if (currentMazeIndex > lastMaze)
        {
            Debug.Log("���� ���������. ������� �����!");

            return;

        }


        // ������� ������ ��������
        foreach (GameObject block in currentMazeBlocks)
        {
            Destroy(block);
        }
        currentMazeBlocks.Clear();




        if (currentMazeIndex >= mazePrefabs.Length)
        {
            Debug.Log("�������� ��������. ����� �� �����.");
            return;
        }


        GameObject mazePrefab = mazePrefabs[currentMazeIndex];



        // ���������� ����� ��������
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                Vector3 position = new Vector3(x * mazePrefab.transform.lossyScale.x, 0, z * mazePrefab.transform.lossyScale.z);
                Instantiate(mazePrefab, position, Quaternion.identity, transform).transform.parent = transform; ;
            }
        }


        // ���������� ������ � ������ ���������
        player.position = new Vector3(mazePrefab.transform.lossyScale.x / 2f, player.position.y, mazePrefab.transform.lossyScale.z / 2f);

        currentMazeIndex++;
    }



    void Start()
    {

        GenerateMaze();
    }
}
