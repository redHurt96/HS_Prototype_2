namespace AI.FluentFiniteStateMachine
{
    public abstract class State<TContext> : IState
    {
        protected readonly TContext Context;
        
        protected State(TContext context) => 
            Context = context;
    }
}