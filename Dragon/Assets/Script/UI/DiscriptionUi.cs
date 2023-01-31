using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DiscriptionUi : MonoBehaviour
{
    [SerializeField]    // 説明UI表示タイムライン
    private PlayableDirector description;
    [SerializeField]    // 説明UI非表示タイムライン
    private PlayableDirector unDescription;


    // 説明UIが出ているか
    private bool descriptionFlag = false;    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
            stopTime();

    }

    // 説明キャンバス生成
     private void stopTime()
    {
        
        if(!descriptionFlag)
        {
            description.Play();
            descriptionFlag = true;
        }
        else
        {
            unDescription.Play();
            descriptionFlag = false;
        }
        
    }

}
