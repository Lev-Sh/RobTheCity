using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;
[RequireComponent(typeof(Outline))]
public class Incredible : MonoBehaviour
{
    public float incredibleRadius = 1f;
    private Outline outline;
    [SerializeField] bool isNoised;
    [SerializeField] private bool Destroys;
    [SerializeField] bool Open = false;
    public Transform Noise;
    int i = 0;
    public houseowner mind;
    public bool Microgame = true;
    float tap_times = 10;
    GameObject Player;
    [SerializeField] GameObject tx;
    List<NavMeshAgent> nav_list = new List<NavMeshAgent>();
    List<Animator> anim_list = new List<Animator>();
    float l = 1.1f;
    void Start()
    {
        //Text text = GameObject.FindGameObjectWithTag("Bg").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0f;
        Player = GameObject.FindGameObjectWithTag("Player");
        if (!Microgame || !isNoised) { tap_times = 0; }
        GameObject[] g = GameObject.FindGameObjectsWithTag("People");
        //tx.gameObject.SetActive(false);
        for (int i = 0; i < g.Length - 1; i++)
        {
            nav_list.Add(g[i].GetComponent<NavMeshAgent>());
            anim_list.Add(g[i].GetComponent<Animator>());
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tx.gameObject.SetActive(false);
            i = 0;
            foreach (NavMeshAgent nav in nav_list)
            {
                nav.speed = 2f;
                Player.GetComponent<NavMeshAgent>().speed = 3.5f;
            }
            foreach (Animator an in anim_list)
            {
                an.speed = 1;
                Player.GetComponent<Animator>().speed = 1f;

            }
        }
    }
    // Update is called once per frame
    public void OnMouseEnter()
    {
        outline.OutlineWidth = 5f;

    }
    public void OnMouseExit()
    {
        outline.OutlineWidth = 0;

    }
    private void OnMouseDown()
    {
        if (Open)
        {
            transform.position += transform.TransformDirection(Vector3.left * l);
            l *= -1;
        }
        if (Destroys)
        {
            Prize();
            Destroy(this.gameObject);
        }
        if (Mathf.Abs(Vector3.Distance(Player.transform.position, transform.position)) < incredibleRadius)
        {

            if (isNoised && !mind.changelights)
            {
                i++;
                tx.gameObject.SetActive(true);
                foreach (NavMeshAgent nav in nav_list)
                {
                    nav.speed = 0.2f;
                    Player.GetComponent<NavMeshAgent>().speed = 0.2f;

                }
                foreach (Animator an in anim_list)
                {
                    an.speed = 0.2f;
                    Player.GetComponent<Animator>().speed = 0.2f;
                }
            }
            if (MiniGame()) {
                i = 20;
                tx.gameObject.SetActive(false);
                foreach (NavMeshAgent nav in nav_list)
                {
                    nav.speed = 2f;
                    Player.GetComponent<NavMeshAgent>().speed = 3.5f;
                }
                foreach (Animator an in anim_list)
                {
                    an.speed = 1;
                    Player.GetComponent<Animator>().speed = 1f;

                }
                mind.Noised = true;
                Noise.transform.position = transform.position;
                Prize();
            }

        }
        
}
    bool MiniGame()
    {
        if (i != tap_times)
        {
            return false;
        }
        return true;
    }
    void Prize()
    {

    }
}

