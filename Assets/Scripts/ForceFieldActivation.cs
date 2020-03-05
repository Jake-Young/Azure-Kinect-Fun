using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.rfilkov.components;

public class ForceFieldActivation : MonoBehaviour
{

    public GameObject m_ParticleWallRight;
    public GameObject m_ParticleWallLeft;
    public GameObject m_ParticleWallTop;

    private bool m_TopPlaying, m_RightPlaying, m_LeftPlaying;

    private Collider2D m_ForceFieldCollider;
    private CubeGestureListener m_GestureListener;
    private float m_TimeElapsed = 0.0f;

    private void Start()
    {
        m_ForceFieldCollider = this.GetComponent<Collider2D>();
        m_GestureListener = CubeGestureListener.Instance;

        m_TopPlaying = false;
        m_RightPlaying = false;
        m_LeftPlaying = false;
    }

    private void Update()
    {
        m_TimeElapsed += Time.deltaTime;

        if (!m_GestureListener)
        {
            return;
        }

        if (m_GestureListener.isTPose())
        {
            ForceFieldSwitch();
        }
        else if (m_GestureListener.IsSwipeLeft())
        {
            m_LeftPlaying = ParticleStartStop(m_ParticleWallLeft, m_LeftPlaying);
        }
        else if (m_GestureListener.IsSwipeRight())
        {
            m_RightPlaying = ParticleStartStop(m_ParticleWallRight, m_RightPlaying);
        }
        else if (m_GestureListener.IsSwipeUp())
        {
            m_TopPlaying = ParticleStartStop(m_ParticleWallTop, m_TopPlaying);
        }
    }

    private void ForceFieldSwitch()
    {
        if (m_ForceFieldCollider.enabled == true)
        {
            m_ForceFieldCollider.enabled = false;
        }
        else
        {
            m_ForceFieldCollider.enabled = true;
        }
    }

    private bool ParticleStartStop (GameObject particleWall, bool playing)
    {
        if (playing) 
        {
            particleWall.GetComponent<ParticleSystem>().Stop();
            playing = false;
        }
        else
        {
            particleWall.GetComponent<ParticleSystem>().Play();
            playing = true;
        }

        return playing;
    }

}
