using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField,HeaderAttribute("遅くする倍率")]
    private float timeScale = 0.5f;


    [SerializeField,HeaderAttribute("遅くなる時間")]
    private float slowTime = 0.5f;                      // 遅くする時間

    private float elapsedTime = 0;                      // 経過時間

    [SerializeField]
    private bool onSlowDown = false;                    // ヒットストップしているかどうか
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if(elapsedTime >= slowTime)
                SetNomalTime();
        }

    }

    // 時間を遅らせる処理
    public void SlowDown()
    {
        if(!onSlowDown)
        {
            elapsedTime = 0;
            Time.timeScale = timeScale;
            onSlowDown = true;
        }
    }

    // 時間をもとに戻す処理
    private void SetNomalTime()
    {
        Time.timeScale = Const.NOMAL_TIME;
        onSlowDown = false;
    }
}
