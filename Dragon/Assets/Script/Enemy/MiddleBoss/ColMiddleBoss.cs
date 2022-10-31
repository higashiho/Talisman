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
    [SerializeField]
    private GameObject MiddleBoss;
    private GameObject MiddleBossCreater;
    private CreateRandom createrandom;
    private MoveMiddleBoss movemiddleboss;

    void Start()
    {
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");
        createrandom = MiddleBossCreater.GetComponent<CreateRandom>();
        movemiddleboss = MiddleBoss.GetComponent<MoveMiddleBoss>();
    }
    void Update()
    {
        if(_hp <= 0)
        {
            Destroy(this.gameObject);
            createrandom._time = 0;
            createrandom._Counter--;
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
        if(movemiddleboss.Marge_OK)
        {
            Debug.Log(movemiddleboss.Marge_OK);
            if(other.gameObject.tag == "Boss")
            {
                // なんか融合させるためのフラグとか???
                Destroy(this.gameObject);
            }
        }
    }
    
}
