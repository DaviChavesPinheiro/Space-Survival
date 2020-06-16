using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
	Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType (typeof(Player)) as Player;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
