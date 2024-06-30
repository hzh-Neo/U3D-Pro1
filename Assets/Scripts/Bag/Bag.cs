using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class BagItem
{
    public string name;
    public int num;
    public int sortID;
}

public class Bag : MonoSingleton<Bag>
{
    public string bagSavePath = Path.Combine(Application.dataPath, "save/bag.save");

    public List<BagItem> bagItems=new List<BagItem>();

    /// <summary>
    /// 根据名称查找项
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public BagItem FindBagItemByName(string name)
    {
        return bagItems.Find(x => x.name == name);
    }

    /// <summary>
    /// 增加项
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public void AddBagItem(string name, int num)
    {
        BagItem bItem = FindBagItemByName(name);
        if (bItem != null)
        {
            bItem.num+=num;
        }
        else
        {
            BagItem bagItem = new BagItem();
            bagItem.name = name;
            bagItem.num = num;
            bagItems.Add(bagItem);
        }
        SaveToFile();
    }

    /// <summary>
    /// 移除项
    /// </summary>
    /// <param name="name"></param>
    public void RemoveBagItem(string name)
    {
        bagItems.Remove(FindBagItemByName(name));
        SaveToFile();
    }

    /// <summary>
    /// 保存至文件
    /// </summary>
    public void SaveToFile()
    {
        Utils.CreateFolder(Path.GetDirectoryName(bagSavePath));
        string json = JsonConvert.SerializeObject(bagItems);
        File.WriteAllText(bagSavePath, json);
    }

    /// <summary>
    /// 加载文件
    /// </summary>
    /// <param name="filePath"></param>
    public void LoadFromFile(string filePath="")
    {
        if (String.IsNullOrEmpty(filePath))
        {
            filePath = bagSavePath;
        }
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            bagItems = JsonConvert.DeserializeObject<List<BagItem>>(json);
        }
        else
        {
            File.WriteAllText(bagSavePath, "");
        }
    }
}

