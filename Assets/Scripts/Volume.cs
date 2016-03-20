using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Volume : MonoBehaviour {

	// Use this for initialization
    public Crowd m_Crowd;

    Scrollbar m_ScollBar;
	void Start () 
    {
        m_ScollBar = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_Crowd)
            m_ScollBar.size = m_Crowd.MicLoudness;

	}
}
