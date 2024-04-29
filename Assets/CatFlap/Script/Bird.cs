using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public event Action OnTriggerRightCollider;
    public event Action OnTriggerLeftCollider;

    private static Bird my_instance;
    private const float jumpForce = 4.5f;
    private Rigidbody2D bird_RB;
    private BirdState birdState;
    private SpriteRenderer spriteRenderer;
    private bool isBackwardJump = false;

    public ScoreManager scoreManager;
    public static Bird GetInstance { get { return my_instance; } }
    public event EventHandler OnDied;
    public event EventHandler OnStartFly;

    [SerializeField] private Sprite flyCat_Sprite;
    [SerializeField] private Sprite defaultCat_Sprite;

    private enum BirdState
    {
        WaitToFly, Fly, Die
    }
    private void Awake()
    {
        my_instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bird_RB = GetComponent<Rigidbody2D>();
        bird_RB.bodyType = RigidbodyType2D.Static;
        birdState = BirdState.WaitToFly;

    }
    void Update()
    {
        switch (birdState)
        {

            case BirdState.WaitToFly:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    birdState = BirdState.Fly;
                    bird_RB.bodyType = RigidbodyType2D.Dynamic;
                    spriteRenderer.sprite = flyCat_Sprite;
                    Jump();
                    if (OnStartFly != null) OnStartFly(this, EventArgs.Empty);
                }
                break;

            case BirdState.Fly:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    spriteRenderer.sprite = flyCat_Sprite;

                    if(isBackwardJump == true) BackwardJump();

                    else if(isBackwardJump == false) Jump();

                }
                else if (Input.GetMouseButtonUp(0))
                {
                    spriteRenderer.sprite = defaultCat_Sprite;
                }
                break;

            case BirdState.Die:
                break;
        }

    }

    private void Jump()
    {
        if (bird_RB.bodyType != RigidbodyType2D.Static)
        {
            bird_RB.velocity = new Vector2(2.5f,jumpForce);
            //SoundManager.GetInstance.PlaySound(SoundManager.audioClipEnum.birdJump);
        }
    }
    private void BackwardJump()
    {
        if(bird_RB.bodyType != RigidbodyType2D.Static)
        {
            bird_RB.velocity = new Vector2(-2.5f, jumpForce);
            //SoundManager.GetInstance.PlaySound(SoundManager.audioClipEnum.birdJump);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bird_RB.bodyType = RigidbodyType2D.Static;
        if (collision.CompareTag("Right_Collider"))
        {
            scoreManager.AddScore(1);
            OnTriggerRightCollider?.Invoke();

            if (isBackwardJump == false)
            {
                isBackwardJump = true;
                this.transform.localScale = new Vector3(-0.1f, 0.1f, 0);
                BackwardJump();
            }

            else
            {
                isBackwardJump = false;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0);
                Jump();
            };
        }
        else if (collision.CompareTag("Left_Collider"))
        {
            scoreManager.AddScore(1);
            OnTriggerLeftCollider?.Invoke();

            if (isBackwardJump == false)
            {
                isBackwardJump = true;
                this.transform.localScale = new Vector3(-0.1f, 0.1f, 0);
                BackwardJump();
            }

            else
            {
                isBackwardJump = false;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0);
                Jump();
            };
        }
        if (collision.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Game Over");
        }
       /* SoundManager.GetInstance.PlaySound(SoundManager.audioClipEnum.dead);
        if (OnDied != null) OnDied(this, EventArgs.Empty);*/
    }
}
