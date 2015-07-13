using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTextController : MonoBehaviour {
	private static Text _helpText;
	private static string _helpTextForUser = "Try to collect all gates. Pass the green one.\n" +
		"Green arrow points to the active gate.\n\n" +
		"Press:\n" +
		"Arrows to move\n" +
		"1-4 to change camera\n" +
		"Left ctrl to speed up\n" +
		"Left alt to speed down";

	// Use this for initialization
	void Start () {
		_helpText = GetComponent<Text>();
	}

	public static void ShowHelpText()
	{
		_helpText.text = _helpTextForUser;
	}

	public static void HideHelpText()
	{
		_helpText.text = "";
	}
}
