using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
	public Boat Boat;
	public Pin StartPin;
	//public Text SelectedLevelText;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start()
	{
		// Pass a ref and default the player Starting Pin
		Boat.Initialise(this, StartPin);
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when Boat is stopped
		if (Boat.IsMoving) return;

		// First thing to do is try get the player input
		CheckForInput();
	}


	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			Boat.TrySetDirection(Direction.Up);
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			Boat.TrySetDirection(Direction.Down);
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Boat.TrySetDirection(Direction.Left);
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Boat.TrySetDirection(Direction.Right);
		}
	}


	/// <summary>
	/// Update the GUI text
	/// </summary>
	//public void UpdateGui()
	//{
	//	SelectedLevelText.text = string.Format("Current Level: {0}", Boat.CurrentPin.SceneToLoad);
	//}
}
