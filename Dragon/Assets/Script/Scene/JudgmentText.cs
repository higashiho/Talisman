using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgmentText : MonoBehaviour
{
    [SerializeField]
    private Text judgmentText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        judgmentText.text = BossController.Judgment;
    }
}
