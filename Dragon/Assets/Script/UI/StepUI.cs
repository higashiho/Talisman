using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStep playerStep;
    [SerializeField]
    private Slider coolTimeSlider;



    // Start is called before the first frame update
    void Start()
    {
        coolTimeSlider.maxValue = playerStep.GetCoolTimer();
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderdisplay();
    }

    private void sliderdisplay()
    {
        coolTimeSlider.value = playerStep.GetCoolTimer();
    }
}
