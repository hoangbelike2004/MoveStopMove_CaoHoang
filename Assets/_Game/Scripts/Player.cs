using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [SerializeField] private VariableJoystick _variableJoyStick;
    bool isMove;
    public static UnityAction LoseEvent;
    private Vector3 moveDirection,startpos;
    public Quaternion currentRotation;
    [SerializeField] Canvas _canvasRangeAttack;
    private void Awake()
    {
        startpos = transform.position;
        currentRotation = transform.rotation;
        hatType = HatType.nonehat;
        shieldType = ShieldType.noneShield;
        pantType = PantType.nonePant;
        if(DataManager.Instance.GetDataPlayer() != null)
        {
            DataPlayer dtplayer = DataManager.Instance.GetDataPlayer();
            ChangeHat(dtplayer.hatType);
            ChangePant(dtplayer.pantTypePlayer);
            ChangShield(dtplayer.shieldType);
            ChangeWeapon(dtplayer.weaponType);
            GameController.Instance.SetScoreSaved(dtplayer.score);
        }
    }
    void Update()
    {
        if (!isDie&&isPlay)
        {
            
            ChangeState();



            CheckSight();
            if (time >= timer)
            {
                isAttack = true;

            }
            time += Time.deltaTime;
        }
        

        //Debug.Log(enemyCurrentPos);
    }
    private void MovePlayer()
    {
        
        
        transform.position += moveDirection * speed * Time.deltaTime;
        
    }
    private void ChangeState()
    {
        if (_variableJoyStick.Horizontal != 0 || _variableJoyStick.Vertical != 0)
        {
            moveDirection = new Vector3(_variableJoyStick.Horizontal, 0, _variableJoyStick.Vertical);
            moveDirection.Normalize();
            transform.forward = moveDirection;
            if (CheckGround())
            {
                MovePlayer();
            }
            
            ChangeAnim( Contains.RUN);
            time = timer;
            //Debug.Log(1);
            _weaponFaketf.gameObject.SetActive(true);

        }
        else if (_variableJoyStick.Horizontal == 0 && _variableJoyStick.Vertical == 0)
        {
            if (!isAttack ||CurrentPos == Vector3.zero)
            {
                ChangeAnim(Contains.IDLE);
                _weaponFaketf.gameObject.SetActive(true);
                time += Time.deltaTime;
            }

            if (CurrentPos != Vector3.zero && isAttack&& time >= timer)
            {


                if (hitcollider.Length != 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(CurrentPos - transform.position);
                    targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);//cho xoay moi truc y ve phia enemy
                                                                                          // Xoay player về phía quái
                    transform.rotation = targetRotation;

                }

                Attack();
            }
        }

    }
    public override void OnInit()
    {
        
        base.OnInit();
        _weaponFaketf.gameObject.SetActive(true);
        transform.position = startpos;
        transform.rotation = currentRotation;
        ChangeAnim(Contains.IDLE);
        score = 0;
        _text.text = score.ToString();
    }
    public override void Die()
    {
        base.Die();
        LoseEvent.Invoke();
        AudioManager.Instance.PlaySound(AudioManager.Instance.acLose, 1);

    }
    private bool CheckGround()
    {
        if(!Physics.Raycast(transform.position + Vector3.up * 2f, transform.forward * .5f + Vector3.down * 2f,planeLayer))
        {
            return false;
        }
        else if (Physics.Raycast(transform.position + Vector3.up * 2f,transform.forward*0.7f+Vector3.up*2f, barrier))
        {
            return false;
        }
        else 
        {
            return true;
        }
        
    }
    void ActiveCanvasRangeAttack()
    {
        _canvasRangeAttack.gameObject.SetActive(true);
    }
    void DeActiveCanvasRangeAttack()
    {
        _canvasRangeAttack.gameObject.SetActive(false);
    }
    void ChangeAnimToDANCE()
    {
        ChangeAnim( Contains.DANCE);
    }
    void ChangeAnimWhenExitSKinToIDLE()
    {
        ChangeAnim(Contains.IDLE);
    }
    void Checkdressing()
    {
        if (_khientf.childCount != 0)
        {
            isShieldNull = false;
        }
        else
        {
            isShieldNull = true;
        }

        if (_hairTf.childCount != 0)
        {
            isHatsNull = false;
        }
        else
        {
            isHatsNull = true;
        }

    }

    public WeaponType GetWeaponTypePlayer()
    {
        return weaponType;
    }
    
    public HatType GetHatTypePlayer()
    {

    return hatType; 
    }

    public PantType GetPantTypePlayer()
    {
        return pantType;
    }

    public ShieldType GetShieldTypePlayer()
    {
        return shieldType;
    }
    protected override void Selected(HatItem hatitem,PantItem pantItem,ShieldItem shielditem,SuitdItem suititem)
    {
        base.Selected(hatitem, pantItem, shielditem, suititem);
       
    }
    //void SaveUserDataWhenQuitGame()
    //{
    //    DataManager.Instance.SaveDataPlayer(hatType, pantType, shieldType, weaponType, GameController.Instance.GetScore());
    //}
    protected override void OnEnable()
    {
        base.OnEnable();
        CanvasBuyWeapon.selectWeaponAction += ChangeWeapon;
        CanvasGamePlay.actionPlayGame += ActiveCanvasRangeAttack;
        CanvasGamePlay.actionChangeSkinCameraFlow += ChangeAnimToDANCE;
        CanvasBuySkin.actionChangeExitSkinCameraFlow += ChangeAnimWhenExitSKinToIDLE;

        //action change SKin
        ItemUIHat.ChangeHatAction += ChangeHat;
        ItemUIPant.ChangePantAction += ChangePant;
        ItemUIShield.ChangeShieldAction += ChangShield;
        CanvasBuySkin.actionSelectHatStart += ChangeHat;
        CanvasBuySkin.actionSelectPantStart += ChangePant;
        CanvasBuySkin.actionSelectShieldStart += ChangShield;


        GameController.OnInitAllAction += OnInit;
        GameController.OnInitAllAction += DeActiveCanvasRangeAttack;
        //GameController.QuitGameEvent += SaveUserDataWhenQuitGame;

        //action xem skin co duoc select hay không
        CanvasBuySkin.actionSelectSkin += Selected;
        CanvasBuySkin.actionNotSelectSkin += NotSelected;
        CanvasGamePlay.actionChangeSkinCameraFlow += Checkdressing;//ktra nguoi choi co dang mac do khi vao shop khong
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        CanvasGamePlay.actionPlayGame -= ActiveCanvasRangeAttack;
        CanvasBuyWeapon.selectWeaponAction -= ChangeWeapon;
        CanvasGamePlay.actionChangeSkinCameraFlow -= ChangeAnimToDANCE;
        CanvasBuySkin.actionChangeExitSkinCameraFlow -= ChangeAnimWhenExitSKinToIDLE; 

        GameController.OnInitAllAction -= OnInit;
        GameController.OnInitAllAction -= DeActiveCanvasRangeAttack;
        //GameController.QuitGameEvent -= SaveUserDataWhenQuitGame;

        ItemUIHat.ChangeHatAction -= ChangeHat;
        ItemUIPant.ChangePantAction -= ChangePant;
        ItemUIShield.ChangeShieldAction -= ChangShield;
        CanvasBuySkin.actionSelectHatStart -= ChangeHat;
        CanvasBuySkin.actionSelectPantStart -= ChangePant;
        CanvasBuySkin.actionSelectShieldStart -= ChangShield;
        CanvasGamePlay.actionChangeSkinCameraFlow -= Checkdressing;

        CanvasBuySkin.actionSelectSkin -= Selected;
        CanvasBuySkin.actionNotSelectSkin -= NotSelected;
    }


}

