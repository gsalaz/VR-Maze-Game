using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float forceAmount;
    public Transform camera;
    private Rigidbody rb;
    float cam_h, cam_v;
    private float horizontalAxisValue, verticalAxisValue;

    float horizontalSpeed = 3.0f;
	float verticalSpeed = 3.0f;

    public bool cubeIsOnTheGround = true;

    public Rigidbody barrel;
    public int numOfBarrels = 0;

    public Rigidbody ball;
    private AudioSource whoosh;
    public BackgroundMusic backgroundMusic;

    private bool paused;
    public GameObject heartCanvas;
    public GameObject pausedScreen;
    public GameObject gameOverScreen;
    public GameObject scoreScreen;

    public UnityEvent endGame;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        whoosh = GetComponent<AudioSource>();

        backgroundMusic = FindObjectOfType<BackgroundMusic>();
        backgroundMusic.bg.volume = 0.25F;

        Time.timeScale = 1;
        paused = false;

        numOfBarrels = 0;
        BarrellHit.numHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
    	cam_h += Input.GetAxis("Mouse X") * horizontalSpeed;
        cam_v += Input.GetAxis("Mouse Y") * verticalSpeed;

        horizontalAxisValue = Input.GetAxis("Horizontal");
        verticalAxisValue = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && cubeIsOnTheGround) {
        	rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        	cubeIsOnTheGround = false;
        }
        
        if(Input.GetMouseButtonDown(1) && !paused) { //TO SPAWN BARREL
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Rigidbody clone;
                clone = Instantiate(barrel, hit.point, Quaternion.identity);
                numOfBarrels++;
            }
            else {
                Rigidbody clone;
                clone = Instantiate(barrel, hit.point + new Vector3(0,0,5), Quaternion.identity);
                numOfBarrels++;
            }

        }

        if (Input.GetMouseButtonDown(0) && !paused) { //TO SHOOT

            Rigidbody clone;

            clone = Instantiate(ball, camera.position + camera.forward, Quaternion.identity);
            clone.AddForce(camera.forward * 500);
            whoosh.Play();

            Destroy(clone.gameObject, 5);
            Destroy(clone, 5);
        }

        if (numOfBarrels != 0 && numOfBarrels == BarrellHit.numHit)
        {
            paused = true;
            Time.timeScale = 0;
            heartCanvas.SetActive(false);
            gameOverScreen.SetActive(true);
            scoreScreen.SetActive(false);
            StartCoroutine(PauseGame(3F));
           
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
         
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
                heartCanvas.SetActive(false);
                scoreScreen.SetActive(false);
                pausedScreen.SetActive(true);

            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                heartCanvas.SetActive(true);
                scoreScreen.SetActive(true);
                pausedScreen.SetActive(false);
            }
            
        }

    }

    public IEnumerator PauseGame(float pauseTime)//pause game
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        backgroundMusic.bg.volume = 0.5F;

        endGame.Invoke();
    }

    private void FixedUpdate()
    {
        camera.eulerAngles = (new Vector3(cam_v * (-1), cam_h, 0));

        rb.AddForce(camera.right * horizontalAxisValue * forceAmount +
              camera.forward * verticalAxisValue * forceAmount);
      
    }

    private void OnCollisionEnter(Collision collision) {
    	if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Object") {
    		cubeIsOnTheGround = true;
    	}
    }
}
