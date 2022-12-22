using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : BaseSkills
{
    [SerializeField]
    private float speed;                                    // 挙動スピード

    public void SetObjectPool(Factory obj) {objectPool = obj;}

    // チャージ時間が足りてるか
    [SerializeField, HeaderAttribute("拡大するか")]
    private bool onSizeUp = false;
    public void SetOnSizeUp(bool b) {onSizeUp = b; }
    public bool GetOnSizeUp() {return onSizeUp;}
    
    private Vector3 scaleChange = new Vector3(20.0f, 5.0f, 0);     // 大きくなる速さ

    // Start is called before the first frame update
    void Start()
    {
        objectPool = Factory.ObjectPool;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();

        if(onSizeUp)
            sizeUp();
    }

    private void move()
    {
        this.gameObject.transform.parent = objectPool.gameObject.transform;
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
        gameObject.transform.position += velocity * Time.deltaTime;
    }

    private void sizeUp()
    {
        if(this.transform.localScale.x <= Const.MAX_SCALE_X)
            this.transform.localScale += scaleChange * Time.deltaTime;
    }
    
    // 画面外に出たらオブジェクト非表示
    private void OnBecameInvisible()
    {
        if(objectPool != null)
        {
            onSizeUp = false;
            float m_sizeX = 4.0f, m_sizeY = 1.0f, m_sizeZ = 1.0f;
            this.transform.localScale = new Vector3(m_sizeX, m_sizeY, m_sizeZ);
            objectPoolCallBack?.Invoke(objectPool.GetShockWaveQueue(),this);
        }
    }

    
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

}
