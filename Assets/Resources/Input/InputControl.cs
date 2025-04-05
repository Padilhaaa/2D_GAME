using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputControl : MonoBehaviour
{
	public static event EventHandler OnActionMapChanged;

	static public InputActionAsset eventSystemAsset { get; private set; }

	static InputActions current;

	public static InputActions Current
	{
		get
		{
			if (current == null)
			{
				// Init
				current = new InputActions();
				eventSystemAsset = Resources.Load<InputActionAsset>("Input/InputActions");
				SetMapActive(ActionMap.Gameplay | ActionMap.UI);
			}
			return current;
		}
	}

	[System.Flags]
	public enum ActionMap
	{
		None = 0b0000,
		UI = 0b0001,
		Gameplay = 0b0010
	}

	const int actionMapCount = 3;

	static ActionMap activeMap;

	static public void SetMapActive(ActionMap newMap, bool throwEvent = true)
	{
		Current.Gameplay.Disable();
		Current.UI.Disable();

		for (int i = 0; i < actionMapCount; i++)
		{
			ActionMap map = (ActionMap)((int)newMap & (1 << i));
			switch (map)
			{
				case ActionMap.None:
					continue;
				case ActionMap.Gameplay:
					Current.Gameplay.Enable();
					break;
				case ActionMap.UI:
					Current.UI.Enable();
					break;
				default:
					break;
			}
		}

		activeMap = newMap;
		if (throwEvent)
			OnActionMapChanged?.Invoke(null, EventArgs.Empty);
	}
}