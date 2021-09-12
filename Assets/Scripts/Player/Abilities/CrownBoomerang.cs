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
    [SerializeField] Transform myCrownSprite;

    public Vector3 myCrownPoint;
    Vector3 myChargeBarOriginalPos;

    float myChargeUp;
    public float myMaxChargeUp;

    public CrownState myCrownState;

    private Player myPlayer;
    private void Start()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        myChargeBarOriginalPos = myChargeBar.transform.position;
    }
    void Update()
    {


        switch (myCrownState)
        {
            case CrownState.Default:

                if (myPlayer != null)
                {
                    if (myPlayer.myPlayerSprite.flipX) myCrown.localScale = new Vector3(-1, 1, 1);
                    if (!myPlayer.myPlayerSprite.flipX) myCrown.localScale = new Vector3(1, 1, 1);

                    myCrownSprite.localScale = new Vector3(1, 1, 1) * (1.0f + (myChargeUp / myMaxChargeUp) * 0.5f);
                }                                               

                myCrownSprite.up = Vector3.up;
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

            float x, y;
            x = Mathf.Sin(Time.time * 50) * (myChargeUp * 5);
            y = Mathf.Sin(Time.time * 100) * (myChargeUp * 5);

            myChargeBar.transform.localPosition = myChargeBarOriginalPos + new Vector3(x, y, 0);
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

        if(EnemyManager.Instance.CircleCollisionEnemy(myCrownSprite.position, 1))
        {
            myCrownState = CrownState.Returning;
        }

        if (dist <= 0.1f) myCrownState = CrownState.Returning;
    }
    public void ReturningCrownState()
    {
        float dist = Vector3.Distance(myCrown.position, myOrigin.position);

        myCrown.position = Vector3.MoveTowards(myCrown.position, myOrigin.position, 15 * Time.deltaTime);

        if (dist <= 1)
        {
            myCrown.parent = transform;
            myCrown.position = myOrigin.position;
            myCrownState = CrownState.Default;
        }
    }

    public void RotateCrown()
    {
        myCrownSprite.Rotate(Vector3.forward * 720 * Time.deltaTime);
    }
}
