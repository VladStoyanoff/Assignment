using UnityEngine;

namespace TriangleFactory
{
	public class Player : MonoBehaviour
	{
		[SerializeField] LayerMask _interactionMask;
		[SerializeField] Camera _camera;
		[SerializeField] Transform _weaponLocation;

        [SerializeField] float movementSpeed = 5f;

		Weapon _weapon;
        InputManager inputManagerScript;

        bool equippedWeapon;

        const float Radius = 1f;
		const float FireDistance = 50.0f;
		const float InteractionDistance = 10.0f;

        void Awake()
        {
            inputManagerScript = FindObjectOfType<InputManager>();
        }

        void Update()
        {
            UpdatePlayerPositionAndRotation();
            TryEquipWeapon();
            TryShoot();
        }

        void TryEquipWeapon()
        {
            if (inputManagerScript.Interact() == false) return;

            if (Physics.SphereCast(_camera.transform.position, Radius, _camera.transform.forward,
                                   out var hitInfo, InteractionDistance, _interactionMask,
                                   QueryTriggerInteraction.Collide) == false) return;

            var weapon = hitInfo.collider.GetComponentInParent<Weapon>();
            EquipWeapon(weapon);
        }

        void EquipWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.transform.SetParent(_weaponLocation);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
            equippedWeapon = true;

            // Set off timer
        }

        void TryShoot()
        {
            if (inputManagerScript.Fire() == false) return;
            if (equippedWeapon == false) return;

            _weapon.Shoot();
            var start = _camera.transform.position;
            var direction = transform.forward;
#if DEBUG
            Debug.DrawRay(start, direction * FireDistance, Color.red, 1.0f);
#endif
            if (Physics.Raycast(start, direction, FireDistance) == false) return;
            Debug.Log("HIT");
        }

        void UpdatePlayerPositionAndRotation()
        {
            var positionVector = Vector3.zero;
            var xRestriction = 1.6f;
            positionVector.x = inputManagerScript.Movement();
            transform.position += positionVector * movementSpeed * Time.deltaTime;
            var restrictedPosition = Mathf.Clamp(transform.position.x, -xRestriction, xRestriction);
            transform.position = new Vector3(restrictedPosition, 0, -2);

            //var yRestrictions = 50f;
            //var yRestrictionsClamped = Mathf.Clamp(transform.rotation.y, -yRestrictions, yRestrictions);
            //transform.rotation = Quaternion.Euler(0, yRestrictionsClamped, 0);
        }
    }
}
