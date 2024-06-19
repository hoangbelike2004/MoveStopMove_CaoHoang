using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Transform lookat;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float valueUpSize = 0.4f;

    [SerializeField] Camera cam;
    private void LateUpdate()
    {
            cam = Camera.main;
            Vector3 newpos = cam.WorldToScreenPoint(lookat.position + Offset);
            if(transform.position != newpos)
                transform.position = newpos;
        if (lookat.GetComponent<Character>().GetIsDie())
        {
            gameObject.SetActive(false);
        }
    }
    void UpOffsetFloatingText()
    {
        Offset.y += valueUpSize;

    }
    private void OnEnable()
    {
        Character.UpSizeEvent += UpOffsetFloatingText;
    }

    private void OnDisable()
    {
        Character.UpSizeEvent -= UpOffsetFloatingText;
    }
}
    