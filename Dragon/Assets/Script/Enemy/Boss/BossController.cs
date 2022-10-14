using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField]
    private List<Vector3> destinations;     //目標座標

    [SerializeField]
   private NavMeshAgent2D agent; //NavMeshAgent2Dを使用するための変数

    [SerializeField] Transform target; 
    // Start is called before the first frame update
    void Awake()
    {
        destinations = new List<Vector3>();
        destinations.Add(new Vector3(44.0f, 44.0f, 0));     // 右上
        destinations.Add(new Vector3(44.0f, -44.0f, 0));     // 右下
        destinations.Add(new Vector3(-44.0f, 44.0f, 0));     // 左上
        destinations.Add(new Vector3(-44.0f, -44.0f, 0));   // 左下
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void move()
    {
        agent.destination = target.position; 
    }
    void OnDestroy()
    {
        destinations.Clear();
    }
}
