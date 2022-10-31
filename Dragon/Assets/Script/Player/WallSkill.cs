using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : MonoBehaviour
{

    [SerializeField]
    private GameObject wallPrefab = default;        //prefab格納用
    private Vector3 mousePos;                       // Mouseの位置
    private float posZ = 10.0f;                     // Mouseのｚ軸調整
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void WallGeneration()
    {
        if(Input.GetMouseButtonDown(1))
        {
            mousePos = Input.mousePosition;
            mousePos.z = posZ;
            Instantiate(wallPrefab,Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
        }
    }

}
