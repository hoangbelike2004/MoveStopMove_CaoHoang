using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Camera _camera;
    [SerializeField] private float timeStartFlo = 40f,timerotationFlow = 15f;
    Vector3 tfstart;
    Quaternion rotationStart;
    bool _isPlaying = false,_isChangeSkin;
    float valuesize;
    IEnumerator Start()
    {
        OnInit();
        rotationStart = transform.rotation;

        while (true)
        {
            if (_isChangeSkin)//khi an vao shop skin
            {
                StartCoroutine(FlowplayerWhenClickChangeSkin());
            }
            else if (!_isChangeSkin && !_isPlaying)//khi dang o canvas gameplay
            {
                StartCoroutine(FlowPlayerWhenClickExitChangeSkin());
            }

            if (_isPlaying)//khi bat dau choi
            {
                
                valuesize = _playerTf.GetComponent<Player>().GetValueSize() * 12;//tang size cua camera
                StartCoroutine(FlowPlayer());

            }
            
            yield return null;
        }

    }
   void OnInit()
    {
        _camera = Camera.main;
        tfstart = transform.position;
        _isPlaying = false;
        _isChangeSkin = false;
    }
    IEnumerator FlowPlayer()
    {
        Quaternion newrotation = Quaternion.Euler(25, 0, 0);
        Vector3 tf = new Vector3(_playerTf.transform.position.x,12+ valuesize, _playerTf.transform.position.z-17.5f- valuesize);
        transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, timerotationFlow * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position,tf,timeStartFlo*Time.deltaTime);
        yield return null;
    }

    IEnumerator FlowplayerWhenClickChangeSkin()
    {
        Quaternion newrotation = Quaternion.Euler(20, 0, 0);
        Vector3 tf = new Vector3(0,2,-17.5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, timerotationFlow * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, tf, 30 * Time.deltaTime);
        yield return null;
    }
    IEnumerator FlowPlayerWhenClickExitChangeSkin()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationStart, timerotationFlow * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, tfstart, 30 * Time.deltaTime);
        yield return null;
    }
    void RunWhenClickPlay()
    {
        _isPlaying = true;
    }
    void ChangeSkin()
    {
        _isChangeSkin = true;
    }

    void ExitChangeSkin()
    {
        _isChangeSkin = false;
    }
    private void OnEnable()
    {
        CanvasGamePlay.actionPlayGame += RunWhenClickPlay;
        CanvasGamePlay.actionChangeSkinCameraFlow += ChangeSkin;
        CanvasBuySkin.actionChangeExitSkinCameraFlow += ExitChangeSkin;
    }
    private void OnDisable()
    {
        CanvasGamePlay.actionPlayGame -= RunWhenClickPlay;
        CanvasGamePlay.actionChangeSkinCameraFlow -= ChangeSkin;
        CanvasBuySkin.actionChangeExitSkinCameraFlow -= ExitChangeSkin;
    }
}
