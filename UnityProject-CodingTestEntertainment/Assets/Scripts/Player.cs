using UnityEngine;
using UnityEngine.InputSystem;

namespace TriangleFactory
{
	public class Player : MonoBehaviour
	{
		[SerializeField] LayerMask _interactionMask;
		[SerializeField] Camera _camera;
		[SerializeField] Transform _weaponLocation;

		Weapon _weapon;

		const float FireDistance = 50.0f;
		const float InteractionDistance = 10.0f;

		public void OnFireAction(InputAction.CallbackContext context)
		{
			if (context.performed == false) return;
            _weapon.Shoot();

            var start = _camera.transform.position;
            var direction = transform.forward;
#if DEBUG
            Debug.DrawRay(start, direction * FireDistance, Color.red, 1.0f);
#endif
            if (Physics.Raycast(start, direction, FireDistance))
            {
                Debug.Log("HIT");
            }
        }

		public void OnInteractAction(InputAction.CallbackContext context)
		{
			if (context.performed == false) return;
			if (_weapon != null) return;

            // TODO: Make radius smaller, so you can only pick up the weapon when looking at it.
            if (Physics.SphereCast(_camera.transform.position, 1.0f, _camera.transform.forward,
                out var hitInfo, InteractionDistance, _interactionMask, QueryTriggerInteraction.Collide))
            {
                var weapon = hitInfo.collider.GetComponentInParent<Weapon>();
                EquipWeapon(weapon);
            }
        }

		void EquipWeapon(Weapon weapon)
		{
			if (weapon = null) return;
            _weapon = weapon;
            _weapon.transform.SetParent(_weaponLocation);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }
	}
}
