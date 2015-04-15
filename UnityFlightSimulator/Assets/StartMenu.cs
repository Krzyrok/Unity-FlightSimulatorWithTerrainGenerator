using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	void Awake () {
		Debug.Log ("Awake");
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
	}
	
	// Update is called once per frame
	void Update () {}
	
	void FixedUpdate() {}
	
	void OnEnable() {
		Debug.Log ("OnEnable");
	}
	
	void OnDisable() {
		Debug.Log ("OnDisable");
	}
	
	void OnGUI() {
		if (GUI.Button (new Rect (0, 0, 100, 100), "START")) {
			Debug.Log ("OnGUI");
			Application.LoadLevel("SimulationScene");
		}
	}
}
