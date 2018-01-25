using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private int count;
    private bool end;
    private int score;
    private float time;
    private float minutes;
    private float seconds;
    private AudioSource audioSource;
    private Vector2 lastDirection;
    private int health;
    private bool clicked;

    public float speed;
    public AudioClip Gold; //Audio
    public AudioClip DoorSound; //Audio
    public AudioClip ChaliceSound; //Audio
    public AudioClip DoorUnlock; //Audio
    public AudioClip Stab; //Audio
    public Text collectedText; //UI
    public Text scoreText; //UI
    public Text timer; //UI
    public GameObject blue; //UI
    public GameObject green; //UI
    public GameObject red; //UI
    public GameObject five; //UI
    public GameObject four; //UI
    public GameObject three; //UI
    public GameObject two; //UI
    public GameObject one; //UI
    public GameObject button; //UI
    public GameObject Win; //UI

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        //score = GlobalController.Instance.score;
        speed = 5f;
        blue.SetActive(false);
        green.SetActive(false);
        red.SetActive(false);
        end = false;
        health = 100;
        button.SetActive(false);
        Win.SetActive(false);

        SetCollected();
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            time += Time.deltaTime;
            minutes = (int)time / 60;
            seconds = (int)time % 60;
            timer.text = minutes.ToString() + " : " + seconds.ToString("F0");
        } else
        {
            timer.color = Color.yellow;
        }

        if(health <= 0)
        {
            one.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
        else if (health == 20)
        {
            two.SetActive(false);
        }
        else if (health == 40)
        {
            three.SetActive(false);
        }
        else if (health == 60)
        {
            four.SetActive(false);
        }
        else if (health == 80)
        {
            five.SetActive(false);
        }     
    }

    void FixedUpdate()
    {
        anim.SetBool("isMoving", false);
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if(moveHorizontal != 0 || moveVertical != 0)
        {
            anim.SetBool("isMoving", true);
            lastDirection = rb.velocity;
        }
        
        movement.Normalize(); //No Diagonal Acceleration 

        rb.velocity = Vector2.zero; //Reset each call
        rb.AddForce(movement * speed, ForceMode2D.Impulse); //Impulse instant, no acceleration

        SendAnimatorInfo();
    }

    void OnTriggerEnter2D(Collider2D Object)
    {
        //Handle collisions
        if(Object.gameObject.CompareTag("BlueKey"))
        {
            Object.gameObject.SetActive(false);
            count++;
            blue.SetActive(true);
            audioSource.PlayOneShot(DoorUnlock, 0.7f);

            score += 100;
            SetCollected();
        }
        else if(Object.gameObject.CompareTag("GreenKey"))
        {
            Object.gameObject.SetActive(false);
            count++;
            green.SetActive(true);
            audioSource.PlayOneShot(DoorUnlock, 0.7f);

            score += 100;
            SetCollected();
        }
        else if(Object.gameObject.CompareTag("RedKey"))
        {
            Object.gameObject.SetActive(false);
            count++;
            red.SetActive(true);
            audioSource.PlayOneShot(DoorUnlock, 0.7f);

            score += 100;
            SetCollected();
        }
        else if(Object.gameObject.CompareTag("Chalice"))
        {
            Object.gameObject.SetActive(false);
            score += 150;
            audioSource.PlayOneShot(ChaliceSound, 0.7f);
            SetCollected();
        } 
        else if(Object.gameObject.CompareTag("Door"))
        {
            audioSource.PlayOneShot(DoorSound, 1f);
        }
        else if(Object.gameObject.CompareTag("Exit"))
        {
            end = true;
            Win.SetActive(true);
            Time.timeScale = 0; //Freeze Game
            button.SetActive(true);
        }
        else if (Object.gameObject.CompareTag("Background"))
        {
            end = true;
            //GlobalController.Instance.score = score;
            button.SetActive(true);
        }
        else if(Object.gameObject.CompareTag("Spikes"))
        {            
            if (health != 0)
            {
                audioSource.PlayOneShot(Stab, 0.9f);
            }
            health -= 20;
            score -= 50;
        }
        else if (Object.gameObject.CompareTag("Gold"))
        {
            audioSource.PlayOneShot(Gold, 0.7f);
            score += 1000;
            Object.gameObject.SetActive(false);
        }
    }

    void SetCollected()
    {
        collectedText.text = "You have collected " + count.ToString() + " of the keys";
        scoreText.text = "$" + score.ToString();
    }

    void SendAnimatorInfo()
    {
        anim.SetFloat("X", rb.velocity.x);
        anim.SetFloat("Y", rb.velocity.y);
        anim.SetFloat("_X", lastDirection.x);
        anim.SetFloat("_Y", lastDirection.y);
    }
}
