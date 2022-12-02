using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBoss : MonoBehaviour
{
    private float waitTime = 6.0f;                              // 待ち時間
    
    // 取得関係
    [SerializeField]
    private FindBoss findBoss;                                  // スクリプト取得用
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip instansAudio, battleAudio;                // 効果音
    [SerializeField]
    private GameObject bossPrefab = default;                    // ボスのprefab
    private GameObject boss;                                    // 生成したか確認用

    // Start is called before the first frame update
    void Start()
    {
        Invoke("instans", waitTime);
        //Componentを取得
        audioSource = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        
        bossAudio();
    }

    // ボス生成
    private void instans()
    {
        audioSource.Stop();

        boss = Instantiate(bossPrefab, this.transform.position, Quaternion.identity);
        findBoss.BossFind();
        Destroy(this.GetComponent<BoxCollider2D>());
    }

    // ボス生成までの効果音
    private void bossAudio()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
        {
            audioSource.Stop();
            return;
        }
        if(!audioSource.isPlaying)
        {
            int m_defaultPitch = 1;
            if(boss == null)
                audioSource.PlayOneShot(instansAudio);
            else
            {
                if(audioSource.pitch > m_defaultPitch)
                    audioSource.pitch = m_defaultPitch;
                audioSource.PlayOneShot(battleAudio);
            }
        }
    }
}
