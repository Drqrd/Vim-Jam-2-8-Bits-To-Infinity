using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public enum CameraBehavior
    {
        FollowTarget,
        FreeCam
    }

    public CameraBehavior myCameraBehavior;

    public Transform Target;

    public float mySpeed;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (myCameraBehavior == CameraBehavior.FollowTarget) myCameraBehavior = CameraBehavior.FreeCam;
            else if (myCameraBehavior == CameraBehavior.FreeCam) myCameraBehavior = CameraBehavior.FollowTarget;

        }

        switch (myCameraBehavior)
        {
            case CameraBehavior.FollowTarget:

                if (Target != null) transform.position = Vector3.Lerp(transform.position, Target.transform.position + new Vector3(0, 0, -10), Time.deltaTime * mySpeed);

                break;
            case CameraBehavior.FreeCam:

                float x, y;
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    transform.position += new Vector3(x, y, 0) * Time.deltaTime * mySpeed * 2.0f;
                }
                else
                {
                    transform.position += new Vector3(x, y, 0) * Time.deltaTime * mySpeed;
                }
                break;
        }
    }
}
