using UnityEngine;

public sealed class Row : MonoBehaviour
{
    // Создаем массив tiles, который будет хранить тайлы строки
    // Для удобства доступа к ним из других классов, мы делаем его public
    public Tile[] tiles;
}
