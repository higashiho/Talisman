using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WachingMouse : MonoBehaviour
{
        private Plane plane = new Plane();
        private float distance;
    // Start is called before the first frame update
    void Start()
    {
        plane.SetNormalAndPosition(Vector3.back, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        waching();
    }

    private void waching()
    {
       var mousPos = Camera.main.ScreenPointToRay(Input.mousePosition);
       if(plane.Raycast(mousPos, out distance))
       {
            // プレイヤーとの交差を求めてキャラクターを向ける
            var lookPoint = mousPos.GetPoint(distance);
            transform.LookAt(transform.localPosition + Vector3.forward, lookPoint - transform.localPosition);
        }
       
    }

}
