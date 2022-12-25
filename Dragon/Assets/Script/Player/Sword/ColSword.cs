using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColSword : SwordContoroller
{
    [SerializeField]
    private GameObject sword;

    private void attack()
    {
        if(CoroutineBool)
        {
            sword.SetActive(true);
        }
        
    }
}
