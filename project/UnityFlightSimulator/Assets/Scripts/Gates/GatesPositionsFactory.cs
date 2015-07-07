using UnityEngine;
using System.Collections;

public class GatesPositionsFactory : MonoBehaviour {
	public int NumberOfGates = 40;
	public float MinHeightDistanceBetweenTerrainAndGate = 50.0f;
	public float MaxHeightDistanceBetweenTerrainAndGate = 100.0f;

	public float DistanceBetweenTerrainBoundariesAndGate = 200.0f;

	public Vector3[] GetGatesPositions(Terrain terrain) {
		var gateLocations = new Vector3[NumberOfGates];
		InitializeGatePositionRange (terrain);
		for (int i = 0; i < NumberOfGates; i++)
		{
			gateLocations[i] = GetGatePosition(terrain);
		}

		return gateLocations;
	}

	private float _leftXBoundary;
	private float _rightXBoundary;
	private float _bottomZBoundary;
	private float _topZBoundary;
	private void InitializeGatePositionRange(Terrain terrain)
	{
		var terrainStartPosition = terrain.GetPosition ();
		_leftXBoundary = terrainStartPosition.x + DistanceBetweenTerrainBoundariesAndGate;
		_rightXBoundary = terrain.terrainData.size.x + terrainStartPosition.x - DistanceBetweenTerrainBoundariesAndGate;

		_bottomZBoundary = terrainStartPosition.z + DistanceBetweenTerrainBoundariesAndGate;
		_topZBoundary = terrain.terrainData.size.z + terrainStartPosition.z - DistanceBetweenTerrainBoundariesAndGate;

		if (_leftXBoundary >= _rightXBoundary || _bottomZBoundary >= _topZBoundary) 
		{
			throw new System.ArgumentException("invalid boundaries for gate positions");
		}
	}

	private System.Random _random = new System.Random ();
	private Vector3 GetGatePosition(Terrain terrain)
	{
		Vector3 result = new Vector3 ();
		result.x = (float)_random.NextDouble () * (_rightXBoundary - _leftXBoundary) + _leftXBoundary;
		result.z = (float)_random.NextDouble () * (_topZBoundary - _bottomZBoundary) + _bottomZBoundary;

		var heightDistanceBetweenTerrainAndGate = (float)_random.NextDouble () * (MaxHeightDistanceBetweenTerrainAndGate - MinHeightDistanceBetweenTerrainAndGate) 
			+ MinHeightDistanceBetweenTerrainAndGate;
		result.y = terrain.SampleHeight (result) + heightDistanceBetweenTerrainAndGate;
		return result;
	}
}
