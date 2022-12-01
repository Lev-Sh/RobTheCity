using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class houseowner : MonoBehaviour {
	
	int rays = 6;
	int distance = 15;
	float angle = 20;
	LevelInc level;
	bool h = false;
    [SerializeField] private Transform Random_Point;
	[SerializeField] public bool changelights = false;
	[SerializeField] private GameObject Posts;
	[SerializeField] private Vector3 offset;
    [SerializeField] private Transform Exit;
	[SerializeField] Animator anim;
    [SerializeField] float Calm_level;
    public bool Noised = false;
    [SerializeField] private Transform Noise;
	[SerializeField] float Post_Wait = 0;
	[SerializeField] int Post_Now = 0;
	[SerializeField] bool UseEmoji = true;
	[SerializeField] bool IPost;
    public int angry  = 0; //0 - dont see player 1 - small noise 2 - see player
	[SerializeField] Transform target;
    NavMeshAgent agent;
	[SerializeField] private Transform[] emoji;
	void Start () 
	{
        agent = GetComponent<NavMeshAgent>();
		Transform Emojis = transform.Find("Emoji");
		if (IPost) { StartCoroutine(PostMove()); }
		h = true;
		emoji = new Transform[3];
		for(int i = 0;i < Emojis.childCount; i++)
        {
			emoji[i] = Emojis.GetChild(i);
        }
	}



	void Update ()
	{
        if (UseEmoji) { SetEmoji(emoji, angry); }
		AnimationSet();
		if (angry == 1 && agent.remainingDistance < 1 && h == true)
        {
			h = false;
			StartCoroutine(Calm());
        } //успокоение
		Transform now_post = Posts.transform.GetChild(Post_Now);
        if (Noised && angry != 2)
        {
			angry = 1;
        }
		if(changelights && angry != 2){
			distance = 5;
		}else{
			distance = 15;
		}
        agent.SetDestination(Angry_Level(angry,Noise,Exit,now_post)); //движение
		if(agent.remainingDistance < 3 && changelights && angry == 1){
				level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelInc>();
				level.ChangeLight();
				changelights = false;
		}
		if(Vector3.Distance(transform.position, target.position) < distance)
		{
			if(RayToScan())
			{
                angry = 2;
				Noised = false;
				agent.speed = 3.5f;
            }
			else
			{
			}
		}//лучи (просмотр на наличие игркоа)
	}
	bool GetRaycast(Vector3 dir)
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		Vector3 pos = transform.position + offset;
		if (Physics.Raycast(pos, dir, out hit, distance))
		{
			if (hit.transform == target)
			{
				result = true;
				Debug.DrawLine(pos, hit.point, Color.green);
			}
			else
			{
				Debug.DrawLine(pos, hit.point, Color.blue);
			}
		}
		else
		{
			Debug.DrawRay(pos, dir * distance, Color.red);
		}
		return result;
	}
	bool RayToScan()
	{
		bool result = false;
		bool a = false;
		bool b = false;
		float j = 0;
		for (int i = 0; i < rays; i++)
		{
			var x = Mathf.Sin(j);
			var y = Mathf.Cos(j);

			j += angle * Mathf.Deg2Rad / rays;

			Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
			if (GetRaycast(dir)) a = true;

			if (x != 0)
			{
				dir = transform.TransformDirection(new Vector3(-x, 0, y));
				if (GetRaycast(dir)) b = true;
			}
		}

		if (a || b) result = true;
		return result;
	}
	public void SetEmoji(Transform[] y, int an)
	{
        switch (an)
        {
			case 0:
				y[0].gameObject.SetActive(true);
				y[1].gameObject.SetActive(false);
				y[2].gameObject.SetActive(false);
				break;
			case 1:
				y[0].gameObject.SetActive(false);
				y[1].gameObject.SetActive(true);
				y[2].gameObject.SetActive(false);
				break;
			case 2:
				y[0].gameObject.SetActive(false);
				y[1].gameObject.SetActive(false);
				y[2].gameObject.SetActive(true);
                break;
            default:
				y[0].gameObject.SetActive(false);
				y[1].gameObject.SetActive(false);
				y[2].gameObject.SetActive(false);
				break;

		}
    }

	public void AnimationSet()
	{
		if (agent.remainingDistance < 2)
		{
			anim.SetBool("Run", false);
			anim.SetBool("Walk", false);
		}
		else
		{
			if (angry == 1 || angry == 0)
			{
				anim.SetBool("Run", false);
				anim.SetBool("Walk", true);
			}
			else if (angry == 2)
			{
				anim.SetBool("Run", true);
				anim.SetBool("Walk", false);
			}
		}
	}

	private Vector3 Angry_Level(int i, Transform Nois,Transform Ex,Transform Rand){
        if(i == 0){
			return Rand.transform.position;
        }else if(i == 1)
		{ 
			return Nois.transform.position;
        }else if(i == 2){
			return Ex.transform.position;
        }else{
            return Vector3.zero;
        }
    }
	IEnumerator PostMove()
	{ 
		yield return new WaitForSeconds(Post_Wait);
		Post_Now++;
		if(Post_Now == Posts.transform.childCount - 1)
        {
			Post_Now = 0;
        }
		StartCoroutine(PostMove());
    }
	IEnumerator Calm()
    {
		yield return new WaitForSeconds(Calm_level);
		Noised = !Noised;
		angry = 0;
		h = true;
    }
}