using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Camera _camera;
    IEnumerator Start()
    {
        _camera = Camera.main;
        while (true)
        {

            StartCoroutine(FlowPlayer());
            yield return null;
        }
       
    }
    IEnumerator FlowPlayer()
    {
        transform.position = new Vector3(_playerTf.transform.position.x,15,_playerTf.transform.position.z-15);
        yield return null;
    }
}
