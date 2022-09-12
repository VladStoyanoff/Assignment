using UnityEngine;

namespace TriangleFactory
{
	public class GameManager : MonoBehaviour
	{

		private static GameManager _instance;
		
		public GameStates CurrentState { get; private set; }

		protected void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		public void ChangeState(GameStates state)
		{
			GameStates previousState = CurrentState;
			OnExitState(previousState);
			CurrentState = state;
			OnEnterState(state);
		}

		void OnEnterState(GameStates state)
		{

		}
		
		void OnExitState(GameStates state)
		{
			
		}

		public static GameManager Instance => _instance;
	}
}
