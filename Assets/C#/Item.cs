using UnityEngine;

[CreateAssetMenu(menuName ="Play/Item")]
public sealed class Item : ScriptableObject
{
    public int value;// ��������� ���� "value", ������� ������ �������� ���� "int"
    public Sprite sprite;// ��������� ���� "sprite", ������� ������ ������ ���� "Sprite"
}
