using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClusterIMacGUI : MonoBehaviour
{
	public Image		border;
	public Image		group;
	public Text			imacName;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public void SetBorderColor(Color c)
	{
		border.color = c;
	}

	public void SetGroupColor(Color c)
	{
		group.color = c;
	}

	public void SetText(string text)
	{
		imacName.text = text;
	}

	public void SetBlank()
	{
		border.color = Colors.transparent;
		group.color = Colors.transparent;
		imacName.text = "";
	}
}
