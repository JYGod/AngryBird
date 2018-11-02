using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    private List<Pig> blockList = new List<Pig>();

    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blockList != null && blockList.Count > 0)
        {
           for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].Dead();
            }
        }
        onClear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enermy")
        {
            blockList.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enermy")
        {
            blockList.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void onClear()
    {
        rigidbody2D.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        renderer.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        drawTrail.ClearTrail();
    }

    protected override void Next()
    {
        base.Next();
        GameManager._instance.birdList.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
