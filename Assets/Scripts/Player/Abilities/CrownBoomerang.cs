using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrownBoomerang : MonoBehaviour
{
    public enum CrownState
    {
        Default,
        Returning,
        Thrown
    }

    [SerializeField] Transform myOrigin;
    [SerializeField] Image myChargeBar;
    [SerializeField] Transform myCrown;

    public Vector3 myCrownPoint;

    float myChargeUp;
    public float myMaxChargeUp;

    public CrownState myCrownState;

    void Update()
    {
        switch(myCrownState)
        {
            case CrownState.Default:
                myCrown.up = Vector3.up;
                DefaultCrownState();
                break;
            case CrownState.Thrown:
                RotateCrown();
                ThrownCrownState();
                break;
            case CrownState.Returning:
                RotateCrown();
                ReturningCrownState();
                break;
        }

    }

    public void DefaultCrownState()
    {
        Vector3 mousePos = Input.mousePosition;
        myCrownPoint = Camera.main.ScreenToWorldPoint(mousePos);
        myCrownPoint.z = 0;

        if (Input.GetMouseButton(0))
        {
            myChargeBar.fillAmount = (float)myChargeUp / (float)myMaxChargeUp;

            if (myChargeUp < myMaxChargeUp) myChargeUp += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (myChargeUp >= myMaxChargeUp)
            {
                myCrownState = CrownState.Thrown;
                Debug.Log("Throw crown");
            }

            myChargeUp = 0;
            myChargeBar.fillAmount = 0;
        }
    }
    public void ThrownCrownState()
    {
        float dist = Vector3.Distance(myCrown.position, myCrownPoint);

        myCrown.transform.parent = null;
        myCrown.position = Vector3.Lerp(myCrown.position, myCrownPoint, 5 * Time.deltaTime);

        if (dist <= 0.1f) myCrownState = CrownState.Returning;
    }
    public void ReturningCrownState()
    {
        float dist = Vector3.Distance(myCrown.position, myOrigin.position);

        myCrown.position = Vector3.MoveTowards(myCrown.position, myOrigin.position, 15 * Time.deltaTime);

        if(dist <= 1)
        {
            myCrown.parent = transform;
            myCrown.position = myOrigin.position;
            myCrownState = CrownState.Default;
        }
    }

    public void RotateCrown()
    {
        myCrown.Rotate(Vector3.forward * 720 * Time.deltaTime);
    }
}
