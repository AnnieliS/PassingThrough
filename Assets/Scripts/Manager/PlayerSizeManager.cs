using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeManager : MonoBehaviour
{
    [SerializeField] Transform player;
    List<PlayerScalePoint> scalepoints = new List<PlayerScalePoint>();

    Vector3 baseSize;
    PlayerScalePoint closestScale;
    void Start()
    {
        GameObject[] tempObject;
        tempObject = GameObject.FindGameObjectsWithTag("scalePoint");

        for (int i = 0; i < tempObject.Length; i++)
        {
            scalepoints.Add(tempObject[i].GetComponent<PlayerScalePoint>());
        }

        baseSize = player.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = Mathf.Infinity;

        float newScale = 1f;

        foreach (PlayerScalePoint scalepoint in scalepoints)
        {
            float tempDist = Vector2.Distance(player.position, scalepoint.transform.position);
            if (tempDist < minDistance)
            {
                minDistance = tempDist;
                closestScale = scalepoint;
            }
        }

        float multi = 2f;
        if(minDistance < 1f)
        multi = minDistance*2;

        if (closestScale.scale > 1)
        {
            newScale = Mathf.Clamp(closestScale.scale/multi , 1f, closestScale.scale);

            player.localScale = new Vector3(baseSize.x * newScale, baseSize.y * newScale, baseSize.z * newScale);
        }

        else
        {
            newScale = Mathf.Clamp(closestScale.scale*multi , closestScale.scale, 1f);

            player.localScale = new Vector3(baseSize.x * newScale, baseSize.y * newScale, baseSize.z * newScale);
        }

    }
}
