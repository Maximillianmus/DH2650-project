using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public class Pin : MonoBehaviour
{
	[Header("Options")] //
	public bool IsAutomatic;
	public bool HideIcon;
	public string SceneToLoad;
	public bool IsFirstLevel;

	[Header("Pins")] //
	public Pin UpPin;
	public Pin DownPin;
	public Pin LeftPin;
	public Pin RightPin;

	private Dictionary<Direction, Pin> _pinDirections;
	private static Material material;
	public bool Locked;
	public Pin[] NextPins;
	private MeshRenderer meshRenderer;


	private void setMaterial()
    {
		int score = gameObject.GetComponent<SaveHelper>().LoadScore(SceneToLoad);
		meshRenderer = gameObject.GetComponent<MeshRenderer>();
		Debug.Log(score);

		if (score > 1 || IsFirstLevel) {
			Locked = false;
			meshRenderer.materials[0] = material;
			meshRenderer.materials[0].SetColor("_BaseColor", Color.white);

			if (score == 0) return;
			foreach (Pin pin in NextPins)
            {
				pin.Unlock(); 
            }
		} else
        {
			Locked = true;
			meshRenderer.materials[0] = material;
			meshRenderer.materials[0].SetColor("_BaseColor", Color.black);
			//Debug.Log(lockedMaterial);
		}
		
	}

	public void Unlock()
    {
		StartCoroutine(UnlockTask());
	}

	IEnumerator UnlockTask()
    {
		yield return new WaitForSeconds(0.01f);
		if (!Locked) yield break;
		Locked = false;
		meshRenderer.materials[0] = material;
		meshRenderer.materials[0].SetColor("_BaseColor", Color.white);
	}

	/// <summary>
	/// Use this for initialisation
	/// </summary>
	private void Start()
	{
		
		if (material == null)
        {
			//material = AssetDatabase.LoadAssetAtPath("Assets/Materials/Level.mat", typeof(Material)) as Material;
		}
        // Load the directions into a dictionary for easy access
        _pinDirections = new Dictionary<Direction, Pin>
		{
			{ Direction.Up, UpPin },
			{ Direction.Down, DownPin },
			{ Direction.Left, LeftPin },
			{ Direction.Right, RightPin }
		};

		// Hide the icon if needed
		if (HideIcon)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}

		setMaterial();
	}


	/// <summary>
	/// Get the pin in a selected direction
	/// Using a switch statement rather than linq so this can run in the editor
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public Pin GetPinInDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
				return UpPin;
			case Direction.Down:
				return DownPin;
			case Direction.Left:
				return LeftPin;
			case Direction.Right:
				return RightPin;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}


	/// <summary>
	/// This gets the first pin thats not the one passed 
	/// </summary>
	/// <param name="pin"></param>
	/// <returns></returns>
	public Pin GetNextPin(Pin pin)
	{
		return _pinDirections.FirstOrDefault(x => x.Value != null && x.Value != pin).Value;
	}


	/// <summary>
	/// Draw lines between connected pins
	/// </summary>
	private void OnDrawGizmos()
	{
		if (UpPin != null) DrawLine(UpPin);
		if (RightPin != null) DrawLine(RightPin);
		if (DownPin != null) DrawLine(DownPin);
		if (LeftPin != null) DrawLine(LeftPin);
	}


	/// <summary>
	/// Draw one pin line
	/// </summary>
	/// <param name="pin"></param>
	protected void DrawLine(Pin pin)
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pin.transform.position);
	}
}
