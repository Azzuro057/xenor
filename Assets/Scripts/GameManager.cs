using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ce script permet de gere tout ce qui doit passer entre les scenes
// Egalement les sauvegarde
// Il permet de sauvegarder des donn�es que l'on souhaite r�cup�rer apr�s le chargement de la sc�ne suivante
// ATTENTION : ce script ne doit �tre plac� qu'une seule fois dans le jeu !!!!!!!!!!!!!!!!! Lors de la premi�re scene.
// ATTENTION : On ne doit pas pouvoir revenir sur la premi�re sc�ne car sinon le script va se dupliquer ce qui entraine des bugs
// Conseil : Placer le script dans le menu de chargement.
// NE PAS OUBLIER : ajouter un EmptyObject nomm� GameManger et contenant le script GameManager sur la premi�re sc�ne du jeu.
public class GameManager : MonoBehaviour
{
    /*
     * Variables Satiques
     * 
     * Permet que l'on puisse y acc�der depuis n'importe quel script m�me s'il n'est pas dans la hierarchie
     */

    // Le Script GameManager lui-m�me
    // Permet que l'on puisse sauvergarder des donn�es depuis n'importe o�
    public static GameManager instance; // Va �tre egal au premier GameManager qu'il trouve dans le jeu


    /*
     * Variables Publiques des trucs qu'il y a � sauvgarder
     */
    public PlayerSettings playerSettings = new PlayerSettings();

    //NE PAS OUBLIER : idem aevc inventory
    // public Inventory inventory = new Inventory();

    private string[] LabyBoxNext = new string[] { "room_tuto1.json", "room_tuto2.json", "room_tuto3.json", "room_tuto4.json", "room_tuto5.json", "room_2_1.json"
    , "room_2_2.json", "room_2_3.json", "room_2_4.json", "room_2_5.json", "room_3_1.json", "room_4_1.json", "loby"};

    private int LabyBoxNextInt = 0;


    /*
     * Fonctions
     */

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        ResetSave(); // si on veut pas recup�rer les donn�es de la derni�re session quand on relance le jeu.

        instance = this;
        SceneManager.sceneLoaded += LoadState; //maintenant quand on load une nouvelle scene on va aussi appeler le truc pour load les donn�es
        DontDestroyOnLoad(gameObject); //ne pas supprimer un objet quand on change de scene
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "CrateLabyrinthScene")
        {
            if (LabyBoxNextInt == 12)
            {
                //charger le loby  
            }
            GameObject.FindGameObjectsWithTag("BoxLabyGenerator")[0].GetComponent<CrateLabyrinthGenerator>().loadScene(LabyBoxNext[LabyBoxNextInt]);
            LabyBoxNextInt++;
        }
    }

    //Fonction SaveState() de sauvegarder toutes les infos que l'on souhaite conserver d'une scene � l'autre
    /**
    * <summary>Permet de sauvegarder toutes les infos que l'on souhaite conserver d'une scene � l'autre</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void SaveState()
    {
        // On utilise le module JsonUtility pour parse tout l'objet
        // NE PAS OUBLIER : il faudra remplacer apr ce qu'il faut pour aller chercher l'inventaire
        // string inventoryData = JsonUtility.ToJson(inventory);

        // Chemin ou va �tre enregistrer le JSON
        // persistentDataPath est un dossier qui ne sera jamais modifier par unity m�me mise � jour
        string filePath = Application.persistentDataPath + "/InventoryData.json";

        // On �crit le fichier
        // NE PAS OUBLIER : R�activer quand se sera ok
        //System.IO.File.WriteAllText(filePath, inventoryData);
    }

    //Fonction SavePlayerSettings() de sauvegarder tous les settings du player apr�s une modification
    /**
    * <summary>Permet de sauvegarder de sauvegarder tous les settings du player apr�s une modification</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void SavePlayerSettings()
    {
        // On utilise le module JsonUtility pour parse tout l'objet
        string playerSettingsData = JsonUtility.ToJson(playerSettings);

        // Chemin ou va �tre enregistrer le JSON
        // persistentDataPath est un dossier qui ne sera jamais modifier par unity m�me mise � jour
        string filePath = Application.persistentDataPath + "/PlayerSettingsData.json";

        // On �crit le fichier
        System.IO.File.WriteAllText(filePath, playerSettingsData);
    }

    //Fonction SaveState() de r�cuperer les infos sauvgarder dans la sc�ne pr�c�dente
    /**
    * <summary>Permet de r�cuperer les infos sauvgarder dans la sc�ne pr�c�dente</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        // Chemin ou est stock� le json de l'inventaire
        string filePath = Application.persistentDataPath + "/InventoryData.json";

        if (System.IO.File.Exists(filePath))
        {
            // On le parse pour r�cup les infos
            string inventoryData = System.IO.File.ReadAllText(filePath);

            // On recr�e un inventaire avec les infos
            // NE PAS OUBLIER : R�activer quand se sera ok
            //inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        }

        // Chemin ou est stock� le json de l'inventaire
        filePath = Application.persistentDataPath + "/PlayerSettingsData.json";

        if (System.IO.File.Exists(filePath))
        {
            // On le parse pour r�cup les infos
            string playerSettingsData = System.IO.File.ReadAllText(filePath);

            // On recr�e un playerSettings avec les infos
            // NE PAS OUBLIER : R�activer quand se sera ok
            playerSettings = JsonUtility.FromJson<PlayerSettings>(playerSettingsData);
        }
    }

    //Fonction ResetSave() permet de supprimer toutes les infos stock� dans les Json
    /**
    * <summary>Permet de supprimer toutes les infos stock� dans les Json</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void ResetSave()
    {
        // Chemin ou est stock� le json de l'inventaire
        string filePath = Application.persistentDataPath + "/InventoryData.json";

        // On d�truit le fichier
        System.IO.File.Delete(filePath);
    }

    //Fonction ResetPlayerSettings() permet de supprimer tous les settings du player
    /**
    * <summary>Permet de supprimer tous les settings du player</summary>
    * 
    * <returns>Return nothing</returns>
    */
    public void ResetPlayerSettings()
    {
        // Chemin ou est stock� le json de l'inventaire
        string filePath = Application.persistentDataPath + "/PlayerSettingsData.json";

        // On d�truit le fichier
        System.IO.File.Delete(filePath);
    }
}