using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    int startupFramesCounter;
    int activeFramesCounter;
    int recoveryFramesCounter;
    GameObject hitbox;
    float moveDistance;
    float recoilDistance;

    bool isHealing;

    bool canDo5A;
    bool canDo5B;
    bool canDo5C;
    bool canDoSpecial;

    List<Vector2> inputReader;
    List<Vector2> qcf; 
    List<Vector2> qcb; 

    int BlockStunFreezeCounter;
    int HitStunFreezeCounter;

    Vector3 playerPos;
    Vector3 startupVector;
    Vector3 recoveryVector;

    Sprite StartupSprite;
    Sprite ActiveSprite;
    Sprite RecoverySprite;

    public override void EnterState(PlayerActions player)
    {
        
        if(player.latestInput.playerIndex == 0)
        {
            qcf = new List<Vector2> { new Vector2(1.00f, 0.00f), new Vector2(1.00f, -1.00f), new Vector2(0.00f, -1.00f) };
            qcb = new List<Vector2> { new Vector2(-1.00f, 0.00f), new Vector2(-1.00f, -1.00f), new Vector2(0.00f, -1.00f) };
        }
        else
        {
            qcf = new List<Vector2> { new Vector2(-1.00f, 0.00f), new Vector2(-1.00f, -1.00f), new Vector2(0.00f, -1.00f) };
            qcb = new List<Vector2> { new Vector2(1.00f, 0.00f), new Vector2(1.00f, -1.00f), new Vector2(0.00f, -1.00f) };
        }


        canDo5A = true; canDo5B = true; canDo5C = true; canDoSpecial = true;
        inputReader = new List<Vector2>();
        Debug.Log("attack state entered");

        //check for which button was pressed
        CheckButton(player);
    }

    public override void UpdateState(PlayerActions player)
    {
        Debug.Log(hitbox.activeInHierarchy);
        if (startupFramesCounter > 0)
        {
            player.SpriteRenderer.sprite = StartupSprite;

            if(player.latestInput.playerIndex== 0)
            {
                player.transform.position += startupVector;
            }
            else
            {
                player.transform.position -= startupVector;
            }

            
            startupFramesCounter--;
            Debug.Log(startupFramesCounter + " frames left of startup frames");
        }
        else if (activeFramesCounter > 0)
        {
            hitbox.SetActive(true);
            player.SpriteRenderer.sprite = ActiveSprite;
            activeFramesCounter--;
            Debug.Log(activeFramesCounter + " frames left of active frames");
            if (player.enemyPlayer.currentState == player.enemyPlayer.BlockState)
            {
                activeFramesCounter = 0; //move out of active frames state
                BlockStunFreezeCounter = player.BlockStunFreezeDuration - 1 ;
                recoveryVector = new Vector3(recoilDistance, 0, 0); //apply recoil distance if opponent blocks
            }

            if (player.enemyPlayer.currentState == player.enemyPlayer.HitState)
            {
                activeFramesCounter = 0; //move out of active frames state
                HitStunFreezeCounter = player.HitStunFreezeDuration - 1;
            }
        }
        else if (BlockStunFreezeCounter > 0) //in blockstun frames
        {
            hitbox.SetActive(false);
            CheckButton(player);
            BlockStunFreezeCounter--;
            Debug.Log(BlockStunFreezeCounter + " frames left of attacking blockstun freeze");

            if (isHealing && BlockStunFreezeCounter > 0)
            {
                player.healEffects.SetActive(true);
                if(player.playerHealth < 20)
                {
                    player.playerHealth++; 
                }
                isHealing = false;
            }
        }
        else if (HitStunFreezeCounter > 0)
        {
            hitbox.SetActive(false);
            CheckButton(player);
            HitStunFreezeCounter--;
            Debug.Log(HitStunFreezeCounter + " frames left of attacking hitstun freeze");
        }
        else if(recoveryFramesCounter > 0)
        {
            isHealing = false;
            player.healEffects.SetActive(false);
            player.SpriteRenderer.sprite = RecoverySprite;
            hitbox.SetActive(false);

            if(player.latestInput.playerIndex== 0)
            {
                player.transform.position -= recoveryVector;
            }
            else
            {
                player.transform.position += recoveryVector;
            }

            recoveryVector.x = (recoveryVector.x) / 2;

            recoveryFramesCounter--;
            Debug.Log(recoveryFramesCounter + " frames left of recovery frames");
        }
        else
        {
            player.healEffects.SetActive(false);

            player.SwitchState(player.IdleState);
        }

        //check if enemy player state is hit or block
        
    }

    public override void OnTriggerEnter2D(PlayerActions player, Collider2D collision)
    {
        //reset any hitbox additions
        if (collision.tag != player.latestInput.playerIndex.ToString())
        {
            hitbox.SetActive(false);
            player.SwitchState(player.HitState);
        }
    }

    void CheckButton(PlayerActions player)
    {

        if (player.latestInput.AButtonPressed)
        {
            if (canDoSpecial)
            {
                CheckSpecial(player);
            }

            if (canDo5A)
            {
                BlockStunFreezeCounter = 0;
                HitStunFreezeCounter = 0;
                Do5A(player);
            }   
        }
        else if (player.latestInput.BButtonPressed)
        {
            if (canDoSpecial)
            {
                CheckSpecial(player);
            }

            if (canDo5B)
            {
                BlockStunFreezeCounter = 0;
                HitStunFreezeCounter = 0;
                Do5B(player);
            }
        }
        else if (player.latestInput.CButtonPressed)
        {
            if (canDoSpecial)
            {
                BlockStunFreezeCounter = 0;
                HitStunFreezeCounter = 0;
                CheckSpecial(player);
            }

            if (canDo5C)
            {
                Do5C(player);
            }
        }
    }

    void CheckSpecial(PlayerActions player)
    {
        inputReader.Clear();
        inputReader.Add(player.latestInput.movementVector);

        for (int i = (player.inputHistory.Count - 1); i > (player.inputHistory.Count - 1) - (player.inputReaderLength - 1); i--)
        {
            
            if (inputReader.Last() != player.inputHistory[i].movementVector)
            {
                inputReader.Add(player.inputHistory[i].movementVector);
            }
            
        }

        if (inputReader.Take<Vector2>(3).SequenceEqual<Vector2>(qcf))
        {
            //qcf special performed
            if (player.latestInput.AButtonPressed)
            {
                Do236A(player);
            }
            else if (player.latestInput.BButtonPressed)
            {
                Do236B(player);
            }
            else if (player.latestInput.CButtonPressed)
            {
                Do236C(player);
            }
            BlockStunFreezeCounter = 0;
            HitStunFreezeCounter = 0;
            StartupSprite = player.sprite236XStartup;
            ActiveSprite = player.sprite236XActive;
            RecoverySprite = player.sprite236XRecovery;

            canDo5A = false; canDo5B= false; canDo5C= false; canDoSpecial= false;
        }

        if (inputReader.Take<Vector2>(3).SequenceEqual<Vector2>(qcb))
        {
            //qcb special performed
            if (player.latestInput.AButtonPressed)
            {
                Do214A(player);
            } 
            else if (player.latestInput.BButtonPressed)
            {
                Do214B(player);
            }
            else if (player.latestInput.CButtonPressed)
            {
                Do214C(player);
            }
            BlockStunFreezeCounter = 0;
            HitStunFreezeCounter = 0;
            StartupSprite = player.sprite214XStartup;
            ActiveSprite = player.sprite214XActive;
            RecoverySprite = player.sprite214XRecovery;

            canDo5A = false; canDo5B = false; canDo5C = false; canDoSpecial = false;
        }

    }

    void Do5A(PlayerActions player)
    {
        Debug.Log("did 5a");
        startupFramesCounter = 5;
        activeFramesCounter = 3;
        recoveryFramesCounter = 10;
        moveDistance = 0f;
        recoilDistance = 0.5f;
        hitbox = player.hitbox5A;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
        canDo5A = false;
        StartupSprite = player.sprite5AStartup;
        ActiveSprite = player.sprite5AActive;
        RecoverySprite= player.sprite5ARecovery;
        
    }

    void Do5B(PlayerActions player)
    {
        Debug.Log("did 5b");
        startupFramesCounter = 9;
        activeFramesCounter = 5;
        recoveryFramesCounter = 10;
        moveDistance = 0.1f;
        recoilDistance = 0.1f;
        hitbox = player.hitbox5B;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
        canDo5A = false; canDo5B = false;
        StartupSprite = player.sprite5BStartup;
        ActiveSprite = player.sprite5BActive;
        RecoverySprite = player.sprite5BRecovery;
    }

    void Do5C(PlayerActions player) //first frametrap tool, encourages you to special cancel.
    {
        Debug.Log("did 5c");
        startupFramesCounter = 14; //beats 5a mash
        activeFramesCounter = 7;
        recoveryFramesCounter = 16; //negative on block, silght advantage on hit but no combo
        moveDistance = 0.1f;
        recoilDistance = 0.1f;
        hitbox = player.hitbox5C;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
        canDo5A = false; canDo5B = false; canDo5C = false;
        StartupSprite = player.sprite5CStartup;
        ActiveSprite = player.sprite5CActive;
        RecoverySprite = player.sprite5CRecovery;
    }


    void Do236A(PlayerActions player) //combo tool, should always chain into on hit or block, no frametrap, punishable on block
    {
        //do special
        Debug.Log("236A performed");
        startupFramesCounter = 8; //always jails
        activeFramesCounter = 5; //more active to act as risky poke
        recoveryFramesCounter = 20; //0 on hit -10 on block
        moveDistance = 0.3f;
        recoilDistance = 0.1f;
        hitbox = player.hitbox236X;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);

    }

    void Do236B(PlayerActions player) //frametrap tool, beats 5a mash leaves at advantage on hit, punishable on block, advantage on hit
    {
        Debug.Log("236B performed");
        startupFramesCounter = 14; //beats 5a mash
        activeFramesCounter = 7;
        recoveryFramesCounter = 18;
        moveDistance = 0.2f;
        recoilDistance = 0.1f;
        hitbox = player.hitbox236X;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
    }

    void Do236C(PlayerActions player) //slow frametrap, loses to mashing, disadvantage on block, movesd forward and low pushback enable combo afterwards if hits
    {
        Debug.Log("236C performed");
        startupFramesCounter = 23; //loses to 5a and 5b mash (not 5c)
        activeFramesCounter = 9; //better poke but worse on whiff
        recoveryFramesCounter = 12; //combo opportunity with 5a
        moveDistance = 0.1f;
        recoilDistance = 0.0f;
        hitbox = player.hitbox236X;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
    }

    void Do214A(PlayerActions player) //safe frametrap pressure ender, but pushes you far away so low reward on hit or block
    {
        Debug.Log("214A performed");
        startupFramesCounter = 14; //beats 5a mash
        activeFramesCounter = 3;
        recoveryFramesCounter = 14; //negative on block but pushes you far away so not punishable, positive on hit but distance makes it low reward
        moveDistance = 0.1f;
        recoilDistance = 0.7f;
        hitbox = player.hitbox214X;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
    }

    void Do214B(PlayerActions player) //loses to 5a, encourages 5a mashing
    {
        Debug.Log("214B performed");
        startupFramesCounter = 19; //loses to 5a mash
        activeFramesCounter = 7;
        recoveryFramesCounter = 9; 
        moveDistance = 0.05f;
        recoilDistance = 0.5f;
        hitbox = player.hitbox214X;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
    }

    void Do214C(PlayerActions player) //super slow, get a combo on hit or pressure on block NEEDS TO BE INTERRUPTED will heal self if blocked, is the reason you dont want to be always blocking
    {
        Debug.Log("214C performed");
        startupFramesCounter = 30;
        activeFramesCounter = 11;
        recoveryFramesCounter = 6;
        moveDistance = 0.1f;
        recoilDistance = 0.1f;
        hitbox = player.hitbox214X;

        isHealing = true;

        startupVector = new Vector3(moveDistance, 0, 0);
        recoveryVector = new Vector3(0, 0, 0);
    }
}
