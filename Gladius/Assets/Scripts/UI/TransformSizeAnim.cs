using UnityEngine;

public class TransformSizeAnim : MonoBehaviour
{
    [SerializeField] private float m_duration = 1.0f;
    [SerializeField] private bool m_isLooping = true;
    [SerializeField] private AnimationCurve m_sizeCurve;
    [SerializeField] private Vector2 m_sizeValues;

    private float m_currentDuration;

    // Update is called once per frame
    void Update()
    {
        m_currentDuration += Time.deltaTime;
        m_currentDuration %= m_duration;

        var curveValue = m_sizeCurve.Evaluate(m_currentDuration / m_duration);
        var size = m_sizeValues.x + (m_sizeValues.y - m_sizeValues.x) * curveValue;
        transform.localScale = new Vector3(size, size, size);
    }
}