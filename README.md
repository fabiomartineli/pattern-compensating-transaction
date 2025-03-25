# Pattern - Transação de Compensação
O pattern Compensating Transaction é um pattern que permite executar ações que revertam, caso haja necessidade, algum processamento anterior realizado.

### Contexto de uso
Este pattern é importante quando o sistema realiza operações de maneira assíncrona e, consequentemente, os processos ocorrem de maneira independente.
Sendo assim, é preciso haver um meio para contornar possíveis erros que possam acontecer (validações de negócio ou erros não esperados) para que as operações que já foram realizadas e confirmadas possam ser revertidas e o processamento como um todo se estabilizar.

### Observações
- As transações de compensação podem ser disparadas de maneira sequencial, ou seja, a medida que uma transação de compensação for finalizada, envia o evento para que outra seja iniciada também.
- As transações de compensação podem ser executadas de maneira assíncrona, ou seja, o processo de reverter as operações não precisa seguir uma linha sequencial.
- Cada serviço fica responsável por processar a transação de compensação, pois é o próprio serviço que sabe como reverter uma operação que já foi finalizada e processada com sucesso.
- Os serviços devem implementar o processamento assíncrono de maneira idempotente, a fim de garantir que um mesmo evento pode ser processado N vezes mas o resultado será único. Para essa garantia, pode-se realizar algumas validações sobre os dados disponíveis. 

### Exemplo
Neste exemplo, que foi feita apenas para demonstração, temos um fluxo que simula um pedido, seguindo os seguintes processamentos:
- Criação do pedido
- Processamento do pagamento
- Processamento do estoque
- Processamento da entrega

Os processamentos de cada etapa é feito de maneira assíncrona através de SNS e SQS da AWS.
Estes processamentos contém handlers que são responsáveis por processar os eventos que chega na fila correspondente. Cada um destes handlers implementam uma interface chamada `Saga`, que contem a assinatura para executar o fluxo principal e também uma assinatura para executar o processo de rollback caso algo dê errado.

```csharp
 public interface ISaga<TRequest>
 {
     Task ExecuteTransactionAsync(TRequest request, CancellationToken cancellationToken);
     Task CompensateTransactionAsync(TRequest request, CancellationToken cancellationToken);
 }
```

```csharp
public class ConfirmDeliveryCommandHandler : ISaga<ConfirmDeliveryCommand>, IRequestHandler<ConfirmDeliveryCommand>
{
    private readonly IDeliveryRepository _repository;
    private readonly IMessageBusPublisher _busPublisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MessageBusDestinationSettings _messageBusDestination;

    public ConfirmDeliveryCommandHandler(IDeliveryRepository repository,
        IMessageBusPublisher busPublisher,
        IUnitOfWork unitOfWork,
        IOptions<MessageBusDestinationSettings> options)
    {
        _repository = repository;
        _busPublisher = busPublisher;
        _unitOfWork = unitOfWork;
        _messageBusDestination = options.Value;
    }

    public async Task CompensateTransactionAsync(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
    {
       
    }

    public async Task ExecuteTransactionAsync(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
    {
        
    }

    public async Task Handle(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
    {
        await ExecuteTransactionAsync(request, cancellationToken);
    }
}
```

Como estes processamentos fazem parte de um message bus, o processo de rollback também é executado caso a mensagem recebida da fila não seja processada corretamente dentro das tentativas máximas.
[Link do arquivo de consumer](https://github.com/fabiomartineli/pattern-compensating-transaction/blob/master/CompensatingTransaction/MessageBus/Consumer/MessageBusConsumer.cs)
