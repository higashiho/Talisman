using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFieldController : BaseSkills
{

    private float scaleUp = 0;       // だんだん大きくする用
    private float scaleZ = 1.0f;         // scaleのz値用

    private float maxScale = 18.0f;         // 最大拡大率

    private bool onScaleUp = true;         // スケールアップできるか

    public bool Damage = false;         // ダメージを与えられるかどうか
    private int giveDamage = 1;         // ダメージの値
    private bool startDamage = false;        // ダメージ処理に入ったかどうか


    
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用


    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();

        // objectPool取得
        objectPool = Factory.ObjectPool;
    }
    private void OnEnable()
    {
        onScaleUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(onScaleUp)
            sizeUp();
            
        if(Damage && !startDamage)
            StartCoroutine(onDamage());
            
    }

    //挙動
    private void sizeUp()
    {
        float m_sizeUpSpeed = 5.0f;
        gameObject.transform.localScale = new Vector3(scaleUp, scaleUp, scaleZ);


        scaleUp += m_sizeUpSpeed * Time.deltaTime;
        if(scaleUp >= maxScale)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0)
            {
                waitTime = Const.START_WAIT_TIME;
                scaleUp = default;
                onScaleUp = false;
                this.gameObject.transform.parent = objectPool.gameObject.transform;
                objectPoolCallBack?.Invoke(objectPool.BossSkillsQueue, this);
            }
        }
    }

    private IEnumerator onDamage()
    {
        startDamage = true;
        playerController.Hp -= giveDamage;
        yield return new WaitForSeconds(waitTime) ;
        startDamage = false;
    }
}
