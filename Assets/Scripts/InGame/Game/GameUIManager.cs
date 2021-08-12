using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : Singleton<GameUIManager> {

    public Text GoldLabel;
    public Text DiaLabel;

    public UILabel[] TowerLevelLabel;
    public GameObject TowerUI;
    public GameObject[] TowerUIBtn;

    public GameObject[] Btns;
    public GameObject[] Layout;


    private Color TowerGoldLabelColor = new Color(0.9803921568627451f, 1f,0.3215686274509804f);

    private void Awake()
    {
    }

    public void RefreshUI()
    {
        //GoldLabel.text = GameDataManager.instance.ReturnValueYellowStone().ToString();
        //DiaLabel.text = GameDataManager.instance.ReturnValueGStone().ToString();
        //TowerLevelLabel[0].text = ("Level. " + GameDataManager.instance.ReturnCommonTowerLevel());
        //TowerLevelLabel[1].text = ("Level. " + GameDataManager.instance.ReturnMeleeTowerLevel());
        //TowerLevelLabel[2].text = ("Level. " + GameDataManager.instance.ReturnArrowTowerLevel());
        //TowerLevelLabel[3].text = ("Level. " + GameDataManager.instance.ReturnMagicTowerLevel());

        //TowerUpgradeValueLabel[0].text = GameDataManager.instance.ReturnCommonTowerUpgradeValue().ToString();
        //TowerUpgradeValueLabel[1].text = GameDataManager.instance.ReturnMeleeTowerUpgradeValue().ToString();
        //TowerUpgradeValueLabel[2].text = GameDataManager.instance.ReturnArrowTowerUpgradeValue().ToString();
        //TowerUpgradeValueLabel[3].text = GameDataManager.instance.ReturnMagicTowerUpgradeValue().ToString();


        //if (GameDataManager.instance.ReturnValueYellowStone() >= GameDataManager.instance.ReturnCommonTowerUpgradeValue())
        //{
        //    TowerUpgradeValueLabel[0].color = TowerGoldLabelColor;
        //}
        //else
        //{
        //    TowerUpgradeValueLabel[0].color = Color.gray;
        //}

        //if (GameDataManager.instance.ReturnValueYellowStone() >= GameDataManager.instance.ReturnMeleeTowerUpgradeValue())
        //{
        //    TowerUpgradeValueLabel[1].color = TowerGoldLabelColor;
        //}
        //else
        //{
        //    TowerUpgradeValueLabel[1].color = Color.gray;
        //}

        //if (GameDataManager.instance.ReturnValueYellowStone() >= GameDataManager.instance.ReturnArrowTowerUpgradeValue())
        //{
        //    TowerUpgradeValueLabel[2].color = TowerGoldLabelColor;
        //}
        //else
        //{
        //    TowerUpgradeValueLabel[2].color = Color.gray;
        //}

        //if (GameDataManager.instance.ReturnValueYellowStone() >= GameDataManager.instance.ReturnMagicTowerUpgradeValue())
        //{
        //    TowerUpgradeValueLabel[3].color = TowerGoldLabelColor;
        //}
        //else
        //{
        //    TowerUpgradeValueLabel[3].color = Color.gray;
        //}
    }

    public void ShowTowerUI(GameObject tower)
    {
        TowerUI.transform.localPosition = tower.transform.localPosition;
        TowerUI.gameObject.SetActive(true);
    }

    public void HideTowerUI()
    {
        TowerUI.gameObject.SetActive(false);
    }

    public void DrawMainDeckCard()
    {
        MainDeckMaster.instance.GetCard();
    }



    public void ClickTab(int index)
    {
        for(int i = 0; i < Layout.Length; i++)
        {
            if(i == index)
            {
                Layout[i].gameObject.SetActive(true);
            }
            else
            {
                Layout[i].gameObject.SetActive(false);
            }
        }
    }
    // public void ClickTowerTab()
    // {
    //     Layout[0].gameObject.SetActive(true);
    //     Layout[1].gameObject.SetActive(false);
    //     Layout[2].gameObject.SetActive(false);
    // }

    // public void ClickInventoryTab()
    // {
    //     Layout[0].gameObject.SetActive(false);
    //     Layout[1].gameObject.SetActive(true);
    //     Layout[2].gameObject.SetActive(false);
    // }

    // public void ClickStoreTab()
    // {
    //     Layout[0].gameObject.SetActive(false);
    //     Layout[1].gameObject.SetActive(false);
    //     Layout[2].gameObject.SetActive(true);
    // }




}
