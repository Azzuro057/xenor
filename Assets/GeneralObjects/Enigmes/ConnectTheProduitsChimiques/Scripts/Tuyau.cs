using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuyau : PressurePlate
{
    public PCTile TileData = new PCTile();

    public PCMap Map;
    public int CoordX;
    public int CoordY;

    //public Tuyau Suivant;
    //public Tuyau Precedent; peut �tre pas besoin (si on appelle le suivant juste si faut mettre la couleur ? avec les direction d'o� elle arrive

    //public bool Colored = false;
    //public bool Colored2 = false;

    public int Rotation = 0;
    private PCTile.PCFluidDirection fluidDirection;
    private PCTile.PCFluidDirection fluidDirection2;
    private PCTile.PCFluidDirection fluidCommingDirection;
    private PCTile.PCFluidDirection fluidCommingDirection2;

    /**
     * Sprites pour tuyaux
     */
    //Sources
    public Sprite Source_Empty;
    public Sprite Source_Rose;
    public Sprite Source_Blue;
    public Sprite Source_Green;

    //Strait
    public Sprite Strait_Empty;
    public Sprite Strait_Rose;
    public Sprite Strait_Blue;
    public Sprite Strait_Green;


    //Corner
    public Sprite Corner_Empty;
    public Sprite Corner_Rose;
    public Sprite Corner_Blue;
    public Sprite Corner_Green;

    //Cross
    public Sprite Cross_Empty;
    public Sprite Cross_RN;
    public Sprite Cross_BN;
    public Sprite Cross_GN;
    public Sprite Cross_NR;
    public Sprite Cross_NB;
    public Sprite Cross_NG;
    public Sprite Cross_BR;
    public Sprite Cross_BG;
    public Sprite Cross_RG;
    public Sprite Cross_RB;
    public Sprite Cross_GB;
    public Sprite Cross_GR;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //SI on fait pas �a le message part d�s qu'il quitte une autre boite donc oblig�
        //On affiche le message qui indique au joueur comment int�ragir avec la porte.
        MessageOnScreenCanvas.GetComponent<FixedTextPopUP>().PressToInteractText("Press R to rotate the pipe");
    }

    private void Rotate()
    {
        Rotation++;
        Rotation %= 4;
        //Debug.Log("Before");
        //Debug.Log(fluidCommingDirection);
        //Debug.Log(fluidDirection);
        if (fluidCommingDirection != PCTile.PCFluidDirection.None)
        {
            if (fluidCommingDirection == PCTile.PCFluidDirection.Down)
            {
                fluidCommingDirection = PCTile.PCFluidDirection.Left;
            }
            else
            {
                fluidCommingDirection--;
            }
        }
        if (fluidDirection != PCTile.PCFluidDirection.End)
        {
            if (fluidDirection == PCTile.PCFluidDirection.Down)
            {
                fluidDirection = PCTile.PCFluidDirection.Left;
            }
            else
            {
                fluidDirection--;
            }
        }
        //Debug.Log("After");
        //Debug.Log(fluidCommingDirection);
        //Debug.Log(fluidDirection);
        this.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        //update l'image (si connect�e a fluid)
        foreach (Tuyau tuyau in Map.Tuyaux)
        {
            tuyau.AffichageUpdate();
        }
        int numeroSource = 0;
        foreach ((int, int) coords in Map.StartsAndEnds)
        {
            //(y,x)
            if (coords.Item1 == -1)
            {
                Map.TuyauxMaze[coords.Item2][0].GetComponent<Tuyau>().ColorUpdate(PCTile.PCFluidDirection.None, (PCTile.PCFluidColor)numeroSource);
                numeroSource++;
            }
        }
    }

    public void InitaliseRotation(int coordX, int coordY)
    {
        CoordX = coordX;
        CoordY = coordY;
        switch (TileData.TileType)
        {
            case PCTile.PCTileType.Strait:
                fluidCommingDirection = PCTile.PCFluidDirection.Left;
                fluidDirection = PCTile.PCFluidDirection.Right;
                break;
            case PCTile.PCTileType.Corner:
                fluidCommingDirection = PCTile.PCFluidDirection.Up;
                fluidDirection = PCTile.PCFluidDirection.Right;
                break;
            case PCTile.PCTileType.Cross:
                fluidCommingDirection = PCTile.PCFluidDirection.Left;
                fluidDirection = PCTile.PCFluidDirection.Right;
                fluidCommingDirection2 = PCTile.PCFluidDirection.Up;
                fluidDirection2 = PCTile.PCFluidDirection.Down;
                break;
            case PCTile.PCTileType.Source:
                if (CoordY == 0)
                {
                    fluidCommingDirection = PCTile.PCFluidDirection.None;
                    fluidDirection = PCTile.PCFluidDirection.Right;
                }
                else
                {
                    fluidCommingDirection = PCTile.PCFluidDirection.Left;
                    fluidDirection = PCTile.PCFluidDirection.End;
                }
                break;
            default:
                break;
        }
    }

    public void AffichageUpdate()
    {
        switch (TileData.TileType)
        {
            case PCTile.PCTileType.Strait:
                this.GetComponent<SpriteRenderer>().sprite = Strait_Empty;
                break;
            case PCTile.PCTileType.Corner:
                this.GetComponent<SpriteRenderer>().sprite = Corner_Empty;
                break;
            case PCTile.PCTileType.Cross:
                this.GetComponent<SpriteRenderer>().sprite = Cross_Empty;
                break;
            case PCTile.PCTileType.Source:
                this.GetComponent<SpriteRenderer>().sprite = Source_Empty;
                break;
            default:
                throw new System.Exception("y a un pb");
        }
    }

    public void ColorUpdate(PCTile.PCFluidDirection commingFrom, PCTile.PCFluidColor color)
    {
        //S'il faut mettre de la couleur
        switch (TileData.TileType)
        {
            case PCTile.PCTileType.Strait:
                //Si on est sur un tiuyaux droit
                //Si le fluid viens de la bonne direction (peut importe le sens)
                if (commingFrom == fluidCommingDirection || commingFrom == fluidDirection)
                {
                    switch (color)
                    {
                        case PCTile.PCFluidColor.blue:
                            this.GetComponent<SpriteRenderer>().sprite = Strait_Blue;
                            break;
                        case PCTile.PCFluidColor.pink:
                            this.GetComponent<SpriteRenderer>().sprite = Strait_Rose;
                            break;
                        case PCTile.PCFluidColor.green:
                            this.GetComponent<SpriteRenderer>().sprite = Strait_Green;
                            break;
                        default:
                            break;
                    }
                    //Colored = true;
                    PCTile.PCFluidDirection pCFluidDirection;
                    //Suivant le sens de circulation du fluide, le liquide sort d'un cot� ou de l'autre
                    if (commingFrom == fluidCommingDirection)
                    {
                        pCFluidDirection = fluidDirection;
                    }
                    else
                    {
                        pCFluidDirection = fluidCommingDirection;
                    }
                    NextTuyauxColor(pCFluidDirection, color);
                }
                break;
            case PCTile.PCTileType.Corner:
                if (commingFrom == fluidCommingDirection || commingFrom == fluidDirection)
                {
                    switch (color)
                    {
                        case PCTile.PCFluidColor.blue:
                            this.GetComponent<SpriteRenderer>().sprite = Corner_Blue;
                            break;
                        case PCTile.PCFluidColor.pink:
                            this.GetComponent<SpriteRenderer>().sprite = Corner_Rose;
                            break;
                        case PCTile.PCFluidColor.green:
                            this.GetComponent<SpriteRenderer>().sprite = Corner_Green;
                            break;
                        default:
                            break;
                    }
                    //Colored = true;
                    PCTile.PCFluidDirection pCFluidDirection;
                    if (commingFrom == fluidCommingDirection)
                    {
                        pCFluidDirection = fluidDirection;
                    }
                    else
                    {
                        pCFluidDirection = fluidCommingDirection;
                    }
                    NextTuyauxColor(pCFluidDirection, color);
                }
                break;
            case PCTile.PCTileType.Cross:
                this.GetComponent<SpriteRenderer>().sprite = Cross_Empty;
                break;
            case PCTile.PCTileType.Source:
                if (commingFrom == fluidCommingDirection)
                {
                    switch (color)
                    {
                        case PCTile.PCFluidColor.blue:
                            this.GetComponent<SpriteRenderer>().sprite = Source_Blue;
                            break;
                        case PCTile.PCFluidColor.pink:
                            this.GetComponent<SpriteRenderer>().sprite = Source_Rose;
                            break;
                        case PCTile.PCFluidColor.green:
                            this.GetComponent<SpriteRenderer>().sprite = Source_Green;
                            break;
                        default:
                            break;
                    }
                    //Colored = true;
                    PCTile.PCFluidDirection pCFluidDirection = fluidDirection;
                    NextTuyauxColor(pCFluidDirection, color);
                }
                break;
            default:
                throw new System.Exception("y a un pb");
        }
    }

    private void NextTuyauxColor(PCTile.PCFluidDirection pCFluidDirection, PCTile.PCFluidColor color)
    {
        switch (pCFluidDirection)
        {
            case PCTile.PCFluidDirection.Down:
                if (CoordY + 1 < Map.TuyauxMaze[0].Length && Map.TuyauxMaze[CoordX][CoordY + 1] != null)
                {
                    //On renvoie une direction diff�rente parce que l'ont veux savoir d'o� arrive le liquide sur la case
                    Map.TuyauxMaze[CoordX][CoordY + 1].ColorUpdate(PCTile.PCFluidDirection.Up, color);
                }
                break;
            case PCTile.PCFluidDirection.Left:
                if (CoordX - 1 >= 0 && Map.TuyauxMaze[CoordX - 1][CoordY] != null)
                {
                    Map.TuyauxMaze[CoordX - 1][CoordY].ColorUpdate(PCTile.PCFluidDirection.Right, color);
                }
                break;
            case PCTile.PCFluidDirection.Up:
                if (CoordY - 1 >= 0 && Map.TuyauxMaze[CoordX][CoordY - 1] != null)
                {
                    Map.TuyauxMaze[CoordX][CoordY - 1].ColorUpdate(PCTile.PCFluidDirection.Down, color);
                }
                break;
            case PCTile.PCFluidDirection.Right:
                if (CoordX + 1 < Map.TuyauxMaze.Length && Map.TuyauxMaze[CoordX + 1][CoordY] != null)
                {
                    Map.TuyauxMaze[CoordX + 1][CoordY].ColorUpdate(PCTile.PCFluidDirection.Left, color);
                }
                break;
            case PCTile.PCFluidDirection.End:
                //TODO : dire que c bon pour 1
                break;
            default:
                break;
        }
    }
}
