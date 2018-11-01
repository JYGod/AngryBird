using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrail : MonoBehaviour {

    public WeaponTrail WeaponTrail;

    private float t = 0.033f;
    private float tempT = 0f;
    private float animationIncrement = 0.003f;

    private void LateUpdate()
    {
        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0)
        {
            while (tempT < t)
            {
                tempT += animationIncrement;

                if (WeaponTrail.time > 0)
                {
                    WeaponTrail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    WeaponTrail.ClearTrail();
                }
            }

            tempT -= t;

            if (WeaponTrail.time > 0)
            {
                WeaponTrail.UpdateTrail(Time.time, t);
            }
        }
    }

    void Start()
    {
        // 默认没有拖尾效果
        WeaponTrail.SetTime(0.0f, 0.0f, 1.0f);
    }

    public void StartTrail()
    {
        //设置拖尾时长
        WeaponTrail.SetTime(2.0f, 0.0f, 1.0f);
        //开始进行拖尾
        WeaponTrail.StartTrail(0.5f, 0.4f);
    }

    public void ClearTrail()
    {
        //清除拖尾
        WeaponTrail.ClearTrail();
    }

}
