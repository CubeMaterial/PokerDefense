using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class TowerManager : Singleton<TowerManager> {

    private JSONNode node;
    public List<string> TowerData;

    public Tower[] Towers;

    private int SelectTowerIndex;
    //private TowerType SelectTowerType;

    private int cost;

    private void Awake()
    {
        node = JSONNode.Parse(GameManager.instance.TowerData.text);
        //Localization.LoadCSV(GameManager.instance.)
    }




    public void LoadTower()
    {
        //string[] TowerLevel = GameDataManager.instance.ReturnTowerArray().Split('_');
        //string[] TowerLock = GameDataManager.instance.ReturnTowerLock().Split('_');

        for (int i = 0; i < Towers.Length; i++)
        {
            //Towers[i].SetLock(TowerLock[i]);
      //      Towers[i].SetTower((TowerType)System.Enum.Parse(typeof(TowerType),TowerLevel[i]));
        }

    }
    
    //public void ClickTower(int TowerIndex, TowerType type)
    //{
    //    SelectTowerIndex = TowerIndex;
    //    SelectTowerType = type;
    //    OnClickUpgradeTower(TowerType.Common);
    //}

    //public void OnClickUpgradeTower(TowerType type)
    //{
    //    cost = (int)ReturnTowerStat(type, TowerStat.Cost);

    //    if (GameDataManager.instance.ReturnValueYellowStone() >= cost)
    //    {
    //        GameDataManager.instance.ChangeValueYellowStone(-cost);
    //        Towers[SelectTowerIndex].SetTower(type);
    //        GameUIManager.instance.RefreshUI();
    //    }
    //    else
    //    {
    //        print("not enough gold");
    //    }
    //}

    public void SaveTower()
    {
        string temp = "";
    }

    //public float ReturnTowerStat(TowerType type, TowerStat stat)
    //{
    //    float f = node[type.ToString()][stat.ToString()].AsInt;
    //    //print("type : " + type.ToString() + " / stat : " + stat.ToString());
    //    return f;
    //}
}
