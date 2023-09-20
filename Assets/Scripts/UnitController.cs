using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health = 100;
    public float speed = 0.1f;
    private Vector3 target;
    public bool moving;

    public Material player0Mat;
    public Material player1Mat;

    private int team;

    public PlayerCommands s;
    void Start()
    {
        moving = false;
        if (transform.tag == "Player0")
        {
            team = 0;
            gameObject.GetComponent<MeshRenderer>().material = player0Mat;
        }
        else if (transform.tag == "Player1")
        {
            team = 1;
            gameObject.GetComponent<MeshRenderer>().material = player1Mat;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (target != gameObject.transform.position)
            {
                float dist = Vector3.Distance(gameObject.transform.position, target);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, speed/dist);
            }
            else
            {
                moving = false;
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void MoveUp(float dist)
    {
        moving = true;
        target = gameObject.transform.position + new Vector3(dist, 0, 0);
    }

    public void MoveDown(float dist)
    {
        moving = true;
        target = gameObject.transform.position + new Vector3(-dist, 0, 0);
    }

    public void MoveRight(float dist)
    {
        moving = true;
        target = gameObject.transform.position + new Vector3(0, 0, dist);
    }

    public void MoveLeft(float dist)
    {
        moving = true;
        target = gameObject.transform.position + new Vector3(0, 0, -dist);
    }

    public void MoveTowards(Vector3 loc)
    {
        moving = true;
        target = loc;
    }

    public MineController GetClosestMine()
    {
        return s.GetClosestMine(transform.position);
    }

    public List<MineController> GetMinesByDistance()
    {
        return s.GetMinesByDistance(transform.position);
    }

    public UnitController GetClosestEnemy()
    {
        return s.GetClosestEnemy(transform.position, team);
    }

    public void Build()
    {
        s.Build(gameObject.transform.position, team);
    }

    public void Mine()
    {
        s.Mine(transform.position, team);
    }

}
