using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColSword : MonoBehaviour
{
    [SerializeField]
    private SwordContoroller swordContoroller;

    [SerializeField]
    private GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
       //sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void attack()
    {
        if(swordContoroller.CoroutineBool)
        {
            sword.SetActive(true);
        }
        
    }
}
