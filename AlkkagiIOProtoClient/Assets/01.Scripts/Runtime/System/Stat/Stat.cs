using System;

[Serializable]
public class Stat
{
    [NonSerialized] public int level = 1;
    public int baseValue;
    public int increaseValue;

    public int CurrentValue => baseValue + increaseValue * (level - 1);

    public Stat(Stat stat)
    {
        level = stat.level;
        baseValue = stat.baseValue;
        increaseValue = stat.increaseValue;
    }
}