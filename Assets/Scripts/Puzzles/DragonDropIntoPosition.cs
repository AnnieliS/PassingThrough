using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDropIntoPosition : MonoBehaviour
{
    [SerializeField] GameObject correctPosition;
    bool moving;
    bool press;

    float startPosX;
    float startPosY;



    // Update is called once per frame
    void Update()
    {
        Debug.Log("moving " + moving);
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            new WaitForEndOfFrame();
            Vector3 mousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));
            Debug.Log(mousePos2);


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
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
        press = false;
    }

    public void PressPuzzlePiece(Component sender, object data)
    {
        press = true;
    }

}
