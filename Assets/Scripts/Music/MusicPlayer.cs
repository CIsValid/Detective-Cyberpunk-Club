using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Music
{
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Audio Source")] [Required]
        public AudioSource audioSource;
        
        public List<AudioClip> playlist = new List<AudioClip>();

        private int songNr;
        private int playlistCount;

        private void Awake()
        {
            CreatePlaylist();
        }

        // Start is called before the first frame update
        private void Start()
        {
            AddSongsFromList();

            if (audioSource.clip == null)
            {
                audioSource.clip = playlist[songNr];
            }

        }

        // Update is called once per frame
        private void Update()
        {
            StartNextSong();
        }

        private void CreatePlaylist()
        {
            Directory.CreateDirectory(@"C:\MusicForDetective");
                
        }

        private void AddSongsFromList()
        {
            for (int i = 0; i < playlist.Count; i++)
            {
                if (File.Exists(i.ToString()))
                {
                    return;
                }

                else
                {
                    File.Create(i.ToString());
                }
                
            }
        }

        private void StartNextSong()
        {
            if (audioSource.clip.length != 0) return;
            playlistCount = (songNr += 1) % playlist.Count;
            audioSource.clip = playlist[playlistCount];
        }
    }
}
