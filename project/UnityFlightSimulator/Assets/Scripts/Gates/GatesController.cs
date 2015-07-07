using UnityEngine;
using System.Collections;

public class GatesController : MonoBehaviour {
	public GameObject ActiveGate;
	public GameObject InactiveGate;

	public GatesPositionsFactory GatesPositionsFactory;

	private ArrayList _gates;
	private Vector3[] _gateLocations;
	private int _activeGateIndex;

	public void GenerateGates(Terrain terrain) {
		_gates = new ArrayList ();
		_gateLocations = GatesPositionsFactory.GetGatesPositions (terrain);

		var newGate = Instantiate (ActiveGate, _gateLocations[0], Quaternion.identity);
		_gates.Add (newGate);
		foreach(var gateLocation in _gateLocations)
		{
			newGate = Instantiate (InactiveGate, gateLocation, Quaternion.identity);
			_gates.Add (newGate);
		}
		
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
