using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kick : MonoBehaviour
{
    public Rigidbody ball;
    public Transform startingPoint;
    public Transform angleMeter;
    public Transform powerMeter;
    public RawImage[] scoredLights;
    public RawImage[] failedLights;
    private float shots=5;
    private int attempt = 1;
    private float power = 0;
    public float forwardForce = 600;
    private float angle = 0;
    private bool kickable = false;
    private bool anglePhase = true;
    private bool powerPhase = false;
    private bool actionTurn = true;
    private float sidePos = 0;
    private IEnumerator kicks;
    private IEnumerator announceRounds;
    private bool goal = false;
    public Text attemptText;
    // Start is called before the first frame update
    void Start()
    {
        announceRounds = announceRound();
        StartCoroutine(announceRounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (attempt<=shots)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (anglePhase)
                {
                    angle = Mathf.Sin(Time.time) * 400;
                    anglePhase = false;
                    powerPhase = true;                  
                }
                else if (powerPhase)
                {
                    power = 250 + Mathf.Sin(Time.time) * 350;
                    powerPhase = false;
                    kicks = kick();
                    StartCoroutine(kicks);
                }
            }

            if (anglePhase)
            {
                angleMeter.localPosition = new Vector3(Mathf.Sin(Time.time)*160 + 1f, angleMeter.localPosition.y, angleMeter.localPosition.z);
            }

            if (powerPhase)
            {
                powerMeter.localPosition = new Vector3(powerMeter.localPosition.x, -10f + Mathf.Sin(Time.time)*160, powerMeter.localPosition.z);
            }
        }
        

    }

    public void scoreGoal()
    {
        if (!goal)
        {
            scoredLights[attempt - 1].enabled = true;
            goal = true;
        }
    }

    private void reset()
    {
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        ball.transform.position = startingPoint.position;
        ball.transform.rotation = startingPoint.rotation;
        anglePhase = true;

        Debug.Log(attempt);
        if (goal)
        {
            goal = false;
        }
        else
        {
            failedLights[attempt - 1].enabled = true;
        }

        attempt++;

        if (attempt <= shots)
        {
            attemptText.text = "INTENTO " + attempt;
            announceRounds = announceRound();
            StartCoroutine(announceRounds);
        }else
        {
            Debug.Log("END GAME");
        }
    }

    IEnumerator announceRound()
    {
        attemptText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        attemptText.enabled = false;
    }

    IEnumerator kick()
    {
        ball.AddForce(angle, power, forwardForce);
        yield return new WaitForSeconds(5.0f);
        reset();
    }
}