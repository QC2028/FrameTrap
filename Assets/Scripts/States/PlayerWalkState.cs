
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    int walk1counter = 10;
    int walk2counter = 10;
    int walk3counter = 10;
    int walk4counter = 10;

    int blockX = -1;

    Vector3 newVector;
    public override void EnterState(PlayerActions player)
    {
        if (player.latestInput.playerIndex == 1)
        {
            blockX = 1;
        }
    }

    public override void UpdateState(PlayerActions player)
    {
        //walk state behaviour

        if(player.enemyPlayer.currentState == player.enemyPlayer.AttackState && player.latestInput.movementVector.x == blockX) //if enemy is in attack state and you are walking backwards
        {
            player.SpriteRenderer.sprite = player.spriteBlocking;
        }
        else
        {
            newVector = new Vector3(player.latestInput.movementVector.x * player.moveSpeed, 0, 0);
            player.transform.position += newVector; //move player according to input

            if (walk1counter > 0) //walk animation
            {
                player.SpriteRenderer.sprite = player.spriteWalk2;
                walk1counter--;
            }
            else if (walk2counter > 0)
            {
                player.SpriteRenderer.sprite = player.spriteWalk3;
                walk2counter--;
            }
            else if (walk3counter > 0)
            {
                player.SpriteRenderer.sprite = player.spriteWalk2;
                walk3counter--;
            }
            else if (walk4counter > 0)
            {
                player.SpriteRenderer.sprite = player.spriteWalk1;
                walk4counter--;
            }
            else
            {
                walk1counter = 10; walk2counter = 10; walk3counter = 10; walk4counter = 10;
            }
        }


        //switch to attack state if attack button is pressed
        if (player.latestInput.AnyButtonPressed)
        {
            player.SwitchState(player.AttackState);
        }        
        else if (player.latestInput.movementVector.x == 0)//switch to idle state if no movement is pressed
        {
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnTriggerEnter2D(PlayerActions player, Collider2D collision)
    {
        if (collision.tag != player.latestInput.playerIndex.ToString())
        {

            if (player.latestInput.movementVector.x == blockX) //if player is walking backwards then switch to block state
            {
                player.SwitchState(player.BlockState);
            }
            else
            {
                player.SwitchState(player.HitState); //otherwise switch to hit state
            }
        }
    }
}
