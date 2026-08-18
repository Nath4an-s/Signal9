using System;
using System.Collections.Generic;

[Serializable]
public class CaseData
{
    public string caseId;
    public string title;
    public string location;
    public string status;
    public string assignedAgent;
    public List<string> attachedFiles;
    public List<string> evidence;
    public List<string> requiredDiscoveries;
}