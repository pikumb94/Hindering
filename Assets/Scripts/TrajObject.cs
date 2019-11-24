using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajObject : TimeBehaviour
{

    public float standard_speed = 0.1f;
    public float arcLength = 0;

    public enum Types {StraightLine, Parabola, Semicircle, Circle, Still};
    public Trajectory[] trajectories = new Trajectory[10];

    public bool backward = true;
    public bool time = true;
    int endValue;
    float speed;
    float xspeed;

    void Start()
    {
      base.Start();
      trajectories[0].SetInitialPos(transform.position);
      for(int i=1; i < trajectories.Length;  i++)
      {
        trajectories[i].SetInitialPos(trajectories[i-1].InitialPos() + new Vector3(
          (trajectories[i-1].type == Types.Circle || trajectories[i-1].type == Types.Still) ? 0 : trajectories[i-1].ranges[0],
          (trajectories[i-1].type == Types.Parabola || trajectories[i-1].type == Types.Circle
            || trajectories[i-1].type == Types.Semicircle  || trajectories[i-1].type == Types.Still)? 0 : trajectories[i-1].ranges[1],
          0));
      }
      for(int i=0; i < trajectories.Length;  i++)
      {
        trajectories[i].SetFinalPos(trajectories[i].InitialPos() + new Vector3(
          (trajectories[i].type == Types.Circle || trajectories[i].type == Types.Still) ? 0 : trajectories[i].ranges[0],
          (trajectories[i].type == Types.Parabola || trajectories[i].type == Types.Circle
            || trajectories[i].type == Types.Semicircle  || trajectories[i].type == Types.Still)? 0 : trajectories[i].ranges[1],
          0));
        if(trajectories[i].speed == 0)
        {
          trajectories[i].speed = standard_speed;
        }
        trajectories[i].SetCenter(trajectories[i].InitialPos()[0] + trajectories[i].ranges[0] / 2f);

        if(trajectories[i].type == Types.Parabola)
        {
          trajectories[i].SetParameterA(trajectories[i].ranges[1] /
              ((Mathf.Pow(trajectories[i].InitialPos()[0] + trajectories[i].ranges[0], 2f) - Mathf.Pow(trajectories[i].InitialPos()[0], 2f)) / trajectories[i].ranges[0] * (trajectories[i].InitialPos()[0] - trajectories[i].Center())
               + trajectories[i].Center() * trajectories[i].Center() - Mathf.Pow(trajectories[i].InitialPos()[0], 2f)));
        }
        if(trajectories[i].type == Types.Circle || trajectories[i].type == Types.Semicircle )
        {
          trajectories[i].SetRay((trajectories[i].ranges[1] >= 0) ? Mathf.Abs(trajectories[i].ranges[0] / 2f) : - Mathf.Abs(trajectories[i].ranges[0] / 2f));
        }
      }

      endValue = trajectories.Length;
    }

    void Update()
    {

      if(time && backward)
      {
        for(int i = endValue - 1; i >= 0;  i--)
        {
          if(arcLength > i)
          {
            Move(i, true);
            break;
          }
        }
      }else if(time)
      {
        for(int i = 0; i < endValue;  i++)
        {
          if(arcLength < i+1)
          {
            Move(i, false);
            break;
          }
        }
      }
    }


    public void Move(int numOfTraj, bool backward){
      Trajectory traj = trajectories[numOfTraj];
      if(traj.ranges[0] != 0)
      {
        speed = (traj.ranges[0] > 0) ? traj.speed : -traj.speed;
      }else
      {
        speed = (traj.ranges[1] > 0) ? traj.speed : -traj.speed;
      }
      if (backward == true)
      {
        speed = -speed;
      }
      speed = (speed != 0) ? speed : standard_speed;
      switch(traj.type)
      {
        case Types.StraightLine:  if(traj.ranges[0] != 0)
                                  {
                                    transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
                                    arcLength = Mathf.Abs(transform.position[0] - (backward ? traj.FinalPos()[0] : traj.InitialPos()[0])) /  Mathf.Abs(traj.ranges[0]);
                                    transform.Translate(Vector3.up * Time.deltaTime * speed * traj.ranges[1] / traj.ranges[0], Space.World);
                                  }else if(traj.ranges[1] != 0)
                                  {
                                    arcLength = Mathf.Abs(transform.position[1] - (backward ? traj.FinalPos()[1] : traj.InitialPos()[1])) / Mathf.Abs(traj.ranges[1]);
                                    transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
                                  }else
                                  {
                                    arcLength = (backward) ? numOfTraj + 1 - arcLength + Time.deltaTime * (-speed) : arcLength + Time.deltaTime * speed;
                                  }
                                  break;
        case Types.Parabola:      arcLength = Mathf.Abs(transform.position[0] - (backward ? traj.FinalPos()[0] : traj.InitialPos()[0])) /  Mathf.Abs(traj.ranges[0]);
                                  xspeed = Mathf.Abs(traj.ranges[0]) / 2f * -Mathf.Sqrt(Mathf.Abs(arcLength - 0.5f)) + Mathf.Abs(traj.ranges[0]) / 2f;
                                  transform.Translate(Vector3.right * Time.deltaTime * speed * xspeed, Space.World);
                                  transform.Translate(Vector3.down * Time.deltaTime *
                                      (speed * xspeed * (2f * traj.ParameterA() * transform.position[0] - 2f * traj.ParameterA() * traj.Center())), Space.World);
                                  break;
      /*  case Types.Semicircle:  arcLength = Mathf.Abs(transform.position[0] - traj.InitialPos()[0]) /  Mathf.Abs(traj.ranges[0]);
                                  xspeed = -Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(Mathf.Abs(arcLength - 0.5f) - Mathf.Pow(traj.Ray(), 2f), 2f)) + Mathf.Abs(traj.Ray()) + basic_speed;
                                  transform.Translate(Vector3.right * Time.deltaTime * speed * xspeed, Space.World);
                                  transform.Translate(Vector3.up * Time.deltaTime *
                                    ((traj.Center() * speed * xspeed - transform.position[0] * speed * xspeed) / Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(transform.position[0] - traj.Center(), 2f))),
                                     Space.World);
                                  break;
        case Types.Circle:        if(arcLength < numOfTraj + 0.5f)
                                  {
                                    arcLength = Mathf.Abs(transform.position[0] - traj.InitialPos()[0]) /  Mathf.Abs(traj.ranges[0]) / 2f;
                                    xspeed = -Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(Mathf.Abs(arcLength - 0.5f) - Mathf.Pow(traj.Ray(), 2f), 2f)) + Mathf.Abs(traj.Ray()) + basic_speed;
                                    transform.Translate(Vector3.right * Time.deltaTime * speed * xspeed, Space.World);
                                    transform.Translate(Vector3.up * Time.deltaTime *
                                        ((traj.Center() * speed * xspeed - transform.position[0] * speed * xspeed) / Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(transform.position[0] - traj.Center(), 2f))),
                                         Space.World);
                                  }else
                                  {
                                    arcLength = Mathf.Abs(transform.position[0] - traj.InitialPos()[0] - traj.ranges[0]) /  Mathf.Abs(traj.ranges[0]) / 2f + 0.5f;
                                    xspeed = -Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(Mathf.Abs(arcLength - 0.5f) - Mathf.Pow(traj.Ray(), 2f), 2f)) + Mathf.Abs(traj.Ray()) + basic_speed;
                                    transform.Translate(Vector3.left * Time.deltaTime * speed * xspeed, Space.World);
                                    transform.Translate(Vector3.up * Time.deltaTime *
                                        ((traj.Center() * speed * xspeed - transform.position[0] * speed * xspeed) / Mathf.Sqrt(Mathf.Pow(traj.Ray(), 2f) - Mathf.Pow(transform.position[0] - traj.Center(), 2f))),
                                         Space.World);
                                  }
                                  break;*/
        case Types.Still:
        default:                  arcLength = (backward) ? numOfTraj + 1 - arcLength + Time.deltaTime * (-speed) : arcLength + Time.deltaTime * speed;
                                  break;
      }
      arcLength = (backward) ? numOfTraj + 1 - arcLength : arcLength + numOfTraj;
    }

    public override void swapTime()
    {
      if(time)
      {
        time = false;
      }else
      {
        time = true;
      }
    }
  }
