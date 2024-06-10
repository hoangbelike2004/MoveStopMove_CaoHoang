using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasGamePlay : UiCanvas
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _weaponButton;
    [SerializeField] Button _skinButton;
    [SerializeField] Button buttonActiveSound;
    [SerializeField] Button buttonDeactiveSound;
    [SerializeField] Button buttonActiveVibrate;
    [SerializeField] Button buttonDeactiveVibrate;
    [SerializeField] GameObject adsobj,soundobj,vibrateobj,scoreobj;
    [SerializeField] TextMeshProUGUI _scoreOutSlide;


    public static UnityAction actionPlayGame;
    public static UnityAction actionChangeSkinCameraFlow;
    
    private void Start()
    {
        _scoreOutSlide.text = GameController.Instance.GetScore().ToString();
        _playButton.onClick.AddListener(PlayGame);
        _weaponButton.onClick.AddListener(ChangeWeaponButton);
        _skinButton.onClick.AddListener(ChangeSkinButton);
        buttonActiveSound.onClick.AddListener(Sound);
        buttonDeactiveSound.onClick.AddListener(Sound);
        buttonActiveVibrate.onClick.AddListener(Vibrate);
        buttonDeactiveVibrate.onClick.AddListener(Vibrate);
    }

    private void PlayGame()
    {
        
        UiManager.Instance.CloseUI<CanvasGamePlay>(.3f);
        UiManager.Instance.OpenUI<CanvasSetting>();
        actionPlayGame?.Invoke();
    }
    
    void Effect()
    {

    }
    private void Vibrate()
    {
        if (buttonActiveVibrate.gameObject.activeSelf == true)
        {
            buttonActiveVibrate.gameObject.SetActive(false);
            buttonDeactiveVibrate.gameObject.SetActive(true);
        }
        else if (buttonDeactiveVibrate.gameObject.activeSelf == true)
        {
            buttonDeactiveVibrate.gameObject.SetActive(false);
            buttonActiveVibrate.gameObject.SetActive(true);
        }
    }
    private void Sound()
    {

        if (buttonActiveSound.gameObject.activeSelf == true)
        {
            buttonActiveSound.gameObject.SetActive(false);
            buttonDeactiveSound.gameObject.SetActive(true);

        }
        else if (buttonDeactiveSound.gameObject.activeSelf == true)
        {

            buttonDeactiveSound.gameObject.SetActive(false);
            buttonActiveSound.gameObject.SetActive(true);
        }
    }
    private void ChangeWeaponButton()
    {
        UiManager.Instance.OpenUI<CanvasBuyWeapon>();
    }

    private void ChangeSkinButton()
    {
        UiManager.Instance.OpenUI<CanvasBuySkin>();
        actionChangeSkinCameraFlow.Invoke();//dung chung action de kiem tra xem nguoi choi cs dang m?c gi khi vao shop skin khong 
        //action nay con de khi mo sho len thi se focus vao cai item mu dau tien
        //va khi an vao shop skin thi camera se thay doi
        UiManager.Instance.CloseUI<CanvasGamePlay>(0f);
    }
    void ScoreText(int score)
    {
        _scoreOutSlide.text = score.ToString();
    }
    private void OnEnable()
    {
        GameController.updateTextScoreAction += ScoreText;
    }

    private void OnDisable()
    {
        GameController.updateTextScoreAction -= ScoreText;
    }
}
