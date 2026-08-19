using System;

[Serializable]
public class DatabaseRecord
{
    public string plate;
    public string ownerName;
    public string ownerAddress;
    public string vehicleModel;
    public string notes;
}

[Serializable]
public class DatabaseRecordList
{
    public DatabaseRecord[] records;
}