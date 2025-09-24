using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StatContainer<TEnum> where TEnum : Enum
{
    [Serializable]
    private class StatInfo
    {
        public TEnum type;
        public Stat stat;
    }

    [SerializeField] List<StatInfo> statInfoList;
    private Dictionary<TEnum, Stat> statTable = null;

    public Stat this[TEnum key]
    {
        get => statTable[key];
        set => statTable[key] = value;
    }

    public void Build()
    {
        statTable ??= new Dictionary<TEnum, Stat>();
        statTable.Clear();
        foreach(var statInfo in statInfoList)
        {
            Stat stat = new Stat(statInfo.stat);
            stat.level = 1;
            statTable[statInfo.type] = stat;
        }
    }
}