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
    public Button rankingButton;

    public TextMeshProUGUI feedbackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        syncButton.onClick.AddListener(SendSyncRequest);
        asyncButton.onClick.AddListener(() => StartCoroutine(SendAsyncRequest()));
        scoreButton.onClick.AddListener(() => StartCoroutine(SendScoreRequest("Michelle", 1500)));
        rankingButton.onClick.AddListener(() => StartCoroutine(GetRanking()));
    }

    void SendSyncRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8080/status");
        request.SendWebRequest();

        feedbackText.text = "Enviado (sincrono)!";
    }

    IEnumerator SendAsyncRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8080/status");
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
        PlayerData score = new PlayerData(player, points);
        string jsonData = JsonUtility.ToJson(score);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/score", "POST");
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

    IEnumerator GetRanking()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8081/ranking");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"players\":" + request.downloadHandler.text + "}";
            PlayerList ranking = JsonUtility.FromJson<PlayerList>(json);

            feedbackText.text = "Ranking:\n";
            foreach (PlayerData player in ranking.players)
            {
                feedbackText.text += $"{player.player} - {player.points} pts\n";
            }
        }
        else
        {
            feedbackText.text = "Erro ao buscar ranking: " + request.error;
        }
    }


}
