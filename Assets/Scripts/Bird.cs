using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public Transform rightPosition;
    public Transform leftPosition;
    public float maxDistance;
    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public AudioClip audioSelect;
    public AudioClip audioFly;
    public float smooth = 3f;
    public Sprite hurt;
    public GameObject boom;

    [HideInInspector]
    public SpringJoint2D springJoint2D;

    [HideInInspector]
    public bool canMove = false;

    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer renderer;
    protected DrawTrail drawTrail;

    private bool isClick = false;
    private bool isFly = false;

    private void Awake()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        drawTrail = GetComponent<DrawTrail>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() // 鼠标按下时
    {
        if (canMove)
        {
            isClick = true;
            rigidbody2D.isKinematic = true;
            AudioPlay(audioSelect);
        }
    }

    private void OnMouseUp()
    {
        if (canMove)
        {
            leftLine.enabled = false;
            rightLine.enabled = false;
            canMove = false;
            isClick = false;
            rigidbody2D.isKinematic = false;
            Invoke("Fly", 0.1f); // 延时调用
        }
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
        MoveCamera();
        if (isFly && Input.GetMouseButtonDown(0)) // 小黄鸟加速
        {
            ShowSkill();
        }
    }

    /// <summary>
    /// 相机跟随小鸟
    /// </summary>
    private void MoveCamera()
    {
        float posX = transform.position.x;
        Vector3 cameraPosition = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(cameraPosition, new Vector3(Mathf.Clamp(posX, 0f, 15f), cameraPosition.y, cameraPosition.z), smooth * Time.deltaTime);
    }

    void Fly() // 小鸟飞出
    {
        isFly = true;
        drawTrail.StartTrail();
        springJoint2D.enabled = false;
        AudioPlay(audioFly);
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
    protected virtual void Next()
    {
        GameManager._instance.birdList.Remove(this);
        //Debug.Log(GameManager._instance.birdList.Count);
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFly)
        {
            renderer.sprite = hurt; 
        }
        isFly = false;
        drawTrail.ClearTrail();
    }

    /// <summary>
    /// 播放音效   
    /// </summary>
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 炫技操作    
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }
}
