using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
      public Material material;
    // Start is called before the first frame update
    private void Start()
    {
       // material = GetComponent<Material>();
    }
    public void ChangeObjectColor(float alpha)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    }
}
