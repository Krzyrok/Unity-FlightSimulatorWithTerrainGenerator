using UnityEngine;
using System.Collections;

public class GatesController : MonoBehaviour {
	public GameObject ActiveGate;
	public GameObject InactiveGate;

	public GatesPositionsFactory GatesPositionsFactory;
	public GatesFactory GatesFactory;

	private ArrayList _gates;
	private Vector3[] _gateLocations;
	private int _activeGateIndex;

	public void GenerateGatesRandomly(Terrain terrain) 
	{
		_gateLocations = GatesPositionsFactory.GetGateRandomPositions (terrain);
		GenerateGatesForLocations (_gateLocations);
	}

	public void GenerateGatesWithPartiallyRandomPosition(Terrain terrain, Vector3 airplaneStartPosition)
	{
		_gateLocations = GatesPositionsFactory.GetGatePartiallyRandomPositions (terrain, airplaneStartPosition);
		GenerateGatesForLocations (_gateLocations);
	}

	private void GenerateGatesForLocations (Vector3[] gateLocations)
	{
		_gates = GatesFactory.InstantiateGatesForLocations (gateLocations, ActiveGate, InactiveGate);
		_activeGateIndex = 0;
	}

	public void ShowNextActiveGateOrEndGame()
	{
		var activeGateToDestroy = (GameObject)_gates[_activeGateIndex];
		Destroy (activeGateToDestroy);

		var nextInaxtiveGateIndex = _activeGateIndex + 1;
		if (nextInaxtiveGateIndex == _gateLocations.Length) 
		{
			EndGameWhenUserWon();
			return;
		}

		var nextInaxtiveGateToDestroy = (GameObject)_gates[nextInaxtiveGateIndex];
		Destroy (nextInaxtiveGateToDestroy);
		
		_activeGateIndex++;
		var newActiveGate = Instantiate (ActiveGate, _gateLocations[_activeGateIndex], Quaternion.identity);
		_gates [_activeGateIndex] = newActiveGate;
	}

	public Vector3 GetPositionOfCurrentActiveGate() 
	{
		return _gateLocations [_activeGateIndex];
	}

	private void EndGameWhenUserWon()
	{
		GameController.EndGame ();
	}
}
