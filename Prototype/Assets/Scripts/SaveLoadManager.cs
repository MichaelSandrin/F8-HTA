using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveLoadManager
{
    public static List<Game> savedGames = new List<Game>();
    
    public static string loadLevel;
    public static void Save(string scene)
    {
       // savedGames.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();     
        FileStream file = new FileStream(Application.persistentDataPath + "/savedGames.txt", FileMode.Create);
        Game data = new Game(scene);
        bf.Serialize(file, data);
        file.Close();
    }

    public static string Load()
    {
        
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(Application.persistentDataPath + "/savedGames.txt", FileMode.Open);
            Game data = bf.Deserialize(file) as Game;
            loadLevel = data.lastLevel;
            file.Close();
           
        }
        return loadLevel;
    }
}



[System.Serializable]
public class Game
{
    public string lastLevel;
 

    public Game(string scene)
    {
        this.lastLevel = scene;
    }

}

