using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

public class MetaLoader 
{
    private static bool unitTesting = false;
	private static Dictionary<string, Action<string[]>> AllMetaTables = new Dictionary<string, Action<string[]>> () 
	{
		{ "[LEADER BOARD]", 		LoadLeaderBoardData }
	};

    public static bool LoadData(bool unitTest = false)
    {
        unitTesting = unitTest;
        string data = null;
        TextAsset textAsset = null;
        StreamReader streamReader = null;
        //Debug.Log"METADATAPATH : " + GetPathForDownloadMeta());
        string pathForDownloadedMeta = GetPathForDownloadMeta();
        if (System.IO.File.Exists(pathForDownloadedMeta))
        {
            streamReader = System.IO.File.OpenText(pathForDownloadedMeta);
            data = streamReader.ReadToEnd();
            //Debug.Log"Loading data from downloaded meta file");
            try
            {
                UnityEngine.Profiling.Profiler.BeginSample("Decrypt downloaded meta file");
                data = ConfigurationLibrary.CryptoString.Decrypt(data);
                UnityEngine.Profiling.Profiler.EndSample();
            }
            catch (Exception ex)
            {
                data = null;
                textAsset = Resources.Load("Files/metaData") as TextAsset;
                data = textAsset.text;
                Debug.Log("Loading data from resources meta file");

            }

            data = data.Replace("\\n\\r", "\n");
            data = data.Replace("\\r\\n", "\n");
            data = data.Replace("\\n", "\n");
            data = data.Replace("\"", "");
            data = string.Format("{0}", data);
        }
        else
        {
            textAsset = Resources.Load("Files/metaData") as TextAsset;
            data = textAsset.text;
            Debug.Log("Loading data from resources meta file");
        }
        if (string.IsNullOrEmpty(data))
        {
            Debug.Log("metaData data not found");
            return false;
        }
        try
        {
            UnityEngine.Profiling.Profiler.BeginSample("LoadDataUsingCSV");
            if (!LoadDataUsingCSV(data))
            {
                throw new Exception("Invalid or incomplete file");
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }
        catch (Exception exception)
        {
            if(unitTesting == false)
            {
                if (System.IO.File.Exists(pathForDownloadedMeta))
                {
                    System.IO.File.Delete(pathForDownloadedMeta);
                }
                else
                {
                    throw exception;
                }
            }
            return false;
        }
		if(textAsset != null)
			Resources.UnloadAsset(textAsset);
		if (streamReader != null)
			streamReader.Close ();

        return true;
	}

	static bool LoadDataUsingCSV (string data)
	{
		int tableCount = 0;
		string lastTableName = "";
		StringReader reader = new StringReader (data);
		if (reader == null) 
		{
			Debug.Log ("metaData data not readable");
			return false;
		}
		else 
		{
			string line;
			while ((line = reader.ReadLine ()) != null) 
			{
				string[] elements = line.Split (',');
				string sectionId = elements [0];
				// the first line of the file must always be a section identifier
				bool skippedHeader = false;
				string dataLine;
				while ((dataLine = reader.ReadLine ()) != null) 
				{
					string[] dataElements = dataLine.Split (',');
					if (dataElements [0] == "[END]")
						break;
					if (!skippedHeader) 
					{
						skippedHeader = true;
						continue;
					}
					if (AllMetaTables.ContainsKey (sectionId)) 
					{
						if (!string.Equals (lastTableName, sectionId)) 
						{
							lastTableName = sectionId;
							tableCount++;
						}
						AllMetaTables [sectionId].Invoke (dataElements);
					}
					else{
						//Debug.Log"Invalid Section ID"+sectionId);
					}
				}
			}
			reader.Close ();
		}
		return tableCount == AllMetaTables.Count;
	}

	public static string GetPathForDownloadMeta()
	{
		string filename = "metaData.txt";
		string saveDirectory = Application.temporaryCachePath;

		if(saveDirectory.Equals(string.Empty))
			saveDirectory = Application.persistentDataPath;
		
		return saveDirectory + "/" + filename;
	}

    #region load game meta data

	public static void LoadLeaderBoardData(string[] elements)
	{
		CharacterModel characterModel = new CharacterModel ();

		characterModel.id = Utility.ToInt (elements [0]); 
		characterModel.name = elements [1];
		characterModel.score = Utility.ToInt (elements [2]); 

		if (unitTesting == false) 
		{
			LeaderBoardController.Instance.AddLeaderBoardCharacter (characterModel);
		}
	}

	#endregion
}