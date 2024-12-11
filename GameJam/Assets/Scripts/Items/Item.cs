using System;
using UnityEngine;

public abstract class Item{
    public enum ItempType{
        Medicines,
        Potion,
        Boost,
        Syringe
    }
    public ItempType itempType;
    public String description;
    public int holdingAnimation;
    public int activeAnimation;
    public abstract void Use();
}