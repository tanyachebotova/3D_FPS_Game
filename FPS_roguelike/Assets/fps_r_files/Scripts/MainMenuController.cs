using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Метод для кнопки "Играть"
    public void PlayGame()
    {
        // Загружаем следующую сцену (сцену игры)
        SceneManager.LoadScene("Scene2"); // Укажите название вашей игровой сцены
    }

    // Метод для кнопки "Выход"
    public void ExitGame()
    {
        // Закрывает игру
        Debug.Log("Выход из игры"); // Для проверки в редакторе
        Application.Quit();
    }
}
