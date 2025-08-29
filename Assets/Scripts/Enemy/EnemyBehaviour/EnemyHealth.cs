public class EnemyHealth : Health
{
    public void Restore()
    {
        _value = MaxValue;
    }
}
