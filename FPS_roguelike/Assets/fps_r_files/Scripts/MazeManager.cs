using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public GameObject[] mazePrefabs; // Массив префабов блоков
    public Transform player;
    public int currentMazeIndex = 0;
    private List<GameObject> generatedMazeBlocks = new List<GameObject>();
    private GameObject currentMazeBlock;

    public void GenerateNextMazeBlock()
    {

        currentMazeIndex++;
        if (currentMazeIndex >= mazePrefabs.Length)
        {
            currentMazeIndex = mazePrefabs.Length -1; // Зацикливаем на последнем типе блока, если их больше нет
            Debug.Log("Типы блоков закончились, блять! Дальше будут повторяться.");


        }


        // Создаем новый блок
        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        // Позиционируем новый блок рядом с текущим
        if (currentMazeBlock != null)
        {
            Transform currentExit = currentMazeBlock.transform.Find("Exit");
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");

            //Debug.Log($"currentExit {currentExit}, newEntrance {newEntrance}");



            newMazeBlock.transform.position = currentExit.position - newEntrance.localPosition; // бесшовный переход
            //Debug.Log($"newMazeBlock.transform.position{newMazeBlock.transform.position}");

            // Уничтожаем вход нового блока (опционально)
            Destroy(newEntrance.gameObject);


        }
        else
        {
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");
            player.transform.position = newEntrance.position;
        }


        // Сохраняем новый блок как текущий
        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);




    }

    void Start()
    {

        GenerateNextMazeBlock();




    }


}
