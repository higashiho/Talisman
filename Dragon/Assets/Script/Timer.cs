using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer;            // タイマー
    [SerializeField]
    private Text timerText = default;       //タイマーテキスト
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        timerText.text = "" + timer.ToString("f2");
    }
}
