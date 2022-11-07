using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ColMiddleBoss : MonoBehaviour
{
    [HeaderAttribute("中ボスヒットポイント"), SerializeField]
    private int _hp = 5;
    [HeaderAttribute("Swordのダメージ"), SerializeField]
    private int SWORD_DAMAGE = 1;
    [HeaderAttribute("RotateSwordのダメージ"), SerializeField]
    private int ROTATESWORD_DAMAGE = 2;
    [HeaderAttribute("アイテム"), SerializeField]
    private GameObject item;
    [SerializeField]
    private GameObject _Boss;
    [SerializeField]
    private BossController bosscontroller;  //スクリプトアタッチ用
    [SerializeField]
    private GameObject MiddleBoss;

    private GameObject bullet;  // プレイヤーが放つホーミング弾
    private GameObject MiddleBossCreater;

    // 以下スクリプト参照用
    private CreateRandom createrandom;
    private MoveMiddleBoss movemiddleboss;
    private BulletController bulletcontroller;
    
    private GameObject player;

    void Start()
    {
        _Boss = GameObject.FindWithTag("Boss");
        bosscontroller = _Boss.GetComponent<BossController>();
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");
        createrandom = MiddleBossCreater.GetComponent<CreateRandom>();
        movemiddleboss = MiddleBoss.GetComponent<MoveMiddleBoss>();

        player = GameObject.FindWithTag("Player");
        bulletcontroller = player.GetComponent<BulletController>();
    }
    void Update()
    {
        if(_hp <= 0)
        {
            Instantiate(item, this.transform.position, Quaternion.identity);
            createrandom._time = 0;
            createrandom._Counter--;
            Destroy(this.gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            _hp -= SWORD_DAMAGE;
        }
        if(other.gameObject.name == "RotateSword")
        {
            _hp -= ROTATESWORD_DAMAGE;
        }
        if(other.gameObject.tag == "Bullet")
        {
            _hp -= bulletcontroller.Attack; 
        }
        if(movemiddleboss.Marge_OK)
        {
            Debug.Log(movemiddleboss.Marge_OK);
            if(other.gameObject.tag == "Boss")
            {
                // なんか融合させるためのフラグとか???
                bosscontroller.Hp += _hp;   // 中ボスの残りHPをボスのHPに加算
                createrandom._Counter--;
                Destroy(this.gameObject);
            }
        }
    }
    
}
