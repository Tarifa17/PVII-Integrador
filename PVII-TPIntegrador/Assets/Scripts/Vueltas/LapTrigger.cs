using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public enum TriggerType { StartLine, Midpoint }
    public TriggerType triggerType;
    public LapTracker tracker;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (triggerType == TriggerType.StartLine)
            tracker.OnStartLineEntered();
        else if (triggerType == TriggerType.Midpoint)
            tracker.OnMidpointEntered();
    }
}
