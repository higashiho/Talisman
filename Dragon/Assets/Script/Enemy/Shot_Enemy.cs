using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Enemy : MonoBehaviour
{
    enum ShotType
    {
        NONE = 0,
        AIM,            // プレイヤーを狙う
        THREE_WAY,      // ３方向
    }

    [System.Serializable]
    struct ShotData
    {
        public int frame;
        public ShotType type;
        public Bullet_Enemy bullet;
    }

    // ショットデータ
    [SerializeField] ShotData shotData = new ShotData { frame = 60, type = ShotType.NONE, bullet = null };

    GameObject playerObj = null;    // プレイヤーオブジェクト
    int shotFrame = 0;              // フレーム

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーオブジェクトを取得する
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
    // ショット処理（これをUpdateなどで呼ぶ）
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
}
