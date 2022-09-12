using UnityEngine;

namespace TriangleFactory
{
	/// <summary>
	/// Controls the behaviour of the weapon.
	/// </summary>
	public class Weapon : MonoBehaviour
	{
		#region Methods

		/// <summary>
		/// Shoot the weapon.
		/// </summary>
		public void Shoot()
		{
			Debug.Log("SHOOT!");
		}

		#endregion
	}
}
