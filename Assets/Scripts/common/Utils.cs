using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Utils
{
    public static void CreateFolder(string folderPath)
    {
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static void GetAssetsFolder()
    {
        
    }
}

