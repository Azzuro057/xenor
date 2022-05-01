using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuyau : PressurePlate
{
    public PCTile TileData = new PCTile();

    //SPrites
    public Sprite Source_Empty;
    public Sprite Strait_Empty;
    public Sprite Corner_Empty;
    public Sprite Cross_Empty;

    /*
     * Fonctions
     */

    //Appel�e a chaque frame
    private void Update()
    {
        //check si on appuie sur E ssi la plaque est pr�ss�e
        if (pressed && Input.GetKeyDown(KeyCode.R))
        {
            //tel�porte les joueurs
            Rotate();
        }
    }

    //Fonction OnPressure() appell�e si un player marche sur le tuyaux, elle affiche le message pour demander une int�raction
    /**
    * <summary>D�termine ce s'il faut faire qqc (et quoi) avec le player qui est sur la plaque</summary>
    * 
    * <param name="other">objet avec qui on a collision�</param>
    * 
    * <returns>Return nothing</returns>
    */
    protected override void OnPressure(Collider2D other)
    {
        //On affiche le message qui indique au joueur comment int�ragir avec la porte.
        MessageOnScreenCanvas.GetComponent<FixedTextPopUP>().PressToInteractText("Press R to rotate the pipe");
    }

    private void Rotate()
    {
        TileData.Rotation++;
        if (TileData.Rotation == 4)
        {
            TileData.Rotation = 0;
        }
        this.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        //update l'image (si connect�e a fluid)
    }

    public void AffichageUpdate()
    {
        switch (TileData.TileType)
        {
            case PCTile.PCTileType.None:
                throw new System.Exception("y a un pb");
                break;
            case PCTile.PCTileType.Strait:
                this.GetComponent<SpriteRenderer>().sprite = Strait_Empty;
                break;
            case PCTile.PCTileType.Corner:
                this.GetComponent<SpriteRenderer>().sprite = Corner_Empty;
                break;
            case PCTile.PCTileType.Cross:
                this.GetComponent<SpriteRenderer>().sprite = Cross_Empty;
                break;
            default:
                throw new System.Exception("y a un pb");
                break;
        }
        
    }
}
