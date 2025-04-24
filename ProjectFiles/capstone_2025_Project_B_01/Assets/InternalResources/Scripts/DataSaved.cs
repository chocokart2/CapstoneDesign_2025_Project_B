using System;
using UnityEngine;

[System.Serializable]
public class DataSaved
{
    [SerializeField] public long dateTimeTick;
    [SerializeField] public int imageIndex;
    [SerializeField] public Vector3 prevPosition;
    [SerializeField] public bool[] checkpointActived;

    public string GetDateTimeString()
    {
        DateTime m_date = new DateTime(dateTimeTick);
        return $"{m_date.Day}/{m_date.Month}/{m_date.Year}";
    }
}
