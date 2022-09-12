using UnityEngine;

namespace TriangleFactory
{
	/// <summary>
	/// Manages the basic states of the game.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		#region Fields

		/// <summary>
		/// The instance of the game manager.
		/// </summary>
		private static GameManager _instance;

		#endregion

		#region Properties

		/// <summary>
		/// The instance of the game manager.
		/// </summary>
		public static GameManager Instance => _instance;
		
		/// <summary>
		/// The current state of the game.
		/// </summary>
		public GameStates CurrentState { get; private set; }

		#endregion

		#region Initialization

		/// <inheritdoc cref="Awake"/>
		protected void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Change the current state of the game.
		/// </summary>
		/// <param name="state">The state to change to.</param>
		public void ChangeState(GameStates state)
		{
			GameStates previousState = CurrentState;
			OnExitState(previousState);
			CurrentState = state;
			OnEnterState(state);
		}

		/// <summary>
		/// Code to execute when entering a state.
		/// </summary>
		/// <param name="state">The state that is entered.</param>
		private void OnEnterState(GameStates state)
		{

		}
		
		/// <summary>
		/// Code to execute when exiting a state.
		/// </summary>
		/// <param name="state">The state that is exited.</param>
		private void OnExitState(GameStates state)
		{
			
		}

		#endregion
	}
}
