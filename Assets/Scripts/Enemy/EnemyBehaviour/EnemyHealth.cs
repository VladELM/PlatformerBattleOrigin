public class EnemyHealth : Health
{
    public void Restore()
    {
        _healthValue = MaxHealthValue;
    }
}
