using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Audio
{
	public class AudioManager : MonoBehaviour 
	{
		public static AudioManager Instance;

		[SerializeField]
		private bool dontDestroyOnLoad = true;

		[SerializeField, Range(0f, 1f)]
		private float volume = 1f;
		[SerializeField]
		private bool saveVolumeValues = true;

		[Space]

		[SerializeField]
		private List<SoundCategory> soundCategories = new List<SoundCategory>();

		private Sound GetSound(string name)
		{
			foreach (var category in soundCategories)
			{
				var sound = category.sounds.Find(s => s.name.Equals(name));
				if(sound != null)
					return sound;
			}
			return null;
		}
		private SoundCategory GetCategory(string category)
		{
			return soundCategories.Find(c => c.categoryName.Equals(name));
		}

		public void PlaySound(string name)
		{
			GetSound(name).Play();
		}

		public void PlaySound(string name, bool _fadeIn = false)
		{
			GetSound(name).Play(_fadeIn);
		}

		public void PauseSound(string name)
		{
			GetSound(name).Pause();
		}

		public void StopSound(string _name)
		{
			GetSound(name).Stop();
		}

		public void StopAll()
		{
			soundCategories.ForEach( soundCat =>
			{
				soundCat.StopAll();
			});
		}

		public void SetGlobalVolume(float _volume)
		{
			this.volume = _volume;
			soundCategories.ForEach( soundCat =>
			{
				soundCat.UpdateGlobalVolume(_volume);
			});
			PlayerPrefs.SetFloat("AudioManager.MainVolume", _volume);
		}

		public void SetCategoryVolume(string category, float volume)
		{
			var soundCat = GetCategory(category);
			if (soundCat != null)
			{
				soundCat.UpdateVolume(volume, volume);
			}
		}

		public float? GetCategoryVolume(string category)
		{
			var cat = GetCategory(category);
			return cat.volume;
		}

		public void PlayRandomFromCategory(string category, bool fadeIn = false)
		{
			var cat = GetCategory(category);
			int rand = Random.Range(0, cat.sounds.Count);
			cat.sounds[rand].Play(fadeIn);
		}

		public void ResumeSoundCategory(string category)
		{
			var cat = GetCategory(category);
			cat.ResumePaused();
		}

		public void PauseSoundCategory(string category)
		{
			var cat = GetCategory(category);
			cat.PauseAll();
		}

		public void StopSoundCategory(string category)
		{
			var cat = GetCategory(category);
			cat.StopAll();
		}

		public float GetSoundClipLength(string sound)
		{
			var soundClip = GetSound(sound);
			return soundClip.GetClipLength();
		}

		private void Awake()
		{
			//Check if AudioManager instance exists, and if no - create one, else - delete this AudioManager
			if(Instance == null)
				Instance = this;
			else
			{
				Destroy(this);
				return;
			}

			if(saveVolumeValues)
				volume = PlayerPrefs.GetFloat("AudioManager.MainVolume", volume);

			foreach(SoundCategory sndCat in soundCategories)
				sndCat.SetUpSounds(this.transform, volume, saveVolumeValues);

			if(dontDestroyOnLoad)
				DontDestroyOnLoad(this);
		}

		void LevelLoaded(Scene s1, Scene s2)
		{
			StopAll();
			StopAllCoroutines();

			foreach(SoundCategory sndCat in soundCategories)
			{
				if(sndCat.autoPlay)
				{
					int rand = Random.Range(0, sndCat.sounds.Count);

					sndCat.sounds[rand].Play();

					StartCoroutine(AutoPlay(sndCat, rand, sndCat.sounds[rand].clip.length + sndCat.autoPlayOffset));
				}
			}
		}

		void Update()
		{
			foreach (var category in soundCategories)
			{
				foreach (var sound in category.sounds)
				{
					sound.Update(Time.unscaledDeltaTime);
				}
			}
		}

		void OnEnable()
		{
			UnityEngine.SceneManagement.SceneManager.activeSceneChanged += LevelLoaded;
		}

		void OnDisable()
		{
			UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= LevelLoaded;
		}

		private IEnumerator AutoPlay(SoundCategory category, int previous, float offset)
		{
			yield return new WaitForSeconds(offset);

			int rand = Random.Range(0, category.sounds.Count);
			if(rand == previous)
			{
				rand++;
				if(rand >= category.sounds.Count)
					rand = 0;
			}
			category.sounds[rand].Play();
			StartCoroutine(AutoPlay(category, rand, category.sounds[rand].clip.length + category.autoPlayOffset));
		}
	}
}
