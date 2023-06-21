using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public sealed class Score : MonoBehaviour
{
    // Определение класса Score
    public static Score Instance { get; private set; }

    private int _score;// Текущее значение счета

    public int ScoreCounter
    {
        get => _score;// Получение значения счета
        set
        {
            if (_score == value) return; // Проверка на неизменность значения счета

            _score = value;// Запись значения счета
            scoreText.SetText($"Score = {_score}");// Обновление отображения счета в текстовом поле

            if (_score > RecordCounter)// Проверка на установку нового рекорда
            {
                RecordCounter = _score;// Обновление значения рекорда
                SaveRecord();// Сохранение результатов
            }
        }
    }

    private int _record;// Текущее значение рекорда

    public int RecordCounter
    {
        get => _record;// Получение значения рекорда
        set
        {
            if (_record == value) return;// Проверка на неизменность значения рекорда

            _record = value;// Обновление значения рекорда
            recordText.SetText($"Record = {_record}");// Обновление отображения рекорда в текстовом поле
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button resetRecordButton;

    private const string RecordKey = "record";

    private void Awake()
    {
        // Создание экземпляра класса Score
        Instance = this;
    }

    private void Start()
    {
        LoadRecord(); // Загрузка значения рекорда
        resetRecordButton.onClick.AddListener(ResetRecord);// Добавление обработчика события нажатия на кнопку сброса рекорда
    }

    private void OnDisable()
    {
        SaveRecord(); // Сохранение результатов перед выходом из приложения
    }

    private void SaveRecord()
    {
        PlayerPrefs.SetInt(RecordKey, RecordCounter);// Сохранение значения рекорда
        PlayerPrefs.Save();// Сохранение настроек в PlayerPrefs
    }

    private void LoadRecord()
    {
        RecordCounter = PlayerPrefs.GetInt(RecordKey, 0);// Загрузка значения рекорда из PlayerPrefs
    }

    private void ResetRecord()
    {
        RecordCounter = 0;// Сброс значения рекорда
        SaveRecord();// Сохранение результатов
    }
}
