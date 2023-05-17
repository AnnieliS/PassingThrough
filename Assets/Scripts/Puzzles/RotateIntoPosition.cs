using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIntoPosition : MonoBehaviour
{
    [SerializeField] GameObject correctPosition;
    GameObject positionCheck;
    Camera cam;
    Vector3 resetPosition;
    bool moving;
    bool finish = false;

    float startPosX;
    float startPosY;
    float angle;

    private void Start()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("puzzleCamera");
        positionCheck = GameObject.FindGameObjectWithTag("rotatePlaceCheck");
        cam = tmp.GetComponent<Camera>();
        resetPosition = this.transform.localPosition;
    }



    // Update is called once per frame
    void Update()
    {
        // Debug.Log("moving " + moving);
        // Debug.Log("rotation: " + this.transform.localRotation);
        if (moving && !finish)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            // Debug.Log("before screentoworldpoint" + mousePos);
            new WaitForEndOfFrame();
            Vector3 mousePos2 = cam.ScreenToWorldPoint(mousePos);
            // Debug.Log("after screentoworldpoint" + mousePos2);

            angle = Mathf.Atan2( mousePos2.x - this.transform.position.x, mousePos2.y - this.transform.position.y) * Mathf.Rad2Deg;
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -angle));


            // this.gameObject.transform.localPosition = new Vector3(mousePos2.x - startPosX, mousePos2.y - startPosY, this.gameObject.transform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse click click click");
            Vector3 mousePos;

            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            Debug.Log(mousePos);
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;

        // CheckIfOnTarget();
    }

    public void InPosition(){
        // Debug.Log("finished");
        finish = true;
    }

    public bool GetMoving(){
        return moving;
    }

    // private void CheckIfOnTarget()
    // {
    //     if (Mathf.Abs(positionCheck.transform.localPosition.x - correctPosition.transform.localPosition.x) <= 0.5f &&
    //         Mathf.Abs(positionCheck.transform.localPosition.y - correctPosition.transform.localPosition.y) <= 0.5)
    //     {
    //         this.transform.localPosition = new Vector3(correctPosition.transform.localPosition.x, correctPosition.transform.localPosition.y, correctPosition.transform.localPosition.z);
    //         finish = true;
    //     }
    //     else
    //     {
    //         this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
    //     }
    // }
}
