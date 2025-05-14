using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class RequestManager : MonoBehaviour
{
    public Button syncButton;
    public Button asyncButton;
    public Button scoreButton;

    public TextMeshProUGUI feedbackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        syncButton.onClick.AddListener(SendSyncRequest);
        asyncButton.onClick.AddListener(() => StartCoroutine(SendAsyncRequest()));
        scoreButton.onClick.AddListener(() => StartCoroutine(SendScoreRequest("Michelle", 1500)));
    }

    void SendSyncRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://URL:5000/sync");
        request.SendWebRequest();

        feedbackText.text = "Enviado (sincrono)!";
    }

    IEnumerator SendAsyncRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://URL:5000/async");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            feedbackText.text = "Resposta: " + request.downloadHandler.text;
        }
        else
        {
            feedbackText.text = "Erro: " + request.error;
        }
    }

    IEnumerator SendScoreRequest(string player, int points)
    {
        ScoreData score = new ScoreData(player, points);
        string jsonData = JsonUtility.ToJson(score);

        UnityWebRequest request = UnityWebRequest.PostWwwForm("http://localhost:8080/score", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            feedbackText.text = "Pontuação enviada!";
        }
        else
        {
            feedbackText.text = "Erro ao enviar pontuação: " + request.error;
        }
    }

}
