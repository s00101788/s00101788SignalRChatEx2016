using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static string name; // Program Class Variable
        static IHubProxy proxy;
        static HubConnection connection = new HubConnection("http://localhost:56663/");

        static void Main(string[] args)
        {
            proxy = connection.CreateHubProxy("ChatHub");
            //connection.Received += Connection_Received;
            Action<string, string> SendMessageRecieved = recieved_a_message;
            proxy.On("broadcastMessage", SendMessageRecieved);

            connection.Start().Wait();

            Console.Write("Enter your Name: ");
            name = Console.ReadLine();

            proxy.Invoke("Send", new object[] { name, "Has joined the chat!" });

            Console.ReadLine();

        }

        private static void Connection_Received(string obj)
        {
            Console.WriteLine("Message Recieved {0}", obj);
        }
        private static void recieved_a_message(string sender, string message)
        {
            Console.WriteLine("{0} : {1}", sender, message);
        }

    }
}
