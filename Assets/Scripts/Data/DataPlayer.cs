using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    public PantType pantTypePlayer;
    public HatType hatType;
    public ShieldType shieldType;
    public WeaponType weaponType;
    public int score;
    public DataPlayer(HatType hatType, PantType pantTypePlayer, ShieldType shieldType, WeaponType weaponType, int score)
    {
        this.hatType = hatType;
        this.pantTypePlayer = pantTypePlayer;
        this.shieldType = shieldType;
        this.weaponType = weaponType;
        this.score = score;
    }
}
