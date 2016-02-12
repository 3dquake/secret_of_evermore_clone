using System;
using System.Collections.Generic;

using UnityEngine;

public class CameraStateFollow : CameraState
{
    public CameraStateFollow(float lerp, float slerp, float miny, float maxy)
    {
        movementSmooth = lerp;
        rotationSmooth = slerp;
        MinY = miny;
        MaxY = maxy;
    }

    public float movementSmooth
    {
        get
        {
            return m_lerp;
        }
        set
        {
            m_lerp = Mathf.Clamp(value, 0f,1f);
        }
    }
    float m_lerp;

    public float rotationSmooth
    {
        get
        {
            return m_slerp;
        }
        set
        {
            m_slerp = Mathf.Clamp(value, 0f, 1f);
        }
    }
    float m_slerp;

    Vector3 m_constPosition = new Vector3(0, 15, -15);

    public float MinY { get; set; }
    public float MaxY { get; set; }

    void LookAt(GameObject target)
    {
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.LookRotation((target.transform.position - camera.transform.position).normalized), m_slerp);
    }

    void Follow(GameObject target)
    {
        camera.transform.position = ClampY(Vector3.Lerp(camera.transform.position, target.transform.position + m_constPosition/* * Distance*/, m_lerp), MinY, MaxY);
        //camera.transform.position =  ;
    }

    Vector3 ClampY(Vector3 value, float min, float max)
    {
        Vector3 result = value;
        result.y = Mathf.Clamp(value.y, min, max);
        return result;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Think()
    {
        if (target != GameManager.Instance.Camera.currentTarget)
            target = GameManager.Instance.Camera.currentTarget;

        if (target)
        {
            LookAt(target);
            Follow(target);
        }

    }
}

