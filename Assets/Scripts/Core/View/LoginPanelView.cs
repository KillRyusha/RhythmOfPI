using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LoginPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private Button _submitButton;
    [SerializeField] private TMP_InputField _loginInput;
    private string _username;
    void Start()
    {
        _submitButton.onClick.RemoveAllListeners();
        _submitButton.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        _username = _loginInput.text;
        StartCoroutine(PostRequest());
    }
    private IEnumerator PostRequest()
    {
        string postUrl = "https://runner-pi-4ab340a7ebbd.herokuapp.com/api/registration/add_user/";
        WWWForm form = new WWWForm();
        string hash = GetMD5Hash(_username);
        form.AddField("username", _username);
        form.AddField("hash", hash);
        postUrl += _username + "/" + hash;
        Debug.Log("MD5 Hash: " + hash);
        UnityWebRequest request = UnityWebRequest.Post(postUrl, form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Server response: " + responseText);
            _loginPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
        }

        request.Dispose();
    }
    public string GetMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
