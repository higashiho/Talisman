using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    // 中心点
    [SerializeField] private Vector3 center = Vector3.zero;

    // 回転軸
    [SerializeField] private Vector3 axis = Vector3.up;

    // 円運動周期
    [SerializeField] private float period = 2;

    [SerializeField, HeaderAttribute("旋回するターゲット")]
    private GameObject player;

    
    [SerializeField]
    private FadeController fadeController;
    // Start is called before the first frame update
    void Start()
    {
        fadeController = GameObject.Find("FadeImage").GetComponent<FadeController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!fadeController.IsFadeIn)
        rotateShield();
    }

    private void rotateShield()
    {
        center = player.transform.position;
        // 中心点centerの周りを、軸axisで、period周期で円運動
        transform.RotateAround(
            center,
            axis,
            360 / period * Time.deltaTime
        );
    }
}
