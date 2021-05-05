using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAnim : MonoBehaviour
{
    [SerializeField] private float m_duration = 1.0f;
    [SerializeField] private bool m_isLooping = true;
    [SerializeField] private AnimationCurve m_sizeCurve;
    [SerializeField] private Gradient m_gradient;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private float m_currentDuration;

    // Update is called once per frame
    void Update()
    {
        m_currentDuration += Time.deltaTime;
        m_currentDuration %= m_duration;

        var curveValue = m_sizeCurve.Evaluate(m_currentDuration / m_duration);
        m_spriteRenderer.color = m_gradient.Evaluate(curveValue);
    }
}
