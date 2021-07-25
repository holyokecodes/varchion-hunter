using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveAndLoad
{
    public static void SaveDigSites(DigSite[] digSites) {
        // Create file and setup for writing
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/digsites.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        // Save DataStore as contents of the file
        DataStore ds = new DataStore(digSites);
        formatter.Serialize(stream, ds);
        stream.Close();
    }
    
    public static DigSite[] LoadDigSites() {
        // Get file from storage and check if it exists
        string path = Application.persistentDataPath + "/digsites.dat";
        if (File.Exists(path))
        {
            // If it does exist turn the serialized data back into our DataStore
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataStore ds = formatter.Deserialize(stream) as DataStore;
            stream.Close();
            // Return the DigSites
            return ds.getDigSites();
        }
        else
        {
            Debug.LogError("Failed to open save file");
            return null;
        }
    }
}