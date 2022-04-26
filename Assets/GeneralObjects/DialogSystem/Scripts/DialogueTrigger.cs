using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	//nom du fichier json ^� est stock� le dialogue
	//NE PAS OUBLIER : de bien d�fnir le bon de premier ficheir l� oi� on mets le dialogue trigger
	public string filePath;

	public void TriggerDialogue()
	{
		if (filePath != "") {
			//Lecture du fichier json
			string dialoguesData = System.IO.File.ReadAllText(Application.dataPath + "/GeneralObjects/DialogSystem/Data/" + filePath);

			//cr�ation de l'objet qui stocke les phrases du dialogue en fonction du json
			Dialogue dialogue = JsonUtility.FromJson<Dialogue>(dialoguesData);

			//changement du chemin du nom du prochain fichier
			filePath = dialogue.nextDialogPath;

			//D�but du dialogue
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
		}
	}
}
