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
    private GameObject player;

    private Vector3 offset;                 // playerとの距離感格納用
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        skillControl = player.GetComponent<SkillController>();
        offset = new Vector3(-5f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        look(); 
    }

    // 挙動
    private void move()
    {
        transform.position = player.transform.position + offset;
    }

    // bossの方を向きながらアイテムがある場合打つ
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