using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data for an ahievement including its completion metrics and how to earn it.
/// </summary>
public class Achievement:MonoBehaviour
{
    private string achievementName = "Default Achievement";
    private string achievementDescription = "This is a default achievement.";
    private bool isCompleted = false;
    private float completionPercentage = 0f;
    private bool displayPercentage = false;

    private List<Tuple<string, bool>> test;
    private Dictionary<string, bool> completionMetrics = new Dictionary<string, bool>() { { "Default metric", false } };

    public AchievementSO achievement;
    
    public void MarkMetric(string metricName, bool status)
    {
        if (completionMetrics.ContainsKey(metricName))
        {
            completionMetrics.Remove(metricName);
            completionMetrics.Add(metricName, status);
        }
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        int numCompleted = 0;
        int numTotal = completionMetrics.Keys.Count;
        foreach (KeyValuePair<string, bool> metric in completionMetrics)
        {
            if (metric.Value == false)
            {
                isCompleted = false;
            }
            else
            {
                numCompleted++;
            }
        }

        if (numCompleted == numTotal)
        {
            isCompleted = true;
        }

        completionPercentage = numCompleted / (float)numTotal;
    }
    

}
