using UnityEngine;

public enum Direction
{
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3
}

public class EnemyMovment : MonoBehaviour
{
    public LayerMask wallMask;

    Vector3[] dirs;
    Vector3 goal;

    int dir;
    float stepDist;

    const float MOVE_SIZE = 20f;
    const float STEP_SIZE = 0.1f;

    void Start()
    {
        dirs = new Vector3[] {
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(0,0,-1),
            new Vector3(-1,0,0)
        };

        stepDist = Mathf.Infinity;
    }

    void Update()
    {
        if (stepDist < STEP_SIZE && !Physics.Raycast(transform.position, dirs[dir], 2.0f))
        {
            stepDist += 0.1f;
            transform.position = Vector3.MoveTowards(transform.position, goal, STEP_SIZE);
        }
        else
        {
            print("fnasdjlkfnkdasjfnadksjnds");
            dir = Random.Range(0, 4);
            while (Physics.Raycast(transform.position, dirs[dir], 5.0f))
            {
                dir = Random.Range(0, 4);
            }
            goal = transform.position + MOVE_SIZE * dirs[dir];
            stepDist = 0.0f;
        }
    }
}
