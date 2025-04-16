public class ActivatedState : ExtinguisherState
{
    public ActivatedState(FireExtinguisherController controller) : base(controller) { }
    
    public override void Enter()
    {
        Controller.StartExtinguishing();
    }

    public override void Exit()
    {
        Controller.StopExtinguishing();
    }

    public override void Update()
    {
        if (Controller.ExtinguishingTime <= 0)
        {
            Controller.ChangeState(new DepletedState(Controller));
        }
    }
}