using UnityEngine;
using System.Collections;

using System.Windows.Forms;
using MapLoader;

public class StartMenu : MonoBehaviour 
{
    public static float[,] m_grayLevels = null;
    public static int m_mapSize = 0;
	

	// Use this for initialization
	void Start () 
    {
        Object.DontDestroyOnLoad(this);

		System.Windows.Forms.Application.EnableVisualStyles();
		System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        System.Windows.Forms.Application.Run(new Form1());
	}
	
	// Update is called once per frame
	void Update () {}
}
