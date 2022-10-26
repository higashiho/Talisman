using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountText : MonoBehaviour
{
    public int[] ItemCountArray = {0, 0, 0};    // アイテムをそれぞれカウントする
    private int itemType = 3;   // アイテムの種類3つ
    [SerializeField]
    private Text[] itemCountTextArray = new Text[3];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 所持しているアイテムの数を表示する
        for(int i=0; i<itemType; i++){
            itemCountTextArray[i].text = ItemCountArray[i].ToString();
        }
    }
}
