using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AccountManager : MonoBehaviour {

    public InputField username;
    public string loggedInUser;
    public string userPath;
    
    public void CheckAccount()
    {
        //string path = "C:\\Users\\Ben\\Documents\\GitHub\\VR-Rowing-Trainer\\Assets\\Accounts\\";
        string path = "..\\..\\Accounts\\";
        Debug.Log(path);
        if (Directory.Exists(path + username.text))
        {
            loggedInUser = username.text;
            userPath = path + loggedInUser;
        }
        else
        {
            Directory.CreateDirectory(path + username.text);
            loggedInUser = username.text;
            userPath = path + loggedInUser;
        }
    }
}
