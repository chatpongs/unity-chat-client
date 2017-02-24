using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour
{
	public Text label;
	private bool connected;

    System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
    // Use this for initialization
    void Start()
    {
        clientSocket = new System.Net.Sockets.TcpClient();
        clientSocket.Connect("192.168.100.8", 5000);
        Debug.Log("Running the client");
		connected = true;
    }

    // Update is called once per frame
    void Update()
    {
		if(connected)
		{
			ReceiveChat();
		}
    }
    public void ReceiveChat()
    {
		NetworkStream networkStream = clientSocket.GetStream();

		if(!networkStream.DataAvailable)
		{
			label.text = "[no data]";
			return;
		}

        try
        {
            byte[] receiveBytes = new byte[1024];
            networkStream.Read(receiveBytes, 0, (int)receiveBytes.Length);
            string message = System.Text.Encoding.ASCII.GetString(receiveBytes);
			label.text = message;
			Debug.Log(message);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception error:" + ex.ToString());
        }
    }

    public void SendChat()
    {
        NetworkStream networkStream = clientSocket.GetStream();
        string message = "Hello world";
        Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
        networkStream.Write(sendBytes, 0, sendBytes.Length);
        networkStream.Flush();
    }
}