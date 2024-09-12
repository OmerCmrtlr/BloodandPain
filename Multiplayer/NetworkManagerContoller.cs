using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkManagerController : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }

        GUILayout.EndArea();
    }

    private void StartButtons()
    {
        if (GUILayout.Button("Host"))
        {
            NetworkManager.Singleton.StartHost();
        }
        if (GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.StartClient();
        }
        if (GUILayout.Button("Server"))
        {
            NetworkManager.Singleton.StartServer();
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }
    }
}
