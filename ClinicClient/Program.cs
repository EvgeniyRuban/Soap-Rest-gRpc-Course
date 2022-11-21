<<<<<<<<< Temporary merge branch 1
﻿namespace ClinicClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
=========
﻿using ClinicServiceProtos;
using Grpc.Core;
using Grpc.Net.Client;
using static ClinicServiceProtos.AuthenticationService;
using static ClinicServiceProtos.ClientService;

namespace ClinicClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5102");

            var authServiceClient = new AuthenticationServiceClient(channel);

            var authResponse = await authServiceClient.LoginAsync(new AuthenticationRequest
            {
                Login = "test",
                Password = "test",
            });

            if (authResponse.Status != 0)
            {
                Console.WriteLine("Authentication error.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Session token: {authResponse.SessionContext.SessionToken}");

            var callCredantials = CallCredentials.FromInterceptor(
                (c, m) =>
                {
                    m.Add("Authorization",
                        $"Bearer {authResponse.SessionContext.SessionToken}");
                    return Task.CompletedTask;
                });

            var ptotectedChannel = GrpcChannel.ForAddress("https://localhost:5102", new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), callCredantials)
            });


            ClientServiceClient clinicServiceClient = new ClientServiceClient(ptotectedChannel);

            var createClientResponse = clinicServiceClient.Add(new CreateClientRequest
            {
                Document = "DOC-Test",
                FirstName = "TestName",
                Patronymic = "TestPatronymic",
                Surname = "TestSurname"
            });

            if (createClientResponse.ErrCode == 0)
            {
                Console.WriteLine($"Client #{createClientResponse.Id} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create client error.\nErrorCode: {createClientResponse.ErrCode}\nErrorMessage: {createClientResponse.ErrMessage}");
            }

            var getClientResponse = clinicServiceClient.GetAll(new GetClientsRequest());

            if (createClientResponse.ErrCode == 0)
            {
                Console.WriteLine("Clients");
                Console.WriteLine("=======\n");

                foreach (var client in getClientResponse.Clients)
                {
                    Console.WriteLine($"#{client.Id} {client.Document} {client.Surname} {client.FirstName} {client.Patronymic}");
                }
            }
            else
            {
            }
        }
    }
}