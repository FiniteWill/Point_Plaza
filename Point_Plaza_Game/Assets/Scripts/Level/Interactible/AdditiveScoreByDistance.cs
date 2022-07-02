using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveScoreByDistance : MonoBehaviour
{
    private WaitForSeconds s_checkRefresh = new WaitForSeconds(0.1f);

    [SerializeField] private string gameID = "SampleScene";
    [SerializeField] private Transform player = null;
    [SerializeField] private int pointsPerIncrement = 5;
    [SerializeField] private float distanceIncrement = 20f;
    [SerializeField] private Vector3 objectiveDirection = new Vector3(0, 1, 0);

    private Vector3 objPos = Vector3.zero;
    private Game game = null;

    private void Awake()
    {
        game = GameManagerSingleton.Instance.GetGame(gameID);
        if (objectiveDirection.x != 0)
        {
            objPos = player.position += new Vector3(objectiveDirection.x > 0 ? distanceIncrement : -distanceIncrement, 0, 0);           
            StartCoroutine(CheckXProgress());

        }
        if (objectiveDirection.y != 0)
        { 
            objPos = player.position += new Vector3(0, objectiveDirection.y > 0 ? distanceIncrement : -distanceIncrement, 0);
            StartCoroutine(CheckYProgress());
        }
        if (objectiveDirection.z != 0)
        {
            objPos = player.position += new Vector3(0, 0, objectiveDirection.z > 0 ? distanceIncrement : -distanceIncrement);
            StartCoroutine(CheckZProgress()); 
        }

    }

    private IEnumerator CheckXProgress()
    {
        if (objectiveDirection.x > 0)
        {
            if (player.position.x >= objPos.x)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }
        else
        {
            if (player.position.x <= objPos.x)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }

        yield return s_checkRefresh;
        StartCoroutine(CheckXProgress());
    }
    private IEnumerator CheckYProgress()
    {
        if (objectiveDirection.y > 0)
        {
            if (player.position.y >= objPos.y)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }
        else
        {
            if (player.position.y <= objPos.y)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }

        yield return s_checkRefresh;
        StartCoroutine(CheckYProgress());
    }
    private IEnumerator CheckZProgress()
    {
        if (objectiveDirection.z > 0)
        {
            if (player.position.z >= objPos.z)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }
        else
        {
            if (player.position.z <= objPos.z)
            {
                objPos = player.position;
                game.AddToScore(pointsPerIncrement);
            }
        }

        yield return s_checkRefresh;
        StartCoroutine(CheckZProgress());
    }

}
