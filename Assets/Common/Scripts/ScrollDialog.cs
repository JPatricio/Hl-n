using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollDialog : MonoBehaviour {

	// This level's dialog
	public TextAsset textFile;
	string[] dialogLines;

	// The UI Text Component
	private Text textDialog;
	private int currentLine; 
	public bool objective;

	// pointer object
	private GameObject pointerArrow;

	public void OnMouseUp () {
		if(objective){
			return;
		}
		if (pointerArrow != null)
			Destroy(pointerArrow);
		if(currentLine < dialogLines.Length){
			currentLine++;
			string lineToShow = dialogLines[currentLine-1];
			if (lineToShow.Contains("_")){
				string[] words = lineToShow.Split("_".ToCharArray());
				string ui_to_show = words[1];
				lineToShow = words[2];

				pointerArrow = Instantiate(Resources.Load("Tutorial/Pointer") as GameObject);
				if (ui_to_show == "gold"){
					pointerArrow.transform.position = pointerArrow.transform.position + new Vector3 (8, 0, 0);
				}else if (ui_to_show == "health"){
					// do nothing
				}else if (ui_to_show == "wave"){
					pointerArrow.transform.position = pointerArrow.transform.position + new Vector3 (17, 0, 0);
				}else if (ui_to_show == "nextwave"){
					pointerArrow.transform.position = pointerArrow.transform.position + new Vector3 (0, -5, 0);
				}else if (ui_to_show == "tower"){
					pointerArrow.transform.position = pointerArrow.transform.position + new Vector3 (4.2f, -4, 0);
					pointerArrow.transform.Rotate(0, 0, -173);
					objective = true;
				}else if (ui_to_show == "meteor"){
					pointerArrow.transform.position = pointerArrow.transform.position + new Vector3 (1, -12.5f, 0);
					pointerArrow.transform.Rotate(0, 180, -173);
				}
			}
			textDialog.text = lineToShow;
		}else {
			Destroy(gameObject);
			textDialog.text = "";
		}
	}

	// Use this for initialization
	void Start () {
		// Make sure there this a text
		// file assigned before continuing
		if(textFile != null)
		{
			// Add each line of the text file to
			// the array using the new line
			// as the delimiter
			dialogLines = ( textFile.text.Split( '\n' ) );
		}
		currentLine = 1;

		GameObject dialog = GameObject.Find("DialogText");
		textDialog = dialog.GetComponent<Text>();

		textDialog.text = dialogLines[0];

		objective = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
