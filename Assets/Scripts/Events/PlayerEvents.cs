using System;

/// <summary>
/// Class with events related to player.
/// </summary>
public class PlayerEvents
{
    /// <summary>
    /// Triggered when player gains experience.
    /// </summary>
    public event Action<int> OnExpAdd;

    /// <summary>
    /// Invokes <see cref="OnExpAdd"/> event.
    /// </summary>
    /// <param name="amount">Amount of experience.</param>
    public void ExpAdd(int amount)
    {
        OnExpAdd?.Invoke(amount);
    }

    /// <summary>
    /// Triggered when player gains gold.
    /// </summary>
    public event Action<int> OnGoldAdd;

    /// <summary>
    /// Invokes <see cref="OnGoldAdd"/> event.
    /// </summary>
    /// <param name="amount">Amount of gold.</param>
    public void GoldAdd(int amount)
    {
        OnGoldAdd?.Invoke(amount);
    }

    /// <summary>
    /// Triggered when player gains an item.
    /// </summary>
    public event Action<Item> OnItemAdd;

    /// <summary>
    /// Invokes <see cref="OnItemAdd"/> event.
    /// </summary>
    /// <param name="item">Item the player gained.</param>
    public void ItemAdd(Item item)
    {
        OnItemAdd?.Invoke(item);
    }
}
