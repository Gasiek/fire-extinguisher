public class PickedUpState : ExtinguisherState
{
    public PickedUpState(FireExtinguisherController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        EventAggregator.Instance.PublishExtinguisherPickedUp();
        if (Controller.IsBoltRemoved)
        {
            Controller.ChangeState(new ReadyState(Controller));
        }
    }
}
