using ClinicServiceNamespace;
using Grpc.Net.Client;
using static ClinicServiceNamespace.ClientService;

namespace ClinicClient;

internal class Program
{
    static void Main(string[] args)
    {
        AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        using var channel = GrpcChannel.ForAddress("http://localhost:5001");
        ClientServiceClient clinicServiceClient = new ClientServiceClient(channel);

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
            Console.WriteLine($"Get clients error.\nErrorCode: {getClientResponse.ErrCode}\nErrorMessage: {getClientResponse.ErrMessage}");
        }

        Console.ReadKey();
    }
}