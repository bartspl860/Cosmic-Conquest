using UnityEngine;

namespace Audio
{
	[System.Serializable]
	public class Sound
	{
		public string name = "Sound";
		public AudioClip clip;

		[Space]

		[Range(0f, 1f)]
		public float volume = 0.7f;
		[Range(0.5f, 2f)]
		public float pitch = 1f;

		[Range(0f, 0.5f)]
		public float volumeRandomness = 0.1f;
		[Range(0f, 0.5f)]
		public float pitchRandomness = 0.1f;

		public bool loop;
		public float fadingSpeed = 6f;

		[HideInInspector]
		public bool isPaused = false;

		private AudioSource source;
		private Transform soundsParent;

		private float targetVolume = 1f;
		private float currentVolume = 0f;
		
		public void SetUp(Transform sndParent, float vol, bool loadVolume)
		{
			soundsParent = sndParent;

			if(loadVolume)
				volume = PlayerPrefs.GetFloat("AudioManager.Sound." + name + ".Volume", volume);

			//Set up volume.
			targetVolume = vol * volume;

			//Creating game object...
			GameObject sound = new GameObject();
			sound.transform.SetParent(this.soundsParent);
			sound.name = "Sound_" + name;

			//... adding audio source
			source = sound.AddComponent<AudioSource>();

			//... setting it up
			source.clip = clip;
			source.spatialBlend = 0;
			source.playOnAwake = false;
			source.loop = loop;
		}


		//This function is called by SoundCategory every time global or category volume changes.
		public void UpdateVolume(float newVolume)
		{
			float rnd = source.volume - targetVolume;
			source.volume = (newVolume * volume) + rnd;

			//Update targetVolume variable
			targetVolume = newVolume * volume;
			currentVolume = targetVolume;

			PlayerPrefs.SetFloat("AudioManager.Sound." + name + ".Volume", volume);
		}

		///
		///Plays this sound.
		///
		public void Play(bool fadeIn = false)
		{
			isPaused = false;

			//Set volume and pitch and randomize them.
			float _volume = targetVolume + Random.Range((-volumeRandomness * targetVolume/2), (volumeRandomness * targetVolume)/2);
			float _pitch = pitch + Random.Range(-(pitch * pitchRandomness)/2, (pitch * pitchRandomness)/2);

			//Set previously calculated volume and pitch, play
			if(!fadeIn)
			{
				currentVolume = _volume;
				source.volume = _volume;
			}
			else
			{
				currentVolume = 0f;
				source.volume = 0f;
			}
			source.pitch = _pitch;
			source.Play();
		}

		public void Update(float deltaTime)
		{
			if(currentVolume < targetVolume)
			{
				currentVolume = Mathf.Lerp(currentVolume, targetVolume, deltaTime * fadingSpeed);
				source.volume = currentVolume;
			}
		}

		///Resumes sound. Only suitable for 1-instance sounds.
		public void Resume()
		{
			if(!isPaused)
				return;

			source.Play();
			isPaused = false;
		}

		///Pauses all source of this sound.
		public void Pause()
		{
			isPaused = true;
			if(source.isPlaying)
				source.Pause();
		}

		///Stops all source of this sound.
		public void Stop()
		{
			isPaused = false;
			source.Stop();
		}

		///Checks if any instance of this sound is playing.
		public bool IsPlaying()
		{
			return source.isPlaying;
		}

		///Returns length of this sound's clip.
		public float GetClipLength()
		{
			return clip.length;
		}
	}
}