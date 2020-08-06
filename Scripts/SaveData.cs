using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using UnityEditor.VersionControl;

public class SaveData : MonoBehaviour
{
    public InputField Email;
    public InputField Nome;
    public InputField Atividade;
    public InputField Empresa;
    public InputField Formacao;

    User userData = new User();
    public void onBtClick()
    {
        userData.email = Email.text;
        userData.nome = Nome.text;
        userData.atividade = Atividade.text;
        userData.empresa = Empresa.text;
        userData.formacao = Formacao.text;

        string jsonData = JsonUtility.ToJson(userData);

        StartCoroutine(PostData("http://agregargestao.net.br:8080/Play2API/cadastro", jsonData));
    }
     

    IEnumerator PostData(string url, string json)
    { 

        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        // Send the request 
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
        
    }

}

[System.Serializable]
public class User
{
    public string email;
    public string nome;
    public string atividade;
    public string empresa;
    public string formacao;
}


