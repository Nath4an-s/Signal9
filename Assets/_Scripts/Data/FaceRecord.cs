using System;

[Serializable]
public class FaceRecord
{
    public string faceId;
    public string identityName;
    public string role;
    public float matchPercent;
    public string notes;
}

[Serializable]
public class FaceRecordList
{
    public FaceRecord[] records;
}