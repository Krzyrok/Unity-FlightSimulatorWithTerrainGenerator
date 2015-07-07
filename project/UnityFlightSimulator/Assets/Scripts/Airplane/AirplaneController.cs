using UnityEngine;
using System.Collections;

public class AirplaneController : MonoBehaviour {

	public float StartHeightDistanceBetweenTerrainAdnAirplane = 200.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public Vector3 InitializePosition(Terrain terrain) 
	{
		var terrainStartPosition = terrain.GetPosition ();
		var xPosition = (terrainStartPosition.x + terrain.terrainData.size.x) / 2.0f;
		var zPosition = (terrainStartPosition.z + terrain.terrainData.size.z) / 2.0f;

		Vector3 airplanePosition = new Vector3 ();
		airplanePosition.x = xPosition;
		airplanePosition.z = zPosition;
		airplanePosition.y = terrain.SampleHeight (airplanePosition) + StartHeightDistanceBetweenTerrainAdnAirplane;

		gameObject.transform.position = airplanePosition;

		return airplanePosition;
	}
}
