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

public void DoTransition(Run r, Func<IRunState, IRunState> transition)
{
	try
	{
		r.State = transition(r.State);
	}
	catch (UndefinedTransitionException ste)
	{
		Console.WriteLine("    " + ste.Message);
	}
	Console.WriteLine($"\nState: {r.State}");
}


public class Run 
{
	public string Name {get; set;}
	public IRunState State {get; set;}
}

public class State
{
	public override string ToString() => $"{GetType().Name}";
}

public interface IRunState
{
	public IRunState Fail => Undefined("Fail");
	public IRunState Finalize => Undefined("Finalize");
	public IRunState Reset => Undefined("Reset");
	public IRunState Start => Undefined("Start");
	public IRunState Cancel => Undefined("Cancel");
	public IRunState Undefined(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
}

public class RunStates
{
	public class Initial : State, IRunState
	{
		public IRunState Start => new InProgress();
	}
	public class InProgress : State, IRunState
	{
		public IRunState Fail => new Failed();
		public IRunState Finalize => new Done();
		public IRunState Cancel => new Cancelled();
	}
	public class Done : State, IRunState
	{
		public IRunState Reset => new Initial();
	}
	public class Cancelled : State, IRunState
	{
		public IRunState Reset => new Initial();
	}
	public class Failed : State, IRunState
	{
		public IRunState Reset => new Initial();
	}
}

public class UndefinedTransitionException : Exception
{
	public const string MessageTemplate = "The transition '{0}' from state '{1}' is not defined!";
	
	public UndefinedTransitionException(string transition, string sourceState)
	: base(string.Format(MessageTemplate, transition, sourceState))	{ }
}
