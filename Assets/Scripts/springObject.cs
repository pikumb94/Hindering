using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springObject : MonoBehaviour
{

    public float speed = 0.1f;
    public float speed2 = 0.1f;
    public float arcLength = 0;

    public enum Types {StraightLine, Parabola, Circle, Still};
    public struct Trajectory {
       public Vector2 ranges;
       public Types type;
    };
    public Trajectory[] trajectories = new Trajectory[10];

    Vector3[] initialPos;
    //Quaternion[] inizialRot = new Quaterion[3];
    bool movement = false;
    float endValue;

    // Start is called before the first frame update
    void Start()
    {
      initialPos = new Vector3[trajectories.Length];
      initialPos[0] = transform.position;
      //inizialRot[0] = transform.rotation;

      trajectories[0].ranges = new Vector2(5f,10f);
      trajectories[0].type = Types.Circle;
      trajectories[1].ranges = new Vector2(-5f,5f);
      trajectories[1].type = Types.StraightLine;
      trajectories[2].ranges = new Vector2(5f,-5f);
      trajectories[2].type = Types.Parabola;
      trajectories[3].ranges = new Vector2(5f,5f);
      trajectories[3].type = Types.Circle;
      for(int i=1; i < trajectories.Length;  i++)
      {
        initialPos[i] = initialPos[i-1] + new Vector3(
          (trajectories[i-1].type == Types.Circle)? 0 : trajectories[i-1].ranges[0],
          (trajectories[i-1].type == Types.Parabola || trajectories[i-1].type == Types.Circle)? 0 : trajectories[i-1].ranges[1],
          0);
      }
      endValue = trajectories.Length;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("f"))
      {
          movement = true;
      }
      if(movement)
      {
        for(int i=0; i < trajectories.Length;  i++)
        {
          if(arcLength < i+1)
          {
            Move(i);
            break;
          }
        }
      }
    }

    public void Move(int numOfTraj){
      Trajectory traj = trajectories[numOfTraj];
      switch(traj.type)
      {
        case Types.StraightLine: transform.Translate(Vector3.right * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                transform.Translate(Vector3.up * Time.deltaTime * speed * traj.ranges[1], Space.World);
                                if(traj.ranges[0] != 0)
                                {
                                  arcLength = Mathf.Abs(transform.position[0] - initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0]) + numOfTraj;
                                }else
                                {
                                  arcLength = Mathf.Abs(transform.position[1] - initialPos[numOfTraj][1]) / Mathf.Abs(traj.ranges[1]) + numOfTraj;
                                }
                                break;
        case Types.Parabola:    transform.Translate(Vector3.right * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                arcLength = Mathf.Abs(transform.position[0] - initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0]) + numOfTraj;
                                if(arcLength - numOfTraj < 0.5f)
                                {
                                  transform.Translate(Vector3.up * Time.deltaTime * (speed * traj.ranges[1] * Mathf.Pow((1 - arcLength + numOfTraj), 2) + speed2 * (1 - arcLength + numOfTraj)), Space.World);
                                }else{
                                  transform.Translate(Vector3.down * Time.deltaTime * (speed * traj.ranges[1] * Mathf.Pow((arcLength - numOfTraj), 2) + speed2 * (arcLength - numOfTraj)), Space.World);
                                }
                                break;
        case Types.Circle:      float ray = Mathf.Max(Mathf.Abs(traj.ranges[0]), Mathf.Abs(traj.ranges[1]));
                                if(arcLength - numOfTraj < 0.25f){
                                  transform.Translate(Vector3.right * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                  arcLength = Mathf.Abs(transform.position[0] - initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0]) / 2f + numOfTraj;
                                  transform.Translate(Vector3.down * Time.deltaTime * speed * Mathf.Sqrt(ray *ray -  Mathf.Pow((arcLength - numOfTraj), 2)), Space.World);
                                }else if(arcLength - numOfTraj < 0.5f){
                                  transform.Translate(Vector3.right * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                  arcLength = Mathf.Abs(transform.position[0] - initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0]) / 2f + numOfTraj;
                                  transform.Translate(Vector3.up * Time.deltaTime * speed * Mathf.Sqrt(ray *ray -  Mathf.Pow((arcLength - numOfTraj), 2)), Space.World);
                                }else if(arcLength - numOfTraj < 0.75f){
                                  transform.Translate(Vector3.left * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                  arcLength = Mathf.Abs(ray - transform.position[0] + initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0]) / 2f + 0.5f + numOfTraj;
                                  transform.Translate(Vector3.up * Time.deltaTime * speed * Mathf.Sqrt(ray *ray -  Mathf.Pow((arcLength - numOfTraj), 2)), Space.World);
                                }else{
                                  transform.Translate(Vector3.left * Time.deltaTime * speed  * traj.ranges[0], Space.World);
                                  arcLength = Mathf.Abs(ray - transform.position[0] + initialPos[numOfTraj][0]) /  Mathf.Abs(traj.ranges[0])  / 2f + 0.5f + numOfTraj;
                                  transform.Translate(Vector3.down * Time.deltaTime * speed * Mathf.Sqrt(ray *ray -  Mathf.Pow((arcLength - numOfTraj), 2)), Space.World);
                                }
                                break;
        default: break;
      }
    }
}
