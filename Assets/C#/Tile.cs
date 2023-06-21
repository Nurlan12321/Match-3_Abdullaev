using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class Tile : MonoBehaviour
{
    // Позиция плитки на игровом поле
    public int x;
    public int y;
    // Объект на плитке
    private Item _item;
    // Свойство Item для получения и изменения объекта на плитке
    public Item Item
    {
        get => _item;
        set
        {
            // Если новый объект равен текущему, ничего не делаем
            if (_item == value) return;
            {
                // Записываем новый объект и меняем изображение иконки плитки
                _item = value;
                icon.sprite = _item.sprite;
            }
        }
    }
    public Image icon;

    public Button button;
    // Свойства для получения соседних плиток
    public Tile Left => x > 0 ? Board.Instance.Tiles[x - 1, y]: null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1]: null;
    public Tile Right => x < Board.Instance.Width - 1? Board.Instance.Tiles[x + 1, y] : null;
    public Tile Bottom => y < Board.Instance.Width - 1 ? Board.Instance.Tiles[x, y + 1] : null;
    // Массив соседних плиток
    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom,
    };
    // Обработчик события клика по кнопке на плитке
    private void Start() =>  button.onClick.AddListener(() => Board.Instance.Select(this));
    // Метод для получения списка связанных плиток
    public List<Tile> GetConnectedTiles(List<Tile> exclude = null)
    {
        // Создаем новый список из текущей плитки
        var result = new List<Tile> { this, };

        // Если список исключенных плиток не передан, создаем новый список и дописываем туда текущую плитку
        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        // Иначе добавляем текущую плитку в список исключенных плиток
        else
        {
            exclude.Add(this);
        }
        // Рекурсивно получаем список связанных плиток для всех соседних плиток
        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        // Возвращаем список связанных плиток
        return result;
    }

}
