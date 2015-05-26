using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public Camera RearCamera;
	public Camera FirstPerspectiveCamera;
	public Camera UpCamera;
	public Camera LeftSideCamera;

	void Start () {
		this.DisableAllCameras ();
		RearCamera.enabled = true;
		RearCamera.GetComponent <AudioListener>().enabled = true;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1))
		{
			DisableAllCameras();
			RearCamera.enabled = true;
			RearCamera.GetComponent <AudioListener>().enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			DisableAllCameras();
			FirstPerspectiveCamera.enabled = true;
			FirstPerspectiveCamera.GetComponent <AudioListener>().enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			DisableAllCameras();
			UpCamera.enabled = true;
			UpCamera.GetComponent <AudioListener>().enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.F4))
		{
			DisableAllCameras();
			LeftSideCamera.enabled = true;
			LeftSideCamera.GetComponent <AudioListener>().enabled = true;
		}
	}
	
	private void DisableAllCameras()
	{
		RearCamera.enabled = false;
		RearCamera.GetComponent <AudioListener>().enabled = false;
		FirstPerspectiveCamera.enabled = false;
		FirstPerspectiveCamera.GetComponent <AudioListener>().enabled = false;
		UpCamera.enabled = false;
		UpCamera.GetComponent <AudioListener>().enabled = false;
		LeftSideCamera.enabled = false;
		LeftSideCamera.GetComponent <AudioListener>().enabled = false;
	}
}
