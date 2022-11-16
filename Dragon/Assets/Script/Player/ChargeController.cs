using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem chargeEfect;         //パーティクルシステム取得

    [SerializeField]
    private SwordContoroller swordContoroller;  //スクリプト取得

    [SerializeField]
    private SkillController skillController;    //取得用

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chargeEfects();
    }

    //チャージ中のエフェクト再生
    private void chargeEfects()
    {     //剣を振っていない、                 左クリックを押している間、       ショックウェーブアイテムが足りている
        if(!swordContoroller.CoroutineBool && Input.GetMouseButtonDown(0) && skillController.Skills[4] > swordContoroller.onshockskill)
        {
            chargeEfect.Play();
        }
        // ショックウェーブが出せるまでチャージできたら    もしくは   左クリックを離したら
        if(swordContoroller.OnTime >= swordContoroller.MaxTime || Input.GetMouseButtonUp(0))
            {
                chargeEfect.Stop();
            }
    }
}
