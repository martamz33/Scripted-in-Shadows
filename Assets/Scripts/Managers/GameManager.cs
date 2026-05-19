using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Genre genrePublic;
    public AbilityType abilitySelected;
    public static Genre genre;

    [Header("Player Global Stats")]
    public int playerCurrentHealth;

    [Header("Room Settings")]
    public bool lastRoomWasBoss = false;

    [Header("Multipliers Game Balance")]
    public float enemyDamageMultiplier = 1f;
    public float enemyHealthMultiplier = 1f;

    [Header("Economic Multiplier")]
    public float inkMultiplier = 1f;
    public float priceMultiplier = 1f;

    [Header("--- Dead Stats (Guardado Persistente) ---")]
    public int totalDeaths;
    public int deathsBeforeBoss;
    public int deathsAgainstBossFantasy;
    public int deathsAgainstBossFinal;

    [Header("--- Victory Stats (Guardado Persistente) ---")]
    public int victoriesAgainstBossFantasy;
    public int victoriesAgainstBossFinal;

    // Variables to know if exclusive power up are active
    [HideInInspector] public bool hasIronSkin;
    [HideInInspector] public bool hasPaperSkin;

    // --- Variable para la  Codicia del Avaro (Power UP) ---
    private bool greedActive = false;
    private int roomSinceActivateGreed =0;
    private bool waitingForShopReset = false;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadGameStats();
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseDifficulty(float extraMultiplier)
    {
        enemyDamageMultiplier +=extraMultiplier;
        enemyHealthMultiplier +=extraMultiplier;
    }

    void Update()
    {
        genre = genrePublic;
    }

    // ---Logic Of the Greed
    public void ActivateGreed()
    {
        greedActive = true;
        roomSinceActivateGreed = 0;
        waitingForShopReset = true;

        inkMultiplier = 2f;
        priceMultiplier = 2f; 

        Debug.Log("Los multiplier son los siguientes " + inkMultiplier + priceMultiplier);
    }

    public void OnRoomEntered()
    {
        if(!greedActive) return;

        roomSinceActivateGreed ++;

        if(greedActive)
        {
            if(roomSinceActivateGreed == 4)
            {
                inkMultiplier = 0.5f;
            }
            else if(roomSinceActivateGreed >= 7)
            {
                inkMultiplier = 1f;
                greedActive = false;
                roomSinceActivateGreed = 0;
            }
        }
        
        if(waitingForShopReset && RoomManager.Instance != null && RoomManager.Instance.roomData != null)
        {
            if(RoomManager.Instance.roomData.roomType == RoomType.Shop)
            {
                priceMultiplier = 1f;
                waitingForShopReset = false;
            }
        }
    }

    // ---Time does not forget logic
    public void OnStartNewRun(GameObject player)
    {
        if(PlayerPrefs.GetInt("TimeDoesNotForgetUnlocked", 0) == 1)
        {
            ApplyTimeDoesNotForget(player);
        }
    }
    public void ApplyTimeDoesNotForget(GameObject player)
    {
        int randomStat = Random.Range(0, 3);

        switch (randomStat)
        {
            case 0:
                PlayerStatsModifier.ModifyHealth(player, 2);
                break;
            case 1:
                PlayerStatsModifier.ModifyDamage(player, 0.0001f);
                break;
            case 2:
                PlayerStatsModifier.ModifyRecharge(player, -0.5f);
                break;
        }

        // Subimos stats de enemigos
        IncreaseDifficulty(0.0001f);
    }

    //function to freeze the player
    public void FreezePlayer(bool freeze)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            // Si freeze es true, congelamos. Si es false, descongelamos.
            player.GetComponent<GhostMovement>().isMovementActive = !freeze;
            player.GetComponent<GhostAttack>().enabled = !freeze;
            player.GetComponent<GhostMovement>().isOscilationActive = !freeze;
            if(freeze)
            {
                Rigidbody2D rg = player.GetComponent<Rigidbody2D>();

                if(rg != null)
                {
                    rg.velocity = Vector2.zero;
                }
            }
        }
    }

    // Logic Register Victories and Deaths
    // Update number of Deaths
    public void RecordPlayerDeath()
    {
        // 1. Sum and save all the TotalDeaths
        totalDeaths++;
        PlayerPrefs.SetInt("TotalDeaths", totalDeaths);

        // 2. Check where it die, thanks to the MapManager
        if (MapManager.Instance != null && MapManager.Instance.currentRoomData != null)
        {
            // Boss Room?
            if (MapManager.Instance.currentRoomData.roomType == RoomType.Boss)
            {
                // Check the boss (0 = Fantasía, 1 = Final)
                int bossIndex = MapManager.Instance.currentBossIndex;

                if (bossIndex == 0)
                {
                    deathsAgainstBossFantasy++;
                    PlayerPrefs.SetInt("Deaths_Boss_Fantasy", deathsAgainstBossFantasy);
                    Debug.Log("El jugador ha muerto contra el Boss de la Fantasía.");
                }
                else if (bossIndex == 1)
                {
                    deathsAgainstBossFinal++;
                    PlayerPrefs.SetInt("Deaths_Boss_Final", deathsAgainstBossFinal);
                    Debug.Log("El jugador ha muerto contra el Boss Final.");
                }
            }
            else
            {
                // If not boss room, it died before it 
                deathsBeforeBoss++;
                PlayerPrefs.SetInt("DeathsBeforeBoss", deathsBeforeBoss);
                Debug.Log("El jugador ha muerto antes de llegar al boss.");
            }
        }
        else
        {
            deathsBeforeBoss++;
            PlayerPrefs.SetInt("DeathsBeforeBoss", deathsBeforeBoss);
        }

        // Force Unity to sav ethe data
        PlayerPrefs.Save(); 
    }


    // Update tha name of victories
    public void RecordBossVictories()
    {
        if (MapManager.Instance != null)
        {
            int bossIndex = MapManager.Instance.currentBossIndex;

            if (bossIndex == 0)
            {
                victoriesAgainstBossFantasy++;
                PlayerPrefs.SetInt("Victories_Boss_Fantasy", victoriesAgainstBossFantasy);
                Debug.Log("¡Victoria registrada contra el Boss de la Fantasía!");
            }
            else if (bossIndex == 1)
            {
                victoriesAgainstBossFinal++;
                PlayerPrefs.SetInt("Victories_Boss_Final", victoriesAgainstBossFinal);
                Debug.Log("¡Victoria registrada contra el Boss Final! ¡Te has pasado el juego!");
            }

            PlayerPrefs.Save();
        }
    }

    // Charge all the data
    private void LoadGameStats()
    {
        // Carga de muertes
        totalDeaths = PlayerPrefs.GetInt("TotalDeaths", 0);
        deathsBeforeBoss = PlayerPrefs.GetInt("DeathsBeforeBoss", 0);
        deathsAgainstBossFantasy = PlayerPrefs.GetInt("Deaths_Boss_Fantasy", 0);
        deathsAgainstBossFinal = PlayerPrefs.GetInt("Deaths_Boss_Final", 0);

        // Carga de victorias (NUEVO)
        victoriesAgainstBossFantasy = PlayerPrefs.GetInt("Victories_Boss_Fantasy", 0);
        victoriesAgainstBossFinal = PlayerPrefs.GetInt("Victories_Boss_Final", 0);
    }

    public void ResetAllGameStats()
    {
        PlayerPrefs.DeleteKey("TotalDeaths");
        PlayerPrefs.DeleteKey("DeathsBeforeBoss");
        PlayerPrefs.DeleteKey("Deaths_Boss_Fantasy");
        PlayerPrefs.DeleteKey("Deaths_Boss_Final");
        
        PlayerPrefs.DeleteKey("Victories_Boss_Fantasy"); 
        PlayerPrefs.DeleteKey("Victories_Boss_Final");  
        
        LoadGameStats(); 
    }
}
