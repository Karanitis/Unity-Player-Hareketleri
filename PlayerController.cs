using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private AudioSource playAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public ParticleSystem dirtyPartic;
    public ParticleSystem explosionPartic;
    private Animator playerAnim;
    public bool gameOver;
    private Rigidbody playerRB;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier;
    [SerializeField] bool isOnGround = true;
    Touch parmak;
    public TextMeshProUGUI bitti;

    // Start is called before the first frame update
    void Start()
    {
        
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playAudio = GetComponent<AudioSource>();
        // Doku();

    }

    // Update is called once per frame
    void Update()
    {
        Doku();

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {

            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtyPartic.Stop();
            playAudio.PlayOneShot(jumpSound, 1.0f);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtyPartic.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;

            Debug.Log("Oyun Bitti  !! ");
            
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            //playerAnim.SetInteger("DeathType_int", 2);
            explosionPartic.Play();
            dirtyPartic.Stop();
            playAudio.PlayOneShot(crashSound, 1.0f);
            bitti.gameObject.SetActive(true);
        }
    }

    public void Doku()
    {
        if (Input.touchCount > 0)
        {
            parmak = Input.GetTouch(0);
            if (parmak.phase == TouchPhase.Began && isOnGround && !gameOver)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtyPartic.Stop();
                playAudio.PlayOneShot(jumpSound, 1.0f);
            }


        }

    }
   

    
}
