using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDropIntoPosition : MonoBehaviour
{
    [SerializeField] GameObject correctPosition;
    Camera cam;
    Vector3 resetPosition;
    bool moving;
    bool finish = false;

    float startPosX;
    float startPosY;

    private void Start()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("dragonDropCamera");
        cam = tmp.GetComponent<Camera>();
        resetPosition = this.transform.localPosition;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("moving " + moving);
        if (moving && !finish)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            Debug.Log("before screentoworldpoint" + mousePos);
            new WaitForEndOfFrame();
            Vector3 mousePos2 = cam.ScreenToWorldPoint(mousePos);
            Debug.Log("after screentoworldpoint" + mousePos2);


            this.gameObject.transform.localPosition = new Vector3(mousePos2.x - startPosX, mousePos2.y - startPosY, this.gameObject.transform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse click click click");
            Vector3 mousePos;

            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;

        CheckIfOnTarget();
    }

    private void CheckIfOnTarget()
    {
        if (Mathf.Abs(this.transform.localPosition.x - correctPosition.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctPosition.transform.localPosition.y) <= 0.5)
        {
            this.transform.localPosition = new Vector3(correctPosition.transform.localPosition.x, correctPosition.transform.localPosition.y, correctPosition.transform.localPosition.z);
            finish = true;
        }
        else
        {
                this.transform.localPosition = new Vector3 (resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }

}
