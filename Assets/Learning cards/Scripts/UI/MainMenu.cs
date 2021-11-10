using Learning_cards.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Learning_cards.Scripts.UI
{
	public class MainMenu : MonoBehaviour
	{
		public void LoadScene(int sceneId) => SceneManager.LoadScene(sceneId);
		public void ReloadDirectory() => Dictionaries.Load();

		public void QuitApplication()
		{
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}
	}
}