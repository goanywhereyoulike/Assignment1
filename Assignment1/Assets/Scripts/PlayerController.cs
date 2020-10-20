using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    static PlayerController Instance;
    public enum JumpState
    {
        Grounded,
        Jumping,
        DoubleJumping

    }
    public float speed = 10.0f;
    public float jumpForce = 100.0f;
    public int playerPoints = 0;
    //private int Level = 1;
    //private bool IsOntheGround = true;
    private Rigidbody rb;
    private JumpState _jumpState = JumpState.Grounded;

    public GameObject uiManager;
    public GameObject levManager;
    // Start is called before the first frame update
    private void Awake()
    {
        UIManager uiMngr = uiManager.GetComponent<UIManager>();
        LevelManager lvMngr = levManager.GetComponent<LevelManager>();
        ServiceLocator.Register<UIManager>(uiMngr);
        ServiceLocator.Register<LevelManager>(lvMngr);


        DontDestroyOnLoad(uiMngr);
        DontDestroyOnLoad(lvMngr);
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        if (transform.position.y < -10.0f)
        {
            ServiceLocator.Get<UIManager>().SetEndUi(0);
            Invoke("StopTheGame", 0.3f);
        }
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        float moveHorizaontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizaontal * speed, 0.0f, moveVertical * speed);

        rb.AddForce(movement);


    }
    private void Jump()
    {
        float JumpVal = Input.GetKeyDown(KeyCode.Space) ? jumpForce : 0.0f;

        switch (_jumpState)
        {
            case JumpState.Grounded:
                if (JumpVal > 0.0f)
                {
                    _jumpState = JumpState.Jumping;
                }
                break;
            case JumpState.Jumping:
                if (JumpVal > 0.0f)
                {
                    _jumpState = JumpState.DoubleJumping;
                }
                break;
            case JumpState.DoubleJumping:
                JumpVal = 0.0f;
                break;

        }
        Vector3 jump = new Vector3(0.0f, JumpVal, 0.0f);
        rb.AddForce(jump);

    }
    private void OnCollisionEnter(Collision collision)
    {

        if ((_jumpState == JumpState.Jumping|| _jumpState==JumpState.DoubleJumping) && collision.gameObject.CompareTag("Ground"))
        {
            _jumpState = JumpState.Grounded;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUp pickup = other.gameObject.GetComponent<PickUp>();
            if (pickup!=null)
            {
                playerPoints += pickup.Collect();
                ServiceLocator.Get<UIManager>().UpdateScoreDisplay(playerPoints);
            }
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            ServiceLocator.Get<LevelManager>().Level++;
            if (ServiceLocator.Get<LevelManager>().Level == 2)
            {
                ServiceLocator.Get<UIManager>().SetEndUi(1);
            }
            else if(ServiceLocator.Get<LevelManager>().Level >= 3)
            {
                ServiceLocator.Get<UIManager>().SetEndUi(2);
            }
            
            
            //Invoke("StopTheGame",0.3f);
            StartCoroutine(LoadScene());

        }
    }
    private void StopTheGame()
    {
        Time.timeScale = 0;

    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3.0f);
        Time.timeScale = 1;
        if (ServiceLocator.Get<LevelManager>().Level == 2)
        {
            ServiceLocator.Get <LevelManager>().LoadScene("Level2");
            //SceneManager.LoadScene("Level2");
            transform.position = new Vector3(0, 0.5f, 0);
           
            ServiceLocator.Get<UIManager>().SetUI();


        }

        else if(ServiceLocator.Get<LevelManager>().Level >= 3)
        {
            //ServiceLocator.Get<UIManager>().SetUI();
            Time.timeScale = 0;
            
        }


    }



}
