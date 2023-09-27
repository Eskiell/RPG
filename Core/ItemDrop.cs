namespace RPG.Core;

public class ItemDrop
{
    public ItemDrop(Item? item, float dropRate)
    {
        Item = item;
        DropRate = dropRate;
    }

    public Item? Item { get; }
    public float DropRate { get; }
}