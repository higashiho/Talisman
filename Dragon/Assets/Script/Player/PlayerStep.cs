using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{

    private Rigidbody2D rd2D;

    [SerializeField, HeaderAttribute("ステップできるか")]
    private bool[] onSteps = new bool[4];        // ステップできるか
    
    [SerializeField, HeaderAttribute("押す間隔")]
    private float[] onTimer = new float[4];     // ダブルクリックの間隔
    
    private float maxTimer = 0.3f;              // 最大待ち時間

    private bool nowStep = false;               // ステップ最中
    private float stepTimer = 0.1f;              // ステップ時間
    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
        bool[] onSteps = {false, false, false, false};
        float[] onTimer = {0.0f, 0.0f, 0.0f, 0.0f};
    }

    // Update is called once per frame
    void Update()
    {
        step();
    }

    private void onStep()
    {
        if(Input.GetKeyUp("w"))
        {
            onSteps[0] = true;
        }
        if(Input.GetKeyUp("a"))
        {
            onSteps[1] = true;
        }
        if(Input.GetKeyUp("s"))
        {
            onSteps[2] = true;
        }
        if(Input.GetKeyUp("d"))
        {
            onSteps[3] = true;
        }
    }

    private void step()
    {
        float power = 100.0f;
        onStep();

        if(onSteps[0])
        {
            onTimer[0] += Time.deltaTime;
            if(onTimer[0] <= maxTimer && Input.GetKeyDown("w"))
            {
                onSteps[0] = false;
                rd2D.velocity = Vector3.up * power;            
                onTimer[0] = default;
                nowStep = true;
            }
            else if(onTimer[0] >= maxTimer)
            {
                onSteps[0] = false;
                onTimer[0] = default;
            }
        }
        if(onSteps[1])
        { 
            onTimer[1] = Time.deltaTime;
            if(onTimer[1] <= maxTimer && Input.GetKeyDown("a"))
            {
                onSteps[1] = false;
                rd2D.velocity = Vector3.left * power;        
                onTimer[1] = default;
                nowStep = true;
            }
            else if(onTimer[1] >= maxTimer)
            {
                onSteps[1] = false;
                onTimer[1] = default;
            }
        }
        if(onSteps[2])
        {
             onTimer[2] += Time.deltaTime;
            if(onTimer[2] <= maxTimer && Input.GetKeyDown("s"))
            {
                onSteps[2] = false;
                rd2D.velocity = Vector3.down * power;            
          
                onTimer[2] = default;
                nowStep = true;

            }
            else if(onTimer[2] >= maxTimer)
            {
                onSteps[2] = false;
                onTimer[2] = default;
            }
        }
        if(onSteps[3])
        {
             onTimer[3] += Time.deltaTime;
            if(onTimer[3] <= maxTimer && Input.GetKeyDown("d"))
            {
                onSteps[3] = false;
                rd2D.velocity = Vector3.right * power;           
                onTimer[3] = default;
                nowStep = true;
            }
            else if(onTimer[3] >= maxTimer)
            {
                onSteps[3] = false;
                onTimer[3] = default;
            }
        }

        if(nowStep)
        {
            float MaxTimer = 0.1f;
            stepTimer -= Time.deltaTime;

            if(stepTimer <= 0)
            {
                rd2D.velocity = Vector3.zero;     
                nowStep = false;
                stepTimer = MaxTimer;      
            }
        }
    }
}
