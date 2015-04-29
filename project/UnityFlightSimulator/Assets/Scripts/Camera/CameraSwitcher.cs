using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public Camera RearCamera;
	public Camera FirstPerspectiveCamera;
	public Camera UpCamera;
	public Camera LeftSideCamera;

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1))
		{
			DisableAllCameras();
			RearCamera.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			DisableAllCameras();
			FirstPerspectiveCamera.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			DisableAllCameras();
			UpCamera.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F4))
		{
			DisableAllCameras();
			LeftSideCamera.enabled = true;
		}
	}
	
	private void DisableAllCameras()
	{
		RearCamera.enabled = false;
		FirstPerspectiveCamera.enabled = false;
		UpCamera.enabled = false;
		LeftSideCamera.enabled = false;
	}
}
