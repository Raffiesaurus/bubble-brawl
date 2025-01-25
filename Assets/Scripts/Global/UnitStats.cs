using System.Collections.Generic;

[System.Serializable]
public class UnitStats {
    public string Name;
    public List<int> Health;
    public List<int> Damage;
    public List<int> AttackSpeed;
    public List<int> AttackRange;
    public List<int> MoveSpeed;
    public int Cost;
    public List<int> UpgradeCost;
}

[System.Serializable]
public class UnitData {
    public List<UnitStats> Units;
}
