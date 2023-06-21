using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using System;
using UnityEngine.UI;

public sealed class Board : MonoBehaviour
{
    // Определение статической переменной для доступа к экземпляру класса 
    public static Board Instance { get; private set; }
    // Звук сбора элемента 
    [SerializeField] private AudioClip collectSound;
    // Компонент для воспроизведения звука 
    [SerializeField] private AudioSource audioSource;
    // Текстовое поле для отображения времени 
    public Text textTimer;
    public float timeStart;
    // Переменная для отслеживания запущенности таймера 
    bool timerRunning = true;
    // Игровые объекты
    public GameObject at;
    public GameObject res;
    public GameObject back;
    // Массив строк, содержащих ячейки 
    public Row[] rows;

    public Tile[,] Tiles { get; private set; }
    // Ширина и высота игрового поля 
    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);
    // Список выбранных ячеек 
    private readonly List<Tile> _selection = new List<Tile>();
    // Длительность анимации перемещения ячейки 
    private const float TweenDuration = 0.25f;


    private void Awake() => Instance = this;
    private void Start()
    {
        at.SetActive(false);
        textTimer.text = timeStart.ToString();
        // Установка начального времени на текстовое поле 
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];
        // Инициализация массива ячеек 
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];
                // Задание координат ячейки 
                tile.x = x;
                tile.y = y;
                // Задание случайного элемента из базы данных 
                tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];
                // Добавление ячейки в массив 
                Tiles[x, y] = tile;
            }
        }
    }
    // Выбор ячейки 
    public async void Select (Tile tile)
    {

        if (!_selection.Contains(tile))
        {
            // Если ячейка является соседней к последней выбранной, добавить её в список выбранных 
            if (_selection.Count > 0)
            {
                if (Array.IndexOf(_selection[0].Neighbours, tile) != -1) _selection.Add(tile);
            }

            else
            {
                // Если список выбранных пуст, добавить ячейку в список 
                _selection.Add(tile);
            }
        }
        // Если выбрано менее двух ячеек, выход из метода
        if (_selection.Count < 2) return;
        Debug.Log($"Selected tiles at ({_selection[0].x}, {_selection[0].y}) and ({_selection[1].x}, {_selection[1].y})");
        
        await Swap(_selection[0], _selection[1]);

        if (CanPop())
        {
            // Выполняем комбинацию и добавляем время
            Pop();
            timeStart += 5;
        }
        else
        {
            // Возвращаем ячейки на исходные позиции
            await Swap(_selection[0], _selection[1]);
              
        }
        // Очистка списка выбранных ячеек
        _selection.Clear();
    }
    // Анимация перемещения ячейки
    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();


        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));


        await sequence.Play()
            .AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;

    }
    // Проверка возможности совершения комбинации
    private bool CanPop()
    {

        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                    return true;
         
        return false;
    }
    // Выполнение комбинации
    private async void Pop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];
                var connectedTiles = tile.GetConnectedTiles();
                
                if (connectedTiles.Skip(1).Count() < 2) continue;
                var deFlateSequence = DOTween.Sequence();
                
                foreach( var connectdTile in connectedTiles) deFlateSequence.Join(connectdTile.icon.transform.DOScale(Vector3.zero, TweenDuration));

                audioSource.PlayOneShot(collectSound);
                
                Score.Instance.ScoreCounter += tile.Item.value * connectedTiles.Count;
                await deFlateSequence.Play()
                    .AsyncWaitForCompletion();

                var inflateSequence = DOTween.Sequence();

                

                foreach(var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                }
                await inflateSequence.Play()
                    .AsyncWaitForCompletion();
                   // Сбрасываем счётчики циклов, чтобы перебрать все ячейки снова
                x = 0;
                y = 0;
            }
        }
    }
    // Обновление времени и отображения текстового поля
    private void Update()
    {
        if (timerRunning == true)
        {
            timeStart -= Time.deltaTime;
            textTimer.text = Mathf.Round(timeStart).ToString();
        }
        // Если время вышло, активируем объект "at" и скрываем объекты "res" и "back"
        if (timeStart <0)
        {
            timeStart = 0;
            at.SetActive(true);
            res.SetActive(false);
            back.SetActive(false);
        }
    }
}
