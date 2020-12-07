void Main()
{
	var r = new Run {
		Name = "TestRun",
		State = new RunStates.Initial()
	};
	Console.WriteLine($"State: {r.State}");
	
	DoTransition(r, state => state.Finalize);
	DoTransition(r, state => state.Start);
	DoTransition(r, state => state.Finalize);
	DoTransition(r, state => state.Reset);
}

public void DoTransition(Run r, Expression<Func<IRunState, IRunState>> transition) 
{
	var sc = new RunStateController();
	try
	{
		sc.InvokeTransition(r, transition);
	}
	catch (UndefinedTransitionException ste)
	{
		Console.WriteLine("    "+ste.Message);
	}
	Console.WriteLine($"\nState: {r.State}");
}


// You can define other methods, fields, classes and namespaces here

public class StateController<TEntity, TState> where TEntity : IStatefulEntity<TState>, new()
{
	protected List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Func<TState, TState> onFailedAction)> Transitions = 
	      new List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Func<TState, TState> onFailedAction)>();

	public virtual void InvokeTransition(TEntity entity, Expression<Func<TState, TState>> transition)
	{
		List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Func<TState, TState> onFailedAction)> haystack
			= new List<(Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Func<TState, TState> onFailedAction)>();
		(Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Func<TState, TState> onFailedAction)? RequestedTransition = null;
		MemberExpression needleMember = null;
		try // to find requested transition 
		{
			var needleType = entity.State.GetType();
			needleMember = transition.Body as MemberExpression;
			if (needleMember == null)
			{
				Console.WriteLine("arrange failed (needle)");
				// throw exception? or
				return; // do nothing?
			}

			// TODO ==> invoking all transition will crash because it will also invoke on UndefinedTransitions 
			Transitions.ForEach(t => 
			{
				var transitionMember = t.transition.Body as MemberExpression;
				if(		needleType == t.Item1
					&&	needleMember.Member.Name == transitionMember.Member.Name
					&&  needleMember.Member.ReflectedType == transitionMember.Member.ReflectedType) 
				{
					RequestedTransition = t;
				}
			});
		}
		catch(Exception ex) 
		{
			// handle error on setting up transition
		}

		Action<TEntity> action = null;
		Func<TState, TState> onSuccess = null;
		Func<TState, TState> onFailure = null;
		if (RequestedTransition != null)
		{
			onSuccess = RequestedTransition.Value.transition?.Compile();
			action = 	RequestedTransition.Value.action;
			onFailure = RequestedTransition.Value.onFailedAction;
		} else {
			var forceUndefinedTransitionException = transition.Compile()(entity.State);
		}

		try // to execute found action and set target-state accordingly
		{
			Console.WriteLine("    => handle event: " + needleMember.Member.Name);
			if (action != null) action(entity);
			entity.State = onSuccess(entity.State);
		}
		catch(Exception ex)
		{
			Console.WriteLine("    " + ex.Message);
			Console.WriteLine("    => handle event: FAIL");
			if (onFailure != null) entity.State = onFailure(entity.State);
		}
		
	}
}

/// <summary>
/// 	RunStateController provides Actions that are to be executed on 
/// 	state transitions. This allows modifications and interactions in
/// 	and of the stated object when the state is about to change.
/// </summary>
public class RunStateController : StateController<Run, IRunState> 
{
	public RunStateController(/* insert any dependencies required to handle the transitions, e. g. repositories, logger, etc. */)
	{
		Transitions.Add((typeof(RunStates.Initial), 	runState => runState.Start, 	run => InvokeStart(run), 	runState => runState.Fail));
		Transitions.Add((typeof(RunStates.InProgress),	runState => runState.Cancel, 	run => InvokeCancel(run),	runState => runState.Fail));
		Transitions.Add((typeof(RunStates.InProgress),	runState => runState.Finalize, 	run => InvokeFinalize(run),	runState => runState.Fail));
		Transitions.Add((typeof(RunStates.InProgress),	runState => runState.Fail, 	run => InvokeFail(run),		runState => runState.Fail));
		Transitions.Add((typeof(RunStates.Done), 	runState => runState.Reset, 	run => InvokeReset(run),	null));
		Transitions.Add((typeof(RunStates.Cancelled), 	runState => runState.Reset, 	null,	null));
		Transitions.Add((typeof(RunStates.Failed), 	runState => runState.Reset, 	null,	null));
		Transitions.Add((typeof(RunStates.Initial), 	runState => runState.Start, 	run => InvokeStart(run),	runState => runState.Fail));
		Transitions.Add((typeof(RunStates.Initial), 	runState => runState.Start, 	run => InvokeStart(run),	runState => runState.Fail));
	}	

	private void InvokeStart(Run run) 	{ Console.WriteLine("   Starting run!");/* implement logic to handle the transition event */}
	private void InvokeFinalize(Run run){ throw new Exception("ERR: Crash on Finalizing the run!"); /*Console.WriteLine("    Finalizing run!");/* implement logic to handle the transition event */}
	private void InvokeCancel(Run run) 	{ Console.WriteLine("    Cancelling run!"); /* implement logic to handle the transition event */}
	private void InvokeAbort(Run run) 	{ Console.WriteLine("    Aborting run!"); /* implement logic to handle the transition event */}
	private void InvokeFail(Run run) 	{ Console.WriteLine("    Failing run!"); /* implement logic to handle the transition event */}
	private void InvokeReset(Run run) 	{ Console.WriteLine("    Resetting run!");}

}

public class Run : IStatefulEntity<IRunState>
{
	public string Name { get; set; }
	public IRunState State { get; set; }
}


public interface IStatefulEntity<TState>
{
	TState State { get; set; }
}

public interface IState<TState>
{
	TState Fail { get; }
	TState Undefined(string transition);

}

public abstract class State<TState> : IState<TState>
{
	public override string ToString() => $"{GetType().Name}";
	public abstract TState Fail {get;}
	public TState Undefined(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
}

public interface IRunState : IState<IRunState>
{
	IRunState Finalize {get;}
	IRunState Reset	  {get;}
	IRunState Start { get; }
	IRunState Cancel { get;}
}

public abstract class RunState : State<IRunState>, IRunState
{
	public virtual  IRunState Finalize 	=> Undefined("Finalize");
	public virtual  IRunState Reset 	=> Undefined("Reset");
	public virtual  IRunState Start 	=> Undefined("Start");
	public virtual  IRunState Cancel 	=> Undefined("Cancel");
	public override IRunState Fail 		=> new RunStates.Failed();

}

public class RunStates
{
	public class Initial : RunState, IState<IRunState>
	{
		public override IRunState Start => new InProgress();
	}
	public class InProgress : RunState, IState<IRunState>
	{
		public override IRunState Finalize => new Done();
		public override IRunState Cancel => new Cancelled();
	}
	public class Done : RunState, IState<IRunState>
	{
		public override IRunState Reset => new Initial();
	}
	public class Cancelled : RunState, IState<IRunState>
	{
		public override IRunState Reset => new Initial();
	}
	public class Failed : RunState, IState<IRunState>
	{
		public override IRunState Reset => new Initial();
	}
}

public class UndefinedTransitionException : Exception
{
	public const string MessageTemplate = "The transition '{0}' from state '{1}' is not defined!";
	
	public UndefinedTransitionException(string transition, string sourceState)
	: base(string.Format(MessageTemplate, transition, sourceState))	{ }
}
