using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private bool startConvo;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnMouseEnter() {
        playerInRange = true;
    }

    private void OnMouseExit() {
        playerInRange = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void StartConversation(Component sender, object data)
    {
        if ((string)data == "dialogue")
            startConvo = true;
    }

    public void startStatueDialogue()
    {
        StartCoroutine(startDelay());
    }

    private IEnumerator startDelay()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.GetInstance().StartConversation(this, "dialogue");
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

}
