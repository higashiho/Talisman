using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private AudioClip moveAudio, noShieldAudio;                // 効果音
    private AudioSource audioSource;
    private PlayerController player;            // Player
    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject.GetComponent<PlayerController>();
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.OnMove)
            moveAudioStart();
        
        else
            moveAudioEnd(); 
    }
    // 挙動時効果音
    private void moveAudioStart()
    {
        
        if(Mathf.Approximately(Time.timeScale, 0f))
        {
            audioSource.Stop();
            return;
        }
        if(!audioSource.isPlaying)
        {
            if(player.OnShield)
                // Shieldがある場合の挙動時効果音
                audioSource.PlayOneShot(moveAudio);
            else
                // Shieldがない場合の挙動時効果音
                audioSource.PlayOneShot(noShieldAudio);
        }
    }
    // 挙動時の効果音終了
    private void moveAudioEnd()
    {
        audioSource.Stop();
    }
}
