using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // ���������� ����� ChangeScenes, ������� ��������� � �������� ��������� ����� ����� � ��������� �
    public void ChangeScenes(int numberSceenas)
    {
        SceneManager.LoadScene(numberSceenas);
    }
    // ���������� ����� Exit, ������� ��������� ����������
    public void Exit()
    {
        Application.Quit();
    }
}
