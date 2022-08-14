using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    void SaveData(SaveData data);
    void LoadData(SaveData data);
}
