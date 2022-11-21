using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float _Timer;            // タイマー
    [SerializeField]
    private Text timerText = default;       //タイマーテキスト

    [SerializeField]
    private FadeController fadeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!fadeController.IsFadeIn)
        {
            _Timer += Time.deltaTime;

            timerText.text = "" + _Timer.ToString("f2");
        }
    }
}
