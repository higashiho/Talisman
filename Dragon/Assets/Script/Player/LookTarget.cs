using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [SerializeField]
    private BulletShot bulletShot;

    [SerializeField]
    private SpriteRenderer gun;             // 銃オブジェクト

    [SerializeField]
    private SkillController skillControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       look(); 
    }

    private void look()
    {
        if(skillControl.Skills[0] > 0 && bulletShot.Target != null)
        {
            gun.enabled = true;
            Vector3 diff = (bulletShot.Target.transform.position - this.transform.position).normalized;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
        }
        else
            gun.enabled = false;
    }
}