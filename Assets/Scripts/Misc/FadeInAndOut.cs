using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{

    [SerializeField] float fadeInSpeed;
    [SerializeField] float fadeOutSpeed;
    [SerializeField] float stayTime;
    Image image;
    private bool fadeOut, fadeIn, wait, ongoing;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ongoing)
        {
            ongoing = true;
            FadeIn();
        }
        if (fadeIn)
        {
            float fadeAmount = image.color.a + (fadeInSpeed * Time.deltaTime);

            Color newcolor = new Color(image.color.r, image.color.g, image.color.b, fadeAmount);
            image.color = newcolor;

            if (image.color.a >= 1)
            {
                fadeIn = false;
                wait = true;
            }

        }
        if (wait)
        {
            StartCoroutine("WaitForFadeout");
        }
        if (fadeOut)
        {
            float fadeAmount = image.color.a - (fadeOutSpeed * Time.deltaTime);

            Color newcolor = new Color(image.color.r, image.color.g, image.color.b, fadeAmount);
            image.color = newcolor;

            if (image.color.a <= 0)
            {
                fadeOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    void FadeIn()
    {
        fadeIn = true;
    }

    IEnumerator WaitForFadeout()
    {
        wait = false;
        yield return new WaitForSeconds(stayTime);
        fadeOut = true;
    }

    public void Activate(Sprite item){
        image.sprite = item;
        ongoing = false;
    }


}