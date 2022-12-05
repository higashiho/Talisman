using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GetItem : MonoBehaviour 
{
    [SerializeField]
    private AudioClip itemAudio;                // 効果音
    private AudioSource audioSource;
    
    void Start () 
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update () 
    {

    }

    public void ItemGet()
    {
        audioSource.PlayOneShot(itemAudio);
    }
}