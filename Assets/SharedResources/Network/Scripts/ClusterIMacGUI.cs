using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClusterIMacGUI : MonoBehaviour
{
	public MeshRenderer	status;
	public MeshRenderer	group;
	public TMP_Text		imacName;

	void Start ()
	{
		//Create a new instance of the material renderers so changing it's color wont affect the real mtaerial
		status.sharedMaterial = status.material;
		group.sharedMaterial = group.material;
	}
	
	void Update ()
	{
		
	}

	public void SetBorderColor(Color c)
	{
		status.sharedMaterial.color = c;
	}

	public void SetGroupColor(Color c)
	{
		group.sharedMaterial.color = c;
	}

	public void SetText(string text)
	{
		imacName.text = text;
	}

	public void SetBlank()
	{
		status.sharedMaterial.color = Colors.transparent;
		group.sharedMaterial.color = Colors.transparent;
		imacName.text = "";
	}
}
