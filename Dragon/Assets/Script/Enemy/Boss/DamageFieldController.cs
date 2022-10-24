using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFieldController : MonoBehaviour
{

    private float scaleUp = 0;       // だんだん大きくする用
    private float scaleZ = 1.0f;         // scaleのz値用

    private float maxScale = 3.0f;         // 最大拡大率

    private bool onScaleUp = true;         // スケールアップできるか

    private float destroyTime = 5.0f;        // 消えるまでの時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onScaleUp)
            sizeUp();
            
            
    }

    //挙動
    private void sizeUp()
    {
        gameObject.transform.localScale = new Vector3(scaleUp, scaleUp, scaleZ);

        scaleUp += Time.deltaTime;

        if(scaleUp >= maxScale)
        {
            onScaleUp = false;
            Destroy(this.gameObject,destroyTime);
        }
    }
}
