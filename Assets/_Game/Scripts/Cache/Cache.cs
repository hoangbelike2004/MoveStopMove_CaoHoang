using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
   private static Dictionary<Collider,Character> cacheCollider = new Dictionary<Collider,Character>();
    private static Dictionary<GameObject, Bot> cacheBot = new Dictionary<GameObject, Bot>();
    private static Dictionary<Character, Character> cacheCharacter = new Dictionary<Character, Character>();
    private static Dictionary<Weapon, Weapon> cacheWeapon = new Dictionary<Weapon, Weapon>();
    private static Dictionary<Boomerang, Boomerang> cachewpBoom = new Dictionary<Boomerang, Boomerang>();
    public static Character GetCharacteOfColliderInCache(Collider collider)
    {
        if (!cacheCollider.ContainsKey(collider))
        {
            cacheCollider.Add(collider, collider.GetComponent<Character>());
        }
        return cacheCollider[collider];
    }
    public static Character GetCharacterInCache(Character Cha)
    {
        if (!cacheCharacter.ContainsKey(Cha))
        {
            cacheCharacter.Add(Cha, Cha.GetComponent<Character>());
        }
        return cacheCharacter[Cha];
    }
    public static Bot GetBotInCache(GameObject gameObject)
    {
        if (!cacheBot.ContainsKey(gameObject))
        {
            cacheBot.Add(gameObject, gameObject.GetComponent<Bot>());
        }
        return cacheBot[gameObject];
    }

    public static Weapon GetWeaponInCache(Weapon gameObject)
    {
        if (!cacheWeapon.ContainsKey(gameObject))
        {
            cacheWeapon.Add(gameObject, gameObject.GetComponent<Weapon>());
        }
        return cacheWeapon[gameObject];
    }
    public static Boomerang GetWeaponBoomerangInCache(Boomerang gameObject)
    {
        if (!cachewpBoom.ContainsKey(gameObject))
        {
            cachewpBoom.Add(gameObject, gameObject.GetComponent<Boomerang>());
        }
        return cachewpBoom[gameObject];
    }
}
