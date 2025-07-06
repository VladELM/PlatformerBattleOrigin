static class DirectionCalculator
{
    static public float GetDirection(float enemyPositionX, float targetPositionX)
    {
        float direction = 0f;

        if (enemyPositionX < targetPositionX)
            direction = 1;
        else if (enemyPositionX > targetPositionX)
            direction = -1;

        return direction;
    }
}
