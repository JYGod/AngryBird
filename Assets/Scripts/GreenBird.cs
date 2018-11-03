using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {

    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rigidbody2D.velocity;
        speed.x *= -1;
        rigidbody2D.velocity = speed;
    }
}
