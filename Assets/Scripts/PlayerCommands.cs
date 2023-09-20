using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

// This script contains all game-related functions that the player can call.
// Specific commands for units are in UnitController
public class PlayerCommands : MonoBehaviour
{
    // Lists
    [SerializeField]
    private List<UnitController> player0Units = new List<UnitController> ();
    [SerializeField]
    private List<UnitController> player1Units = new List<UnitController> ();
    [SerializeField]
    private List<MineController> mines = new List<MineController> ();

    // Objects and types
    public GameObject testCube;
    public GameObject[] unitTypes;

    // Game balance
    public int unitCost;

    // Resources
    public int player0Metal;
    public int player1Metal;
    public TMP_Text player0MetalDisplay;
    public TMP_Text player1MetalDisplay;


    //-------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------


    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    { 
        // Update metal displays
        player0MetalDisplay.text = player0Metal.ToString();
        player1MetalDisplay.text = player1Metal.ToString();


        CheckIfUnitsWereKilled();
        
        // Simple Message for when someone wins.
        if (player0Units.Count == 0)
        {
            Debug.Log("Player 1 Won!");
        }
        else if (player1Units.Count == 0)
        {
            Debug.Log("Player 0 Won!");
        }
    }
    

    //----------------------------------------------------------------------------------------
    //----------------------- PUBLIC FUNCTIONS -----------------------------------------------
    //----------------------------------------------------------------------------------------


    // Gets all units belonging to your team.
    public List<UnitController> GetUnits(int player)
    {
        if (player == 0) {
            return player0Units;
        }
        else {
            return player1Units;
        }
    }

    // Gets all mines in the map.
    public List<MineController> GetMines()
    {
        return mines;
    }

    // Returns the time since game start.
    public float GetTime()
    {
        return Time.realtimeSinceStartup;
    }

    // Gets the amount of metal you have.
    public int GetMetal(int player)
    {
        if (player == 0)
        {
            return player0Metal;
        }
        else
        {
            return player1Metal;
        }
    }

    // Returns the closest mine to a given position
    public MineController GetClosestMine(Vector3 loc)
    {
        int closest = -1;
        float closestDistance = 1000f;
        for (int i = 0; i < mines.Count; i++)
        {
            if (Vector3.Distance(mines[i].GetPosition(), loc) < closestDistance)
            {
                closest = i;
                closestDistance = Vector3.Distance(mines[i].GetPosition(), loc);
            }
        }
        if (closest == -1)
        {
            Debug.Log("No Mines?");
        }
        return mines[closest];
    }

    // Returns all mines, sorted by distance to a given unit

    public List<MineController> GetMinesByDistance(Vector3 loc)
    {
        return mines.OrderBy(o => Vector3.Distance(o.GetPosition(), loc)).ToList();
    }


    // Gets closest enemy

    public UnitController GetClosestEnemy(Vector3 loc, int player)
    {
        int closest = -1;
        float closestDistance = 1000f;
        for (int i = 0; i < mines.Count; i++)
        {
            if (Vector3.Distance(player == 1 ? player0Units[i].GetPosition() : player1Units[i].GetPosition(), loc) < closestDistance)
            {
                closest = i;
                closestDistance = Vector3.Distance(mines[i].GetPosition(), loc);
            }
        }
        if (closest == -1)
        {
            Debug.Log("No Units?");
        }
        return player == 1 ? player0Units[closest] : player1Units[closest];
    }


    // Returns all enemy units, sorted by distance to a given unit

    public List<UnitController> GetEnemiesByDistance(Vector3 loc, int player)
    {
        if (player == 1)
        {
            return player0Units.OrderBy(o => Vector3.Distance(o.GetPosition(), loc)).ToList();
        }
        else
        {
            return player1Units.OrderBy(o => Vector3.Distance(o.GetPosition(), loc)).ToList();
        }
    }


    // Creates a crude formation at a location, looking at a target.

    public void CreateFormation(List<UnitController> units, Vector3 loc, Vector3 target)
    {
        // to-do
    }


    // Attempts to build a unit at a location.
    public void Build(Vector3 loc, int player)
    {
        if (player == 0)
        {
            if (player0Metal >= unitCost)
            {
                player0Metal -= unitCost;
                GameObject unit = Instantiate(unitTypes[0], loc + new Vector3(0f, 1f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                unit.transform.tag = "Player0";
                player0Units.Add(unit.GetComponent<UnitController>());
            }
            else
            {
                Debug.Log("Not enough metal to build unit for player 0!");
            }
        }
        else
        {
            Debug.Log(player1Metal);
            if (player1Metal >= unitCost)
            {
                player1Metal -= unitCost;
                GameObject unit = Instantiate(unitTypes[0], loc + new Vector3(0f, 1f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                unit.transform.tag = "Player1";
                player1Units.Add(unit.GetComponent<UnitController>());
            }
            else
            {
                Debug.Log("Not enough metal to build unit for player 1!");
            }
        }
    }


    // Attempts to mine at a certain location.
    public void Mine(Vector3 loc, int player)
    {
        bool mined = false;
        foreach (MineController mine in mines)
        {
            if (Vector3.Distance(mine.GetPosition(), loc) < 1)
            {
                mined = true;
                if (player == 0)
                {
                    player0Metal++;
                }
                else
                {
                    player1Metal++;
                }
            }
        }
        if (!mined)
        {
            Debug.Log("Attempted to mine non-mineable location at: " + loc.ToString());
        }
    }



    //------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------- PRIVATE FUNCTIONS ---------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------------


    // Initializes all the starting units and mines for the game.
    private void InitGame() 
    {

        player0Units.Add(Instantiate(unitTypes[0], new Vector3(-18f, 14f, -19f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<UnitController>());
        player0Units[0].transform.tag = "Player0";

        player1Units.Add(Instantiate(unitTypes[0], new Vector3(19f, 14f, 17f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<UnitController>());
        player1Units[0].transform.tag = "Player1";

        mines.Add(Instantiate(unitTypes[1], new Vector3(14f, 1f, 12f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());
        mines.Add(Instantiate(unitTypes[1], new Vector3(-13f, 1f, -14f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());
        mines.Add(Instantiate(unitTypes[1], new Vector3(-13f, 1f, 12f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());
        mines.Add(Instantiate(unitTypes[1], new Vector3(14f, 1f, -14f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());
        mines.Add(Instantiate(unitTypes[1], new Vector3(0f, 1f, -18f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());
        mines.Add(Instantiate(unitTypes[1], new Vector3(0f, 1f, 14f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<MineController>());

    }

    // Checks if any units are at or below 0 health, and deletes them if they are.
    private void CheckIfUnitsWereKilled()
    {
        for (int i = 0; i < player0Units.Count; i++)
        {
            if (player0Units[i].health <= 0 || player0Units[i].GetPosition().y < 0)
            {
                Destroy(player0Units[i].gameObject);
                player0Units.RemoveAt(i);
            }
        }
        for (int i = 0; i < player1Units.Count; i++)
        {
            if (player1Units[i].health <= 0 || player1Units[i].GetPosition().y < 0)
            {
                Destroy(player1Units[i].gameObject);
                player1Units.RemoveAt(i);
            }
        }
    }
}

