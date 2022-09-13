using System;
using UnityEngine;

namespace TriangleFactory
{
	public class Player : MonoBehaviour
	{
        public static event EventHandler OnWeaponEquipped;

        [SerializeField] LayerMask _interactionMask;
        [SerializeField] LayerMask targetLayerMask;
		[SerializeField] Camera _camera;
        [SerializeField] GameObject muzzleLocation;
		[SerializeField] Transform _weaponLocation;

        [SerializeField] float movementSpeed = 5f;

		Weapon _weapon;
        InputManager inputManagerScript;
        ScoreManager scoreManagerScript;

        bool equippedWeapon;
        bool countingDown;

        int pointsOnMiss = -100;
        int pointsOnHit = 1000;

        const float Radius = 1f;
		const float FireDistance = 50.0f;
		const float InteractionDistance = 10.0f;

        void Awake()
        {
            inputManagerScript = FindObjectOfType<InputManager>();
            scoreManagerScript = FindObjectOfType<ScoreManager>();
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
            OnWeaponEquipped?.Invoke(this, EventArgs.Empty);
            SwitchCountingDownBool();
            FindObjectOfType<TargetManager>().DisableTargets();
        }

        public void SwitchCountingDownBool()
        {
            countingDown = !countingDown;
        }

        void TryShoot()
        {
            if (countingDown) return;
            if (inputManagerScript.Fire() == false) return;
            if (equippedWeapon == false) return;

            _weapon.Shoot();

            if (Physics.Raycast(GetRay(), out RaycastHit hit, float.MaxValue, targetLayerMask))
            {
                hit.transform.gameObject.SetActive(false);
                FindObjectOfType<ScoreManager>().ModifyScore(pointsOnHit);
                Debug.Log("HIT");
            }

            else
            {
                scoreManagerScript.ModifyScore(pointsOnMiss);
            }
        }

        Ray GetRay() => new Ray(_camera.transform.position, _camera.transform.forward);

        void UpdatePlayerPositionAndRotation()
        {
            var positionVector = Vector3.zero;
            var xRestriction = 1.6f;
            positionVector.x = inputManagerScript.Movement();
            transform.position += positionVector * movementSpeed * Time.deltaTime;
            var restrictedPosition = Mathf.Clamp(transform.position.x, -xRestriction, xRestriction);
            transform.position = new Vector3(restrictedPosition, 0, -2);
        }
    }
}
