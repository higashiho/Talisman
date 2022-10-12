using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabSlime;   // ƒXƒ‰ƒCƒ€”z—ñ
    [SerializeField]
    private float sponTime = 10f;
    [SerializeField]
    private float pos_z = 0;
    private int slime_number;

    public int Slime_counter;
    [SerializeField]
    private int slime_Max = 10;
    private float pos_x = 50f;
    private float pos_y = 50f;
    void Start()
    {
        Slime_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Slime_counter < slime_Max)
            random();
    }

    private void random()
    {
        slime_number = Random.Range(0, prefabSlime.Length);
        float x = Random.Range(-pos_x, pos_x);
        float y = Random.Range(-pos_y, pos_y);

        Vector3 pos = new Vector3(x, y, pos_z);
        StartCoroutine("spon");
        Instantiate(prefabSlime[slime_number], pos, Quaternion.identity);
        Slime_counter++;
    }
    private IEnumerator spon()
    {

        yield return new WaitForSeconds(sponTime);
        //Instantiate(prefabItem[item_number], pos, Quaternion.identity);
    }

}
