using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketThingy : MonoBehaviour
{
    [SerializeField] GameObject bucket;
    [SerializeField] GameObject water;
    [SerializeField] GameEvent startConvo;
    [SerializeField] GameEvent itempickup;
    [SerializeField] TextAsset toHeavyText;
    [SerializeField] CollectibleItem waterGlass;

    public void PutBucketDown()
    {
        bucket.SetActive(true);
        StartCoroutine("GetWater");

    }

    IEnumerator GetWater()
    {
        yield return new WaitForSeconds(1f);
        water.SetActive(true);
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            startConvo.Raise(this, "dialogue");
            DialogueManager.GetInstance().EnterDialogueMode(toHeavyText);

        }

        itempickup.Raise(waterGlass, "");


    }
}
