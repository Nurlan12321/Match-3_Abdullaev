using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    // Объявляем метод RestartLevel, который будет перезагружать уровень 
    public void RestartLevel()
    {
        // Загружаем сцену "Play"
        SceneManager.LoadScene("Play");
    }
}
