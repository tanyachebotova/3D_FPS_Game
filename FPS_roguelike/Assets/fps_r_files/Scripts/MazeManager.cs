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
        //Проверка на завершение генерации (добавляем условие)
        if (currentMazeIndex >= mazePrefabs.Length)
        {
            Debug.Log("Все типы блоков закончились. Игра окончена.");
            return; // Выходим из функции, если больше блоков нет
        }


        currentMazeIndex++;
        GameObject newMazeBlock = Instantiate(mazePrefabs[currentMazeIndex], transform);

        // Позиционирование относительно последнего выхода
        if (currentMazeBlock != null)
        {
            Transform lastExit = currentMazeBlock.transform.Find("Exit"); // Находим выход предыдущего блока

            if (lastExit == null)
            {
                Debug.LogError($"Не найден выход в блоке {currentMazeIndex}. Проверьте, правильно ли назван объект с Exit в префабе.");
                return; // Если выхода нет, не продолжаем
            }

            Transform newEntrance = newMazeBlock.transform.Find("Entrance");

            if (newEntrance == null)
            {
                Debug.LogError($"Не найден вход в блоке {currentMazeIndex}. Проверьте, правильно ли назван объект с Entrance в префабе.");
                Destroy(newMazeBlock);
                return; // Если входа нет, уничтожаем блок и не продолжаем
            }


            newMazeBlock.transform.position = lastExit.position;

            // Корректировка смещения, чтобы вход нового блока был в правильном месте
            newMazeBlock.transform.position += newEntrance.localPosition;
        }
        else
        {
            // Если это первый блок, позиционируем относительно входа
            Transform newEntrance = newMazeBlock.transform.Find("Entrance");
            if (newEntrance == null)
            {
                Debug.LogError($"Не найден вход в блоке 0. Проверьте, правильно ли назван объект с Entrance в префабе.");
                Destroy(newMazeBlock);
                return;
            }
            newMazeBlock.transform.position = newEntrance.position;
            player.position = newEntrance.position;
        }


        //Сохраняем текущий блок
        currentMazeBlock = newMazeBlock;
        generatedMazeBlocks.Add(newMazeBlock);

    }

    void Start()
    {
        GenerateNextMazeBlock();
    }
}
