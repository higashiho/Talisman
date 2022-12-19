using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEfectController : MonoBehaviour
{
    private SceneController sceneController;
    private ParticleSystem efect;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.Find("Fade").GetComponent<SceneController>();
        efect = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        stopEfect();
    }

    // Scene転移が始まったらエフェクトを止める
    private void stopEfect()
    {
        if(!sceneController.SceneMove)
            efect.Stop();
    }
}
