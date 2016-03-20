using UnityEngine;
using System.Collections;

public class MakeSomeNoise : MonoBehaviour {

	// Use this for initialization

    public Crowd m_Crowd;
    public float m_Sacle = 1;
    private bool m_ScaleBool = true;
	void Start () 
    {
        m_Crowd = GameObject.FindGameObjectWithTag("Crowd").GetComponent<Crowd>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_ScaleBool)
          StartCoroutine(ChangeScale());
	}

    IEnumerator ChangeScale()
    {
        m_ScaleBool = false;

        if (m_Crowd)
        {
            m_Sacle = m_Crowd.GetMicLoudness();
            m_Sacle = 1 + m_Sacle*1.0f;
            transform.localScale = new Vector3(m_Sacle, m_Sacle);
        }

            yield return new WaitForSeconds(0.5f);

            m_ScaleBool = true;
    }

}
