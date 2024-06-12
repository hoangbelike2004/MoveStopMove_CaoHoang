using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    

    public void SaveDataSkin(List<int> hats, List<int> pants, List<int> shields)
    {
        DataSkin datask = new DataSkin(hats,pants,shields);
        string Jsonstrsk = JsonUtility.ToJson(datask);
        PlayerPrefs.SetString(Contains.DATA_SKIN ,Jsonstrsk);
    }
    public void SaveDataPlayer(HatType hatTypePlayer, PantType pantTypePlayer, ShieldType shieldType, WeaponType weaponType, int score)
    {
        DataPlayer datapl = new DataPlayer(hatTypePlayer, pantTypePlayer, shieldType, weaponType, score);
        string Jsonstrpl = JsonUtility.ToJson(datapl);
        PlayerPrefs.SetString(Contains.DATA_PLAYER, Jsonstrpl);
    }
    public DataPlayer GetDataPlayer()
    {
        string s = PlayerPrefs.GetString(Contains.DATA_PLAYER);
        DataPlayer dtpl;
        if (PlayerPrefs.HasKey(Contains.DATA_PLAYER))
        {
            dtpl = JsonUtility.FromJson<DataPlayer>(s);
            return dtpl;
        }
        else
        {
            return null;
        }


    }
    public DataSkin GetDataSkin()
    {
        string s = PlayerPrefs.GetString( Contains.DATA_SKIN );
        DataSkin dtsk;
        if (PlayerPrefs.HasKey(Contains.DATA_SKIN))
        {
            dtsk = JsonUtility.FromJson<DataSkin>(s);
            return dtsk;
        }
        else
        {
            return null;
        }

       
    }
    public void GetData<T>(string _datastr)
    {
        string s = PlayerPrefs.GetString(_datastr, "");
        if ( s != "" )
        {
            T objectdata;
            objectdata = JsonUtility.FromJson<T>(s);
        }
    }
    public void CheckData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string ab = PlayerPrefs.GetString(key);
            DataSkin dtsk= JsonUtility.FromJson<DataSkin>(ab);
            Debug.Log(dtsk.hatstates[2]);
        }
    }

}

