using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Helpers.Conectivity.Roku
{

    public class RokuScanner
    {
        public async Task<List<string>> FindRokuDevices()
        {
            List<string> rokuDevices = new List<string>();

            using (UdpClient udpClient = new UdpClient())
            {
                IPEndPoint multicastEP = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);
                string ssdpRequest = "M-SEARCH * HTTP/1.1\r\n" +
                                     "HOST: 239.255.255.250:1900\r\n" +
                                     "MAN: \"ssdp:discover\"\r\n" +
                                     "ST: roku:ecp\r\n\r\n";

                byte[] requestBytes = Encoding.UTF8.GetBytes(ssdpRequest);
                await udpClient.SendAsync(requestBytes, requestBytes.Length, multicastEP);

                udpClient.Client.ReceiveTimeout = 5000; // Espera 5 segundos
                IPEndPoint responseEP = new IPEndPoint(IPAddress.Any, 0);

                try
                {
                    while (true)
                    {
                        byte[] responseBytes = udpClient.Receive(ref responseEP);
                        string response = Encoding.UTF8.GetString(responseBytes);

                        if (response.Contains("roku:ecp"))
                        {
                            rokuDevices.Add(responseEP.Address.ToString());
                        }
                    }
                }
                catch (SocketException)
                {
                    // Fin del escaneo
                }
            }
            return rokuDevices;
        }
    }
}
