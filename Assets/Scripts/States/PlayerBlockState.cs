using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{

    int BlockStunFramesCounter;
    int BlockStunFreezeCounter;
    Vector3 playerPos;
    Vector3 newVector;

    public override void EnterState(PlayerActions player)
    {
        BlockedHit(player);
        
    }

    public override void UpdateState(PlayerActions player)
    {
        player.SpriteRenderer.sprite = player.spriteBlocking;
        if (BlockStunFreezeCounter > 0)
        {
            player.blockEffects.SetActive(true);
            player.transform.position = playerPos; //lock player position to position when they Block
            BlockStunFreezeCounter--;
            Debug.Log(BlockStunFreezeCounter + " frames left of Blockstun freeze");
            //set animator to Blockstun freeze and maybe flash character
        }
        else if (BlockStunFramesCounter > 0) //after Blockstun freeze counter has run out
        {
            player.blockEffects.SetActive(false);

            if(player.latestInput.playerIndex == 0) //move player by knockback amount while in Blockstun frames
            {
                player.transform.position -= newVector;
            }
            else
            {
                player.transform.position += newVector;
            }
            newVector.x = (newVector.x) / 2; //half knockback amount to reduce knockback on every frame for slower knockback later on
            BlockStunFramesCounter--;
            Debug.Log(BlockStunFramesCounter + " frames left of Blockstun frames");
            //set animator to Blockstun 
        }
        else
        {
            player.blockEffects.SetActive(false);
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnTriggerEnter2D(PlayerActions player, Collider2D collision)
    {
        
        if (collision.tag != player.latestInput.playerIndex.ToString())
        {
            BlockedHit(player);
        }
    }

    void BlockedHit(PlayerActions player)
    {
        playerPos = player.transform.position;
        BlockStunFramesCounter = player.BlockStunDuration; //reset Blockstun counter
        //Blockstun freeze
        BlockStunFreezeCounter = player.BlockStunFreezeDuration;
        newVector = new Vector3(player.BlockStunKnockback, 0, 0);
    }
}
