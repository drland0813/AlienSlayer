using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class PlayerInputAction : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 Move;
		public Vector2 ShootDirection;
		public bool Shoot;
		public bool Sprint;
		public bool ChangeGun;

		[Header("Movement Settings")]
		public bool AnalogMovement;
		public Action OnChangGunAction;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.Get<Vector2>());
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		// public void OnShoot(InputValue value)
		// {
		// 	ShootInput(value.isPressed);
		// }
		//
		public void OnChangeGun()
		{
			ChangeGunInput();
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			Move = newMoveDirection;
		}

		public void ShootInput(Vector2 shootDirection)
		{
			
			Shoot = shootDirection != Vector2.zero;
			ShootDirection = shootDirection;
		} 
		
		public void SprintInput(bool newSprintState)
		{
			Sprint = newSprintState;
		}

		public void ChangeGunInput()
		{
			OnChangGunAction?.Invoke();
		}
	}
	
}