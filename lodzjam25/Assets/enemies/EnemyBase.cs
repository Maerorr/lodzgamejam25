using UnityEngine;

public interface EnemyBase
{

    void moveForward();
    void moveBackward();
    void movePatrol();
    void attackAndMoveForward();
    void attackAndMoveBackward();
    void distanceAttack();
    void death();

    void setCurrentState();

    void stay();

    void attack();



}
