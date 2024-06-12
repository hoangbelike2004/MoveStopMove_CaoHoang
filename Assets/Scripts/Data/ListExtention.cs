using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Change
{

        public static string ToJsonString(this List<int> list)
        {
            return JsonUtility.ToJson(list);
        }

        public static List<int> FromJsonString(this string jsonString)
        {
            return JsonUtility.FromJson<List<int>>(jsonString);
        }

}
