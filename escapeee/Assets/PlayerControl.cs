using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
    public float score = 0.0f;
    public bool eating = false;
    public bool freezescore = false;
    public GameObject ENDING;
    public GameObject GoodEnd;
    public GameObject[] todisable;
    public bool starting = true;
    void OnTriggerStay2D(Collider2D c)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if(c.tag == "Food" && !freezescore) { 
                score += 1.0f;
                eating = true;
            }
        }
        else
        {
            eating = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(starting) { return; }
        if(c.gameObject.tag != "Exit") { return; }
        freezescore = true;
        GoodEnd.SetActive(true);
        foreach (GameObject o in todisable)
        {
            o.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag != "Exit") { return; }
        starting = false;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Enemy" && c.gameObject.GetComponent<EnemyBehaviour>().catching && !freezescore)
        {
            freezescore = true;
            ENDING.SetActive(true);
            foreach(GameObject o in todisable)
            {
                o.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(freezescore) { return;  }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(10.0f * h * Time.deltaTime, 10.0f * v * Time.deltaTime, 0.0f));
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
	}
}
