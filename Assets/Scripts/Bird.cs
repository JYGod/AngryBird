using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;
    private Rigidbody2D rigidbody2D;

    [HideInInspector]
    public SpringJoint2D springJoint2D;

    public Transform rightPosition;
    public Transform leftPosition;
    public float maxDistance;
    public LineRenderer leftLine;
    public LineRenderer rightLine;


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
        leftLine.enabled = false;
        rightLine.enabled = false;
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
            Line();
        }
    }

    void Fly() // 小鸟飞出
    {
        springJoint2D.enabled = false;
        Invoke("Next", 5f);
    }


    void Line() // 画线
    {
        leftLine.enabled = true;
        rightLine.enabled = true;
        leftLine.SetPosition(0, leftPosition.position);
        leftLine.SetPosition(1, transform.position);
        rightLine.SetPosition(0, rightPosition.position);
        rightLine.SetPosition(1, transform.position);
    }

    /// <summary>
    /// 处理下一只小鸟
    /// </summary>
    void Next()
    {
        GameManager._instance.birdList.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
