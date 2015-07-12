using UnityEngine;
using System.Collections;

public class GatesController : MonoBehaviour {
	public GameObject ActiveGate;
	public GameObject InactiveGate;

	public GatesPositionsFactory GatesPositionsFactory;
	public GatesFactory GatesFactory;

	private ArrayList _gates;
	private Vector3[] _gatePositions;
	private int _activeGateIndex;

	public void GenerateGatesRandomly(Terrain terrain) 
	{
		_gatePositions = GatesPositionsFactory.GetGateRandomPositions (terrain);
		GenerateGatesForPositions (_gatePositions);
	}

	public void GenerateGatesWithPartiallyRandomPosition(Terrain terrain, Vector3 airplaneStartPosition)
	{
		_gatePositions = GatesPositionsFactory.GetGatePartiallyRandomPositions (terrain, airplaneStartPosition);
		GenerateGatesForPositions (_gatePositions);
	}

	private void GenerateGatesForPositions (Vector3[] gatePositions)
	{
		_gates = GatesFactory.InstantiateGatesForPositions (gatePositions, ActiveGate, InactiveGate);
		_activeGateIndex = 0;
	}

	public void ShowNextActiveGateOrEndGame()
	{
		var activeGateToDestroy = (GameObject)_gates[_activeGateIndex];
		Destroy (activeGateToDestroy);

		var nextInaxtiveGateIndex = _activeGateIndex + 1;
		if (nextInaxtiveGateIndex == _gatePositions.Length) 
		{
			EndGameWhenUserWon();
			return;
		}

		var nextInaxtiveGateToDestroy = (GameObject)_gates[nextInaxtiveGateIndex];
		Destroy (nextInaxtiveGateToDestroy);
		
		_activeGateIndex++;
		var newActiveGate = Instantiate (ActiveGate, _gatePositions[_activeGateIndex], Quaternion.identity);
		_gates [_activeGateIndex] = newActiveGate;
	}

	public Vector3 GetPositionOfActiveGate() 
	{
		return _gatePositions [_activeGateIndex];
	}

	private void EndGameWhenUserWon()
	{
		GameController.EndGame ();
	}
}
