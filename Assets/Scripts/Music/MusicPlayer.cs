using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Audio Source")] [Required]    
        public AudioSource audioSource;
        
        public List<AudioClip> playlist = new List<AudioClip>();

        private AudioClip lastPlayedSong;

        private int songRandomizer;
        private int songNr;
        private int playlistCount;
        
        // Start is called before the first frame update
        private void Start()
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
            
            CreatePlaylist();

            songRandomizer = Random.Range(0, playlist.Count);

            StartCoroutine(StartPlay());

            audioSource.time += 20;

        }

        private void CreatePlaylist()
        {
            AudioClip[] arrayStore = Resources.LoadAll<AudioClip>("Songs");
            
            for (int i = 0; i < arrayStore.Length; i++)
            {
                playlist.Add(arrayStore[i]);
            }
          
        }

        private void Update()
        {
            StartCoroutine(StartPlay());
        }

        IEnumerator StartPlay()
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            
            playlistCount = (songRandomizer += 1) % playlist.Count;
            audioSource.clip = playlist[playlistCount];
            audioSource.Play();

            yield return null;

        }
    }
}