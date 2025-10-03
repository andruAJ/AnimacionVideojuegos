
public interface IdamageSender<TDamage> where TDamage : struct
{
    void SendDamage(IdamageReceiver<TDamage> receiver);
}
