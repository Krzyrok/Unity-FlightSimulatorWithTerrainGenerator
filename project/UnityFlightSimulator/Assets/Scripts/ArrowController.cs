using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public GatesController GatesController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (GatesController.GetPositionOfActiveGate ());
	}
}
