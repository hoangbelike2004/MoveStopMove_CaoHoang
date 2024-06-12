using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSkin
{
    public List<int> hatstates;
    public List<int> pantstates;
    public List<int> shieldstates;
    public DataSkin(List<int> hats,List<int> pants,List<int> shields)
    {
        hatstates = new List<int>();
        pantstates = new List<int>();
        shieldstates = new List<int>();
        foreach (int value in hats)
        {
            hatstates.Add(value);
        }
        foreach (int value in pants)
        {
            pantstates.Add(value);
        }
        foreach (int value in shields)
        {
            shieldstates.Add(value);
        }
    }
}
