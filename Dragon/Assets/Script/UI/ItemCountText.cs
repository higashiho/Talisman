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

    [SerializeField]
    private SkillController skillController;


    public int GetItemCountArray(int i) {return ItemCountArray[i];}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        displayItemCount();
    }

    private void displayItemCount(){    // 所持しているアイテムの数を表示する
        ItemCountArray[0] = skillController.Skills[0];
        ItemCountArray[1] = skillController.Skills[1];
        ItemCountArray[2] = skillController.Skills[4];
        
        for(int i=0; i<itemType; i++){
            itemCountTextArray[i].text = ItemCountArray[i].ToString();
        }
    }
}
