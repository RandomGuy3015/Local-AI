using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Player1Mediator s;

    void Start()
    {
        s = gameObject.GetComponent<Player1Mediator>();
    }


    void Update()
    {
        List<UnitController> units = s.GetUnits();
        List<UnitController> enemyUnits = s.GetEnemyUnits();
        List<MineController> mines = s.GetMines();


        if (units.Count == 0 || enemyUnits.Count == 0)
        {
            return;
        }

        if (s.GetMetal() > 500)
        {
            units[units.Count - 1].Build();
        }

        if (units.Count < 5 * enemyUnits.Count)
        {
            for (int i = 0; i < units.Count; i++)
            {
                List<MineController> sortedMines = units[i].GetMinesByDistance();

                units[i].MoveTowards(sortedMines[0].GetPosition());
                units[i].Mine();
            }
        }
        else
        {
            for (int i = 1; i < units.Count; i++)
            {
                units[i].MoveTowards(enemyUnits[i % enemyUnits.Count].GetPosition());
            }
        }
    }
}