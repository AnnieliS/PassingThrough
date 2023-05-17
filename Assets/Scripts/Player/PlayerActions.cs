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
    [Header("Items Events")]
    [SerializeField] GameEvent tvChannelSurf;
    [SerializeField] GameEvent puzzle;

    #endregion

    [Header("Params")]
    [SerializeField] float teleportShowTime = 0.2f;
    [SerializeField] float dialogueContinueTime = 0.5f;

    #region dialogue params
    bool canPressContinue = false;
    bool insideDialogue = false;

    #endregion
    string clickTag = "";

    private GameObject tempGameObj;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Debug.Log(clickTag);
    }
    void OnInteract()
    {
        
        DialogueInteraction();
        // Teleport();
        ItemPickup();
        TVClick();
        Puzzle();
    }

    void OnSingleClick()
    {
        DialogueContinue();
    }

    #region collision functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("enter " + other.tag);
        clickTag = other.tag;
        tempGameObj = other.gameObject;
        if(clickTag == "ui"){
            pausePlayerMovement.Raise(this, tempGameObj);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("exit " + other.tag);
        if(other.tag == "ui"){
            resumePlayerMovement.Raise(this, "");
        }
        clickTag = "";
        
    }

    #endregion

    #region Dialogue Function
    private void DialogueInteraction()
    {
        if (clickTag == "dialogue" && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("start dialogue 1");
            pausePlayerMovement.Raise(this, "");
            StartCoroutine(ResetPress());
            startDialogue.Raise(this, clickTag);
        }



    }

    private void DialogueContinue()
    {
        if (canPressContinue)
        {
            // Debug.Log("continue");
            canPressContinue = false;
            continueDialogue.Raise(this, clickTag);
            StartCoroutine(ResetPress());
        }
    }

    void OnStopDia()
    {
        if (insideDialogue)
        {
            stopDialogue.Raise(this, "");
        }
    }


    private IEnumerator ResetPress()
    {
        yield return new WaitForSeconds(dialogueContinueTime);
        canPressContinue = true;
    }

    public void DisableDialogueBool(){
        insideDialogue = false;
    }

    #endregion

    #region teleportation functions

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

    private IEnumerator SpriteShowDelay(float time)
    {
        yield return new WaitForSeconds(time);
        resumePlayerMovement.Raise(this, "");
        spriteRenderer.enabled = true;
    }

    #endregion

#region items functions


void ItemPickup(){
    if(clickTag == "item"){
        tempGameObj.GetComponent<CollectibleItem>().PickupItem();
    }
}

void TVClick(){
    if(clickTag == "tv"){
        tvChannelSurf.Raise(this, "");
    }
}

void Puzzle(){
    if(clickTag == "puzzle"){
        puzzle.Raise(this, tempGameObj);
        clickTag = "";
        
    }
}

public void DetectOverObject(Component sender, object data){
    string tmptag = (string)data;
    clickTag = tmptag;
    tempGameObj = sender.gameObject;
}

#endregion

}
