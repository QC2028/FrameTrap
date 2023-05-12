using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerActions : MonoBehaviour //this class controls the players actions based on inputs that are passed in from the player controller
{
    public int inputReaderLength = 15; //how far back the inputs are held for special moves

    public float moveSpeed = 0.1f; //player movement multiplier

    public int HitStunDuration;
    public int HitStunFreezeDuration;
    public float HitStunKnockback;

    public int BlockStunDuration;
    public int BlockStunFreezeDuration;
    public float BlockStunKnockback;

    public int playerHealth = 10;

    public TextMeshProUGUI playerHealthUI;



    public List<InputData> inputHistory; //new list of inputdata struct
    List<InputData> inputReader;
    public InputData latestInput; //empty inputdata to hold latest input when recieved
    public bool isBusy = false; //used to check if player cannot do other actions because they are doing something else
    public bool isBlocking;
    public bool isHit;
    public Animator animator;

    public PlayerActions enemyPlayer;

    //SPRITES
    public SpriteRenderer SpriteRenderer; // sprite renderer for player
    public Sprite sprite5AStartup;
    public Sprite sprite5AActive;
    public Sprite sprite5ARecovery;

    public Sprite sprite5BStartup;
    public Sprite sprite5BActive;
    public Sprite sprite5BRecovery;

    public Sprite sprite5CStartup;
    public Sprite sprite5CActive;
    public Sprite sprite5CRecovery;

    public Sprite sprite236XStartup;
    public Sprite sprite236XActive;
    public Sprite sprite236XRecovery;

    public Sprite sprite214XStartup;
    public Sprite sprite214XActive;
    public Sprite sprite214XRecovery;

    public Sprite spriteBlocking;
    public Sprite spriteHitFreeze;
    public Sprite spriteHitStun;
    public Sprite spriteIdle;
    public Sprite spriteWalk1;
    public Sprite spriteWalk2;
    public Sprite spriteWalk3;

    //PLAYER EFFECTS
    public GameObject hitEffects;
    public GameObject blockEffects;
    public GameObject healEffects;

    //HITBOXES
    public GameObject hitbox5A;
    public GameObject hitbox5B;
    public GameObject hitbox5C;
    public GameObject hitbox236X;
    public GameObject hitbox214X;


    //STATES
    public PlayerBaseState currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerWalkState WalkState = new PlayerWalkState(); 
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerBlockState BlockState = new PlayerBlockState();
    public PlayerHitState HitState = new PlayerHitState();

    private void Awake()
    {
        inputHistory = new List<InputData>(); //new list of inputdata struct
        inputReader = new List<InputData>();
    }


    private void Start()
    {
        currentState = IdleState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        playerHealthUI.text = playerHealth.ToString();
    }

    private void FixedUpdate()
    {

        if(inputHistory.Count > inputReaderLength) 
        {
            latestInput = inputHistory[inputHistory.Count - 1];



            currentState.UpdateState(this);
        }
    }

    public void InputAction(InputData input) //pass in input data from player controller
    {
        inputHistory.Add(input); //add this struct to the input history
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}

