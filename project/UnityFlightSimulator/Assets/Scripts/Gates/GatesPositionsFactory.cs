using UnityEngine;
using System.Collections;

public class GatesPositionsFactory : MonoBehaviour {
	public int NumberOfGates = 40;
	public float MinHeightDistanceBetweenTerrainAndGate = 50.0f;
	public float MaxHeightDistanceBetweenTerrainAndGate = 100.0f;

	public float DistanceBetweenTerrainBoundariesAndGate = 200.0f;

	public float AllowedXDistanceBetweenGates = 400.0f;
	public float AllowedZDistanceBetweenGates = 400.0f;

	public Vector3[] GetGateRandomPositions(Terrain terrain) {
		InitializeGatePositionRange (terrain);

		var gateLocations = new Vector3[NumberOfGates];
		for (int i = 0; i < NumberOfGates; i++)
		{
			gateLocations[i] = GetGateRandomPosition(terrain);
		}

		return gateLocations;
	}

	public Vector3[] GetGatePartiallyRandomPositions(Terrain terrain, Vector3 airplaneStartPosition) {
		InitializeGatePositionRange (terrain);
		
		var gateLocations = new Vector3[NumberOfGates];
		gateLocations[0] = GetGatePositionAccordingToThePassedPosition(terrain, airplaneStartPosition);
		for (int i = 1; i < NumberOfGates; i++)
		{
			gateLocations[i] = GetGatePositionAccordingToThePassedPosition(terrain, gateLocations[i-1]);
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
	private Vector3 GetGateRandomPosition(Terrain terrain)
	{
		Vector3 result = new Vector3 ();
		result.x = (float)_random.NextDouble () * (_rightXBoundary - _leftXBoundary) + _leftXBoundary;
		result.z = (float)_random.NextDouble () * (_topZBoundary - _bottomZBoundary) + _bottomZBoundary;

		var heightDistanceBetweenTerrainAndGate = (float)_random.NextDouble () * (MaxHeightDistanceBetweenTerrainAndGate - MinHeightDistanceBetweenTerrainAndGate) 
			+ MinHeightDistanceBetweenTerrainAndGate;
		result.y = terrain.SampleHeight (result) + heightDistanceBetweenTerrainAndGate;
		return result;
	}

	private int _moveForward = 1;
	private Vector3 GetGatePositionAccordingToThePassedPosition(Terrain terrain, Vector3 previousPosition)
	{
		Vector3 result = new Vector3 ();

		result.x = previousPosition.x + (float)_random.NextDouble () * AllowedXDistanceBetweenGates - AllowedXDistanceBetweenGates / 2.0f;
		if (result.x <= _leftXBoundary + DistanceBetweenTerrainBoundariesAndGate || result.x >= _rightXBoundary - DistanceBetweenTerrainBoundariesAndGate) 
		{
			result.x = (float)_random.NextDouble () * (_rightXBoundary - _leftXBoundary) + _leftXBoundary;
		}

		result.z = previousPosition.z + _moveForward * (float)_random.NextDouble () * AllowedZDistanceBetweenGates;
		while (result.z <= _bottomZBoundary + DistanceBetweenTerrainBoundariesAndGate || result.z >= _topZBoundary - DistanceBetweenTerrainBoundariesAndGate) 
		{
			_moveForward *= -1;
			result.z = previousPosition.z + _moveForward * (float)_random.NextDouble () * AllowedZDistanceBetweenGates;
		}

		var heightDistanceBetweenTerrainAndGate = (float)_random.NextDouble () * (MaxHeightDistanceBetweenTerrainAndGate - MinHeightDistanceBetweenTerrainAndGate) 
			+ MinHeightDistanceBetweenTerrainAndGate;
		result.y = terrain.SampleHeight (result) + heightDistanceBetweenTerrainAndGate;
		return result;
	}
}
