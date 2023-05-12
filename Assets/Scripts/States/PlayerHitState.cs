using Unity.Profiling;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    int HitStunFramesCounter;
    int HitStunFreezeCounter;
    Vector3 playerPos;
    Vector3 newVector;

    public override void EnterState(PlayerActions player)
    {
        GotHit(player);
        
    }

    public override void UpdateState(PlayerActions player)
    {
        if(HitStunFreezeCounter > 0)
        {

            player.SpriteRenderer.sprite = player.spriteHitFreeze;
            player.hitEffects.SetActive(true);

            player.transform.position = playerPos; //lock player position to position when they got hit
            HitStunFreezeCounter--;
            Debug.Log(HitStunFreezeCounter + " frames left of hitstun freeze");
        } 
        else if (HitStunFramesCounter > 0) //after hitstun freeze counter has run out
        {
            player.hitEffects.SetActive(false);
            player.SpriteRenderer.sprite = player.spriteHitStun;

            if(player.latestInput.playerIndex == 0)
            {
                player.transform.position -= newVector; //move player by knockback amount while in hitstun frames
            }
            else
            {
                player.transform.position += newVector;
            }

            
            newVector.x = (newVector.x) / 1.2f; //half knockback amount to reduce knockback on every frame for slower knockback later on
            HitStunFramesCounter--;
            Debug.Log(HitStunFramesCounter + " frames left of hitstun frames");            
            //set animator to hitstun 
        }
        else
        {
            player.hitEffects.SetActive(false);
            Debug.Log("got here");
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnTriggerEnter2D(PlayerActions player, Collider2D collision)
    {
        Debug.Log("player hit collision detected");
        if (collision.tag != player.latestInput.playerIndex.ToString())
        {
            GotHit(player);
            
        }
    }

    void GotHit(PlayerActions player)
    {
        Debug.Log("got hit 1");
        playerPos = player.transform.position;
        HitStunFramesCounter = player.HitStunDuration; //reset hitstun counter
        //hitstun freeze
        HitStunFreezeCounter = player.HitStunFreezeDuration;
        newVector = new Vector3(player.HitStunKnockback, 0, 0);
        player.playerHealth--;
    }
}
