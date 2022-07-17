using System.IO;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerHud: NetworkBehaviour
{
    private NetworkVariable<NetworkString> playersName = new NetworkVariable<NetworkString>();

    private bool overlaySet = false;
    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
            {
                string savedCredentials = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
                LoginData loadedLoginData = JsonUtility.FromJson<LoginData>(savedCredentials);
                playersName.Value = $"{loadedLoginData.Username}";
            }
            else
            {
                playersName.Value = $"Player {OwnerClientId}";
            }
        }
    }

    public void SetOverlay()
    {
        var localPlayerName = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerName.text = playersName.Value.ToString();
        overlaySet = true;
    }

    public void Update()
    {
        if(!overlaySet && !string.IsNullOrEmpty(playersName.Value))
        {
            SetOverlay();
        }
    }
}

public struct NetworkString : INetworkSerializable
{
    private ForceNetworkSerializeByMemcpy<FixedString32Bytes> info;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }

    public override string ToString()
    {
        return info.Value.ToString();
    }

    public static implicit operator string(NetworkString s) => s.ToString();
    public static implicit operator NetworkString(string s) => new NetworkString() { info = new FixedString32Bytes(s) };
}