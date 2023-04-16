using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerActions : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent onDoubleClick;
    [SerializeField] GameEvent startDialogue;
    [SerializeField] GameEvent continueDialogue;
    [SerializeField] GameEvent stopDialogue;
    bool canPressContinue = false;
    string clickTag = "";

    private GameObject tempGameObj;

    void OnInteract()
    {
        Debug.Log("click click");
        DialogueInteraction();
    }

    private void DialogueInteraction()
    {
        if (clickTag == "dialogue"){
        canPressContinue = true;
        startDialogue.Raise(this, clickTag);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        clickTag = other.tag;
        tempGameObj = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        clickTag = "";
    }

    private IEnumerator ResetPress()
    {
        yield return new WaitForSeconds(1f);
        canPressContinue = true;
    }

}
