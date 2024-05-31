using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Camera _camera;
    [SerializeField] private float timeStartFlo = 40f,timerotationFlow = 15f;
    bool _isPlaying = false;
    float valuesize;
    IEnumerator Start()
    {
        _camera = Camera.main;

        while (true)
        {
            if (_isPlaying)
            {
                
                valuesize = _playerTf.GetComponent<Character>().GetValueSize() * 12;//tang size cua camera
                StartCoroutine(FlowPlayer());

            }
            
            yield return null;
        }

    }
   
    IEnumerator FlowPlayer()
    {
        Quaternion newrotation = Quaternion.Euler(40, 0, 0);
        Vector3 tf = new Vector3(_playerTf.transform.position.x,15+ valuesize, _playerTf.transform.position.z-15- valuesize);
        transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, timerotationFlow * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position,tf,timeStartFlo*Time.deltaTime);
        yield return null;
    }
    void RunCoroutine()
    {
        _isPlaying = true;
    }
    private void OnEnable()
    {
        CanvasGamePlay.actionPlayGame += RunCoroutine;
    }
    private void OnDisable()
    {
        CanvasGamePlay.actionPlayGame -= RunCoroutine;
    }
}
