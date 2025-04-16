using System;

public class EventAggregator
{
    private static EventAggregator _instance;
    public static EventAggregator Instance 
    {
        get 
        {
            if (_instance == null) _instance = new EventAggregator();
            return _instance;
        }
    }

    public event Action FireExtinguished;
    public event Action ExtinguisherPickedUp;
    public event Action ExtinguisherStarted;
    public event Action<DateTime> ExtinguisherActivated;

    public void PublishExtinguisherStarted()
    {
        ExtinguisherStarted?.Invoke();
    }
    
    public void PublishFireExtinguished() 
    {
        FireExtinguished?.Invoke();
    }

    public void PublishExtinguisherPickedUp() 
    {
        ExtinguisherPickedUp?.Invoke();
    }

    public void PublishExtinguisherActivated(DateTime pickupTime) 
    {
        ExtinguisherActivated?.Invoke(pickupTime);
    }
}