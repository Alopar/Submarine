using Gameplay.Units.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private Image _fillbar;
    [SerializeField] private RoomType _roomType;
    [SerializeField] private CrewCellPlace[] _cells;
    [SerializeField] private RectTransform Krestik;

    public event Action<int, RoomType, RoomType> OnCrewCellDrop;

    void Awake()
    {
        _cells = FindObjectsOfType<CrewCellPlace>().Where(c => c.GetRoom() == this).ToArray();

        foreach (CrewCellPlace cell in _cells)
        {
            cell.OnCrewDrop += CrewDropHandler;
        }
        
    }

    private void CrewDropHandler(int crewID, RoomType roomFrom, RoomType roomTo)
    {
        OnCrewCellDrop?.Invoke(crewID, roomFrom, roomTo);
    }

    public void SetHealth(float value)
    {
        _fillbar.fillAmount = value;
        Krestik.gameObject.SetActive(value<0.5f);
    }

    public RoomType GetRoomType()
    {
        return _roomType;
    }

    void OnDestroy()
    {
        foreach (CrewCellPlace cell in _cells)
        {
            cell.OnCrewDrop -= CrewDropHandler;
        }
    }
}
