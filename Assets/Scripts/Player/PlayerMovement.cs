using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Animator playerAnim;
    private float moveSpeed;
    Rigidbody2D myRigidbody;

    Transform playerTransform;

    #region mouse params
    Vector2 lastClickPos;
    Vector2 oldPos;

    #endregion

    #region anim params
    bool isWalking;
    bool isMoving;
    bool canMove = true;
    bool isFront = true;
    bool horizontalFace;
    string walk = "isWalking";
    string dirc = "faceDirection";
    #endregion

    #region scale params
    [SerializeField] float scaleMulti;
    [SerializeField] GameObject vanishingPoint;

    Vector3 baseSize;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        moveSpeed = speed;
        baseSize = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        // playerAnim.SetBool(dirc, true);
    }

    // Update is called once per frame
    void Update()
    {
        ScalePlayer();
        if (canMove)
        {
            MovePlayer();
        }
    }

    void OnWalk(InputValue value)
    {
        Debug.Log("click");
        if (canMove)
        {
            oldPos = (Vector2)transform.position;
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
            FaceDirection();
        }
    }

    void MovePlayer()
    {
        if (isMoving && (float)transform.position.x != lastClickPos.x)
        {
            Debug.Log("move");
            playerAnim.SetBool(walk, true);
            float step = speed * Time.deltaTime;
            Vector2 goTo = new Vector2(lastClickPos.x, transform.position.y);
            // transform.position = Vector2.MoveTowards(transform.position, goTo, step);
            transform.position = Vector2.MoveTowards(transform.position, lastClickPos, step);

        }
        else
        {
            playerAnim.SetBool(walk, false);
            isWalking = false;
            isMoving = false;
        }
    }

    void ScalePlayer()
    {
        float newScale = Vector2.Distance(playerTransform.position, vanishingPoint.transform.position)*scaleMulti;
        newScale = Mathf.Clamp(newScale, 0.75f, 1f);
        // Debug.Log(newScale);
        playerTransform.localScale = new Vector3(baseSize.x * newScale, baseSize.y * newScale, baseSize.z * newScale);
    }


    public void RestorePlayer(Component sender, object data)
    {
        canMove = true;
        speed = moveSpeed;
    }

    public void ChangeLastMousePos(Component sender, object data)
    {
        Vector3 newpos = (Vector3)data;

        lastClickPos = newpos;
    }



    void FaceDirection()
    {
        float posX = oldPos.x - lastClickPos.x;
        float posY = oldPos.y - lastClickPos.y;
        if (Mathf.Abs(posX) >= Mathf.Abs(posY))
        {
            horizontalFace = true;
        }
        else
        {
            horizontalFace = false;
        }

        if (horizontalFace)
        {
            if (posX <= 0)
            {
                playerAnim.SetInteger(dirc, 0);
            }
            else
            {
                playerAnim.SetInteger(dirc, 1);
            }
        }
        else
        {
            if (posY >= 0)
            {
                playerAnim.SetInteger(dirc, 2);
            }
            else
            {
                playerAnim.SetInteger(dirc, 3);
            }
        }

    }
}
