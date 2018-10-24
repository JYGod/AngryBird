using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;
    public Transform rightPosition;
    public float maxDistance;
    private SpringJoint2D springJoint2D;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown() // 鼠标按下时
    {
        isClick = true;
        rigidbody2D.isKinematic = true;
    }

    private void OnMouseUp()
    { 
        isClick = false;
        rigidbody2D.isKinematic = false;
        Invoke("Fly", 0.1f); // 延时调用
    }

    private void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position, rightPosition.position) > maxDistance) // 限制小鸟距离
            {
                transform.position = (transform.position - rightPosition.position).normalized * maxDistance + rightPosition.position;
            }
        }
    }

    void Fly() // 小鸟飞出
    {
        springJoint2D.enabled = false;
    }
}
