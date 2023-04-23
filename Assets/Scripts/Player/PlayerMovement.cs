using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private float moveSpeed;
    Animator playerAnim;
    Rigidbody2D myRigidbody;
    Vector2 lastClickPos;
    Vector2 oldPos;
    bool isWalking;
    bool isMoving;
    bool canMove = true;
    bool isFront = true;

    #region anim params
    string walk = "isWalking";
    string dirc = "isFront";
    #endregion


    // Start is called before the first frame update
    void Awake()
    {
        // playerAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = speed;
        // playerAnim.SetBool(dirc, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovePlayer();
            // FlipSprite();
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
        }
    }

    void MovePlayer()
    {
        if (isMoving && (float)transform.position.x != lastClickPos.x)
        {
            Debug.Log("move");
            // playerAnim.SetBool(walk, true);
            float step = speed * Time.deltaTime;
            Vector2 goTo = new Vector2(lastClickPos.x, transform.position.y);
            // transform.position = Vector2.MoveTowards(transform.position, goTo, step);
            transform.position = Vector2.MoveTowards(transform.position, lastClickPos, step);

        }
        else
        {
            // playerAnim.SetBool(walk, false);
            isWalking = false;
            isMoving = false;
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(-Mathf.Sign(transform.position.x - oldPos.x), 1f);
        if (oldPos.y - lastClickPos.y > 0)
        {
            playerAnim.SetBool(dirc, true);
        }
        else
        {
            playerAnim.SetBool(dirc, false);
        }
    }

    public void StopPlayer(Component sender, object data)
    {
        canMove = false;
        speed = 0f;
    }

    public void RestorePlayer(Component sender, object data)
    {
        canMove = true;
        speed = moveSpeed;
    }

    public void ChangeLastMousePos(Component sender, object data){
        Vector3 newpos = (Vector3)data;

        lastClickPos = newpos;
    }


}
