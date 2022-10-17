using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Enemy : MonoBehaviour
{
    enum ShotType
    {
        NONE = 0,
        AIM,            // 自機狙い
        THREE_WAY,      // ３方向
    }

    [System.Serializable]
    struct ShotData
    {
        public int frame;            // 発射間隔
        public ShotType type;       //発射モード
        public Bullet_Enemy bullet; 
    }

    // ショットデータ
    [SerializeField] ShotData shotData = new ShotData { frame = 60, type = ShotType.NONE, bullet = null };

    GameObject playerObj = null;    // プレイヤー
    int shotFrame = 0;              // フレーム

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを取得
        switch (shotData.type)
        {
            case ShotType.AIM:
                playerObj = GameObject.Find("Player");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }
    // 弾を発射する
    void Shot()
    {
        ++shotFrame;
        if (shotFrame > shotData.frame)
        {
            switch (shotData.type)
            {
                // プレイヤーを狙う
                case ShotType.AIM:
                    {
                        if (playerObj == null) { break; }
                        Bullet_Enemy bullet = (Bullet_Enemy)Instantiate(
                            shotData.bullet,
                            transform.position,
                            Quaternion.identity
                        );
                        bullet.SetMoveVec(playerObj.transform.position - transform.position);
                    }
                    break;

                // ３方向
                case ShotType.THREE_WAY:
                    {
                        Bullet_Enemy bullet = (Bullet_Enemy)Instantiate(
                            shotData.bullet,
                            transform.position,
                            Quaternion.identity
                        );
                        bullet = (Bullet_Enemy)Instantiate(shotData.bullet, transform.position, Quaternion.identity);
                        bullet.SetMoveVec(Quaternion.AngleAxis(15, new Vector3(0, 0, 1)) * new Vector3(-1, 0, 0));
                        bullet = (Bullet_Enemy)Instantiate(shotData.bullet, transform.position, Quaternion.identity);
                        bullet.SetMoveVec(Quaternion.AngleAxis(-15, new Vector3(0, 0, 1)) * new Vector3(-1, 0, 0));
                    }
                    break;
            }

            shotFrame = 0;
        }
    }

    //プレイヤーに当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
    if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
