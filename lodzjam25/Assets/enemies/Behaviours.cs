using UnityEngine;

public class Stay : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Stoje");
        enemy.stay();
    }
}

public class Patrol : Behaviour
{
    public void execute(EnemyBase enemy)
    {
      //  Debug.Log("Patroluje");
        enemy.movePatrol();
    }
}

public class MoveToPlayer : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("gonie");
        enemy.moveForward();
    }
}


public class MoveFromPlayer : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Spierdalam");
        enemy.moveBackward();   
    }
}


public class Attack : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Napierdalam");
        enemy.attack();
    }
}


public class DistanceAttack : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Nakurwiam");
        enemy.distanceAttack();
    }
}

public class AttackInForwardMovement : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("NapierdalamWBiegu");
        enemy.attackAndMoveForward();
    }
}

public class AttackInEscape : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Napierdalam spierdalaj¹c");
        enemy.attackAndMoveBackward(); 
    }
}

public class Death : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        Debug.Log("Zdycham");
        enemy.death();
    }
}