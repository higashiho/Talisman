using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{


    public void move(RectTransform subject, Vector3 destination, float moveSpeed)
    {
        subject.position = Vector3.MoveTowards(subject.transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
