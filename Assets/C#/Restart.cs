using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    // ��������� ����� RestartLevel, ������� ����� ������������� ������� 
    public void RestartLevel()
    {
        // ��������� ����� "Play"
        SceneManager.LoadScene("Play");
    }
}
