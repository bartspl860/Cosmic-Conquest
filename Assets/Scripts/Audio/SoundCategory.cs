using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    [System.Serializable]
    public class SoundCategory
    {
        public string categoryName = "New category";
        [Range(0f, 1f)]
        public float volume = 1f;

        [Space]

        public bool autoPlay = false;
        public float autoPlayOffset = 10f;

        [Space]

        public List<Sound> sounds = new List<Sound>();
        
        ///Sets up all sounds. It should be done only once for AudioManager instance.
        public void SetUpSounds(Transform parent, float startingVolume, bool loadVolume)
        {
            if(loadVolume)
                volume = PlayerPrefs.GetFloat("AudioManager.Category." + categoryName + ".Volume", volume);

            sounds.ForEach(sound => sound.SetUp(parent, startingVolume * volume, loadVolume));
        }

        ///Updates volume of all sounds of this category.
        public void UpdateVolume(float globalVolume, float newVolume)
        {
            this.volume = newVolume;

            UpdateGlobalVolume(newVolume);

            PlayerPrefs.SetFloat("AudioManager.Category." + categoryName + ".Volume", volume);
        }

        ///Updates volume of all sounds of this category based on given global volume.
        public void UpdateGlobalVolume(float globalVolume)
        {
            sounds.ForEach(sound => sound.UpdateVolume(globalVolume * volume));
                
        }

        ///Resumes all paused sounds of this category.
        public void ResumePaused()
        {
            sounds.ForEach(sound =>
            {
                if(sound.isPaused)
                    sound.Resume();
            });
        }

        ///Pauses all sounds of this category.
        public void PauseAll()
        {
            sounds.ForEach(sound => sound.Pause());
        }

        ///Stops all sounds of this category.
        public void StopAll()
        {
            sounds.ForEach(sound => sound.Stop());
        }
    }
}