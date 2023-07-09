using MainHandlers;

public  class BaseObservables
{
    public readonly ReactiveProperty<int> StrengthLevel = new();
    
    public readonly ReactiveProperty<RunnerStates> RunnerState = new(RunnerStates.Idle);

    public enum RunnerStates
    {
        Idle,
        Walking,
        Hit,
        Fall,
        Death,
        Victory
    }
}