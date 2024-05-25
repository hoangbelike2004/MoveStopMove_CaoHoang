using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Camera _camera;
    float valuesize,valuesizetmp;
    IEnumerator Start()
    {
        _camera = Camera.main;
        while (true)
        {
            valuesize = _playerTf.GetComponent<Character>().GetValueSize() * 10;
            if(valuesizetmp <= valuesize)
            {
                
                valuesizetmp += Time.deltaTime;
                
            }
            StartCoroutine(FlowPlayer());
            yield return null;
        }
       
    }
    IEnumerator FlowPlayer()
    {
        transform.position = new Vector3(_playerTf.transform.position.x,15+ valuesizetmp, _playerTf.transform.position.z-15- valuesizetmp);
        yield return null;
    }
}
