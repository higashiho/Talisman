using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCompleteController : MonoBehaviour
{
    [SerializeField]
    private SwordContoroller swordContoroller;      //参照

    [SerializeField]
    private ParticleSystem chargeCompleteEfect;           //取得

    private GameObject player;                       //Player取得

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChargeCompleteEfect();
    }

    private void ChargeCompleteEfect()
    {
        this.transform.position = player.transform.position;
        if(swordContoroller.OnTime >= swordContoroller.MaxTime)
        {
            chargeCompleteEfect.Play();
        }

        if(swordContoroller.OnTime >= swordContoroller.MaxTime && Input.GetMouseButtonUp(0))
        {
            chargeCompleteEfect.Stop();
        }
    }
}
