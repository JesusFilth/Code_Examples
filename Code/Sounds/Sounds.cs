using UnityEngine;
using Random = UnityEngine.Random;

public class Sounds : MonoBehaviour, 
    IItemSounds, 
    IFinalGameSounds, 
    IFlooLavaSound, 
    IMineralWorkSounds, 
    ITempleBuildSounds
{
    private const float MinPitchWorkSound = 0.7f;
    private const float MaxPitchWorkSound = 1.3f;

    [SerializeField] private AudioSource _audioEffect;
    [SerializeField] private AudioSource _audioCraft;
    [SerializeField] private AudioSource _background;

    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _lose;

    [SerializeField] private AudioClip _flooLava;

    [SerializeField] private AudioClip _craftMineral;
    [SerializeField] private AudioClip _mineralExtraction;

    [SerializeField] private AudioClip _workBuild;
    [SerializeField] private AudioClip _buildBlock;
     
    public void Lose()
    {
        _background.Stop();
        _audioEffect.PlayOneShot(_lose);
    }

    public void Win()
    {
        _background.Stop();
        _audioEffect.PlayOneShot(_win);
    }

    public void PlayClip(AudioClip clip)
    {
        _audioEffect.PlayOneShot(clip);
    }

    public void Play()
    {
        _audioEffect.PlayOneShot(_flooLava);
    }

    public void CraftMineral()
    {
        _audioCraft.pitch = Random.Range(MinPitchWorkSound, MaxPitchWorkSound);
        _audioCraft.PlayOneShot(_craftMineral);
    }

    public void ExtractionMineral()
    {
        _audioEffect.pitch = Random.Range(MinPitchWorkSound, MaxPitchWorkSound);
        _audioEffect.PlayOneShot(_mineralExtraction);
    }

    public void ToBuild()
    {
        _audioCraft.pitch = Random.Range(MinPitchWorkSound, MaxPitchWorkSound);
        _audioCraft.PlayOneShot(_workBuild);
    }

    public void ToCompetedBlockBuild()
    {
        _audioEffect.pitch = Random.Range(MinPitchWorkSound, MaxPitchWorkSound);
        _audioEffect.PlayOneShot(_buildBlock);
    }
}
