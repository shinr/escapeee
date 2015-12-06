using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject player;
    public float suspicion = 0.0f;
    public bool catching = false;
    public Vector3 movementDir;
    bool pushed = false;
    bool fighting = false;
    GameObject fightingPartner;
    float backUpTimer = .5f;
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Player")
        {
            pushed = true;
            movementDir = c.gameObject.transform.position - transform.position;
            movementDir *= -1.0f;
            movementDir *= 2.0f;

        } else if (c.gameObject.tag == "Enemy")
        {
            if(pushed)
            {
                fighting = true;
                fightingPartner = c.gameObject;
                fightingPartner.GetComponent<EnemyBehaviour>().Aggro(this.gameObject);
                Invoke("CalmDown", 10.0f);
            }
        }
    }

    public void Aggro(GameObject t)
    {
        fighting = true;
        fightingPartner = t;
        Invoke("CalmDown", 10.0f);
    }

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        movementDir = new Vector3(1.0f * Random.value - 1.0f * Random.value, 1.0f * Random.value - 1.0f * Random.value, 0.0f);
        Invoke("NewDirection", 2.0f);
	}

    void CalmDown()
    {
        fighting = false;
    }
	
    void NewDirection()
    {
        if(pushed) { pushed = false; }
        if (!catching)
        {
            movementDir = new Vector3(1.0f * Random.value - 1.0f * Random.value, 1.0f * Random.value - 1.0f * Random.value, 0.0f);
            Invoke("NewDirection", Random.value * 5.0f);
        }
    }

	// Update is called once per frame
	void Update () {
        foreach(GameObject c in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(Vector2.Distance(transform.position, player.transform.position) < 4.0f)
            {
                if (c.GetComponent<EnemyBehaviour>().catching)
                {
                    catching = true;
                }
            }

        }
	    if(Vector2.Distance(transform.position, player.transform.position) < 8.0f)
        {
            if (player.GetComponent<PlayerControl>().eating)
            {
                suspicion += .01f;
                if (suspicion > 1.0f)
                {
                    catching = true;
                }
            }
        }
        if(catching)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .07f);
            //transform.Translate();
        } else if (fighting)
        {
            backUpTimer -= Time.deltaTime;
            if (backUpTimer < .0f)
            {
                transform.Translate((fightingPartner.transform.position - transform.position) * Time.deltaTime * 3.0f);
                if(backUpTimer < -.2f)
                {
                    backUpTimer = .5f;
                }
            } else
            {
                transform.Translate((fightingPartner.transform.position - transform.position) *-1.0f * Time.deltaTime);
            }
            
        }

        else
        {
            transform.Translate(movementDir * Time.deltaTime);
        }
	}
}
