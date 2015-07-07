using UnityEngine;
using System.Collections;

public class GatesPositionsFactory : MonoBehaviour {
	public int NumberOfGates = 40;
	public float DistanceBetweenTerrainAndGate = 100.0f;

	public Vector3[] GetGatesPositions(Terrain terrain, TerrainData terrainData) {
		var terrainSize = terrainData.size.x;
		var terrainHeights = terrainData.GetHeights(0,0, 256, 256);
		var pos = terrain.GetPosition ();
		var gateLocations = new Vector3[NumberOfGates];
		for (int i = 0; i < NumberOfGates; i++)
		{
			gateLocations[i] = GetGatePosition(terrain);
		}

		return gateLocations;
	}

	private float _xL = 285.9f, _xP = 2183.4f; 
	private float _zD = 302.1f, _zG = 2076.4f;
	private System.Random _random = new System.Random ();
	private Vector3 GetGatePosition(Terrain terrain)
	{
		Vector3 result = new Vector3 ();

		result.x = (float)_random.NextDouble () * (_xP - _xL) + _xL;
		result.z = (float)_random.NextDouble () * (_zG - _zD) + _zD;
		result.y = terrain.SampleHeight (result) + DistanceBetweenTerrainAndGate;
		return result;
	}
}
