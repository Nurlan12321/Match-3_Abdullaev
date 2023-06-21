using UnityEngine;

[CreateAssetMenu(menuName ="Play/Item")]
public sealed class Item : ScriptableObject
{
    public int value;// Публичное поле "value", которое хранит значение типа "int"
    public Sprite sprite;// Публичное поле "sprite", которое хранит объект типа "Sprite"
}
