using Gameplay.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crew : MonoBehaviour
{
    [SerializeField] private Image _fillbar;
    [SerializeField] private RoomType _roomType;
    [SerializeField] private int _id;
    public CrewCellPlace cell;

    public int GetCrewID()
    {
        return _id;
    }

    public RoomType GetRoomType()
    {
        return _roomType;
    }

    public void SetHealth(float value)
    {
        _fillbar.fillAmount = value;
    }

    public void SetRoomType(RoomType roomType)
    {
        _roomType = roomType;
    }
}
