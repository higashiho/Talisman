using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFieldController : MonoBehaviour
{

    private float scaleUp = 0;       // だんだん大きくする用
    private float scaleZ = 1.0f;         // scaleのz値用

    private float maxScale = 3.0f;         // 最大拡大率

    private bool onScaleUp = true;         // スケールアップできるか

    private float destroyTime = 5.0f;        // 消えるまでの時間

    public bool Damage = false;         // ダメージを与えられるかどうか
    private int giveDamage = 1;         // ダメージの値
    private bool startDamage = false;        // ダメージ処理に入ったかどうか


    
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用

    private float waitTime = 2.0f;                  // 遅延時間

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
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
        gameObject.transform.localScale = new Vector3(scaleUp, scaleUp, scaleZ);

        scaleUp += Time.deltaTime;

        if(scaleUp >= maxScale)
        {
            onScaleUp = false;
            Destroy(this.gameObject,destroyTime);
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
