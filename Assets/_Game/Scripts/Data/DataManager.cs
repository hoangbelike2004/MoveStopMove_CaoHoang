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
    public void SaveDataWeapon(List<int> weapon)
    {
        DataWeapon datawp = new DataWeapon(weapon);
        string Jsonstrwp = JsonUtility.ToJson(datawp);
        PlayerPrefs.SetString(Contains.DATA_WEAPON, Jsonstrwp);
    }
    public void SaveDataBot(int value)
    {
        PlayerPrefs.SetInt(Contains.DATA_LEVELBOT , value);
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
    public int GetDataBot(string a)
    {
        return PlayerPrefs.GetInt(a);
    }
    public void CheckData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string ab = PlayerPrefs.GetString(key);
            DataWeapon dtsk= JsonUtility.FromJson<DataWeapon>(ab);
            Debug.Log(dtsk.weapondataList[2]);
        }
    }

}
public class MyClass<T>
{
    public static T GetDataKey(string key)
    {
        
        string s = PlayerPrefs.GetString(key);
        T dtwp = default(T);
        if (PlayerPrefs.HasKey(key))
        {
            dtwp = JsonUtility.FromJson<T>(s);
            return dtwp;
        }
        else
        {
            return dtwp;
        }
    }
}
