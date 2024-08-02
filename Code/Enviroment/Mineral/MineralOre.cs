using Reflex.Attributes;
using System;
using UnityEngine;

public class MineralOre : WorkProcess<PlayerMiner>
{
    [SerializeField] private MineralType _type;
    [SerializeField] private int _count;

    [Inject] private IMineralWorkSounds _sound;
    [Inject] private MessageBox _messageBox;
    [Inject] private LocalizationTranslate _localizationTranslate;

    private void Start()
    {
        SetCurrentCount(_count);
    }

    public void SetType(MineralType type) => _type = type;

    public void SetCount(int count) => _count = count;

    public override void ToWork()
    {
        if (Player == null)
            throw new ArgumentNullException(nameof(Player));

        if (Player.CanExtract() == false)
        {
            _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.ConteinerFull));
            return;
        }

        _sound.CraftMineral();
        AddProgress(Player.GetWorkPower());

        Player.Transform.LookAt(gameObject.transform);

        CheckComplete();
    }

    protected override void Complete()
    {
        _count--;
        Player.AddMineral(_type);
        SendMineralCubeToTrack();
        CheckDestroy();

        SetCurrentCount(_count);

        _sound.ExtractionMineral();
    }

    private void CheckDestroy()
    {
        if(_count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void SendMineralCubeToTrack()
    {
        MineralMovmentSettings mineralSettings = new MineralMovmentSettings();
        mineralSettings.StartPoint = Transform;
        mineralSettings.EndPoint = Player.GetTrackPoint();
        mineralSettings.Type = _type;
        mineralSettings.FinalAction = Player.GetTruckEndPoint();

        MineralPool.Instance.Create(mineralSettings);
    }
}
