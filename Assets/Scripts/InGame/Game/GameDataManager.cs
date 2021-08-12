using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using System.Security.Cryptography;
public class GameDataManager : Singleton<GameDataManager>
{

    private int iCurrentGold;
    private int iStageLevel;

    private int[] iDeckValue;
    private int[] iDeckLimitCount;

    private GameData gData;


    private string FilePath;

    private void Awake()
    {
        gData = new GameData();
        FilePath = Application.persistentDataPath + "/data.g0dzer0";

        //iDeckValue = new int[]

        if (File.Exists(FilePath) == false)
        {
            Restart();
            GameUIManager.instance.RefreshUI();
        }
        else
        {
            Load();
        }

    }

    private void Start()
    {
    }

    public void Restart()
    {
        iStageLevel = 0;
    }

    public int ReturnCurrentLevel()
    {
        return iStageLevel;
    }

    public void Save()
    {
        gData = null;
        WriteData();
        BinarySerialize(gData);
    }

    public void WriteData()
    {
        print("Write Data");
    }

    public void Load()
    {
        BinaryDeserialize();
        ReadData();
    }

    void ReadData()
    {
      

        print("Read Data");
    }

    private bool ReturnExistSaveFile()
    {
        return File.Exists(FilePath);
    }

    public void BinarySerialize(GameData sData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if (ReturnExistSaveFile())
        {
            stream = new FileStream(FilePath, FileMode.Open);
        }
        else
        {
            stream = new FileStream(FilePath, FileMode.Create);
        }
        formatter.Serialize(stream, sData);
        stream.Close();
    }

    public GameData BinaryDeserialize()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Open);
        GameData data = (GameData)formatter.Deserialize(stream);
        gData = data;
        stream.Close();

        return gData;
    }
}
