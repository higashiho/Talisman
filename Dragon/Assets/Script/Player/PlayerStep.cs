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

    private bool noStep = false;                // クールタイムかどうか
    private float coolTimer = 10.0f;            // クールタイム時間

    public float GetCoolTimer() {return coolTimer;}
    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
        bool[] onSteps = {false};
        float[] onTimer = {0.0f};
    }

    // Update is called once per frame
    void Update()
    {
        if(noStep)
            coolTime();
        else
            step();
    }

    private void onStep()
    {
        if(Input.GetKeyUp("w") && !onSteps[0])
        {
            onSteps[0] = true;
        }
        if(Input.GetKeyUp("a") && !onSteps[1])
        {
            onSteps[1] = true;
        }
        if(Input.GetKeyUp("s") && !onSteps[2])
        {
            onSteps[2] = true;
        }
        if(Input.GetKeyUp("d") && !onSteps[3])
        {
            onSteps[3] = true;
        }
    }

    // ステップのクールタイム
    private void coolTime()
    {
        float defaultTimer = 10.0f;
        coolTimer -= Time.deltaTime;

        if(coolTimer <= 0)
        {
            noStep = false;
            coolTimer = defaultTimer;
        }
    }

    private void stepControl(int num, string str)
    {
        string m_up = "w", m_down = "s", m_right = "d";
        float power = 150.0f;
        if(onSteps[num])
        {
            onTimer[num] += Time.deltaTime;
            if(onTimer[num] <= maxTimer && Input.GetKeyDown(str))
            {
                onSteps[num] = false;
                if(str == m_up)
                    rd2D.velocity = Vector3.up * power;  
                else if(str == m_down)
                    rd2D.velocity = Vector3.down * power;  
                else if(str == m_right)
                    rd2D.velocity = Vector3.right * power;  
                else 
                    rd2D.velocity = Vector3.left * power;  

                onTimer[num] = default;
                nowStep = true;
            }
            else if(onTimer[num] >= maxTimer)
            {
                onSteps[num] = false;
                onTimer[num] = default;
            }
        }
    }

    private void step()
    {
        onStep();

        int m_up = 0, m_left = 1, m_down = 2, m_right = 3;

        

        stepControl(m_up, "w");
        stepControl(m_left, "a");
        stepControl(m_down, "s");
        stepControl(m_right, "d");


        if(nowStep)
        {
            float MaxTimer = 0.1f;
            stepTimer -= Time.deltaTime;

            if(stepTimer <= 0)
            {
                rd2D.velocity = Vector3.zero;  
                nowStep = false;   
                stepTimer = MaxTimer;   
                noStep = true; 
                bool[] onSteps = {false};
                float[] onTimer = {0.0f};  
            }
        }
    }
}
