namespace CoreLib.Utilities
{
    /// <summary>
    /// Represents a class that can save, load, and clear <see cref="ISaveData"/> instances.
    /// <br></br>
    /// e.g.
    /// <code>
    /// 
    /// [Serializable]
    /// public class Data : ISaveData {
    ///     public int MyContent;
    /// }
    /// 
    /// [SerializeField] private Data _saveData = new();
    /// 
    /// public void SaveData() {
    ///     if (_saveData == null) return;
    ///     SaveDataUtilities.Save(_saveData, "MySaveFile");
    /// }
    /// 
    /// public void LoadData() {
    ///     Data loadedData = SaveDataUtilities.Load&lt;Data&gt;("MySaveFile");
    ///     _saveData = loadedData;
    /// }
    /// 
    /// public void ClearData() {
    /// SaveDataUtilities.ClearData("MySaveFile");
    /// }
    /// 
    /// </code>
    /// </summary>
    public interface ISaveDataHandler
    {
        void SaveData();
        void LoadData();
        void ClearData();
    }
}