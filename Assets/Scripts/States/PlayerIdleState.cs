using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerActions player)
    {
        
    }

    public override void UpdateState(PlayerActions player)
    {
        player.SpriteRenderer.sprite = player.spriteIdle;
        //switch to attack state if attack button pressed
        if (player.latestInput.AnyButtonPressed)
        {
            player.SwitchState(player.AttackState);
        }       
        else if (player.latestInput.movementVector.x != 0) //switch to walk state if player is inputting a direction
        {
            player.SwitchState(player.WalkState);
        }

        
    }

    public override void OnTriggerEnter2D(PlayerActions player, Collider2D collision)
    {
        if(collision.tag != player.latestInput.playerIndex.ToString())
        {
            Debug.Log("collided with different pID");
            player.SwitchState(player.HitState);
        }
    }
}
