using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CoreLib.Utilities
{
    public static class SaveDataUtilities
    {
        private const string SAVEGAME_FOLDER_NAME = "SaveFolder";

        public static void Save<T>(T saveData, string fileName, string folderName = null) where T : ISaveData
        {
            if (saveData == null)
            {
                Debug.LogError("Invalid data. Aborting.");
                return;
            }

            string filePath = GetFilePath(fileName, folderName);

            using FileStream fileStream = new(filePath, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(fileStream, saveData);

            Debug.Log($"Saved '{filePath}'");
        }

        public static T Load<T>(string fileName, string folderName = null) where T : ISaveData, new()
        {
            string filePath = GetFilePath(fileName, folderName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning("No saved data to load. Returning a new instance.");
                return new T();
            }

            using FileStream fileStream = new(filePath, FileMode.Open);
            BinaryFormatter formatter = new();
            T loadData;

            try
            {
                loadData = (T)formatter.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogWarning("Save data error. Returning a new instance.");
                return new T();
            }

            Debug.Log($"Loaded '{filePath}'");
            return loadData;         
        }

        public static void ClearData(string fileName, string folderName = null)
        {
            string filePath = GetFilePath(fileName, folderName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning("No saved data to delete. Aborting.");
                return;
            }

            File.Delete(filePath);
            Debug.Log($"Deleted '{filePath}'");
        }

        public static void ClearAllData(string folderName = null)
        {
            string directoryPath = GetFolderPath(folderName);

            if (!Directory.Exists(directoryPath))
            {
                Debug.LogWarning("No directory to clear. Aborting.");
                return;
            }

            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log($"Deleted '{file}'");
            }

            Debug.Log($"Cleared all data in '{directoryPath}'");
        }

        private static string GetFilePath(string fileName, string folderName = null)
        {
            string folderPath = GetFolderPath(folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return Path.Combine(folderPath, fileName);
        }

        public static string GetFolderPath(string folderName = null)
        {
            if (!string.IsNullOrEmpty(folderName))
                return Path.Combine(Application.persistentDataPath, folderName);
            else
                return Path.Combine(Application.persistentDataPath, SAVEGAME_FOLDER_NAME);
        }
    }
}
