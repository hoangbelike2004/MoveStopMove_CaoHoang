using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWatPoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    [SerializeField] Camera _cam;
    //IEnumerator Start()
    //{
    //    _cam = Camera.main;
    //    while (true)
    //    {
    //        float minX = img.GetPixelAdjustedRect().width;
    //        float maxX = Screen.width - minX-50;
    //        //Debug.Log($"{minX}//// {maxX}////{Screen.width}");
    //        img.transform.position = _cam.WorldToScreenPoint(target.position);
    //        float minY = img.GetPixelAdjustedRect().height / 2;
    //        float maxY = Screen.height - minY;
    //        Vector2 pos = _cam.WorldToScreenPoint(target.position);
    //        if(Vector3.Dot((target.position - transform.position),transform.forward) < 0)
    //        {
    //            if(pos.x < Screen.width / 2)
    //            {
    //                pos.x = maxX;
    //            }
    //            else
    //            {
    //                pos.x = minX;
    //            }
    //        }

    //        pos.x = Mathf.Clamp(pos.x, minX, maxX);
    //        pos.y = Mathf.Clamp(pos.y, minY, maxY);

    //        img.transform.position = pos;
    //        yield return null;
    //    }
    //}
}
