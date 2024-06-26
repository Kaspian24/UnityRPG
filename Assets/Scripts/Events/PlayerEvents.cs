using System;

public class PlayerEvents
{
    public event Action<int> OnExpAdd; // subscribe to this to react on experience addition
    public void ExpAdd(int amount) // call this to notify about experience being added
    {
        OnExpAdd?.Invoke(amount);
    }

    public event Action<int> OnGoldAdd; // subscribe to this to react on gold addition
    public void GoldAdd(int amount) // call this to notify about gold being added
    {
        OnGoldAdd?.Invoke(amount);
    }

    public event Action<Item> OnItemAdd; // subscribe to this to react on item addition
    public void ItemAdd(Item item) // call this to notify about item being added
    {
        OnItemAdd?.Invoke(item);
    }
}
