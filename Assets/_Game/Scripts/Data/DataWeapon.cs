using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWeapon
{
    public List<int> weapondataList;

    public DataWeapon(List<int> weapons)
    {
        weapondataList = new List<int>();

        foreach (int value in weapons)
        {
            weapondataList.Add(value);
        }

    }
}
