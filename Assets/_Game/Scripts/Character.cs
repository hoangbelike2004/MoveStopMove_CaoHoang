using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    
    [SerializeField] private Animator anim;
    [SerializeField] protected float speed, valueSize, valuesizetmp, rangeAttack = 7f;
    [SerializeField] protected int planeLayer,layerWeapon, barrier;
    [SerializeField] protected Transform _weaponFaketf,_hairTf,_khientf;
    [SerializeField] SkinnedMeshRenderer _pants;

    [SerializeField] protected Weapon weaponPrefab;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected CapsuleCollider capsuleCollider;

    //data
    [SerializeField] protected WeaponData1 weaponData1;
    [SerializeField] protected PantData pantData;
    [SerializeField] protected HatData hatData;
    [SerializeField] protected ShieldData shieldData;

    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected HatType hatType;
    [SerializeField] protected PantType pantType;
    [SerializeField] protected ShieldType shieldType;

    [SerializeField] protected HatItem hatItem;
    [SerializeField] protected PantItem pantItem;
    [SerializeField] protected ShieldItem shieldItem;
    [SerializeField] protected SuitdItem suitdItem;

    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected int score;
    public bool isAttack,isPlay,isSelect,isHatsNull,isShieldNull,isSuitNull;
    public Material savepantMaterial;
   
    public float time,timer;
    public delegate void UpSizeDelegate();
    public static UpSizeDelegate UpSizeEvent;
    public List<GameObject> hats,shields,suits;
    public Collider[] hitcollider;
    public List<GameObject> listgameObjectHitcollider;

    protected bool isDie, isSelectEnemy, isAugment;
    protected Vector3 CurrentPos;

    
    private string currentAnim;
    private bool[] isUpSize = new bool[5];


    protected virtual void Start()
    {
        OnInit();
        CheckScoreForUpSize(score);
    }
    //protected virtual void Move()
    //{

    //}
    public virtual void OnInit()
    {
        isDie = false;
        transform.localScale = Vector3.one;
       savepantMaterial = _pants.material;
        speed = 10f;
        valueSize = 0.1f;
        time = 0;
        isAttack = false;
        capsuleCollider.enabled = true;
        for(int i = 0; i < isUpSize.Length; i++)
        {
            isUpSize[i] = true;
        }
        
        
    }

    public virtual void OnDespawn(GameObject newobject)
    {
       
    }


    public virtual void ChangeAnim(string nameAnim)
    {
            anim.SetTrigger(nameAnim);
    }

    protected virtual void ChangeWeapon(WeaponType newwp)
    {
       
        weaponPrefab = weaponData1.GetWeapon(newwp);
        weaponType = newwp;
        GameObject _weaponFake = weaponData1.GetWeaponGOB(newwp);
        Destroy(_weaponFaketf.GetChild(0).gameObject);
        Instantiate(_weaponFake, _weaponFaketf);

        //_weaponFake.GetComponent<Weapon>().enabled = false;
    }

    protected virtual void ChangeHat(HatType newType)
    {
        hatType = newType;
        if(_hairTf.childCount != 0)
        {
            _hairTf.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            isHatsNull = true;
        }
        if (hatType != HatType.nonehat)
        {
            GameObject hat = Instantiate(hatData.GetHat(hatType), _hairTf);


            if (hats.Count >= 2)
            {
                Destroy(hats[hats.Count - 1].gameObject);
              //deactive thang sau thang duoc them vao la(hat)
            }

            hats.Add(hat);
            hat.transform.SetParent(_hairTf);
        }
        
    }
    protected void NotSelected()
    {
        _pants.material = savepantMaterial;//khi ko chon thi tra ve cai material ban dau
        pantType = PantType.nonePant;
        //SAVEN'T HAT
        if (isHatsNull)//nguoi choi ko co hat tren dau khi chua vao shop
        {
            hatType = HatType.nonehat;
            for (int i = 0; i < hats.Count; i++)
            {
                Destroy(hats[i].gameObject);
            }
            hats.Clear();
        }
        else if(!isHatsNull && hats.Count > 0)
        {
            
            hats[0].gameObject.SetActive(true);
            for (int i = hats.Count - 1; i > 0; i--)
            {
                Destroy(hats[i].gameObject);
                hats.RemoveAt(i);
            }
        }

        //SAVEN'T SHIELD
        if (isShieldNull)//nguoi choi ko co hat tren dau khi chua vao shop
        {
            shieldType = ShieldType.noneShield;
            for (int i = 0; i < shields.Count; i++)
            {
                Destroy(shields[i].gameObject);
            }
            shields.Clear();
        }
        else if (!isShieldNull && shields.Count > 0)
        {
            shields[0].gameObject.SetActive(true);
            for (int i = shields.Count - 1; i > 0; i--)
            {
                Destroy(shields[i].gameObject);
                shields.RemoveAt(i);
            }
        }
    }
    protected virtual void Selected
    (HatItem hatItem, PantItem pantItem, ShieldItem shielditem, SuitdItem suititem)
    {
        isSuitNull = false;
        if(pantItem != null)
        {
            savepantMaterial = _pants.material;
        }
        else
        {
            _pants.material = savepantMaterial;
        }
        
        //SAVE HAT
        if(hatItem != null)
        {
            for (int i = hats.Count - 2; i >= 0; i--)
            {
                Destroy(hats[i].gameObject);
                hats.RemoveAt(i);
            }
        }
        else
        {
            hats[0].gameObject.SetActive(true);
            for (int i = hats.Count - 1; i > 0; i--)
            {
                Destroy(hats[i].gameObject);
                hats.RemoveAt(i);
            }
        }



        //SAVE SHIELD
        if (shielditem != null)
        {
            for (int i = shields.Count - 2; i >= 0; i--)
            {
                Destroy(shields[i].gameObject);
                shields.RemoveAt(i);
            }
        }
        else
        {
            shields[0].gameObject.SetActive(true);
            for (int i = shields.Count - 1; i > 0; i--)
            {
                Destroy(shields[i].gameObject);
                shields.RemoveAt(i);
            }
        }
        isShieldNull = _khientf.childCount != 0 ? true : false;
        isHatsNull = _hairTf.childCount != 0 ? true : false;
    }
    protected virtual void ChangShield(ShieldType newType)
    {
        shieldType = newType;
        if (_khientf.childCount != 0)
        {
            _khientf.GetChild(0).gameObject.SetActive(false);
        }
        if(shieldType != ShieldType.noneShield)
        {
            GameObject khien = Instantiate(shieldData.GetShield(newType), _khientf);
            if (shields.Count >= 2)
            {
                Destroy(shields[shields.Count - 1].gameObject);//deactive thang sau thang duoc them vao la(Shield)
            }
            shields.Add(khien);
            khien.transform.SetParent(_khientf);
        }
        
    }
    protected virtual void ChangSuite(SuitType newType)
    {

    }

    protected virtual void ChangePant(PantType newType)
    {
        pantType = newType;
        if(savepantMaterial == null)
        {
            savepantMaterial = _pants.material;
        }
        if(pantType != PantType.nonePant)
        {
            _pants.material = pantData.GetPants(newType);
        }
        
    }

    protected virtual void AttackRang()
    {
        
    }

    protected virtual void UpSize()
    {
        transform.localScale = new Vector3(transform.localScale.x+valueSize,transform.localScale.y+valueSize,transform.localScale.z+valueSize);
        rangeAttack += valueSize*5f;
        valuesizetmp += valueSize;
        UpSizeEvent?.Invoke();//tang chieu cao cho floating text
        
        
    }
    public void SetScore(int score)// dung de tang score khi kill bot
    {
        this.score += score;
        _text.text = this.score.ToString();
        CheckScoreForUpSize(this.score);
    }
    public int GetScore()//dung de lay score de check nen tang bao diem khi giet bot
    {
        return score;
    }
    protected void CheckScoreForUpSize(int score)// dung de tang size khi dat moc cua score
    {
        
        if (score >= 2 && isUpSize[0])
        {
            isUpSize[0] = false;
            UpSize();
        }
        else if(score >= 6 && isUpSize[1])
        {
            isUpSize[1] = false;
            UpSize();
        }
        else if (score >= 14 && isUpSize[2])
        {
            isUpSize[2] = false;
            UpSize();
        }
        else if (score >= 30 && isUpSize[3])
        {
            isUpSize[3] = false;
            UpSize();
        }
        else if (score >= 64 && isUpSize[4])
        {
            isUpSize[4] = false;
            UpSize();
        }
    }

    public void AddScore()
    {
        if (score < 2)
        {
            SetScore(1);
        }
        else if (score < 6)
        {
            SetScore(2);
        }
        else if (score < 14)
        {
            SetScore(3);
        }
        else if (score < 30)
        {
            SetScore(4);
        }
        else if (score < 64)
        {
            SetScore(5);
        }
    } 
    
    public float GetRangeAttack()
    {
        return rangeAttack;
    }
    public float GetValueSize()
    {
        return valuesizetmp;
    }
    public bool GetIsDie()
    {
        return isDie;
    }

    public virtual void Attack()
    {

        CurrentPos = Vector3.zero;
        ChangeAnim(Contains.ATTACK);
        

    }
    public void DeActiveAttack()
    {
        isAttack = false;
        time = 0;
    }
    public void InstanWeapon()
    {
        Weapon bullet = SimplePool.Spawn<Weapon>((PoolType_One)weaponType, firePos.position, Quaternion.identity);
           // Instantiate(weaponPrefab, firePos.position, firePos.rotation);
        Weapon newWeapon = Cache.GetWeaponInCache(bullet);
        newWeapon.SetCharracterParent(this);
        newWeapon.SetTarget(CurrentPos);
        newWeapon.OnInit();
    }
   
    public virtual void Die()
    {
        CurrentPos = Vector3.zero;
        isDie = true;
        capsuleCollider.enabled = false;
        ChangeAnim(Contains.DIE);
        
    }
    protected virtual void Win()
    {
        ChangeAnim(Contains.DANCE_WIN);
    }
    public int Compare(Collider x, Collider y)//sap xep mang hitcollider theo distance uu tien tu gan den xa
    {
        float distanceX = Vector3.Distance(x.transform.position, transform.position);
        float distanceY = Vector3.Distance(y.transform.position, transform.position);

        if (distanceX < distanceY)
        {
            return -1;
        }
        else if (distanceX > distanceY)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    protected virtual void CheckSight()
    {
        
        hitcollider = Physics.OverlapSphere(transform.position, rangeAttack);
        int value = 0;

        Array.Sort(hitcollider, Compare);
        if(hitcollider.Length != 0)
        {     
            for (int i = 0; i < hitcollider.Length; i++)
            {
                if (hitcollider[i].gameObject.layer != planeLayer&& hitcollider[i].gameObject.layer != barrier
                    && hitcollider[i].gameObject != this.gameObject && hitcollider[i].gameObject.layer != layerWeapon)
                {
                    Character newChar = Cache.GetCharacteOfColliderInCache(hitcollider[i]);
                    CurrentPos = newChar.transform.position;
                    value = i;
                    break;
                }
            }
            if(value != 0 && gameObject.tag != "Bot")//dung de bat target cua bot
            {
                if (hitcollider.Contains(hitcollider[value]))//khi van ton tai collider cua bot tong array
                {
                    hitcollider[value].GetComponent<Bot>().IsSelect(true);
                    
                }
                if(Vector3.Distance(transform.position, hitcollider[value].transform.position) > rangeAttack)
                {
                    hitcollider[value].GetComponent<Bot>().IsSelect(false);
                    CurrentPos = Vector3.zero;
                }
                
            }
            
        }
    }

    protected virtual void PlayGame()
    {
        
        isPlay = true;
        isDie = false;
        
    }
    public virtual void NotPlayGame()
    {
        isPlay = false;
        Debug.Log(1);
        CurrentPos = Vector3.zero;

    }
    protected virtual void OnEnable()
    {
        //Weapon.ActionAddScore += AddScore;
        CanvasGamePlay.actionPlayGame += PlayGame;
        GameController.OnInitAllAction += NotPlayGame;
        
    }
    protected virtual void OnDisable()
    {
        //Weapon.ActionAddScore -= AddScore;
        CanvasGamePlay.actionPlayGame -= PlayGame;
        GameController.OnInitAllAction -= NotPlayGame;

    }


}
