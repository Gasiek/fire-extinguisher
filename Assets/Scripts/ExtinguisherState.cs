public abstract class ExtinguisherState
{
    protected FireExtinguisherController Controller;

    public ExtinguisherState(FireExtinguisherController controller)
    {
        Controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}