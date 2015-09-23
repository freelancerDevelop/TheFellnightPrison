﻿using UnityEngine;
using System;

public class Weapon : BaseItem{

    private string Name;
    private double Id;
    private int Range, EleDmgAmt, PhysDmgAmt;
    private DmgType PhysDmg;
    private EleDmgType EleDmg;
    private WeaponType WeapType;
    private BaseItem ItemBase;

    public Weapon(string _name)
    {
        Name = _name;
        Id = (double)UnityEngine.Random.Range(0, 1000000000);
        Range = UnityEngine.Random.Range(1, 20);
        Array values = EleDmgType.GetValues(typeof(EleDmgType));
        System.Random random = new System.Random();
        EleDmg = (EleDmgType)values.GetValue(random.Next(values.Length));
        values = DmgType.GetValues(typeof(DmgType));
        random = new System.Random();
        PhysDmg = (DmgType)values.GetValue(random.Next(values.Length));
        if (EleDmg != EleDmgType.None)
        {
            EleDmgAmt = UnityEngine.Random.Range(0, 100);
        }
        else
        {
            EleDmgAmt = 0;
        }
        PhysDmgAmt = UnityEngine.Random.Range(0, 60);
        if (Range < 2) {
            WeapType = WeaponType.Low;
        } else if (Range < 4){
            WeapType = WeaponType.Medium;
        } else if (Range < 10) {
            WeapType = WeaponType.High;
        } else if (Range < 30) {
            WeapType = WeaponType.RLow;
        } else {
            WeapType = WeaponType.RHigh;
        }
        ItemBase = new BaseItem();
    }

    public Weapon(string _name, DmgType _dmgType, int _physDmgAmt, EleDmgType _eleDmgType, int _eleDmgAmt, int _range)
    {
        Name = _name;
        Id = (double)UnityEngine.Random.Range(0, 1000000000);
        Range = _range;
        EleDmgAmt = _eleDmgAmt;
        PhysDmgAmt = _physDmgAmt;
        PhysDmg = _dmgType;
        EleDmg = _eleDmgType;
        if (Range < 2)
        {
            WeapType = WeaponType.Low;
        }
        else if (Range < 4)
        {
            WeapType = WeaponType.Medium;
        }
        else if (Range < 10)
        {
            WeapType = WeaponType.High;
        }
        else if (Range < 30)
        {
            WeapType = WeaponType.RLow;
        }
        else
        {
            WeapType = WeaponType.RHigh;
        }
        ItemBase = new BaseItem();
    }

    public Weapon(string _name, DmgType _dmgType, int _physDmgAmt, EleDmgType _eleDmgType, int _eleDmgAmt, int _range, int _dura, int _weight)
    {
        Name = _name;
        Id = (double)UnityEngine.Random.Range(0, 1000000000);
        Range = _range;
        EleDmgAmt = _eleDmgAmt;
        PhysDmgAmt = _physDmgAmt;
        PhysDmg = _dmgType;
        EleDmg = _eleDmgType;
        if (Range < 2)
        {
            WeapType = WeaponType.Low;
        }
        else if (Range < 4)
        {
            WeapType = WeaponType.Medium;
        }
        else if (Range < 10)
        {
            WeapType = WeaponType.High;
        }
        else if (Range < 30)
        {
            WeapType = WeaponType.RLow;
        }
        else
        {
            WeapType = WeaponType.RHigh;
        }
        ItemBase = new BaseItem(_dura, _weight);
    }

    public void SetName(string _value) { Name = _value; }
    public string GetName() { return Name; }
    public void SetRange(int _value) { Range = _value; }
    public int GetRange() { return Range; }
    public void SetDmgType(DmgType _value) { PhysDmg = _value; }
    public DmgType GetDmgType() { return PhysDmg; }
    public void SetPhysDmgAmt(int _value) { PhysDmgAmt = _value; }
    public int GetPhysDmgAmt() { return PhysDmgAmt; }
    public void SetEleDmgType(EleDmgType _value) { EleDmg = _value; }
    public EleDmgType GetEleDmgType() { return EleDmg; }
    public void SetEleDmgAmt(int _value) { EleDmgAmt = _value; }
    public int GetEleDmgAmt() { return EleDmgAmt; }
    public void SetWeapType(WeaponType _value) { WeapType = _value; }
    public WeaponType GetWeapType() { return WeapType; }
    public void SetBaseItem(BaseItem _value) { ItemBase = _value; }
    public BaseItem GetBaseItem() { return ItemBase; }
	
}
