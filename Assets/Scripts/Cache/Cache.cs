using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
   private static Dictionary<Collider,Character> cacheCharacter = new Dictionary<Collider,Character>();
    private static Dictionary<GameObject, Bot> cacheBot = new Dictionary<GameObject, Bot>();
    public static Character GetCharacterInCache(Collider collider)
    {
        if (!cacheCharacter.ContainsKey(collider))
        {
            cacheCharacter.Add(collider, collider.GetComponent<Character>());
        }
        return cacheCharacter[collider];
    }
    public static Bot GetBotInCache(GameObject gameObject)
    {
        if (!cacheBot.ContainsKey(gameObject))
        {
            cacheBot.Add(gameObject, gameObject.GetComponent<Bot>());
        }
        return cacheBot[gameObject];
    }
}
