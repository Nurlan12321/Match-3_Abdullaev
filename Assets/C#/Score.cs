using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public sealed class Score : MonoBehaviour
{
    // ����������� ������ Score
    public static Score Instance { get; private set; }

    private int _score;// ������� �������� �����

    public int ScoreCounter
    {
        get => _score;// ��������� �������� �����
        set
        {
            if (_score == value) return; // �������� �� ������������ �������� �����

            _score = value;// ������ �������� �����
            scoreText.SetText($"Score = {_score}");// ���������� ����������� ����� � ��������� ����

            if (_score > RecordCounter)// �������� �� ��������� ������ �������
            {
                RecordCounter = _score;// ���������� �������� �������
                SaveRecord();// ���������� �����������
            }
        }
    }

    private int _record;// ������� �������� �������

    public int RecordCounter
    {
        get => _record;// ��������� �������� �������
        set
        {
            if (_record == value) return;// �������� �� ������������ �������� �������

            _record = value;// ���������� �������� �������
            recordText.SetText($"Record = {_record}");// ���������� ����������� ������� � ��������� ����
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button resetRecordButton;

    private const string RecordKey = "record";

    private void Awake()
    {
        // �������� ���������� ������ Score
        Instance = this;
    }

    private void Start()
    {
        LoadRecord(); // �������� �������� �������
        resetRecordButton.onClick.AddListener(ResetRecord);// ���������� ����������� ������� ������� �� ������ ������ �������
    }

    private void OnDisable()
    {
        SaveRecord(); // ���������� ����������� ����� ������� �� ����������
    }

    private void SaveRecord()
    {
        PlayerPrefs.SetInt(RecordKey, RecordCounter);// ���������� �������� �������
        PlayerPrefs.Save();// ���������� �������� � PlayerPrefs
    }

    private void LoadRecord()
    {
        RecordCounter = PlayerPrefs.GetInt(RecordKey, 0);// �������� �������� ������� �� PlayerPrefs
    }

    private void ResetRecord()
    {
        RecordCounter = 0;// ����� �������� �������
        SaveRecord();// ���������� �����������
    }
}
