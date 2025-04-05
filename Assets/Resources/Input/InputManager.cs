using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
	public Action CONTROLLER_DISCONNECTED;

	public enum ControllerType
	{
		MouseKeyboard,
		Controler
	};

	public enum GamepadType
	{
		PS4,
		PS5,
		XboxOne,
		None
	};

	private ControllerType controllerType;

	public ControllerType CurrentController = ControllerType.MouseKeyboard;

	public GamepadType CurrentGamepad = GamepadType.None;

	public static InputManager instance { get; private set; }
	InputDevice lastDevice;
	public static bool controllerdisconnected;

	public GameObject controlDiscPopUp;

	Gamepad currentGamepadVar;

	public Gamepad currentGamepad
	{
		get => currentGamepadVar;
		set
		{
			if (value == currentGamepad)
				return;
			currentGamepadVar = value;
		}
	}

	public static InputActionAsset eventSystemAsset;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
		eventSystemAsset = Resources.Load<InputActionAsset>("Input/InputActions");
	}

	private void Start()
	{
		SetInputBindings();

		Debug.Log($"Gamepads {Gamepad.all.Count}");
		InputSystem.onDeviceChange += InputSystem_onDeviceChange;
		InputUser.onChange += InputUser_onChange;

		DetectJoystick();
	}

	private void InputUser_onChange(InputUser arg1, InputUserChange arg2, InputDevice arg3)
	{
		Debug.Log($"Input user change: {arg2}");
	}

	public void SetInputBindings()
	{
		InputControl.Current.asset.bindingMask = InputBinding.MaskByGroups(new string[] { "Keyboard", "Gamepad" });
		eventSystemAsset.bindingMask = InputBinding.MaskByGroups(new string[] { "Keyboard", "Gamepad" });
	}

	private void InputSystem_onDeviceChange(InputDevice device, InputDeviceChange deviceChangeType)
	{
		switch (deviceChangeType)
		{
			case InputDeviceChange.Added:
				currentGamepad = device as Gamepad;

				DetectJoystick();

				controllerdisconnected = false;
				break;

			case InputDeviceChange.Disconnected:

				if (InputSystem.devices.Count == 0)
				{
					controllerdisconnected = true;

					currentGamepad = null;
					return;
				}
				break;
		}
	}

	public void DetectJoystick()
	{
		if (currentGamepad != null)
		{
			if (currentGamepad.name == "DualShock4GamepadHID")
			{
				CurrentGamepad = GamepadType.PS4;
			}
			else if (currentGamepad.name == "DualSenseGamepadHID")
			{
				CurrentGamepad = GamepadType.PS5;
			}
			else
			{
				CurrentGamepad = GamepadType.XboxOne;
			}

			CurrentController = ControllerType.Controler;

			SetInputBindings();
		}
		else
		{
			CurrentController = ControllerType.MouseKeyboard;
		}
	}
}