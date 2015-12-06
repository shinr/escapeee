using UnityEngine;
using System.Collections;

public class ScoreUpdate : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<TextMesh>().text = "YOUR\nSCORE\n" + player.GetComponent<PlayerControl>().score.ToString();
	}
}
