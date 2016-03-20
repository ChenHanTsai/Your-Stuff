using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public float m_TimeRemain = 2.0f;
    public stuff m_Stuff;
    public Text m_Time;
    public Text m_Height;
    public Animator m_Animation;
    public Text m_LV;
    public Camera m_Main;
    public Camera m_StuffCamera;
    public float m_StuffCameraDistance = -20.0f;
	// Use this for initialization
	void Start ()
    {
        m_Stuff = GameObject.FindGameObjectWithTag("Stuff").GetComponent<stuff>();
        m_Time = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        m_Height = GameObject.FindGameObjectWithTag("Height").GetComponent<Text>();
        m_Height.enabled = false;
        m_LV = GameObject.FindGameObjectWithTag("LV").GetComponent<Text>();

        m_Main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        m_StuffCamera = GameObject.FindGameObjectWithTag("StuffCamera").GetComponent<Camera>();

        SetCameraIndex(true, false);
       
        m_Animation.enabled = false;

     
	}
	
    void SetCameraIndex(bool _main, bool _stuffCamera)
    {
        m_Main.enabled = _main;
        m_Main.gameObject.GetComponent<AudioListener>().enabled = _main;

        m_StuffCamera.enabled = _stuffCamera;
        m_StuffCamera.gameObject.GetComponent<AudioListener>().enabled = _stuffCamera;
    }
	// Update is called once per frame
	void Update ()
    {
        switch(m_Stuff.m_State)
        {
            case stuff.State.circle:
                m_TimeRemain -= Time.deltaTime;
                m_Time.text = "Time Remain: " + m_TimeRemain.ToString("F2");
                m_LV.text = "LV: " + m_Stuff.m_EnergyLevel.ToString("F0");
                if (m_TimeRemain <= 0)
                {
                    m_Time.gameObject.SetActive(false);
                    m_Height.enabled = true;

                    SetCameraIndex(false, true);
                    m_Stuff.m_State = stuff.State.jump;
                    SetStuffCameraDistance(m_StuffCameraDistance);

                    GameObject[] _circleText =  GameObject.FindGameObjectsWithTag("CircleStateText");

                    foreach (var child in _circleText)
                        child.gameObject.SetActive(false);

                   // if (m_Stuff.m_EnergyLevel>=5.0f)
                   // m_Stuff.m_EnergyLevel = 5.0f;
              
                }
                  

                break;

            case stuff.State.jump:
                m_Height.text = "Height: " + m_Stuff.m_StuffCollection.transform.position.y.ToString("F0") + " m";
                break;

            case stuff.State.fallDown:
              //  m_Animation.enabled = true;
                break;
        }
	
	}

    void SetStuffCameraDistance(float m_distance)
    {
        m_StuffCamera.transform.position = new Vector3(m_Stuff.transform.position.x, m_Stuff.transform.position.y, m_distance);
    }
}
