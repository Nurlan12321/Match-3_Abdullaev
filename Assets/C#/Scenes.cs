using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // Определяем метод ChangeScenes, который принимает в качестве параметра номер сцены и загружает её
    public void ChangeScenes(int numberSceenas)
    {
        SceneManager.LoadScene(numberSceenas);
    }
    // Определяем метод Exit, который закрывает приложение
    public void Exit()
    {
        Application.Quit();
    }
}
