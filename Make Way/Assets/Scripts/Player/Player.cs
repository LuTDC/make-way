using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    private float speed = 5f;

    private bool gotKey = false;
    private bool isDoor = false;

    private AudioManager audioManager;

    [SerializeField]
    private GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        audioManager = FindObjectOfType<AudioManager>();

        endScreen.SetActive(false);

        audioManager.Play("Static");
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        OpenDoor();
    }

    private void Walk(){
        float movex = Input.GetAxis("Horizontal"), movey = Input.GetAxis("Vertical");
        rigidBody.velocity = new Vector2(movex*speed, movey*speed);

        if(movex != 0 || movey != 0){
            animator.SetBool("Walk", true);

            if(!audioManager.Status("Walk")) audioManager.Play("Walk");
        }
        else{
            animator.SetBool("Walk", false);
            audioManager.Stop("Walk");
        }

        animator.SetFloat("MoveX", movex);
        animator.SetFloat("MoveY", movey);
    }

    private void OpenDoor(){
        if(gotKey && isDoor && Input.GetKeyDown("e")){
            gotKey = false;
            audioManager.Stop("Static");
            audioManager.Play("Door");

            endScreen.SetActive(true);
        }
    }

    public bool GetKeyStatus(){
        return gotKey;
    }

    public void NextLevel(){
        if(SceneManager.GetActiveScene().name != "Level05") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene("Credits");
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Key"){
            gotKey = true;
            audioManager.Play("Key");
            Destroy(other.gameObject);
        }
        else if(other.tag == "Door") isDoor = true;
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Door") isDoor = false;
    } 
}
