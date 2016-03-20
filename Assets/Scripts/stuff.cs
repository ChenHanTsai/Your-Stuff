using UnityEngine;
using System.Collections;

public class stuff : MonoBehaviour {

	// Use this for initialization
    public Crowd m_Crowd;

    private float m_X = 0;
    private float m_Z = 0;
    public float m_Radius = 10.0f;
    public float m_EnergyDeltaTime = 0;
    public float m_EnergyLevel = 0;
    public float m_LevelDelta = 0.5f;
    public Animation m_Animation;
    public bool m_Araise = false;
    public enum State { circle, jump, fallDown,hangOnRim }
    public State m_State = State.circle;
    public float m_JumpSpeed = 0.1f;
    public GameObject m_StuffCollection;
    public GameObject m_Rim;
    public Vector3 m_FallDir;
    public GameObject[] flames;
    public GameObject m_Ball;
    public float test;
    public GameObject _yourStuff;
    public float m_fallSpeed = 0;
    public float HighYSpeed = 100;
    public float MiddleYSpeed = 10;
      
   // public AudioClip m_AudioClip;
    public AudioSource[] m_AudioSoucre;
	void Start () 
    {
        m_Crowd = GameObject.FindGameObjectWithTag("Crowd").GetComponent<Crowd>() ;
        m_StuffCollection = transform.parent.gameObject;
        flames = GameObject.FindGameObjectsWithTag("Flame");
        foreach (var child in flames)
            child.gameObject.SetActive(false);

        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_Rim = GameObject.FindGameObjectWithTag("Rim");

         _yourStuff = GameObject.FindGameObjectWithTag("YourStuff");

        _yourStuff.gameObject.SetActive(false);

  
        m_AudioSoucre = GetComponents<AudioSource>();

       

      //  m_AudioSoucre.clip = Resources.Load(" AcetyleneTorchRun_FSHOF.mp3") as AudioClip;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (m_State)
        {
            case State.circle:
                runAround();

                 if (!m_AudioSoucre[0].isPlaying)
                    m_AudioSoucre[0].Play();

                 if (!m_AudioSoucre[1].isPlaying)
                    m_AudioSoucre[1].Play();

                break;

            case State.jump:
                if (!m_AudioSoucre[0].isPlaying)
                    m_AudioSoucre[0].Play();

                if (m_AudioSoucre[1].isPlaying)
                    m_AudioSoucre[1].Stop();

                if (!m_AudioSoucre[2].isPlaying)
                m_AudioSoucre[2].Play();

                SlamDunk();
                break;

            case State.fallDown:

                Falling();
                break;
        }
       
         
      

      
	}

   void runAround()
    {
        //  if (m_Crowd.GetMicLoudness()>=1.0f)
        //{
        //    m_EnergyLevel += m_LevelDelta;    
        //}
          m_EnergyLevel += m_Crowd.GetMicLoudness();    

        //if (m_Crowd.GetMicLoudness() <= 0.5f)
        //{
        //    m_Level -= m_LevelDelta;
        //    if (m_Level <= 0)
        //        m_Level = 0;
        //}
           

        m_EnergyDeltaTime += Time.deltaTime * m_EnergyLevel;

        m_X = m_Radius * Mathf.Cos(m_EnergyDeltaTime);
        m_Z = m_Radius * Mathf.Sin(m_EnergyDeltaTime);
        transform.position = new Vector3(m_X,transform.position.y, m_Z);
    }
    public void SlamDunk()
    {
        if (m_EnergyLevel >=20)
        m_EnergyLevel -= Time.deltaTime*10;
        else if(m_EnergyLevel <20)
        m_EnergyLevel -= Time.deltaTime*5;
        else
            m_EnergyLevel -= Time.deltaTime*2;

     //   m_AudioSoucre.clip = Resources.Load("RocketLaunch_FSHOF.751") as AudioClip;
        if (m_EnergyLevel <= 0)
        {
            m_State = State.fallDown;
            m_FallDir = new Vector3(m_Rim.transform.position.x - m_Ball.transform.position.x,
                                    m_Rim.transform.position.y - m_Ball.transform.position.y,
                                  m_Rim.transform.position.z - m_Ball.transform.position.z);

            m_FallDir.Normalize();


        }
          
          
        else
        {
            m_StuffCollection.transform.position = new Vector3(m_StuffCollection.transform.position.x, m_StuffCollection.transform.position.y +
                Time.deltaTime * m_JumpSpeed, m_StuffCollection.transform.position.z);
            
        }           
    }
   

     public void Falling()
    {

        if (transform.position.y >= 100)
            m_fallSpeed = HighYSpeed;
        else  if (transform.position.y >= 100)
            m_fallSpeed = MiddleYSpeed;
        else
            m_fallSpeed = m_JumpSpeed;

        m_StuffCollection.transform.Translate(m_FallDir * Time.deltaTime * m_fallSpeed);

        transform.Rotate(new Vector3(0,0,1),10);

        test = Vector3.Distance(m_Ball.transform.position, m_Rim.transform.position);

        if (Vector3.Distance(m_Ball.transform.position, m_Rim.transform.position) <= 5.0f)
         {
           

            //m_StuffCollection.transform.position = new Vector3(16.96f,6.5f,4.24f   );
             //transform.position = new Vector3(m_Rim.transform.position.x, m_Rim.transform.position.y, m_Rim.transform.position.z);
             transform.localRotation = Quaternion.EulerRotation(0, 0, 0);

             Vector3 shift = m_Rim.transform.position - m_Ball.transform.position;
             m_StuffCollection.transform.Translate(shift);

             foreach (var child in flames)
                 child.gameObject.SetActive(true);

             m_State = State.hangOnRim;

             _yourStuff.gameObject.SetActive(true);

             if (m_AudioSoucre[1].isPlaying)
                 m_AudioSoucre[1].Stop();

             if (m_AudioSoucre[2].isPlaying)
                 m_AudioSoucre[2].Stop();

         }
    }
}
