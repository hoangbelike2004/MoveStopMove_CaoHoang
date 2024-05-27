using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Camera _camera;
    float valuesize;
    IEnumerator Start()
    {
        _camera = Camera.main;
        while (true)
        {
            valuesize = _playerTf.GetComponent<Character>().GetValueSize() * 12;//tang size cua camera
            StartCoroutine(FlowPlayer());
            yield return null;
        }

    }
    IEnumerator FlowPlayer()
    {
        Quaternion newrotation = Quaternion.Euler(40, 0, 0);
        Vector3 tf = new Vector3(_playerTf.transform.position.x,12+ valuesize, _playerTf.transform.position.z-12- valuesize);
        transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, 10 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position,tf,10*Time.deltaTime);
        yield return null;
    }
}
