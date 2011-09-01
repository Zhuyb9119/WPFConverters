using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//node to hold a reference to a variable
	internal sealed class VariableNode : Node
	{
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(VariableNode));

		private readonly int _index;

		public VariableNode(int index)
		{
			Debug.Assert(index >= 0);
			_index = index;
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			Debug.Assert(evaluationContext != null);
			exceptionHelper.ResolveAndThrowIf(!evaluationContext.HasArgument(_index), "ArgumentNotFound", _index);
			//variable values are passed inside the context for each evaluation
			return evaluationContext.GetArgument(_index);
		}
	}
}
