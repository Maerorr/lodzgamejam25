using UnityEngine;

public class Stay : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        
        enemy.stay();
    }
}

public class Patrol : Behaviour
{
    public void execute(EnemyBase enemy)
    {
      
        enemy.movePatrol();
    }
}

public class MoveToPlayer : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        
        enemy.moveForward();
    }
}


public class MoveFromPlayer : Behaviour
{
    public void execute(EnemyBase enemy)
    {
       
        enemy.moveBackward();   
    }
}


public class Attack : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        
        enemy.attack();
    }
}


public class DistanceAttack : Behaviour
{
    public void execute(EnemyBase enemy)
    {
       
        enemy.distanceAttack();
    }
}

public class AttackInForwardMovement : Behaviour
{
    public void execute(EnemyBase enemy)
    {
     
        enemy.attackAndMoveForward();
    }
}

public class AttackInEscape : Behaviour
{
    public void execute(EnemyBase enemy)
    {
      
        enemy.attackAndMoveBackward(); 
    }
}

public class Death : Behaviour
{
    public void execute(EnemyBase enemy)
    {
        
        enemy.death();
    }
}