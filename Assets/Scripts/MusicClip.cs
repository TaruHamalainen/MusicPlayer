using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New AudioClip")]
public class MusicClip : ScriptableObject
{
    [SerializeField] AudioClip clip;
    [SerializeField] string fileName;
    [SerializeField] string artistName;
    [SerializeField] string albumName;
    [SerializeField] Sprite clipImage;

    public AudioClip GetAudioClip() { return clip; }
    public string GetName() { return fileName; }
    public string GetArtist() { return artistName; }
    public string GetAlbum() { return albumName; }
    public Sprite GetSprite() { return clipImage; }

}
