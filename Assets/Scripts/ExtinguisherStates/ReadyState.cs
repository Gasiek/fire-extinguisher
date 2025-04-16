public class ReadyState : ExtinguisherState
{
    public ReadyState(FireExtinguisherController controller) : base(controller) { }

    public override void Enter()
    {
        EventAggregator.Instance.PublishExtinguisherActivated(Controller.PickupTime);
    }
}