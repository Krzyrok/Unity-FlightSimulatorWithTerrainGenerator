using UnityEngine;
using System.Collections;

using System.Windows.Forms;
using MapLoader;

public class StartMenu : MonoBehaviour {
	
	void Awake () {
		Debug.Log ("Awake");
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Start");

		System.Windows.Forms.Application.EnableVisualStyles();
		System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
		System.Windows.Forms.Application.Run(new Form1());
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
		Debug.Log ("OnGUI");
	}
}
