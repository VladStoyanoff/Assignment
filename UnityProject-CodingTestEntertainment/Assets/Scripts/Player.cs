using UnityEngine;
using UnityEngine.InputSystem;

namespace TriangleFactory
{
	/// <summary>
	/// Controls the behaviour of the player.
	/// </summary>
	public class Player : MonoBehaviour
	{
		#region Editor Fields

		/// <summary>
		/// The mask used for grabbing items.
		/// </summary>
		[SerializeField]
		private LayerMask _interactionMask;

		/// <summary>
		/// The camera of the player.
		/// </summary>
		[SerializeField]
		private Camera _camera;

		/// <summary>
		/// The location to set the weapon.
		/// </summary>
		[SerializeField]
		private Transform _weaponLocation;

		#endregion
		
		#region Fields

		/// <summary>
		/// The current equipped weapon.
		/// </summary>
		private Weapon _weapon;

		/// <summary>
		/// The distance of the fire raycast.
		/// </summary>
		private const float FireDistance = 50.0f;

		/// <summary>
		/// The distance of the interaction spherecast.
		/// </summary>
		private const float InteractionDistance = 10.0f;

		#endregion
		
		#region Methods

		/// <summary>
		/// Callback when the shoot action was triggered.
		/// </summary>
		/// <param name="context">The context of the shoot action.</param>
		public void OnFireAction(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				_weapon.Shoot();

				Vector3 start = _camera.transform.position;
				Vector3 direction = transform.forward;
#if DEBUG
				Debug.DrawRay(start, direction * FireDistance, Color.red, 1.0f);
#endif
				if (Physics.Raycast(start, direction, FireDistance))
				{
					Debug.Log("HIT");
				}
			}
		}

		/// <summary>
		/// Callback when the grab action was triggered.
		/// </summary>
		/// <param name="context">The context of the grab action.</param>
		public void OnInteractAction(InputAction.CallbackContext context)
		{
			if (context.performed && _weapon == null)
			{
				// TODO: Make radius smaller, so you can only pick up the weapon when looking at it.
				if (Physics.SphereCast(_camera.transform.position, 1.0f, _camera.transform.forward,
					out RaycastHit hitInfo, InteractionDistance, _interactionMask, QueryTriggerInteraction.Collide))
				{
					Weapon weapon = hitInfo.collider.GetComponentInParent<Weapon>();
					EquipWeapon(weapon);
				}
			}
		}

		/// <summary>
		/// Equip a weapon.
		/// </summary>
		/// <param name="weapon">The weapon to equip.</param>
		private void EquipWeapon(Weapon weapon)
		{
			if (weapon != null)
			{
				_weapon = weapon;

				_weapon.transform.SetParent(_weaponLocation);

				_weapon.transform.localPosition = Vector3.zero;
				_weapon.transform.localRotation = Quaternion.identity;
			}
		}

		#endregion
	}
}
