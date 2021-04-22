using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
		{
			Boat.TrySetDirection(Direction.Up);
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
		{
			Boat.TrySetDirection(Direction.Down);
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
		{
			Boat.TrySetDirection(Direction.Left);
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
		{
			Boat.TrySetDirection(Direction.Right);
		}
		else if (Input.GetKeyUp(KeyCode.Return))
        {
			if (Boat.CurrentPin.Locked)
            {
				return;
            }
			SceneManager.LoadScene(Boat.CurrentPin.SceneToLoad);
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
