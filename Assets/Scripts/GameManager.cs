using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This script is controlling any game object instantiating on a previous scene,
    /// or a scene that you already destroyed
    /// Initializing the player gender to the scene and keep track of that object on all scenes
    /// </summary>
    public static GameManager instance;

    [Header("Characters")] [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    
    [Header("Don't apply any changes")]
    public string characterGender = "male";
    public GameObject spawnedChar = null;
    
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnValueChangeCharacters(string _characterGender)
    {
        characterGender = _characterGender;
    }

    public void CharacterSpawn(Transform spawnPoint)
    {
        spawnedChar = Instantiate(characterGender == "male" ? characters[0] : characters[1], spawnPoint.transform.position, Quaternion.Euler(0,0,0));
    }
    public GameObject InitCharacter()
    {
        return spawnedChar;
    }
    public void InstantiateCharacter()
    {
        SceneManager.LoadSceneAsync(1);
        // var spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        // var characterSpawned = Instantiate(CharacterSpawn(),spawnPoint.transform);
        // var cam = FindObjectOfType<Camera>();
        //var cinemachineCam = FindObjectOfType<CinemachineFreeLook>();

        //cinemachineCam.m_Follow = characterSpawned.transform;

    }
}