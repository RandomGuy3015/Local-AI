using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Mediator : MonoBehaviour
{
    PlayerCommands s;
    void Start()
    {
        s = gameObject.transform.parent.GetComponent<PlayerCommands>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetTime()
    {
        return s.GetTime();
    }

    public List<UnitController> GetUnits()
    {
        return s.GetUnits(1);
    }

    public List<UnitController> GetEnemyUnits()
    {
        return s.GetUnits(0);
    }

    public int GetMetal()
    {
        return s.GetMetal(1);
    }

    public List<MineController> GetMines()
    {
        return s.GetMines();
    }
}
