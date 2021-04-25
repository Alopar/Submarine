using Gameplay.Units.Player;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrewCellPlace : MonoBehaviour, IDropHandler
{
    private Room _room;
    private RectTransform _rectTransform;

    public event Action<int, RoomType, RoomType> OnCrewDrop;

    void Awake()
    {
        _room = transform.parent.GetComponent<Room>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public Room GetRoom()
    {
        return _room;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = _rectTransform.position;

            Crew _crew = eventData.pointerDrag.GetComponent<Crew>();
            int _crewID = _crew.GetCrewID();
            RoomType _roomFrom = _crew.GetRoomType();
            RoomType _roomTo = _room.GetRoomType();

            OnCrewDrop?.Invoke(_crewID, _roomFrom, _roomTo);

            _crew.SetRoomType(_roomTo);
            _crew.cell = this;
        }
    }
}
