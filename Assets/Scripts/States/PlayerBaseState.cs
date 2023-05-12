using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerActions player);

    public abstract void UpdateState(PlayerActions player);

    public abstract void OnTriggerEnter2D(PlayerActions player, Collider2D collider);
}
