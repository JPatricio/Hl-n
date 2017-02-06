using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class GameManager : MonoBehaviour {

	public Game gameInstance;

	private static class SaveLoad {

		public static void Save(Game game) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
			bf.Serialize(file, game);
			file.Close();
		}

		public static Game Load() {
			if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
				Game loadedGame = (Game)bf.Deserialize(file);
				file.Close();
				return loadedGame;
			}else
				return null;
		}
	}

	public void save(){
		SaveLoad.Save(gameInstance);
	}

	// Use this for initialization
	void Start () {
		gameInstance = SaveLoad.Load();
		if (gameInstance == null){
			gameInstance = new Game();
		}
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
