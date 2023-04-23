using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerActions : MonoBehaviour
{
    #region events
    [Header("Events")]
    [SerializeField] GameEvent onDoubleClick;
    [Header("Dialogue Events")]
    [SerializeField] GameEvent startDialogue;
    [SerializeField] GameEvent continueDialogue;
    [SerializeField] GameEvent stopDialogue;
    [Header("Movement Events")]
    [SerializeField] GameEvent pausePlayerMovement;
    [SerializeField] GameEvent resumePlayerMovement;
    [SerializeField] GameEvent resetMousePos;

    #endregion

    [Header("Params")]
    [SerializeField] float teleportShowTime = 0.2f;
    bool canPressContinue = false;
    string clickTag = "";

    private GameObject tempGameObj;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnInteract()
    {
        Debug.Log("click click");
        DialogueInteraction();
        Teleport();
    }

    #region collision functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        clickTag = other.tag;
        tempGameObj = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        clickTag = "";
    }

    #endregion

    #region Dialogue Function
    private void DialogueInteraction()
    {
        if (clickTag == "dialogue")
        {
            canPressContinue = true;
            startDialogue.Raise(this, clickTag);
            pausePlayerMovement.Raise(this, "");
        }

        if (canPressContinue)
        {
            canPressContinue = false;
            continueDialogue.Raise(this, clickTag);
            StartCoroutine(ResetPress());
        }

    }

    void OnStopDia()
    {
        stopDialogue.Raise(this, "");
    }


    private IEnumerator ResetPress()
    {
        yield return new WaitForSeconds(0.7f);
        canPressContinue = true;
    }

    #endregion

    private void Teleport()
    {
        if (clickTag == "teleport")
        {
            Vector3 newTeleportPosition = tempGameObj.GetComponent<Teleporter>().destinationPoint.transform.position;
            spriteRenderer.enabled = false;
            pausePlayerMovement.Raise(this, "");
            resetMousePos.Raise(this, newTeleportPosition);
            StartCoroutine(SpriteShowDelay(teleportShowTime));
            gameObject.transform.position = newTeleportPosition;
        }
    }

    private IEnumerator SpriteShowDelay(float time){
        yield return new WaitForSeconds(time);
        resumePlayerMovement.Raise(this, "");
        spriteRenderer.enabled = true;
    }


}
