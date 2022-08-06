using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    private Game g = null;
    [SerializeField] private Text textToUpdate = null;
    private int lastScore = 0;

    private void Start()
    {
        Debug.Log("IT IS " + g);
        g = GameManagerSingleton.Instance.GetGameForCurScene();
        Assert.IsNotNull(g, $"{name} is missing a {typeof(Game)} but requires one.");
        Assert.IsNotNull(g, $"{name} is missing a {typeof(Text)} but requires one.");
    }
    // Update is called once per frame
    void Update()
    {
        if(g.GetScore() != lastScore)
        {
            textToUpdate.text = $"SCORE: {g.GetScore()}";
            lastScore = g.GetScore();
        }
    }
}
