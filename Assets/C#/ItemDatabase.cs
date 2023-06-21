using UnityEngine;

public static class ItemDatabase 
{
public static Item[] Items { get; private set; }
    // Атрибут, гарантирующий вызов метода до загрузки сцены
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]  private static void Initialize() => Items = Resources.LoadAll <Item>("Items/");
}
