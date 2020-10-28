public static class EventManager
{
    public delegate void AdsEvent(AdsPlacementType type);
    public static event AdsEvent PlayAdsEvent;
    public static void TriggerPlayAds(AdsPlacementType type)
    {
        PlayAdsEvent?.Invoke(type);
    }
}
