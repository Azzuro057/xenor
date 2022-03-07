using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sert a afficher un message (passer en argument) sur l'�cran du joueur
// A mettre sur le canvas qui contient le texte de message d'interaction.
// ATTENTION : Le Canvas doit contenir un �l�ment Texte 
// ATTENTION : Le script est sur le Canvas
// NE PAS OUBLIER : de d�sactiv� le canva (et seulement le canva) dans unity ! 
public class FixedTextPopUP : MonoBehaviour
{
    //Fonction PressToInteractText() permet d'afficher un message utilitaire sur l'�crand du joueur
    /**
    * <summary>Permet d'afficher un message sur l'�cran du joueur</summary>
    * 
    * <param name="message">message que l'on souhaite afficher</param>
    * 
    * <returns>Return nothing</returns>
    */
    public void PressToInteractText(string message)
    {
        //Cherche l'�l�ment texte du texte dans le Canvas
        gameObject.GetComponentInChildren<Text>().text = message;
        gameObject.SetActive(true);
    }

    //Fonction PressToInteractText() permet de supprimer un �ventuel message pr�sent sur l'�cran du joueur
    /**
    * <summary>Permet de d�sactiver l'affichage du message de l'�cran du joueur</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void SupprPressToInteractText()
    {
        gameObject.SetActive(false);
    }

}
