public class DisableOnDeath : Death
{
    public override void OnDeath() => gameObject.SetActive(false);
}
