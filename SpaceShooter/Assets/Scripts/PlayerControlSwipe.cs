using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSwipe : MonoBehaviour
{

    private Vector2 startTouchPosition, endTouchPosition;
    private Vector3 startRocketPosition, endRocketPosition;
    private float flytime;
    private float flightDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if ((endTouchPosition.x < startTouchPosition.x) && transform.position.x > -1.75f)
            {
                //transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
                StartCoroutine(Fly("left"));
            }

            if ((endTouchPosition.x > startTouchPosition.x) && transform.position.x < 1.75f)
            {
                //transform.position = new Vector2(transform.position.x + 1.75f, transform.position.y);
                StartCoroutine(Fly("right"));
            }
        }
    }

    private IEnumerator Fly(string wheretofly)
    {
        switch (wheretofly)
        {
            case "left":
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x - 1.75f, transform.position.y, transform.position.z);

                while(flytime < flightDuration)
                {
                    flytime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    yield return null;
                }
                break;
            case "right":
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x + 1.75f, transform.position.y, transform.position.z);

                while(flytime < flightDuration)
                {
                    flytime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    yield return null;
                }
                break;
        }
    }
}
