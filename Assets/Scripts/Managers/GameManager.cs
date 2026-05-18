using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Genre genrePublic;
    public AbilityType abilitySelected;
    public static Genre genre;

    [Header("Room Settings")]
    public bool lastRoomWasBoss = false;

    [Header("Multipliers Game Balance")]
    public float enemyDamageMultiplier = 1f;
    public float enemyHealthMultiplier = 1f;

    [Header("Economic Multiplier")]
    public float inkMultiplier = 1f;
    public float priceMultiplier = 1f;

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

    public void OptionsOfDEstiny()
    {
        
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
}
