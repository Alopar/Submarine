using System.Linq;
using UnityEngine;
using Gameplay.Units.Player;
using System;

public class ControllerRooms : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    private PlayerSubmarine _playerSubmarine;
    private Room[] _rooms;
    private Crew[] _crews;

    public static Action<int, RoomType, RoomType> OnCrewCellRoomDrop;

    void Awake()
    {
        _rooms = FindObjectsOfType<Room>();
        _crews = FindObjectsOfType<Crew>();
        _gameController.OnPlayerSubmarineReady += PlayerSubmarineReadyHandler;

        foreach (Room room in _rooms)
        {
            room.OnCrewCellDrop += CrewCellDropHandler;
        }
    }

    private void CrewCellDropHandler(int crewID, RoomType roomFrom, RoomType roomTo)
    {
        Debug.Log($"Crew Drop: {crewID}, {roomFrom}, {roomTo}");
        OnCrewCellRoomDrop?.Invoke(crewID, roomFrom, roomTo);
    }

    private void PlayerSubmarineReadyHandler(PlayerSubmarine playerSubmarine)
    {
        _playerSubmarine = playerSubmarine;
        _playerSubmarine.OnRoomHealthUpdate += RoomHealthUpdateHandler;
        _playerSubmarine.OnCrewMemberHealthUpdate += CrewMemberHealthUpdateHandler;
        _playerSubmarine.OnCrewMemberDeath += CrewMemberDeathHandler;
    }

    private void CrewMemberDeathHandler(int _crewID)
    {
        Destroy(_crews.Where(c => c.GetCrewID() == _crewID).FirstOrDefault().gameObject);
    }

    private void RoomHealthUpdateHandler(RoomType roomType, float value)
    {
        float _normalazedHP = value / _playerSubmarine.GetMaxRoomHP(roomType);
        _rooms.Where(r => r.GetRoomType() == roomType).FirstOrDefault().SetHealth(_normalazedHP);
    }
    
    private void CrewMemberHealthUpdateHandler(int _crewId, float value)
    {
        float _normalazedHP = value / _playerSubmarine.GetCrewMemberMaxHealthById(_crewId);
        _crews.Where(c => c.GetCrewID() == _crewId).FirstOrDefault().SetHealth(_normalazedHP);
    }

    void OnDestroy()
    {
        _gameController.OnPlayerSubmarineReady -= PlayerSubmarineReadyHandler;
        _playerSubmarine.OnRoomHealthUpdate -= RoomHealthUpdateHandler;
        _playerSubmarine.OnCrewMemberHealthUpdate -= CrewMemberHealthUpdateHandler;
        _playerSubmarine.OnCrewMemberDeath -= CrewMemberDeathHandler;

        foreach (Room room in _rooms)
        {
            room.OnCrewCellDrop -= CrewCellDropHandler;
        }
    }
}
