using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavMesh : MonoBehaviour { 
    public NavMeshAgent agent;
    public LayerMask layer;
    public Animator Animator;
    [SerializeField] private int floor;
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    bool wolk;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        floor = ChangeView();
        foreach(GameObject y in objects)
        {
            y.SetActive(true);
            objects[floor].layer = 3;
        }
        for (int i = floor + 1; i < objects.Count; i++)
        {
            objects[i].SetActive(false);
            objects[floor].layer = 4;
        }

        if (agent.remainingDistance == 0)
            {
                Animator.SetBool("iswalk",false);
            }
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(myRay,out hit,600, layer))
            {
                Animator.SetBool("iswalk",true);
                agent.SetDestination(hit.point);
                
            }
        }

    }
    public int ChangeView()
    {
        return Mathf.RoundToInt(transform.position.y / 7) + 1;
    }
}
