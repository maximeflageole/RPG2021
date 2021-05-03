using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2d : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;
    [SerializeField]
    private float m_speed;

    // Update is called once per frame
    void Update()
    {
        var vec2 = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            vec2 += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vec2 += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vec2 += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vec2 += Vector2.right;
        }
        vec2 *= m_speed;
        m_rigidbody.velocity = vec2;
    }
}
