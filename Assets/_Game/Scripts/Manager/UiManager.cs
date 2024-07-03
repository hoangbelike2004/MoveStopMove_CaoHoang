using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    Dictionary<System.Type,UiCanvas> canvasActives = new Dictionary<System.Type,UiCanvas>();//dung de luu uicanvas
    Dictionary<System.Type,UiCanvas> canvasPrefabs = new Dictionary<System.Type,UiCanvas>();//noi chua prefab
    [SerializeField] Transform parent;
    [SerializeField] CanvasGroup canvasGroupIndicator;
    private void Awake()
    {
        //Load UI tu resource
        UiCanvas[] canvas = Resources.LoadAll<UiCanvas>("UI/");
        for(int i = 0; i < canvas.Length; i++)
        {
            canvasPrefabs.Add(canvas[i].GetType(), canvas[i]);
        }
    }
    private void Start()
    {
        UiManager.Instance.OpenUI<CanvasGamePlay>();
    }

    //mo canvas
    public T OpenUI<T>() where T : UiCanvas
    {
        T canvas = GetUI<T>();
        canvas.SetUp();

        canvas.Open();
        return canvas;
    }

    //dong canvas sau time
    public void CloseUI<T>(float time) where T : UiCanvas
    {
        if (IsLoaded<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }


    //dong sau 0s
    public void CloseUIDirectly<T>() where T : UiCanvas
    {
        if (IsLoaded<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
    }


    //kiem tra xem UI da duoc create hay ch
    public bool IsLoaded<T>() where T : UiCanvas
    {
        //tra ve neu ton tai canvas cos kieu la T va no khac null
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }

    //kiem tra xem ui da duoc active hay chua
    public bool IsOpen<T>() where T : UiCanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }


    // lay UI
    public T GetUI<T>() where T : UiCanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab,parent);
            canvasActives[typeof(T)] = canvas;
        }

        return canvasActives[typeof(T)] as T;
    }

    private T GetUIPrefab<T>() where T : UiCanvas
    {
        
            return canvasPrefabs[typeof(T)] as T;
        
    }
    // dong tat ca cac ui
    public void CloseAll()
    {
        foreach(var canvas in canvasActives)
        {
            if (canvas.Value != null && canvas.Value.gameObject.activeSelf == true)
            {
                canvas.Value.Close(0);
            }
        }
    }

    public void SetAlphaIndicator(bool isPlay)
    {
        if (isPlay)
        {
            canvasGroupIndicator.alpha = 1;
        }
        else
        {
            canvasGroupIndicator.alpha = 0;
        }
    }
}
