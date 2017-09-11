using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SciChain.network
{
    class P2P
    {
        private UdpClient udpclient;
        private static IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
        private IPEndPoint remoteep = new IPEndPoint(multicastaddress, 49120);


        public void SendOpenMessage(string data)
        {
            udpclient = new UdpClient();
            udpclient.JoinMulticastGroup(multicastaddress);

            Byte[] buffer = Encoding.UTF8.GetBytes(data);

            udpclient.Send(buffer, buffer.Length, remoteep);
        }

        public string Listen()
        {
            UdpClient client = new UdpClient()
            {
                ExclusiveAddressUse = false
            };

            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 49120);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            Console.WriteLine("Listening started");

            string formatted_data;

            while (true)
            {
                formatted_data = Encoding.UTF8.GetString(client.Receive(ref localEp));

                if (formatted_data != "GET")
                {
                    return formatted_data;
                }
              
            }

        }

        public void ListenToGive()
        {
            UdpClient client = new UdpClient()
            {
                ExclusiveAddressUse = false
            };

            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 49120);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            string formatted_data;

            while (true)
            {
                Console.WriteLine("Listening for GET");
                
                formatted_data = Encoding.UTF8.GetString(client.Receive(ref localEp));
                Console.WriteLine(formatted_data);

                if (formatted_data == "GET")
                {
                    if (File.Exists("chain.json"))
                    {
                        SendOpenMessage(File.ReadAllText("chain.json"));
                    }
                }

            }

        }

        public void ListenForNew()
        {
            UdpClient client = new UdpClient()
            {
                ExclusiveAddressUse = false
            };

            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 49120);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            string formatted_data;

            while (true)
            {
                Console.WriteLine("Listening for NEW");

                formatted_data = Encoding.UTF8.GetString(client.Receive(ref localEp));
                Console.WriteLine(formatted_data);

                if (formatted_data.StartsWith("NEWBLOCKS"))
                {
                    formatted_data = formatted_data.TrimStart("NEWBLOCKES ".ToCharArray());
                    if (File.Exists("chain.json"))
                    {
                        File.WriteAllText("chain.json", formatted_data, Encoding.UTF8);
                    }
                }

            }
        }

       
    }
}
