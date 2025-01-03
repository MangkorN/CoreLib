using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Utilities;

namespace CoreLib.UnitTest.Utilities
{
    [Serializable]
    public class TestSaveData : ISaveData
    {
        public int Integer { get; set; }
        public bool Boolean { get; set; }
        public List<int> LargeList { get; set; }
    }

    [Serializable]
    public class ComplexSaveData : ISaveData
    {
        public List<TestSaveData> DataList { get; set; }
    }

    public class SaveDataUtilitiesTest
    {
        private const string TEST_FILE_NAME = "test_file.dat";
        private const string TEST_FOLDER_NAME = "TestFolder";
        private string _fullTestFilePath;

        [SetUp]
        public void SetUp()
        {
            _fullTestFilePath = Path.Combine(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME), TEST_FILE_NAME);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_fullTestFilePath))
            {
                File.Delete(_fullTestFilePath);
            }

            // Delete the overridden directory itself
            string overriddenDirPath = SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME);
            if (Directory.Exists(overriddenDirPath))
            {
                Directory.Delete(overriddenDirPath, true);
            }
        }

        [Test]
        public void SaveData_ValidData_SavesToFile()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            Assert.IsTrue(File.Exists(_fullTestFilePath), "File should be saved, but it wasn't.");
        }

        private void AssertValidLoadedData(TestSaveData expectedData, TestSaveData actualData)
        {
            Assert.IsNotNull(actualData, "Loaded data should not be null.");
            Assert.AreEqual(expectedData.Integer, actualData.Integer, "Loaded integer data is incorrect.");
            Assert.AreEqual(expectedData.Boolean, actualData.Boolean, "Loaded boolean data is incorrect.");
        }

        [Test]
        public void Load_ValidData_ReturnsCorrectData()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            var loadedData = SaveDataUtilities.Load<TestSaveData>(TEST_FILE_NAME, TEST_FOLDER_NAME);

            AssertValidLoadedData(testData, loadedData);
        }

        [Test]
        public void Load_FileDoesNotExist_ReturnsNewInstance()
        {
            var loadedData = SaveDataUtilities.Load<TestSaveData>(TEST_FILE_NAME, TEST_FOLDER_NAME);

            Assert.IsNotNull(loadedData, "Loaded data should not be null.");
            Assert.AreEqual(default(int), loadedData.Integer, "Loaded integer data should be default.");
            Assert.AreEqual(default(bool), loadedData.Boolean, "Loaded boolean data should be default.");
        }

        [Test]
        public void Load_ReadOnlyFile_ReturnsCorrectData()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            string filePath = Path.Combine(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME), TEST_FILE_NAME);
            File.SetAttributes(filePath, FileAttributes.ReadOnly);

            try
            {
                var loadedData = SaveDataUtilities.Load<TestSaveData>(TEST_FILE_NAME, TEST_FOLDER_NAME);
                AssertValidLoadedData(testData, loadedData);
            }
            finally
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }
        }

        [Test]
        public void TryLoad_FileExistsAndIsValid_ReturnsTrueAndOutputsData()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            bool result = SaveDataUtilities.TryLoad(TEST_FILE_NAME, out TestSaveData loadedData, TEST_FOLDER_NAME);

            Assert.IsTrue(result, "TryLoad should return true when the file exists and is valid.");
            AssertValidLoadedData(testData, loadedData);
        }

        [Test]
        public void TryLoad_FileDoesNotExist_ReturnsFalseAndOutputsDefault()
        {
            bool result = SaveDataUtilities.TryLoad(TEST_FILE_NAME, out TestSaveData loadedData, TEST_FOLDER_NAME);

            Assert.IsFalse(result, "TryLoad should return false when the file does not exist.");
            Assert.IsNull(loadedData, "Loaded data should be null when the file does not exist.");
        }

        [Test]
        public void TryLoad_FileExistsButIsInvalid_ReturnsFalseAndOutputsDefault()
        {
            string invalidFilePath = Path.Combine(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME), TEST_FILE_NAME);

            // Create an invalid file (e.g., not serialized data)
            Directory.CreateDirectory(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME));
            File.WriteAllText(invalidFilePath, "This is not valid serialized data.");

            bool result = SaveDataUtilities.TryLoad(TEST_FILE_NAME, out TestSaveData loadedData, TEST_FOLDER_NAME);

            Assert.IsFalse(result, "TryLoad should return false when the file exists but is invalid.");
            Assert.IsNull(loadedData, "Loaded data should be null when the file is invalid.");
        }

        [Test]
        public void TryLoad_ReadOnlyFile_ReturnsTrueAndOutputsData()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            string filePath = Path.Combine(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME), TEST_FILE_NAME);
            File.SetAttributes(filePath, FileAttributes.ReadOnly);

            try
            {
                bool result = SaveDataUtilities.TryLoad(TEST_FILE_NAME, out TestSaveData loadedData, TEST_FOLDER_NAME);

                Assert.IsTrue(result, "TryLoad should return true when the file exists and is valid, even if read-only.");
                AssertValidLoadedData(testData, loadedData);
            }
            finally
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }
        }

        [Test]
        public void ClearData_FileExists_FileIsDeleted()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            SaveDataUtilities.ClearData(TEST_FILE_NAME, TEST_FOLDER_NAME);

            Assert.IsFalse(File.Exists(_fullTestFilePath), "File should be deleted, but it still exists.");
        }

        [Test]
        public void ClearData_FileDoesNotExist_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() => SaveDataUtilities.ClearData(TEST_FILE_NAME, TEST_FOLDER_NAME), "Exception was thrown when trying to delete a non-existent file.");
        }

        [Test]
        public void ClearAllData_DirectoryExists_AllFilesDeleted()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);
            SaveDataUtilities.Save(testData, "another_test_file.dat", TEST_FOLDER_NAME);

            SaveDataUtilities.ClearAllData(TEST_FOLDER_NAME);

            string[] files = Directory.GetFiles(SaveDataUtilities.GetFolderPath(TEST_FOLDER_NAME));
            Assert.AreEqual(0, files.Length, "All files should be deleted, but some files still exist.");
        }

        [Test]
        public void ClearAllData_DirectoryDoesNotExist_NoExceptionThrown()
        {
            Assert.DoesNotThrow(() => SaveDataUtilities.ClearAllData("NonExistentFolder"), "Exception was thrown when trying to clear a non-existent directory.");
        }

        [Test]
        public void SaveData_LargeData_SavesToFile()
        {
            var testData = new TestSaveData
            {
                Integer = 123,
                Boolean = true,
                LargeList = new List<int>(Enumerable.Range(0, 1000000)) // This will create a list with 1 million integers.
            };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            Assert.IsTrue(File.Exists(_fullTestFilePath), "File with large data should be saved, but it wasn't.");

            FileInfo fileInfo = new FileInfo(_fullTestFilePath);
            long fileSizeInBytes = fileInfo.Length;
            double fileSizeInMegabytes = (double)fileSizeInBytes / (1024 * 1024); // Convert bytes to megabytes

            // Size of a single int for reference
            int singleInt = 123;
            byte[] intBytes = BitConverter.GetBytes(singleInt);
            long intSize = intBytes.Length;

            // Size of TestSaveData without the LargeList for reference
            var smallTestData = new TestSaveData { Integer = 123, Boolean = true };
            using MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, smallTestData);
            long smallTestDataSize = memoryStream.Length;

            Debug.Log($"Size of a single int: {intSize} bytes");
            Debug.Log($"Size of TestSaveData without the LargeList: {smallTestDataSize} bytes");
            Debug.Log($"File size with LargeList: {fileSizeInBytes} bytes ({fileSizeInMegabytes:0.##} MB)");
            Debug.Log($"The large data is approximately {(double)fileSizeInBytes / smallTestDataSize:0.##} times bigger than the small data.");
        }

        [Test]
        public void SaveData_ComplexData_SavesToFile()
        {
            var testData = new ComplexSaveData
            {
                DataList = new List<TestSaveData>
        {
            new TestSaveData { Integer = 123, Boolean = true },
            new TestSaveData { Integer = 456, Boolean = false }
        }
            };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);

            Assert.IsTrue(File.Exists(_fullTestFilePath), "File with complex data should be saved, but it wasn't.");
        }

        [Test]
        public void SaveData_ReadOnlyFile_ThrowsException()
        {
            var testData = new TestSaveData { Integer = 123, Boolean = true };
            SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME);
            File.SetAttributes(_fullTestFilePath, FileAttributes.ReadOnly);

            try
            {
                Assert.Throws<UnauthorizedAccessException>(() => SaveDataUtilities.Save(testData, TEST_FILE_NAME, TEST_FOLDER_NAME), "Expected an UnauthorizedAccessException to be thrown, but it wasn't.");
            }
            finally
            {
                // Reset the attributes regardless of the test outcome
                File.SetAttributes(_fullTestFilePath, FileAttributes.Normal);
            }
        }

    }
}
